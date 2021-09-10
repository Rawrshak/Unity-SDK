using UnityEngine;
using System;
using System.Collections.Generic;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Rawrshak;

// This extends the content metadata to account for developer data that the developer expects.
[Serializable]
public class ContentMetadataSample : ContentMetadataBase
{
    public DevProperties properties;

    public new static ContentMetadataSample Parse(string jsonString)
    {
        return JsonUtility.FromJson<ContentMetadataSample>(jsonString);
    }
}

[Serializable]
public class DevProperties {
    public string simple_property;
    public RichProperty rich_property;
}

[Serializable]
public class RichProperty {
    public string name;
    public int value;
    public string display_value;
    public string prop_class;
}