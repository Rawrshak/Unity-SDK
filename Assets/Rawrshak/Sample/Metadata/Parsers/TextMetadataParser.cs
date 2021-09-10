using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rawrshak;

public class TextMetadataParser : MonoBehaviour
{
    public TextMetadataBase metadata;
    
    void Start()
    {
        GetMetadata("");
    }

    public void GetMetadata(string uri)
    {
        // This uses the metadata in the Resources Folder
        TextAsset metadataTextAsset = (TextAsset)Resources.Load("textassetmetadata");
        metadata = (TextMetadataBase)PublicMedatadataParser.Parse(metadataTextAsset.text);

        // // This uses the metadata that's stored from a uri
        // StartCoroutine(Downloader.Instance.DownloadMetadata(uri, (metadataJson) => {
        //     metadata = (TextMetadataBase)PublicMedatadataParser.Parse(metadataJson);
        // }));
    }
}