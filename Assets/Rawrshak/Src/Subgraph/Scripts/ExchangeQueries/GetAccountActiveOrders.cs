using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using GraphQlClient.Core;

namespace Rawrshak
{
    public class GetAccountActiveOrders : QueryBase
    {
        public static async Task<ReturnData> Fetch(string accountAddress, int pageSize, string lastOrderId, OrderType orderType) {
            // Load query if this is the first Fetch
            string query = LoadQuery(Constants.GET_ACCOUNT_ACTIVE_ORDERS_QUERY_STRING_LOCATION);

            // Load the query parameters
            string queryWithArgs = String.Format(query, accountAddress.ToLower(), pageSize, lastOrderId.ToLower(), orderType.ToString());

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
        }
    }
}
