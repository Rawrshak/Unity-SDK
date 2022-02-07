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
        public static ReturnData LastFetchData = null;

        public static async Task<ReturnData> Fetch(string contractAddress, int first, string lastId) {
            // Load query if this is the first Fetch
            string query = LoadQuery(Constants.GET_ASSETS_IN_CONTENT_CONTRACT_QUERY_STRING_LOCATION);

            // Note: Default sorting is by ID and in ascending alphanumeric order (not by creation time)
            // Load the query parameters
            string queryWithArgs = String.Format(query, contractAddress.ToLower(), first, lastId);

            // Post query
            string returnData = await PostAsync(Subgraph.Instance.contentsSubgraphUri, queryWithArgs);

            // Parse data
            LastFetchData = JsonUtility.FromJson<ReturnData>(returnData);
            return LastFetchData;
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
