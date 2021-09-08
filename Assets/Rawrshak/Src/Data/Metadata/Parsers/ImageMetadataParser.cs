using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rawrshak;

public class ImageMetadataParser : MonoBehaviour
{
    public ImageMetadataBase metadata;

    public void GetMetadata(string uri)
    {
        // TextAsset metadataTextAsset=(TextAsset)Resources.Load("imageassetmetadata");
        StartCoroutine(MetadataDownloader.Instance.Download(uri, () => {
            string jsonString = MetadataDownloader.Instance.text;
            metadata = (ImageMetadataBase)PublicMedatadataParser.Parse(jsonString);
        }));
    }
}
