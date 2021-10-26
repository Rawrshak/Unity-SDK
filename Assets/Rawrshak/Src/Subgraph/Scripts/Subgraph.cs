using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

namespace Rawrshak
{
    [CreateAssetMenu(fileName="Subgraph", menuName="Rawrshak/Create Subgraph Object")]
    public class Subgraph : SingletonScriptableObject<Subgraph>
    {
        public string contentsSubgraphUri = "https://api.thegraph.com/subgraphs/name/gcbsumid/contents";
        public string exchangeSubgraphUri = "https://api.thegraph.com/subgraphs/name/gcbsumid/exchange";
    }
}