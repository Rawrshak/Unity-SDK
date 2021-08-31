using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rawrshak;

public class LoadContentMetadata : MonoBehaviour
{
    public ContentMetadataSample metadata;

    // Start is called before the first frame update
    void Start()
    {
        TextAsset metadataTextAsset=(TextAsset)Resources.Load("contentmetadata");
        string jsonString = metadataTextAsset.text;

        metadata = ContentMetadataSample.CreateFromJSON(jsonString);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
