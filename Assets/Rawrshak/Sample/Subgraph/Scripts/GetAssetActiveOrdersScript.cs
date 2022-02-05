using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rawrshak;

public class GetAssetActiveOrdersScript : MonoBehaviour
{
    // Input
    public string contractAddress;
    public string tokenId;
    public int pageSize;
    public string lastOrderId;
    public OrderType orderType;

    // Return Value
    public GetAssetActiveOrders.ReturnData data;
    
    // Start is called before the first frame update
    async void Start()
    {
        data = await GetAssetActiveOrders.Fetch(contractAddress, tokenId, pageSize, lastOrderId, orderType);
    }
}
