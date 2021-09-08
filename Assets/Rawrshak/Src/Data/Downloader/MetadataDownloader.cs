using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Events;

public class MetadataDownloader : SingletonScriptableObject<MetadataDownloader>
{
    public string text;
    public string error;

    public IEnumerator Download(string uri, UnityAction callback)
    {
        using (UnityWebRequest uwr = UnityWebRequest.Get(uri))
        {
            // Request and wait for the text json file to be downloaded
            yield return uwr.SendWebRequest();

            if (uwr.result != UnityWebRequest.Result.Success)
            {
                error = uwr.error;
                Debug.LogError(uwr.error);
            }
            else
            {
                // Show results as text
                Debug.Log(uwr.downloadHandler.text);
                text = uwr.downloadHandler.text;

                callback();
            }
        }
    }
}
