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
    public class QueryGetAssetInfo : MonoBehaviour
    {
        public GraphApi contentsSubgraph;

        public QueryGetAssetInfoReturnData data;

        private string query;

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
            await FetchAssetInfo("0x25c71B0B48AE6e8478B3404CEC960a4387f4fDF3", 0);
        }

        public async Task FetchAssetInfo(string contractAddress, int _tokenId) {
            if (contentsSubgraph == null) {
                Debug.LogError("Content Subgraph API not set");
                return;
            }

            // // Get the Query and set Args
            GraphApi.Query query = contentsSubgraph.GetQueryByName("GetAssetInfoFromTokenId", GraphApi.Query.Type.Query);

            query.SetArgs(new{ where = new {parentContract = contractAddress.ToLower(), tokenId = _tokenId}});

            // Post query
            UnityWebRequest request = await contentsSubgraph.Post(query);
            Debug.Log(HttpHandler.FormatJson(request.downloadHandler.text));

            // Parse data
            data = QueryGetAssetInfoReturnData.ParseJson(request.downloadHandler.text);
            Debug.Log("TokenId: " + data.data.assets[0].tokenId + ", currentSupply:" + data.data.assets[0].currentSupply);
        }
    }
}
