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

        public async Task ApproveNftContract(string contractAddress, bool approved)
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

        public bool IsValid()
        {
            return isValid;
        }
    }
}