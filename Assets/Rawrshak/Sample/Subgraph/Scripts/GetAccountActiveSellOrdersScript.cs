using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetAccountActiveSellOrdersScript : MonoBehaviour
{
    // Input
    public string accountAddress;
    public int pageSize;
    public string lastOrderId;

    // Return Value
    public Rawrshak.GetAccountActiveSellOrders.ReturnData data;
    
    // Start is called before the first frame update
    async void Start()
    {
        data = await Rawrshak.GetAccountActiveSellOrders.Fetch(accountAddress, pageSize, lastOrderId);
    }
}
