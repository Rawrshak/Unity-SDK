using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

namespace Rawrshak
{
    [CreateAssetMenu(fileName="Network", menuName="Rawrshak/Create Network Object")]
    public class Network : ScriptableObject
    {
        public string chain;
        public string network;
        public BigInteger chainId;
    }
}