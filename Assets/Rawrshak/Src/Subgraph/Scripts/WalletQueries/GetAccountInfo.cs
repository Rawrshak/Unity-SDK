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
        public GetAccountInfoReturnData data;

        [Serializable]
        public class GetAccountInfoReturnData
        {
            public DataObject data;
            
            public static GetAccountInfoReturnData ParseJson(string jsonString)
            {
                return JsonUtility.FromJson<GetAccountInfoReturnData>(jsonString);
            }
        }

        [Serializable]
        public class DataObject 
        {
            public AccountData account;
        }

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
            data = GetAccountInfoReturnData.ParseJson(returnData);
        }
    }
}
