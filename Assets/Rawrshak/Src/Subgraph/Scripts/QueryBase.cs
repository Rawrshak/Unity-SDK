using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Numerics;
using UnityEngine;
using UnityEngine.Networking;
using GraphQlClient.Core;

namespace Rawrshak
{
    public abstract class QueryBase : MonoBehaviour
    {
        public Subgraph subgraph = null;
        protected string query;

        protected void LoadQueryIfEmpty(string queryLocation) {
            if (String.IsNullOrEmpty(query)) {
                TextAsset metadataTextAsset=(TextAsset)Resources.Load(queryLocation);
                query = metadataTextAsset.text;
            }
        }

        protected void CheckSubgraph() {
            if (subgraph == null) {
                subgraph = Subgraph.Instance;
            }
        }

        protected async Task<string> PostAsync(string uri, string queryWithArgs) {
            // Post query
            UnityWebRequest request = await HttpHandler.PostAsync(uri, queryWithArgs, null);
            Debug.Log(HttpHandler.FormatJson(request.downloadHandler.text));

            return request.downloadHandler.text;
        }
    }
}
