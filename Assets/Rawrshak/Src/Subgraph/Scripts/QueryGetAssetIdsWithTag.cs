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
    public class QueryGetAssetIdsWithTag : QueryBase
    {
        public QueryGetAssetIdsWithTagReturnData data;

        [Serializable]
        public class QueryGetAssetIdsWithTagReturnData
        {
            public DataObject data;
            
            public static QueryGetAssetIdsWithTagReturnData ParseJson(string jsonString)
            {
                return JsonUtility.FromJson<QueryGetAssetIdsWithTagReturnData>(jsonString);
            }
        }

        [Serializable]
        public class DataObject 
        {
            public TagData[] tags;
        }

        async void Start()
        {
            TextAsset metadataTextAsset=(TextAsset)Resources.Load("GetAssetIdsWithTag");
            query = metadataTextAsset.text;

            // Note: These are for test purposes
            // await Fetch(15, "", "Rawrshak");
        }

        public async Task Fetch(int first, string lastId, string tag) {
            string queryWithArgs = String.Format(query, first, lastId, tag);
            Debug.Log(queryWithArgs);

            // Post query
            string returnData = await PostAsync(queryWithArgs);

            // Parse data
            data = QueryGetAssetIdsWithTagReturnData.ParseJson(returnData);
        }
    }
}
