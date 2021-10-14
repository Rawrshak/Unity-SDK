using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using GraphQlClient.Core;

namespace Rawrshak
{
    public class GetWalletAssetsFromContract : QueryBase
    {
        public ReturnData data;

        async void Start()
        {
            TextAsset metadataTextAsset=(TextAsset)Resources.Load("GetWalletAssetsFromContract");
            query = metadataTextAsset.text;

            // await Fetch("", 10, "");
        }

        public async Task Fetch(string walletAddress, string contractAddress, int first, string lastId) {
            string queryWithArgs = String.Format(query, walletAddress.ToLower(), contractAddress.ToLower(), first, lastId);
            Debug.Log(queryWithArgs);

            // Post query
            string returnData = await PostAsync(queryWithArgs);

            // Parse data
            data = JsonUtility.FromJson<ReturnData>(returnData);
        }

        [Serializable]
        public class ReturnData
        {
            public DataObject data;
        }

        [Serializable]
        public class DataObject 
        {
            public AccountData account;
        }
    
        [Serializable]
        public class AccountData
        {
            public AssetBalanceData[] assetBalances;
        }
        
        [Serializable]
        public class AssetBalanceData 
        {
            public string id;
            public int amount;
            public AssetData asset;
        }

        [Serializable]
        public class AssetData 
        {
            public string id;
            public string tokenId;
            public ContentData parentContract;
        }

        [Serializable]
        public class ContentData 
        {
            public string id;
        }
    }
}
