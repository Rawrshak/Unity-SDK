using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using GraphQlClient.Core;

namespace Rawrshak
{
    public class GetAssetExchangeInfo : QueryBase
    {
        public static async Task<ReturnData> Fetch(string contractAddress, string tokenId) {
            // Load query if this is the first Fetch
            string query = LoadQuery(Constants.GET_ASSET_EXCHANGE_INFO_QUERY_STRING_LOCATION);

            // Load the query parameters
            string queryWithArgs = String.Format(query, contractAddress.ToLower(), tokenId.ToLower());

            // Post query
            string returnData = await PostAsync(Subgraph.Instance.exchangeSubgraphUri, queryWithArgs);

            // Parse data
            return JsonUtility.FromJson<ReturnData>(returnData);
        }

        [Serializable]
        public class ReturnData
        {
            public DataObject data;
        }

        [Serializable]
        public class DataObject 
        {
            public Asset assets;
        }

        [Serializable]
        public class Asset
        {
            public string assetVolumeTransacted;
        }
    }
}
