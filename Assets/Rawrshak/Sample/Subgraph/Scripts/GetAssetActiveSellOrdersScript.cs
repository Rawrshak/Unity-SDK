using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetAssetActiveSellOrdersScript : MonoBehaviour
{
    // Input
    public string contractAddress;
    public string tokenId;
    public int pageSize;
    public string lastOrderId;

    // Return Value
    public Rawrshak.GetAssetActiveSellOrders.ReturnData data;
    
    // Start is called before the first frame update
    async void Start()
    {
        data = await Rawrshak.GetAssetActiveSellOrders.Fetch(contractAddress, tokenId, pageSize, lastOrderId);
    }
}
