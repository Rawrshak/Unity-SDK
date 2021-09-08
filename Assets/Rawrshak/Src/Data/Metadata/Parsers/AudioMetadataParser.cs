using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rawrshak;

public class AudioMetadataParser : MonoBehaviour
{
    public AudioMetadataBase metadata;

    public void GetMetadata(string uri)
    {
        // TextAsset metadataTextAsset=(TextAsset)Resources.Load("audioassetmetadata");
        StartCoroutine(MetadataDownloader.Instance.Download(uri, () => {
            string jsonString = MetadataDownloader.Instance.text;
            metadata = (AudioMetadataBase)PublicMedatadataParser.Parse(jsonString);
        }));
    }
}