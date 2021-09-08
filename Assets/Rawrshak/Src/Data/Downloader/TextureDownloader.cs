using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

public class TextureDownloader : SingletonScriptableObject<TextureDownloader>
{
    public Texture texture;
    public string error;

    public IEnumerator Download(string uri, UnityAction callback)
    {
        using (UnityWebRequest uwr = UnityWebRequestTexture.GetTexture(uri))
        {
            // Request and wait for the texture file to be downloaded
            yield return uwr.SendWebRequest();

            if (uwr.result != UnityWebRequest.Result.Success)
            {
                error = uwr.error;
                Debug.LogError(uwr.error);
            }
            else
            {
                texture = ((DownloadHandlerTexture)uwr.downloadHandler).texture;

                callback();
            }
        }
    }
}
