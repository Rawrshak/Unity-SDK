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
    public class QueryGetAssetsInContentContract : QueryBase
    {
        public QueryGetAssetsInContentContractReturnData data;

        [Serializable]
        public class QueryGetAssetsInContentContractReturnData
        {
            public DataObject data;
            
            public static QueryGetAssetsInContentContractReturnData ParseJson(string jsonString)
            {
                return JsonUtility.FromJson<QueryGetAssetsInContentContractReturnData>(jsonString);
            }
        }

        [Serializable]
        public class DataObject 
        {
            public ContentData content;
        }

        async void Start()
        {
            TextAsset metadataTextAsset=(TextAsset)Resources.Load("GetAssetsInContentContract");
            query = metadataTextAsset.text;

            await Fetch("0x25c71B0B48AE6e8478B3404CEC960a4387f4fDF3", 10, "");
        }

        public async Task Fetch(string contractAddress, int first, string lastId) {
            // Note: Default sorting is by ID and in ascending alphanumeric order (not by creation time)
            string queryWithArgs = String.Format(query, contractAddress.ToLower(), first, lastId);
            Debug.Log(queryWithArgs);

            // Post query
            string returnData = await PostAsync(queryWithArgs);

            // Parse data
            data = QueryGetAssetsInContentContractReturnData.ParseJson(returnData);
            // Debug.Log("TokenId: " + data.data.assets[0].tokenId + ", currentSupply:" + data.data.assets[0].currentSupply);
        }
    }
}
