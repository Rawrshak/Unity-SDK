using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rawrshak;

public class TextMetadataParser : MonoBehaviour
{
    public TextMetadataBase metadata;

    public void GetMetadata(string uri)
    {
        // TextAsset metadataTextAsset=(TextAsset)Resources.Load("textassetmetadata");
        StartCoroutine(MetadataDownloader.Instance.Download(uri, () => {
            string jsonString = MetadataDownloader.Instance.text;
            metadata = (TextMetadataBase)PublicMedatadataParser.Parse(jsonString);
        }));
    }
}