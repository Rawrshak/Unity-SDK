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
    public class GetAssetIdsWithTag : QueryBase
    {
        static string QUERY_STRING_LOCATION = "ContentInfo/GetAssetIdsWithTag";
        public ReturnData data;

        // async void Start()
        // {
        //     // Test Query
        //     // await Fetch("Rawrshak", 15, "");
        //     await Fetch("Rawrshak", 5, "0xd0938b7fdb19de29c85f90bcbe33c094a29ae285-2");
        // }

        public async Task Fetch(string tag, int first, string lastId) {
            // Make sure Url has been set.
            CheckSubgraph();
            
            // Load query if this is the first Fetch
            LoadQueryIfEmpty(QUERY_STRING_LOCATION);

            string queryWithArgs = String.Format(query, first, lastId, tag);
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
