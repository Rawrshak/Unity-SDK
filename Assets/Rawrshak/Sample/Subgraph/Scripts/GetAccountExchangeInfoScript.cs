using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetAccountExchangeInfoScript : MonoBehaviour
{
    // Input
    public string accountAddress;

    // Return Value
    public Rawrshak.GetAccountExchangeInfo.ReturnData data;
    
    // Start is called before the first frame update
    async void Start()
    {
        data = await Rawrshak.GetAccountExchangeInfo.Fetch(accountAddress);
    }
}
