using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class TextureDownloader : MonoBehaviour
{
    public string uri;
    public Texture texture;
    public string error;

    public IEnumerator Download()
    {
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(uri);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success) {
            Debug.LogError(www.error);
            error = www.error;
        }
        else {
            texture = ((DownloadHandlerTexture)www.downloadHandler).texture;
        }
    }
}
