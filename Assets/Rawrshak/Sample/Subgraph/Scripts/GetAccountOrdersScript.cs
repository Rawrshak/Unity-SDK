using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetAccountOrdersScript : MonoBehaviour
{
    // Input
    public string accountAddress;
    public int pageSize;
    public string lastOrderId;

    // Return Value
    public Rawrshak.GetAccountOrders.ReturnData data;
    
    // Start is called before the first frame update
    async void Start()
    {
        data = await Rawrshak.GetAccountOrders.Fetch(accountAddress, pageSize, lastOrderId);
    }
}
