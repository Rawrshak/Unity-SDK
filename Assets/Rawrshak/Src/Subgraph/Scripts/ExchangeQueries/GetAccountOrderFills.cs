using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using GraphQlClient.Core;

namespace Rawrshak
{
    public class GetAccountOrderFills : QueryBase
    {
        public static ReturnData LastFetchData = null;

        public static async Task<ReturnData> Fetch(string accountAddress, int pageSize, string lastOrderId) {
            // Load query if this is the first Fetch
            string query = LoadQuery(Constants.GET_ACCOUNT_ORDER_FILLS_QUERY_STRING_LOCATION);

            // Load the query parameters
            string queryWithArgs = String.Format(query, accountAddress.ToLower(), pageSize, lastOrderId.ToLower());

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
            public Account account;
        }

        [Serializable]
        public class Account
        {
            public OrderFill[] orderFills;
        }

        [Serializable]
        public class OrderFill
        {
            public string id;
            public Order order;
            public string amount;
            public string pricePerItem;
            public string totalPrice;
            public Token token;
            public string createdAtTimestamp;
        }

        [Serializable]
        public class Order
        {
            public string id;
        }

        [Serializable]
        public class Token
        {
            public string address;
        }
    }
}
