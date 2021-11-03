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

        // async void Start()
        // {
        //     // Test Query
        //     await Fetch("0x393d8e12aa7f22f8999bf9ddac6842db2bb6f096", 6, "");
        //     // await Fetch("0x393d8e12aa7f22f8999bf9ddac6842db2bb6f096", 6, "0x393d8e12aa7f22f8999bf9ddac6842db2bb6f096-5");
        // }

        public async Task Fetch(string contractAddress, int first, string lastId) {
            // Make sure Url has been set.
            CheckSubgraph();
            
            // Load query if this is the first Fetch
            LoadQueryIfEmpty(QUERY_STRING_LOCATION);

            // Note: Default sorting is by ID and in ascending alphanumeric order (not by creation time)
            string queryWithArgs = String.Format(query, contractAddress.ToLower(), first, lastId);
            Debug.Log(queryWithArgs);

            // Post query
            string returnData = await PostAsync(subgraph.contentsSubgraphUri, queryWithArgs);

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
            public string imageUri;
            public string currentSupply;
            public string maxSupply;
            public string latestPublicUriVersion;
            public string latestHiddenUriVersion;
            public string latestPublicUri;
            public TagData[] tags;
        }

        [Serializable]
        public class TagData 
        {
            public string id;
        }
    }
}
