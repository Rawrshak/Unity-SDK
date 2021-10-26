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
        static string QUERY_STRING_LOCATION = "WalletInfo/GetAccountInfo";
        public ReturnData data;

        // async void Start()
        // {
        //     // Test Query
        //     await Fetch("0xB796BCe3db9A9DFb3F435A375f69f43a104b4caF");
        // }

        public async Task Fetch(string address) {
            // Make sure Url has been set.
            CheckSubgraph();
            
            // Load query if this is the first Fetch
            LoadQueryIfEmpty(QUERY_STRING_LOCATION);

            string queryWithArgs = String.Format(query, address.ToLower());
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
            
            public static ReturnData ParseJson(string jsonString)
            {
                return JsonUtility.FromJson<ReturnData>(jsonString);
            }
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
