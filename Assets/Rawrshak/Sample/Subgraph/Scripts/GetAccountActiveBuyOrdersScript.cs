using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetAccountActiveBuyOrdersScript : MonoBehaviour
{
    // Input
    public string accountAddress;
    public int pageSize;
    public string lastOrderId;

    // Return Value
    public Rawrshak.GetAccountActiveBuyOrders.ReturnData data;
    
    // Start is called before the first frame update
    async void Start()
    {
        data = await Rawrshak.GetAccountActiveBuyOrders.Fetch(accountAddress, pageSize, lastOrderId);
    }
}
