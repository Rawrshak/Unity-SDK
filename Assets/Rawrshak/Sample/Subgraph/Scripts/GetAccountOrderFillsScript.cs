using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetAccountOrderFillsScript : MonoBehaviour
{
    // Input
    public string accountAddress;
    public int pageSize;
    public string lastOrderId;

    // Return Value
    public Rawrshak.GetAccountOrderFills.ReturnData data;
    
    // Start is called before the first frame update
    async void Start()
    {
        data = await Rawrshak.GetAccountOrderFills.Fetch(accountAddress, pageSize, lastOrderId);
    }
}
