using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Numerics;
using UnityEngine;
using UnityEngine.Networking;
using GraphQlClient.Core;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Helika
{
    public class EventManager : HelikaSingletonScriptableObject<EventManager>
    {
        private string _apiKey;
        protected string _baseUrl;
        protected string _gameId;
        protected string _sessionID;
        protected bool _isInitialized = false;


        public void Init(string apiKey, string gameId, string baseUrl)
        {
            if (_isInitialized)
            {
                return;
            }

            if (!HelikaBaseURL.validate(baseUrl))
            {
                throw new ArgumentException("Invalid Base URL");
            }

            _apiKey = apiKey;
            _gameId = gameId;
            _baseUrl = baseUrl;
            _sessionID = Guid.NewGuid().ToString();
        }

        public async Task<string> EmitEvent(string data)
        {
            return await PostAsync("/game/game-event", data);
        }

        public async Task<string> TestHelikaAPI()
        {
            var jsonObject = new
            {
                message = "test"
            };
            var jsonString = JsonConvert.SerializeObject(jsonObject);
            Debug.Log(jsonString);

            string returnData = await PostAsync("/game/test-event", jsonString);
            Debug.Log(returnData);
            return returnData;
            // return JsonUtility.FromJson<ReturnData>(returnData);
        }

        protected async Task<string> PostAsync(string url, string data)
        {
            // Create a UnityWebRequest object
            UnityWebRequest request = new UnityWebRequest(_baseUrl.ToString() + url, "POST");

            // Set the request method and content type
            // request.method = "POST";
            request.SetRequestHeader("Content-Type", "application/json");
            request.SetRequestHeader("x-api-key", _apiKey);

            // Convert the data to bytes and attach it to the request
            byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(data);
            request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();

            // Send the request asynchronously
            await request.SendWebRequest();

            // Check for errors
            if (request.result == UnityWebRequest.Result.Success)
            {
                // Display the response text
                Debug.Log("Response: " + request.downloadHandler.text);
            }
            else
            {
                // Display the error
                Debug.LogError("Error: " + request.error + ", data: " + request.downloadHandler.text);
            }

            return request.downloadHandler.text;
        }
    }
}
