using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using GraphQlClient.Core;

namespace Rawrshak
{
    public class GetAccountExchangeInfo : QueryBase
    {
        public static ReturnData LastFetchData = null;

        public static async Task<ReturnData> Fetch(string accountAddress) {
            // Load query if this is the first Fetch
            string query = LoadQuery(Constants.GET_ACCOUNT_EXCHANGE_INFO_QUERY_STRING_LOCATION);

            // Load the query parameters
            string queryWithArgs = String.Format(query, accountAddress.ToLower());

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
            public string id;
            public string address;
            public string ordersCount;
            public string orderFillsCount;
            public string activeBuyOrders;
            public string activeSellOrders;
            public string activeOrdersCount;
            public string filledOrdersCount;
            public string volume;
            public string volumeAsBuyer;
            public string volumeAsSeller;
            public string daysActive;
        }
    }
}
