using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Numerics;
using UnityEngine;
using UnityEngine.Networking;
using GraphQlClient.Core;

namespace Rawrshak
{
    public class GetOrderInfo : QueryBase
    {
        public static ReturnData LastFetchData = null;

        public static async Task<ReturnData> Fetch(BigInteger orderId) {
            // Load query if this is the first Fetch
            string query = LoadQuery(Constants.GET_ORDER_INFO_QUERY_STRING_LOCATION);

            // Load the query parameters
            string orderIdStr = "0x" + orderId.ToString("X");
            string queryWithArgs = String.Format(query, orderIdStr.ToLower());

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
            public Order order;
        }

        [Serializable]
        public class Order
        {
            public string id;
            public Asset asset;
            public string type;
            public string price;
            public string amountOrdered;
            public string amountFilled;
            public string amountClaimed;
            public string status;
            public string createdAtTimestamp;
            public string filledAtTimestamp;
            public string cancelledAtTimestamp;
            public string lastClaimedAtTimestamp;
        }

        [Serializable]
        public class Asset
        {
            public string parentContract;
            public string tokenId;
        }
    }
}
