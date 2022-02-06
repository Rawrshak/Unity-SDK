using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using GraphQlClient.Core;

namespace Rawrshak
{
    public class GetAccountInfo : QueryBase
    {
        public static ReturnData LastFetchData = null;

        public static async Task<ReturnData> Fetch(string address) {
            // Load query if this is the first Fetch
            string query = LoadQuery(Constants.GET_ACCOUNT_INFO_QUERY_STRING_LOCATION);

            // Load the query parameters
            string queryWithArgs = String.Format(query, address.ToLower());

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
            public string id;
            public string address;
            public string mintCount;
            public string burnCount;
            public string uniqueAssetCount;
        }
    }
}
