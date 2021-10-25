using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

namespace Rawrshak
{
    [Serializable]
    public class BurnTransactionData
    {
        string account;
        List<BigInteger> tokenIds;
        List<BigInteger> amounts;
    }
}