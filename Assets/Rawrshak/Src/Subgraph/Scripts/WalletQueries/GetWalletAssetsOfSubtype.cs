using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using GraphQlClient.Core;

namespace Rawrshak
{
    public class GetWalletAssetsOfSubtype : QueryBase
    {
        static string QUERY_STRING_LOCATION = "WalletInfo/GetWalletAssetsOfSubtype";
        public ReturnData data;

        // async void Start()
        // {
        //     // Test Query
        //     await Fetch("0xB796BCe3db9A9DFb3F435A375f69f43a104b4caF", "title", 3, "");
        // }

        public async Task Fetch(string walletAddress, string subtype, int first, string lastId) {
            // Make sure Url has been set.
            CheckSubgraph();
            
            // Load query if this is the first Fetch
            LoadQueryIfEmpty(QUERY_STRING_LOCATION);

            string queryWithArgs = String.Format(query, walletAddress.ToLower(), subtype.ToLower(), first, lastId);
            Debug.Log(queryWithArgs);

            // Post query
            string returnData = await PostAsync(subgraph.contentsSubgraphUri, queryWithArgs);

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
