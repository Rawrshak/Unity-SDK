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
    public class QueryGetAssetInfoFromName : MonoBehaviour
    {
        public GraphApi contentsSubgraph;

        public QueryGetAssetInfoFromNameReturnData data;

        async void Start()
        {
            await FetchAssetInfo("0x25c71B0B48AE6e8478B3404CEC960a4387f4fDF3", "Demoman Title");
        }

        public async Task FetchAssetInfo(string contractAddress, string _name) {
            if (contentsSubgraph == null) {
                Debug.LogError("Content Subgraph API not set");
                return;
            }

            // Get the Query and set Args
            GraphApi.Query query = contentsSubgraph.GetQueryByName("GetAssetInfoFromName", GraphApi.Query.Type.Query);

            query.SetArgs(new{ where = new {parentContract = contractAddress.ToLower(), name = _name}});

            // Post query
            UnityWebRequest request = await contentsSubgraph.Post(query);
            Debug.Log(HttpHandler.FormatJson(request.downloadHandler.text));

            // Parse data
            data = QueryGetAssetInfoFromNameReturnData.ParseJson(request.downloadHandler.text);
            Debug.Log("TokenId: " + data.data.assets[0].tokenId + ", currentSupply:" + data.data.assets[0].currentSupply);
        }

        [Serializable]
        public class QueryGetAssetInfoFromNameReturnData
        {
            public DataObject data;
            
            public static QueryGetAssetInfoFromNameReturnData ParseJson(string jsonString)
            {
                return JsonUtility.FromJson<QueryGetAssetInfoFromNameReturnData>(jsonString);
            }
        }

        [Serializable]
        public class DataObject 
        {
            public AssetData[] assets;
        }
    }
}
