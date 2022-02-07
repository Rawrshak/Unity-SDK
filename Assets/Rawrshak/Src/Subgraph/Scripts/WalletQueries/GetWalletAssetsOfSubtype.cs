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
        public static ReturnData LastFetchData = null;
        
        public static async Task<ReturnData> Fetch(string walletAddress, string subtype, int first, string lastId) {
            // Load query if this is the first Fetch
            string query = LoadQuery(Constants.GET_WALLET_ASSETS_OF_SUBTYPE_QUERY_STRING_LOCATION);

            // Load the query parameters
            string queryWithArgs = String.Format(query, walletAddress.ToLower(), subtype.ToLower(), first, lastId);

            // Post query
            string returnData = await PostAsync(Subgraph.Instance.contentsSubgraphUri, queryWithArgs);

            // Parse data
            LastFetchData = JsonUtility.FromJson<ReturnData>(returnData);
            return LastFetchData;
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
