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
        public ReturnData data;

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
            public ContentData content;
        }

        [Serializable]
        public class ContentData 
        {
            public string id;
            public AssetData[] assets;
        }

        [Serializable]
        public class AssetData 
        {
            public string id;
            public string tokenId;
            public string name;
            public string type;
            public string subtype;
            public string currentSupply;
            public string maxSupply;
            public string latestPublicUriVersion;
            public string latestHiddenUriVersion;
            public TagData[] tags;
        }

        [Serializable]
        public class TagData 
        {
            public string id;
        }
    }
}