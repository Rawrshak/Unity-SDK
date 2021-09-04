using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class MetadataDownloader : MonoBehaviour
{
    public string uri;
    public string metadata;
    public string error;

    public IEnumerator Download()
    {
        UnityWebRequest www = UnityWebRequest.Get(uri);
        yield return www.SendWebRequest();
 
        if (www.result != UnityWebRequest.Result.Success) {
            Debug.LogError(www.error);
            error = www.error;
        }
        else {
            // Show results as text
            Debug.Log(www.downloadHandler.text);
 
            metadata = www.downloadHandler.text;
        }
    }
}
