using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetAssetExchangeInfoScript : MonoBehaviour
{
    // Input
    public string contractAddress;
    public string tokenId;

    // Return Value
    public Rawrshak.GetAssetExchangeInfo.ReturnData data;
    
    // Start is called before the first frame update
    async void Start()
    {
        data = await Rawrshak.GetAssetExchangeInfo.Fetch(contractAddress, tokenId);
    }
}
