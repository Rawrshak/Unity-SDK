using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Events;

public class AssetBundleDownloader : SingletonScriptableObject<AssetBundleDownloader>
{
    public AssetBundle bundle;
    public string error;

    public IEnumerator Download(string uri, UnityAction callback)
    {
        using (UnityWebRequest uwr = UnityWebRequestAssetBundle.GetAssetBundle(uri))
        {
            // Note: https://docs.unity3d.com/Manual/AssetBundles-Cache.html 
            // Request and wait for the asset bundle file to be downloaded
            yield return uwr.SendWebRequest();

            if (uwr.result != UnityWebRequest.Result.Success)
            {
                error = uwr.error;
                Debug.LogError(uwr.error);
            }
            else
            {
                bundle = DownloadHandlerAssetBundle.GetContent(uwr);

                callback();
            }
        }
    }
}
