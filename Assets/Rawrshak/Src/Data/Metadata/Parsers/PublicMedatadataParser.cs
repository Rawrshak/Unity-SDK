using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rawrshak;

public class PublicMedatadataParser : ScriptableObject
{
    // public string uri;

    // Todo: update this for uri
    public static object Parse(string jsonString)
    {
        var baseMetadata = PublicAssetMetadataBase.Parse(jsonString);
        if (baseMetadata == null)
            return null;
        switch (baseMetadata.type)
        {
            case "text":
            {
                var textMetadata = TextMetadataBase.Parse(jsonString);
                Debug.Log("Text Loaded");
                return textMetadata;
            }
            case "image":
            {
                var imageMetadata = ImageMetadataBase.Parse(jsonString);
                Debug.Log("Image Loaded");
                return imageMetadata;
            }
            case "audio":
            {
                var audioMetadata = AudioMetadataBase.Parse(jsonString);
                Debug.Log("Audio Loaded");
                return audioMetadata;
            }
            default:
            {
                Debug.LogError("Incorrectly Loaded Public Metadata");
                return PublicAssetMetadataBase.Parse(jsonString);
            }
        }
    }
}
