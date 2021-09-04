using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class AssetBundleDownloader : MonoBehaviour
{
    public string uri;
    public AssetBundle bundle;
    public string error;

    public IEnumerator Download()
    {
        // Note: https://docs.unity3d.com/Manual/AssetBundles-Cache.html 
        UnityWebRequest www = UnityWebRequestAssetBundle.GetAssetBundle(uri);
        yield return www.SendWebRequest();
 
        if (www.result != UnityWebRequest.Result.Success) {
            Debug.LogError(www.error);
            error = www.error;
        }
        else {
            bundle = DownloadHandlerAssetBundle.GetContent(www);
        }
    }
}
