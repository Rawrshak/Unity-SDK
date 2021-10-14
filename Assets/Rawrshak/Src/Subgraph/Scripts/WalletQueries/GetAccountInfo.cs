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
        public ReturnData data;

        async void Start()
        {
            TextAsset metadataTextAsset=(TextAsset)Resources.Load("GetAccountInfo");
            query = metadataTextAsset.text;

            // await Fetch("0x25c71B0B48AE6e8478B3404CEC960a4387f4fDF3");
        }

        public async Task Fetch(string address) {
            string queryWithArgs = String.Format(query, address.ToLower());
            Debug.Log(queryWithArgs);

            // Post query
            string returnData = await PostAsync(queryWithArgs);

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
