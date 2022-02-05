using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace Rawrshak
{
    [CreateAssetMenu(fileName="Exchange", menuName="Rawrshak/Create Exchange Object")]
    public class Exchange : ScriptableObject
    {
        public static string exchangeAddress;
        public string tokenAddress;
        private int statusCheckSleepDuration = 1000;
        private static BigInteger MaxApproveAmount = BigInteger.Pow(2, 256);

        // Private Variables
        private Network network;
        private bool isValid = false;

        public void OnEnable()
        {
            network = Network.Instance;
        }
        
        public void OnDisable()
        {
        }

        public async Task<bool> VerifyContracts()
        {
            if (network == null)
            {
                Debug.LogError("Network is not set.");
                isValid = false;
                return isValid;
            }
            if (tokenAddress == null)
            {
                Debug.LogError("Token Address is not set.");
                isValid = false;
                return isValid;
            }

            isValid = await ExchangeManager.SupportsInterface(network.chain, network.network, exchangeAddress, Constants.RAWRSHAK_IEXCHANGE_INTERFACE_ID, network.httpEndpoint);
            return isValid;
        }

        public async Task<string> WaitForTransaction(string transactionId) 
        {
            string transactionStatus = "pending";
            while (transactionStatus == "pending")
            {
                // Poll every duration to check if the transaction has occurred. 
                // Todo: If the transaction id is invalid, does it return success or fail?
                transactionStatus = await EVM.TxStatus(network.chain, network.network, transactionId, network.httpEndpoint);
                Thread.Sleep(statusCheckSleepDuration);
            }

            return transactionStatus;
        }

        public async Task<bool> ApproveToken(string tokenAddress)
        {
            try
            {
                string oper = await ExchangeManager.GetTokenEscrowAddress(network.chain, network.network, exchangeAddress, network.httpEndpoint);

                string response = await ERC20Extension.Approve(network.chain, network.network, tokenAddress, oper, MaxApproveAmount, network.httpEndpoint);
                
                return Boolean.Parse(response);
            }
            catch (Exception e)
            {
                Debug.LogException(e, this);
            }

            return false;
        }

        public async Task ApproveNftContract(string contractAddress, bool approved = true)
        {
            try
            {
                string oper = await ExchangeManager.GetNftEscrowAddress(network.chain, network.network, exchangeAddress, network.httpEndpoint);

                await ContentManager.SetApprovedForAll(network.chain, network.network, contractAddress, oper, approved, network.httpEndpoint);
            }
            catch (Exception e)
            {
                Debug.LogException(e, this);
            }
        }

        public async Task<List<Order>> GetActiveOrders(Asset asset, OrderType orderType, int pageSize = 5, string lastOrderId = "", bool loadData = false)
        {
            List<Order> orderList = new List<Order>();

            if (!asset.IsAssetTokenIdValid())
            {
                Debug.LogError("[Exchange] Invalid Asset Token input.");
                return null;
            }

            GetAssetActiveOrders.ReturnData data = await GetAssetActiveOrders.Fetch(asset.contractAddress, asset.tokenId, pageSize, lastOrderId, orderType);

            if (data.data.asset == null)
            {
                Debug.LogError("[Exchange] Asset could not be found.");
                return null;
            }

            foreach (var order in data.data.asset.orders)
            {
                Order orderObj = new Order();
                orderObj.orderId = BigInteger.Parse(order.id);
                if (loadData)
                {
                    await orderObj.Load();
                }

                orderList.Add(orderObj);
            }

            return orderList;
        }

        public async Task<List<Order>> GetActiveOrders(string accountAddress, OrderType orderType, int pageSize = 5, string lastOrderId = "", bool loadData = false)
        {
            List<Order> orderList = new List<Order>();

            // Todo: Check if Account Address is a valid Wallet Address

            GetAccountActiveOrders.ReturnData data = await GetAccountActiveOrders.Fetch(accountAddress, pageSize, lastOrderId, orderType);

            if (data.data.account == null)
            {
                Debug.LogError("[Exchange] Account could not be found.");
                return null;
            }

            foreach (var order in data.data.account.orders)
            {
                Order orderObj = new Order();
                orderObj.orderId = BigInteger.Parse(order.id);
                if (loadData)
                {
                    await orderObj.Load();
                }

                orderList.Add(orderObj);
            }

            return orderList;
        }

        public async Task<List<Order>> GetAllOrders(string accountAddress, int pageSize = 5, string lastOrderId = "", bool loadData = false)
        {
            List<Order> orderList = new List<Order>();

            // Todo: Check if Account Address is a valid Wallet Address

            GetAccountOrders.ReturnData data = await GetAccountOrders.Fetch(accountAddress, pageSize, lastOrderId);

            if (data.data.account == null)
            {
                Debug.LogError("[Exchange] Account could not be found.");
                return null;
            }

            foreach (var order in data.data.account.orders)
            {
                Order orderObj = new Order();
                orderObj.orderId = BigInteger.Parse(order.id);
                if (loadData)
                {
                    await orderObj.Load();
                }

                orderList.Add(orderObj);
            }

            return orderList;
        }

        public async Task<List<OrderFill>> GetAllOrderFills(string accountAddress, int pageSize = 5, string lastOrderId = "")
        {
            List<OrderFill> orderFillList = new List<OrderFill>();

            // Todo: Check if Account Address is a valid Wallet Address

            GetAccountOrderFills.ReturnData data = await GetAccountOrderFills.Fetch(accountAddress, pageSize, lastOrderId);

            if (data.data.account == null)
            {
                Debug.LogError("[Exchange] Account could not be found.");
                return null;
            }

            foreach (var orderFill in data.data.account.orderFills)
            {
                OrderFill orderFillObj = new OrderFill();
                orderFillObj.Load(orderFill);

                orderFillList.Add(orderFillObj);
            }

            return orderFillList;
        }

        public async Task<BigInteger> GetAssetExchangeVolume(Asset asset)
        {
            if (!asset.IsAssetTokenIdValid())
            {
                Debug.LogError("[Exchange] Invalid Asset Token input.");
                return 0;
            }

            GetAssetExchangeInfo.ReturnData data = await GetAssetExchangeInfo.Fetch(asset.contractAddress, asset.tokenId);

            if (data.data.asset == null)
            {
                Debug.LogError("[Exchange] Asset could not be found.");
                return 0;
            }

            return BigInteger.Parse(data.data.asset.assetVolumeTransacted);
        }

        public async Task<WalletExchangeData> GetWalletExchangeData(string accountAddress)
        {
            // Todo: Check if Account Address is a valid Wallet Address

            GetAccountExchangeInfo.ReturnData data = await GetAccountExchangeInfo.Fetch(accountAddress);

            if (data.data.account == null)
            {
                Debug.LogError("[Exchange] Account could not be found.");
                return null;
            }

            WalletExchangeData walletData = new WalletExchangeData();
            walletData.id = data.data.account.id;
            walletData.address = data.data.account.address;
            walletData.ordersCount = BigInteger.Parse(data.data.account.ordersCount);
            walletData.orderFillsCount = BigInteger.Parse(data.data.account.orderFillsCount);
            walletData.activeBuyOrders = BigInteger.Parse(data.data.account.activeBuyOrders);
            walletData.activeSellOrders = BigInteger.Parse(data.data.account.activeSellOrders);
            walletData.activeOrdersCount = BigInteger.Parse(data.data.account.activeOrdersCount);
            walletData.filledOrdersCount = BigInteger.Parse(data.data.account.filledOrdersCount);
            walletData.volume = BigInteger.Parse(data.data.account.volume);
            walletData.volumeAsBuyer = BigInteger.Parse(data.data.account.volumeAsBuyer);
            walletData.volumeAsSeller = BigInteger.Parse(data.data.account.volumeAsSeller);
            walletData.daysActive = BigInteger.Parse(data.data.account.daysActive);

            return walletData;
        }

        public bool IsValid()
        {
            return isValid;
        }
    }
}