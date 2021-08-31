using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rawrshak;

public class AudioMetadataParser : MonoBehaviour
{
    public string uri;
    public AudioMetadataBase metadata;

    public void Start()
    {
        TextAsset metadataTextAsset=(TextAsset)Resources.Load("audioassetmetadata");
        string jsonString = metadataTextAsset.text;

        metadata = (AudioMetadataBase)PublicMedatadataParser.Parse(jsonString);
    }
}