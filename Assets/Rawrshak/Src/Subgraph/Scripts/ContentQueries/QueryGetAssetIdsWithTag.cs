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
        public ReturnData data;

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
            data = JsonUtility.FromJson<ReturnData>(returnData);
        }

        [Serializable]
        public class ReturnData
        {
            public DataObject data;
        }

        [Serializable]
        public class DataObject 
        {
            public TagData[] tags;
        }

        [Serializable]
        public class TagData 
        {
            public string id;
            public AssetData[] assets;
        }

        [Serializable]
        public class AssetData 
        {
            public string id;
            public string tokenId;
            public ContentIdData parentContract;
        }

        [Serializable]
        public class ContentIdData 
        {
            public string id;
        }
    }
}
