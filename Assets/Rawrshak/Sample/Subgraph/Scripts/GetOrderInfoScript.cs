using System.Numerics;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System;
using UnityEngine;

public class GetOrderInfoScript : MonoBehaviour
{
    // Input
    public string orderId;

    // Return Value
    public Rawrshak.GetOrderInfo.ReturnData data;
    
    // Start is called before the first frame update
    async void Start()
    {
        BigInteger orderIdBigInt = BigInteger.Parse(orderId);
        data = await Rawrshak.GetOrderInfo.Fetch(orderIdBigInt);
    }
}
