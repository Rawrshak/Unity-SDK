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
    public class QueryGetAssetInfo : QueryBase
    {
        public QueryGetAssetInfoReturnData data;

        [Serializable]
        public class QueryGetAssetInfoReturnData
        {
            public DataObject data;
            
            public static QueryGetAssetInfoReturnData ParseJson(string jsonString)
            {
                return JsonUtility.FromJson<QueryGetAssetInfoReturnData>(jsonString);
            }
        }

        [Serializable]
        public class DataObject 
        {
            public AssetData[] assets;
        }

        async void Start()
        {
            TextAsset metadataTextAsset=(TextAsset)Resources.Load("GetAssetInfo");
            query = metadataTextAsset.text;

            await Fetch("0x25c71B0B48AE6e8478B3404CEC960a4387f4fDF3", 0);
        }

        public async Task Fetch(string contractAddress, int tokenId) {
            string queryWithArgs = String.Format(query, contractAddress.ToLower(), tokenId);
            Debug.Log(queryWithArgs);

            // Post query
            string returnData = await PostAsync(queryWithArgs);

            // Parse data
            data = QueryGetAssetInfoReturnData.ParseJson(returnData);
            // Debug.Log("TokenId: " + data.data.assets[0].tokenId + ", currentSupply:" + data.data.assets[0].currentSupply);
        }
    }
}
