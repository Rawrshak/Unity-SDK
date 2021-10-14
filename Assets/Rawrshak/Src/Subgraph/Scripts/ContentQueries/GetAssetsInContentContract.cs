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
    public class GetAssetsInContentContract : QueryBase
    {
        static string QUERY_STRING_LOCATION = "ContentInfo/GetAssetsInContentContract";
        public ReturnData data;

        async void Start()
        {
            url = "http://localhost:8000/subgraphs/name/gcbsumid/contents";

            await Fetch("0x25c71B0B48AE6e8478B3404CEC960a4387f4fDF3", 10, "");
        }

        public async Task Fetch(string contractAddress, int first, string lastId) {
            // Load query if this is the first Fetch
            LoadQueryIfEmpty(QUERY_STRING_LOCATION);

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
