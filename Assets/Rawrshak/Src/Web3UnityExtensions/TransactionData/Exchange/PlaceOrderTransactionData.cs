using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Newtonsoft.Json;

namespace Rawrshak
{
    [Serializable]
    public class PlaceOrderTransactionData
    {        
        public class AssetData {
            public string contractAddress;
            public BigInteger tokenId;
        }

        public AssetData asset;
        public string owner;
        public string token;
        public BigInteger price;
        public BigInteger amount;
        public bool isBuyOrder; 
        
        public string GenerateArgsForCreateContractData() {
            object[] assetData = {
                asset.contractAddress,
                asset.tokenId
            };

            object[] orderInputData = {
                assetData,
                owner,
                token,
                price,
                amount,
                isBuyOrder
            };
            object[][] results = { orderInputData };
            return JsonConvert.SerializeObject(results);
        }
    }
}