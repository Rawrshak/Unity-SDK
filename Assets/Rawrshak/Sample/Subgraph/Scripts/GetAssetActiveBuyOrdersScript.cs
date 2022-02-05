using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetAssetActiveBuyOrdersScript : MonoBehaviour
{
    // Input
    public string contractAddress;
    public string tokenId;
    public int pageSize;
    public string lastOrderId;

    // Return Value
    public Rawrshak.GetAssetActiveBuyOrders.ReturnData data;
    
    // Start is called before the first frame update
    async void Start()
    {
        data = await Rawrshak.GetAssetActiveBuyOrders.Fetch(contractAddress, tokenId, pageSize, lastOrderId);
    }
}
