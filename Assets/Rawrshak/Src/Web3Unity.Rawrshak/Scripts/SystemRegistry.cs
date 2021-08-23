using System.Collections;
using System.Numerics;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;

namespace Rawrshak
{
    public class SystemRegistry
    {
        private static string AbiFileLocation = "Abis/SystemRegistry";
        private static string abi = Resources.Load<TextAsset>(AbiFileLocation).text;

        public static async Task<bool> IsOperatorRegistered(string _chain, string _network, string _contract, string _operator, string _rpc="")
        {
            string method = "isOperatorRegistered";
            string[] obj = { _operator };
            string args = JsonConvert.SerializeObject(obj);
            string response = await EVM.Call(_chain, _network, _contract, abi, method, args, _rpc);
            try 
            {
                return response.ToLower() == "true";
            } 
            catch 
            {
                Debug.LogError(response);
                throw;
            }
        }

        public static async Task<bool> UserApproval(string _chain, string _network, string _contract, string _user, string _rpc="")
        {
            string method = "userApproval";
            string[] obj = { _user };
            string args = JsonConvert.SerializeObject(obj);
            string response = await EVM.Call(_chain, _network, _contract, abi, method, args, _rpc);
            try 
            {
                return response.ToLower() == "true";
            } 
            catch 
            {
                Debug.LogError(response);
                throw;
            }
        }
    }
}
