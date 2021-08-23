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
        public string to;
        public List<BigInteger> tokenIds;
        public List<BigInteger> amounts;
        public BigInteger nonce;
        public string signer;
        public string signature;
    }
}