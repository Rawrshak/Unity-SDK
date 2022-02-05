using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Newtonsoft.Json;

namespace Rawrshak
{
    [Serializable]
    public class FillOrderTransactionData
    {
        public List<BigInteger> orderIds = null;
        public BigInteger amount = 0;
        public BigInteger maxSpend = 0;
        
        public string GenerateArgsForCreateContractData() {
            object[] ordersData = {
                orderIds,
                amount,
                maxSpend
            };
            object[][] results = { ordersData };
            return JsonConvert.SerializeObject(results);
        }
    }
}