using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Threading.Tasks;
using UnityEngine;

namespace Rawrshak
{
    public class OrderFill
    {
        public string id;
        public BigInteger matchingOrderId;
        public BigInteger amount;
        public BigInteger pricePerItem;
        public BigInteger totalPrice;
        public string tokenAddress;
        public BigInteger createdAtTimestamp;

        public void Load(GetAccountOrderFills.OrderFill data)
        {
            id = data.id;
            matchingOrderId = BigInteger.Parse(data.order.id);
            amount = BigInteger.Parse(data.amount);
            pricePerItem = BigInteger.Parse(data.pricePerItem);
            totalPrice = BigInteger.Parse(data.totalPrice);
            tokenAddress = data.token.address;
            createdAtTimestamp = BigInteger.Parse(data.createdAtTimestamp);
        }
    }
}