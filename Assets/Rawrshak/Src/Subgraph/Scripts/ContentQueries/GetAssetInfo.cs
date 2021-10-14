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
    public class GetAssetInfo : QueryBase
    {
        static string QUERY_STRING_LOCATION = "ContentInfo/GetAssetInfo";
        public ReturnData data;

        async void Start()
        {
            url = "http://localhost:8000/subgraphs/name/gcbsumid/contents";

            await Fetch("0x25c71B0B48AE6e8478B3404CEC960a4387f4fDF3", 0);
        }

        public async Task Fetch(string contractAddress, int tokenId) {
            // Load query if this is the first Fetch
            LoadQueryIfEmpty(QUERY_STRING_LOCATION);
            
            string queryWithArgs = String.Format(query, contractAddress.ToLower(), tokenId);
            Debug.Log(queryWithArgs);

            // Post query
            string returnData = await PostAsync(queryWithArgs);

            // Parse data
            data = JsonUtility.FromJson<ReturnData>(returnData);
            // Debug.Log("TokenId: " + data.data.assets[0].tokenId + ", currentSupply:" + data.data.assets[0].currentSupply);
        }

        [Serializable]
        public class ReturnData
        {
            public DataObject data;
        }

        [Serializable]
        public class DataObject 
        {
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
