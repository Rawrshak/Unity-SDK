using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class TextureDownloader : MonoBehaviour
{
    public Texture texture;
    public string error;

    public IEnumerator Download(string uri)
    {
        using (UnityWebRequest uwr = UnityWebRequestAssetBundle.GetTexture(uri))
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
                texture = ((DownloadHandlerTexture)www.downloadHandler).texture;
            }
        }
    }
}
