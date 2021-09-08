using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class MetadataDownloader : MonoBehaviour
{
    public string metadata;
    public string error;

    public IEnumerator Download(string uri)
    {
        using (UnityWebRequest uwr = UnityWebRequest.Get(uri))
        {
            // Request and wait for the metadata json file to be downloaded
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
                metadata = uwr.downloadHandler.text;
            }
        }
    }
}
