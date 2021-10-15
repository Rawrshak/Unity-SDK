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
        async void Start()
        {
            url = "http://localhost:8000/subgraphs/name/gcbsumid/contents";

            // Test Query
            // await Fetch("0x25c71B0B48AE6e8478B3404CEC960a4387f4fDF3");
        }

        public async Task Fetch(string address) {
            // Make sure Url has been set.
            CheckUrl();
            
            // Load query if this is the first Fetch
            LoadQueryIfEmpty(QUERY_STRING_LOCATION);

            string queryWithArgs = String.Format(query, address.ToLower());
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
            public string name;
            public string game;
            public string creator;
            public string owner;
            public string contractAddress;
            public string contractUri;
            public int royaltyRate;
            public TagData[] tags;
        }

        [Serializable]
        public class TagData 
        {
            public string id;
        }
    }
}
