using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rawrshak;

public class GetAccountActiveOrdersScript : MonoBehaviour
{
    // Input
    public string accountAddress;
    public int pageSize;
    public string lastOrderId;
    public OrderType orderType;

    // Return Value
    public GetAccountActiveOrders.ReturnData data;
    
    // Start is called before the first frame update
    async void Start()
    {
        data = await GetAccountActiveOrders.Fetch(accountAddress, pageSize, lastOrderId, orderType);
    }
}
