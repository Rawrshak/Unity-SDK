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
        string to;
        BigInteger[] tokenIds;
        BigInteger[] amounts;
        BigInteger nonce;
        string signer;
    }
}