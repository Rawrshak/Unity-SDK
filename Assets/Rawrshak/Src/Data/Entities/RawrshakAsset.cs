using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

namespace Rawrshak
{
    [CreateAssetMenu(fileName="RawrshakAsset", menuName="Rawrshak/Create Rawrshak Asset Object")]
    public class RawrshakAsset : ScriptableObject
    {
        // This is a string because RawrshakAssets received from the GraphQL may be from other contracts that do
        // not belong to this developer.
        public string contract;
        public BigInteger tokenId;
    }
}