using System;
using System.Collections;
using System.Numerics;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;
using WalletConnectSharp.Core.Models.Ethereum;
using WalletConnectSharp.Unity;

namespace Rawrshak
{
    public class ExchangeManager
    {
        private static string AbiFileLocation = "Abis/Exchange";
        private static string abi = Resources.Load<TextAsset>(AbiFileLocation).text;

        public class AssetData {
            public string contractAddress;
            public string tokenId;
        }

        public class Order {
            public AssetData asset;
            public string owner;
            public string token;
            public string price;
            public string amountOrdered;
            public string amountFilled;
            public bool isBuyOrder;
            public string state;
        }

        public class OrderResponse {
            public Order order;
        }

        public class ClaimableRoyaltiesResponse {
            public string[] tokens;
            public string[] amounts;
        }

        // View Functions
        public static async Task<OrderResponse> GetOrderInfo(string _chain, string _network, string _contract, string _orderId, string _rpc="")
        {
            string method = "getOrder";
            string[] obj = { _orderId };
            string args = JsonConvert.SerializeObject(obj);
            string response = await EVM.Call(_chain, _network, _contract, abi, method, args, _rpc);

            OrderResponse orderData = JsonUtility.FromJson<OrderResponse>(response);
            return orderData;
        }
        
        public static async Task<string> GetTokenEscrowAddress(string _chain, string _network, string _contract, string _rpc="")
        {
            string method = "tokenEscrow";
            string[] obj = { };
            string args = JsonConvert.SerializeObject(obj);
            return await EVM.Call(_chain, _network, _contract, abi, method, args, _rpc);
        }
        
        public static async Task<string> GetNftEscrowAddress(string _chain, string _network, string _contract, string _rpc="")
        {
            string method = "nftsEscrow";
            string[] obj = { };
            string args = JsonConvert.SerializeObject(obj);
            return await EVM.Call(_chain, _network, _contract, abi, method, args, _rpc);
        }

        public static async Task<ClaimableRoyaltiesResponse> GetOrderInfo(string _chain, string _network, string _contract, string _rpc="")
        {
            string method = "claimableRoyalties";
            string[] obj = { };
            string args = JsonConvert.SerializeObject(obj);
            string response = await EVM.Call(_chain, _network, _contract, abi, method, args, _rpc);

            ClaimableRoyaltiesResponse orderData = JsonUtility.FromJson<ClaimableRoyaltiesResponse>(response);
            return orderData;
        }

        // Mutative Functions
        public static async Task<string> PlaceOrder(string _chain, string _network, string _contract, PlaceOrderTransactionData data, string _rpc="")
        {
            string method = "placeOrder";
            string args = data.GenerateArgsForCreateContractData();
            string contractData = await EVM.CreateContractData(abi, method, args);
            
            string address = WalletConnect.ActiveSession.Accounts[0];
            var transaction = new TransactionData()
            {
                from = address,
                to = _contract,
                data = contractData
            };
            return await WalletConnect.ActiveSession.EthSendTransaction(transaction);
        }

        public static async Task<string> FillBuyOrder(string _chain, string _network, string _contract, FillOrderTransactionData _data, string _rpc="")
        {
            string method = "fillBuyOrder";
            string args = _data.GenerateArgsForCreateContractData();
            string contractData = await EVM.CreateContractData(abi, method, args);
            
            string address = WalletConnect.ActiveSession.Accounts[0];
            var transaction = new TransactionData()
            {
                from = address,
                to = _contract,
                data = contractData
            };
            return await WalletConnect.ActiveSession.EthSendTransaction(transaction);
        }

        public static async Task<string> FillSellOrder(string _chain, string _network, string _contract, FillOrderTransactionData _data, string _rpc="")
        {
            string method = "fillSellOrder";
            string args = _data.GenerateArgsForCreateContractData();
            string contractData = await EVM.CreateContractData(abi, method, args);
            
            string address = WalletConnect.ActiveSession.Accounts[0];
            var transaction = new TransactionData()
            {
                from = address,
                to = _contract,
                data = contractData
            };
            return await WalletConnect.ActiveSession.EthSendTransaction(transaction);
        }

        public static async Task<string> CancelOrders(string _chain, string _network, string _contract, List<BigInteger> _orderIds, string _rpc="")
        {
            string method = "cancelOrders";
            object[] orderIds = {
                _orderIds
            };
            string args = JsonConvert.SerializeObject(orderIds);
            string contractData = await EVM.CreateContractData(abi, method, args);
            
            string address = WalletConnect.ActiveSession.Accounts[0];
            var transaction = new TransactionData()
            {
                from = address,
                to = _contract,
                data = contractData
            };
            return await WalletConnect.ActiveSession.EthSendTransaction(transaction);
        }

        public static async Task<string> ClaimOrders(string _chain, string _network, string _contract, List<BigInteger> _orderIds, string _rpc="")
        {
            string method = "claimOrders";
            object[] orderIds = {
                _orderIds
            };
            string args = JsonConvert.SerializeObject(orderIds);
            string contractData = await EVM.CreateContractData(abi, method, args);
            
            string address = WalletConnect.ActiveSession.Accounts[0];
            var transaction = new TransactionData()
            {
                from = address,
                to = _contract,
                data = contractData
            };
            return await WalletConnect.ActiveSession.EthSendTransaction(transaction);
        }
    }
}
