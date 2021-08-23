using System;
using System.Collections;
using System.Numerics;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;

namespace Rawrshak
{
    public class Content
    {
        private static string AbiFileLocation = "Abis/Content";
        private static string abi = Resources.Load<TextAsset>(AbiFileLocation).text;

        // ERC1155 API
        public static async Task<BigInteger> BalanceOf(string _chain, string _network, string _contract, string _account, string _tokenId, string _rpc="")
        {
            string method = "balanceOf";
            string[] obj = { _account, _tokenId };
            string args = JsonConvert.SerializeObject(obj);
            string response = await EVM.Call(_chain, _network, _contract, abi, method, args, _rpc);
            try 
            {
                return BigInteger.Parse(response);
            } 
            catch 
            {
                Debug.LogError(response);
                throw;
            }
        }

        public static async Task<List<BigInteger>> BalanceOfBatch(string _chain, string _network, string _contract, string[] _accounts, string[] _tokenIds, string _rpc="")
        {
            string method = "balanceOfBatch";
            string[][] obj = { _accounts, _tokenIds };
            string args = JsonConvert.SerializeObject(obj);
            string response = await EVM.Call(_chain, _network, _contract, abi, method, args, _rpc);
            try 
            {
                string[] responses = JsonConvert.DeserializeObject<string[]>(response);
                List<BigInteger> balances = new List<BigInteger>();
                for (int i = 0; i < responses.Length; i++)
                {
                    balances.Add(BigInteger.Parse(responses[i]));
                }
                return balances;
            } 
            catch 
            {
                Debug.LogError(response);
                throw;
            }        
        }

        public static async Task<string> URI(string _chain, string _network, string _contract, string _tokenId, string _rpc="")
        {
            string method = "uri";
            string[] obj = { _tokenId };
            string args = JsonConvert.SerializeObject(obj);
            string response = await EVM.Call(_chain, _network, _contract, abi, method, args, _rpc);
            return response;
        }

        // Allow the Rawrshak registered systems (Crafting, lootboxing, etc) to manage assets
        public static async Task<string> ApproveAllSystems(string _chain, string _network, string _contract, string _approval, string _rpc="")
        {
            string method = "approveAllSystems";
            string[] obj = { _approval };
            string args = JsonConvert.SerializeObject(obj);
            string response = await EVM.MultiCall(_chain, _network, _contract, abi, method, args, _rpc);
            return response;
        }

        // Get Most recent public token uri
        public static async Task<string> TokenUri(string _chain, string _network, string _contract, string _tokenId, string _rpc="")
        {
            string method = "tokenUri";
            string[] obj = { _tokenId };
            string args = JsonConvert.SerializeObject(obj);
            string response = await EVM.MultiCall(_chain, _network, _contract, abi, method, args, _rpc);
            return response;
        }

        // Get Most recent public token uri with specified version
        public static async Task<string> TokenUriWithVersion(string _chain, string _network, string _contract, string _tokenId, string _version, string _rpc="")
        {
            string method = "tokenUri";
            string[] obj = { _tokenId, _version };
            string args = JsonConvert.SerializeObject(obj);
            string response = await EVM.MultiCall(_chain, _network, _contract, abi, method, args, _rpc);
            return response;
        }

        // Get Most recent private token uri
        public static async Task<string> HiddenTokenUri(string _chain, string _network, string _contract, string _tokenId, string _rpc="")
        {
            string method = "hiddenTokenUri";
            string[] obj = { _tokenId };
            string args = JsonConvert.SerializeObject(obj);
            string response = await EVM.MultiCall(_chain, _network, _contract, abi, method, args, _rpc);
            return response;
        }

        // Get Most recent private token uri with specified version
        public static async Task<string> HiddenTokenUriWithVersion(string _chain, string _network, string _contract, string _tokenId, string _version, string _rpc="")
        {
            string method = "hiddenTokenUri";
            string[] obj = { _tokenId, _version };
            string args = JsonConvert.SerializeObject(obj);
            string response = await EVM.MultiCall(_chain, _network, _contract, abi, method, args, _rpc);
            return response;
        }

        // Get the current supply of an asset 
        public static async Task<BigInteger> Supply(string _chain, string _network, string _contract, string _rpc="")
        {
            string method = "supply";
            string[] obj = {};
            string args = JsonConvert.SerializeObject(obj);
            string response = await EVM.MultiCall(_chain, _network, _contract, abi, method, args, _rpc);
            return BigInteger.Parse(response);
        }
        
        // Get the max supply of an asset 
        public static async Task<BigInteger> MaxSupply(string _chain, string _network, string _contract, string _rpc="")
        {
            string method = "maxSupply";
            string[] obj = {};
            string args = JsonConvert.SerializeObject(obj);
            string response = await EVM.MultiCall(_chain, _network, _contract, abi, method, args, _rpc);
            return BigInteger.Parse(response);
        }

        // Mint an array of assets; MintData must be signed by internal developer wallet
        public static async Task<string> MintBatch(string _chain, string _network, string _contract, MintTransactionData data, string _rpc="")
        {
            string method = "mintBatch";
            string args = JsonConvert.SerializeObject(data);
            string response = await EVM.MultiCall(_chain, _network, _contract, abi, method, args, _rpc);
            return response;
        }

        // Burn an array of assets; only user can burn
        public static async Task<string> BurnBatch(string _chain, string _network, string _contract, BurnTransactionData data, string _rpc="")
        {
            string method = "burnBatch";
            string args = JsonConvert.SerializeObject(data);
            string response = await EVM.MultiCall(_chain, _network, _contract, abi, method, args, _rpc);
            return response;
        }
    }
}
