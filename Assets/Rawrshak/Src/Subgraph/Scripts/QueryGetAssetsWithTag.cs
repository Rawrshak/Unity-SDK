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
    public class QueryGetAssetsWithTag : QueryBase
    {
        public QueryGetAssetsWithTagReturnData data;

        [Serializable]
        public class QueryGetAssetsWithTagReturnData
        {
            public DataObject data;
            
            public static QueryGetAssetsWithTagReturnData ParseJson(string jsonString)
            {
                return JsonUtility.FromJson<QueryGetAssetsWithTagReturnData>(jsonString);
            }
        }

        [Serializable]
        public class DataObject 
        {
            public TagData[] tags;
        }

        async void Start()
        {
            TextAsset metadataTextAsset=(TextAsset)Resources.Load("GetAssetsWithTag");
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
            data = QueryGetAssetsWithTagReturnData.ParseJson(returnData);
        }
    }
}
