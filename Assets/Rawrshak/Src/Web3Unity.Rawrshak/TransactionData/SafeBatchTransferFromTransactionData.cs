using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

namespace Rawrshak
{
    [Serializable]
    public class SafeBatchTransferFromTransactionData
    {
        string from;
        string to;
        string[] ids;
        string[] amounts;
        string bytes;
    }
}