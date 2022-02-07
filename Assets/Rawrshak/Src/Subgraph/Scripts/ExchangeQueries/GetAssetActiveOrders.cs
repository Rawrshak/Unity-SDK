using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using GraphQlClient.Core;

namespace Rawrshak
{
    public class GetAssetActiveOrders : QueryBase
    {
        public static ReturnData LastFetchData = null;

        public static async Task<ReturnData> Fetch(string contractAddress, string tokenId, int pageSize, string lastOrderId, OrderType orderType) {
            // Load query if this is the first Fetch
            string query = LoadQuery(Constants.GET_ASSET_ACTIVE_ORDERS_QUERY_STRING_LOCATION);

            // Load the query parameters
            string queryWithArgs = String.Format(query, contractAddress.ToLower(), tokenId.ToLower(), pageSize, lastOrderId.ToLower(), orderType.ToString());

            // Post query
            string returnData = await PostAsync(Subgraph.Instance.exchangeSubgraphUri, queryWithArgs);

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
            public Asset asset;
        }

        [Serializable]
        public class Asset
        {
            public Order[] orders;
        }

        [Serializable]
        public class Order
        {
            public string id;
        }
    }
}
