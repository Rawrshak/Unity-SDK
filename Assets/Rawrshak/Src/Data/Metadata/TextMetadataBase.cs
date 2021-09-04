using UnityEngine;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Rawrshak
{
    // This Text Asset Metadata is based off of the Rawrshak Metadata Framework (https://github.com/Rawrshak/RawrshakAssetFrameworks/blob/draft-metadata-standards/Drafts/ImageAssetMetadata.md)
    // If you would like to add additional developer properties, you can add them in the 'properties' in the metadata.
    // You need to define your content metadata class based on this one as seen in /Assets/Rawrshak/Sample/Metadata/ContentMetadataSample.cs
    [Serializable]
    public class TextMetadataBase : PublicAssetMetadataBase
    {
        public static int MAX_TITLE_LENGTH = 40;
        public enum MAX_DESCRIPTION_LENGTH {
            Title = 500,
            Lore = 5000
        };

        public TextProperties assetProperties;

        public static new TextMetadataBase CreateFromJSON(string jsonString)
        {
            return JsonUtility.FromJson<TextMetadataBase>(jsonString);
        }

        public bool VerifyData()
        {
            if (type != "text") return false;

            switch (subtype)
            {
                case "title": 
                {
                    if (assetProperties.description.Length > (int)MAX_DESCRIPTION_LENGTH.Title) return false;
                    if (assetProperties.title.Length > MAX_TITLE_LENGTH) return false;
                    break;
                }
                case "lore":
                {
                    if (assetProperties.description.Length > (int)MAX_DESCRIPTION_LENGTH.Lore) return false;
                    if (assetProperties.title.Length > MAX_TITLE_LENGTH) return false;
                    break;
                }
                case "custom":
                {
                    // Custom Text assets allow the developer to create whatever text-based nft they would like
                    // without restrictions.
                    break;
                }
                default:
                {
                    // Invalid subtype
                    return false;
                }
            }
            return true;
        }
    }

    [Serializable]
    public class TextProperties
    {
        public string title;
        public string description;
    }
}