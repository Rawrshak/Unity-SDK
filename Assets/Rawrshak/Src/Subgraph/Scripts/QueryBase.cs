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
        public GraphApi contentsSubgraph;

        protected string query;

        protected async Task<string> PostAsync(string queryWithArgs) {
            if (contentsSubgraph == null) {
                Debug.LogError("Content Subgraph API not set");
                throw new Exception("Content Subgraph API not set.");
            }

            // Post query
            UnityWebRequest request = await contentsSubgraph.Post(queryWithArgs);
            Debug.Log(HttpHandler.FormatJson(request.downloadHandler.text));

            return request.downloadHandler.text;
        }
    }
}
