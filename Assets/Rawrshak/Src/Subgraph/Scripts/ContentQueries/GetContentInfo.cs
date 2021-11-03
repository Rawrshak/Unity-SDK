using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using GraphQlClient.Core;

namespace Rawrshak
{
    public class GetContentInfo : QueryBase
    {
        static string QUERY_STRING_LOCATION = "ContentInfo/GetContentInfo";
        public ReturnData data;

        // async void Start()
        // {
        //     // Test Query
        //     await Fetch("0x393d8e12aa7f22f8999bf9ddac6842db2bb6f096");
        // }

        public async Task Fetch(string address) {
            // Make sure Url has been set.
            CheckSubgraph();
            
            // Load query if this is the first Fetch
            LoadQueryIfEmpty(QUERY_STRING_LOCATION);

            string queryWithArgs = String.Format(query, address.ToLower());
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
            public string name;
            public string game;
            public string creator;
            public string creatorAddress;
            public OwnerData owner;
            public string contractAddress;
            public string contractUri;
            public int royaltyRate;
            public TagData[] tags;
        }

        [Serializable]
        public class OwnerData 
        {
            public string id;
        }

        [Serializable]
        public class TagData 
        {
            public string id;
        }
    }
}
