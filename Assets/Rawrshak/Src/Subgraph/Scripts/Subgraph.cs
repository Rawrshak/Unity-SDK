using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

namespace Rawrshak
{
    [CreateAssetMenu(fileName="Subgraph", menuName="Rawrshak/Create Subgraph Object")]
    public class Subgraph : SingletonScriptableObject<Subgraph>
    {
        // // These are the deployed subgraph uri
        // public string contentsSubgraphUri = "https://api.thegraph.com/subgraphs/name/gcbsumid/contents";
        // public string exchangeSubgraphUri = "https://api.thegraph.com/subgraphs/name/gcbsumid/exchange";
        
        // // These are the pending subgraph uri
        public string contentsSubgraphUri = "https://api.thegraph.com/subgraphs/id/QmZGnRxXUmFaw7UrgLXTNk8tmF2KwknZfGRa3dUHUto5JM";
        public string exchangeSubgraphUri = "https://api.thegraph.com/subgraphs/id/QmdH6EPwh1saKq52hQjGYayWizEp1Caky5iCMVWuCFGjLk";
    }
}