using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using WalletConnectSharp.Core.Models.Ethereum;
using WalletConnectSharp.Unity;

namespace Rawrshak
{
    // The RawrshakWallet Script queries the subgraph for account information and an account's list of assets. It has
    // several methods of filtering the subgraph query. The RawrshakWallet doesn't keep track of these assets though. 
    // The user should manage that. 
    public class RawrshakWallet : MonoBehaviour
    {
        public BigInteger mintCount = 0;
        public BigInteger burnCount = 0;
        public BigInteger uniqueAssetCount = 0;

        void Start()
        {
            
        }

        void Update()
        {
            
        }

        public async Task LoadAccountInfo()
        {
            GetAccountInfo.ReturnData responseData = await GetAccountInfo.Fetch(GetWallet());

            mintCount = BigInteger.Parse(responseData.data.account.mintCount);
            burnCount = BigInteger.Parse(responseData.data.account.burnCount);
            uniqueAssetCount = BigInteger.Parse(responseData.data.account.uniqueAssetCount);
        }

        public async Task<List<KeyValuePair<RawrshakAsset, int>>> GetAllAssetsInWallet(int amount, string lastId)
        {
            List<KeyValuePair<RawrshakAsset, int>> assets = new List<KeyValuePair<RawrshakAsset, int>>();

            GetAssetsInWallet.ReturnData responseData = await GetAssetsInWallet.Fetch(GetWallet(), amount, lastId);

            foreach (var balanceData in responseData.data.account.assetBalances)
            {
                RawrshakAsset asset = new RawrshakAsset();
                asset.contractAddress = balanceData.asset.parentContract.id;
                asset.contractAddress = balanceData.asset.parentContract.id;
                
                assets.Add(new KeyValuePair<RawrshakAsset, int>(asset, balanceData.amount));
            }

            return assets;
        }

        public async Task<List<KeyValuePair<RawrshakAsset, int>>> GetAssetsInContract(string contractAddress, int amount, string lastId)
        {
            List<KeyValuePair<RawrshakAsset, int>> assets = new List<KeyValuePair<RawrshakAsset, int>>();

            GetWalletAssetsFromContract.ReturnData responseData = await GetWalletAssetsFromContract.Fetch(GetWallet(), contractAddress, amount, lastId);


            foreach (var balanceData in responseData.data.account.assetBalances)
            {
                RawrshakAsset asset = new RawrshakAsset();
                asset.contractAddress = balanceData.asset.parentContract.id;
                asset.contractAddress = balanceData.asset.parentContract.id;
                
                assets.Add(new KeyValuePair<RawrshakAsset, int>(asset, balanceData.amount));
            }

            return assets;
        }

        public async Task<List<KeyValuePair<RawrshakAsset, int>>> GetAssetsOfType(string type, int amount, string lastId)
        {
            List<KeyValuePair<RawrshakAsset, int>> assets = new List<KeyValuePair<RawrshakAsset, int>>();

            GetWalletAssetsOfType.ReturnData responseData = await GetWalletAssetsOfType.Fetch(GetWallet(), type, amount, lastId);

            foreach (var balanceData in responseData.data.account.assetBalances)
            {
                RawrshakAsset asset = new RawrshakAsset();
                asset.contractAddress = balanceData.asset.parentContract.id;
                asset.contractAddress = balanceData.asset.parentContract.id;
                
                assets.Add(new KeyValuePair<RawrshakAsset, int>(asset, balanceData.amount));
            }

            return assets;
        }

        public async Task<List<KeyValuePair<RawrshakAsset, int>>> GetAssetsOfSubtype(string subtype, int amount, string lastId)
        {
            List<KeyValuePair<RawrshakAsset, int>> assets = new List<KeyValuePair<RawrshakAsset, int>>();

            GetWalletAssetsOfSubtype.ReturnData responseData = await GetWalletAssetsOfSubtype.Fetch(GetWallet(), subtype, amount, lastId);

            foreach (var balanceData in responseData.data.account.assetBalances)
            {
                RawrshakAsset asset = new RawrshakAsset();
                asset.contractAddress = balanceData.asset.parentContract.id;
                asset.contractAddress = balanceData.asset.parentContract.id;
                
                assets.Add(new KeyValuePair<RawrshakAsset, int>(asset, balanceData.amount));
            }

            return assets;
        }

        private string GetWallet()
        {
            if (WalletConnect.Instance == null || !WalletConnect.Instance.Connected)
            {
                throw new Exception("No WalletConnect Session.");
            }
            return WalletConnect.ActiveSession.Accounts[0];
        }
    }
}
