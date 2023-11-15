using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Numerics;
using UnityEngine;
using UnityEngine.Networking;
using GraphQlClient.Core;

namespace Helika
{
    public abstract class EventManager
    {
        private string _apiKey;
        protected string _baesUrl;
        protected string _gameId;
        protected string _sessionID;

        public override void Init(string apiKey, string gameId, HelikaBaseURL baseUrl)
        {
            _apiKey = apiKey;
            _gameId = gameId,
            _baseUrl = baseUrl;
            
            // todo: generate Session ID
        }

        public async Task<ReturnData> EmitEvent()
        {
            string returnData = await PostAsync("/game/game-event", queryWithArgs);
            return JsonUtility.FromJson<ReturnData>(returnData);
        }


        // protected static string LoadQuery(string queryLocation)
        // {
        //     TextAsset metadataTextAsset=(TextAsset)Resources.Load(queryLocation);
        //     return metadataTextAsset.text;
        // }

        protected static async Task<string> PostAsync(string uri, string queryWithArgs)
        {
            // Post query
            using (UnityWebRequest request = await HttpHandler.PostAsync(_baseUrl + uri, queryWithArgs, null))
            {
                if (request.result != UnityWebRequest.Result.Success)
                {
                    Debug.LogError(request.error);
                    return String.Empty;
                }
                
                Debug.Log(HttpHandler.FormatJson(request.downloadHandler.text));
                return request.downloadHandler.text;
            }
        }
    }
}
