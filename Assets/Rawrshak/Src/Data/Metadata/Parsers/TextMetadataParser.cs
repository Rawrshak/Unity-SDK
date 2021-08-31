using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rawrshak;

public class TextMetadataParser : MonoBehaviour
{
    public string uri;
    public TextMetadataBase metadata;

    public void Start()
    {
        TextAsset metadataTextAsset=(TextAsset)Resources.Load("textassetmetadata");
        string jsonString = metadataTextAsset.text;

        metadata = (TextMetadataBase)PublicMedatadataParser.Parse(jsonString);
    }
}