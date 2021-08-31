using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rawrshak;

public class ImageMetadataParser : MonoBehaviour
{
    public string uri;
    public ImageMetadataBase metadata;

    public void Start()
    {
        TextAsset metadataTextAsset=(TextAsset)Resources.Load("imageassetmetadata");
        string jsonString = metadataTextAsset.text;

        metadata = (ImageMetadataBase)PublicMedatadataParser.Parse(jsonString);
    }
}
