using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class TransactionCreator : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        List<BigInteger> array = new List<BigInteger>();
        array.Add(100);
        array.Add(200);
        array.Add(300);

        BigInteger len = array.Count;
        Debug.Log("Length: " + len.ToString("X64"));

        for (int i = 0; i < array.Count; i++) {
            Debug.Log(String.Format("[{0}]: {1}", i, array[i].ToString("X64")));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
