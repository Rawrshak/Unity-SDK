using System.Collections;
using System.Numerics;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Numerics;
using Newtonsoft.Json;
using UnityEngine;
using WalletConnectSharp.Core.Models.Ethereum;
using WalletConnectSharp.Unity;

namespace Rawrshak
{
    public class ERC20Extension
    {
        private static string AbiFileLocation = "Abis/ERC20";
        private static string abi = Resources.Load<TextAsset>(AbiFileLocation).text;
        
        // Mutative Functions, Rawrshak Extensions
        public static async Task<string> Approve(string _chain, string _network, string _contract, string _spender, BigInteger _amount, string _rpc="")
        {
            string method = "approve";
            object[] inputArgs = {
                _spender,
                _amount
            };

            string args = JsonConvert.SerializeObject(inputArgs);
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
