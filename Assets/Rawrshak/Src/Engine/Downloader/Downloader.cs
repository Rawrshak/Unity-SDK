using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Events;

namespace Rawrshak
{
    public class Downloader : SingletonScriptableObject<Downloader>
    {
        public string lastError;
        
        public IEnumerator DownloadMetadata(string uri, UnityAction<string> callback)
        {
            using (UnityWebRequest uwr = UnityWebRequest.Get(uri))
            {
                // Request and wait for the text json file to be downloaded
                yield return uwr.SendWebRequest();

                if (uwr.result != UnityWebRequest.Result.Success)
                {
                    lastError = uwr.error;
                    Debug.LogError(uwr.error);
                }
                else
                {
                    // Show results as text
                    Debug.Log(uwr.downloadHandler.text);
                    callback(uwr.downloadHandler.text);
                }
            }
        }
        
        public IEnumerator DownloadTexture(string uri, UnityAction<Texture> callback)
        {
            using (UnityWebRequest uwr = UnityWebRequestTexture.GetTexture(uri))
            {
                // Request and wait for the texture file to be downloaded
                yield return uwr.SendWebRequest();

                if (uwr.result != UnityWebRequest.Result.Success)
                {
                    lastError = uwr.error;
                    Debug.LogError(uwr.error);
                }
                else
                {
                    Texture texture = ((DownloadHandlerTexture)uwr.downloadHandler).texture;
                    callback(texture);
                }
            }
        }

        public IEnumerator DownloadAssetBundle(string uri, UnityAction<AssetBundle> callback)
        {
            using (UnityWebRequest uwr = UnityWebRequestAssetBundle.GetAssetBundle(uri))
            {
                // Note: https://docs.unity3d.com/Manual/AssetBundles-Cache.html 
                // Request and wait for the asset bundle file to be downloaded
                yield return uwr.SendWebRequest();

                if (uwr.result != UnityWebRequest.Result.Success)
                {
                    lastError = uwr.error;
                    Debug.LogError(uwr.error);
                }
                else
                {
                    AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(uwr);
                    callback(bundle);
                }
            }
        }
    }
}