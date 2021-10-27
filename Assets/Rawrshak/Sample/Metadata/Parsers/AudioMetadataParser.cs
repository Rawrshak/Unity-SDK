using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rawrshak;

public class AudioMetadataParser : MonoBehaviour
{
    public AudioMetadataBase metadata;

    void Start()
    {
        GetMetadata("");
    }

    public void GetMetadata(string uri)
    {
        // This uses the metadata in the Resources Folder
        TextAsset metadataTextAsset=(TextAsset)Resources.Load("audioassetmetadata");
        metadata = (AudioMetadataBase)PublicMedatadataParser.Parse(metadataTextAsset.text);

        // // This uses the metadata that's stored from a uri
        // string metadataJson = Downloader.DownloadMetadata(uri).Result;
        // metadata = (AudioMetadataBase)PublicMedatadataParser.Parse(metadataJson);
    }
}