using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using GraphQlClient.Core;

namespace Rawrshak
{
    public class GetAccountOrders : QueryBase
    {
        public static async Task<ReturnData> Fetch(string accountAddress, int pageSize, string lastOrderId) {
            // Load query if this is the first Fetch
            string query = LoadQuery(Constants.GET_ACCOUNT_ORDERS_QUERY_STRING_LOCATION);

            // Load the query parameters
            string queryWithArgs = String.Format(query, accountAddress.ToLower(), pageSize, lastOrderId.ToLower());

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
            public Account account;
        }

        [Serializable]
        public class Account
        {
            public Order[] orders;
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
