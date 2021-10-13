using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using GraphQlClient.Core;

namespace Rawrshak
{
    public class GetAssetsInWallet : QueryBase
    {
        public GetAssetsInWalletReturnData data;

        [Serializable]
        public class GetAssetsInWalletReturnData
        {
            public DataObject data;
            
            public static GetAssetsInWalletReturnData ParseJson(string jsonString)
            {
                return JsonUtility.FromJson<GetAssetsInWalletReturnData>(jsonString);
            }
        }

        [Serializable]
        public class DataObject 
        {
            public AccountAssetBalancesData account;
        }

        async void Start()
        {
            TextAsset metadataTextAsset=(TextAsset)Resources.Load("GetAssetsInWallet");
            query = metadataTextAsset.text;

            // await Fetch("", 10, "");
        }

        public async Task Fetch(string address, int first, string lastId) {
            string queryWithArgs = String.Format(query, address.ToLower());
            Debug.Log(queryWithArgs);

            // Post query
            string returnData = await PostAsync(queryWithArgs);

            // Parse data
            data = GetAssetsInWalletReturnData.ParseJson(returnData);
        }
    }
}
