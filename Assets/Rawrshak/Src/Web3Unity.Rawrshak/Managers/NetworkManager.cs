using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

namespace Rawrshak
{
    [CreateAssetMenu(fileName="NetworkManager", menuName="Rawrshak/Create Network Manager Object")]
    public class NetworkManager : SingletonScriptableObject<NetworkManager>
    {
        public string chain = "ethereum";
        public string network = "optimistic-kovan";
        public BigInteger chainId = 69;
        public string httpEndpoint = "https://kovan.optimism.io";
    }
}