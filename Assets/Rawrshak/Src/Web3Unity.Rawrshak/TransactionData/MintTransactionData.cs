using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

namespace Rawrshak
{
    [Serializable]
    public class MintTransactionData
    {
        // keccak256("mintBatch((address,uint256[],uint256[],uint256,address,bytes))") = 0x84547d25
        private static string FUNCTION_SELECTOR = "0x84547d25";

        // Mint Public Data
        public string to;
        public List<BigInteger> tokenIds;
        public List<BigInteger> amounts;
        public BigInteger nonce;
        public string signer;
        public string signature;

        // public string GenerateTransactionData() {
        //     // Todo:
        //     return String.Empty;
        // }

        // private string ConvertListOfBigIntToTransactionString(List<BigInteger> array) {
        //     // Todo: This
        //     BigInteger len = array.Count;
        //     Debug.Log("Length: " + len.ToString("X32"));

        //     for (int i = 0; i < array.Count; i++) {
        //         Debug.Log(String.Format("[{0}]: {1}", i, array[i].ToString("X32")));
        //     }

        //     return String.Empty;
        // }
    }
}