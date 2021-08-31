using UnityEngine;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Rawrshak
{
    // This Image Asset Metadata is based off of the Rawrshak Metadata Framework (https://github.com/Rawrshak/RawrshakAssetFrameworks/blob/draft-metadata-standards/Drafts/ImageAssetMetadata.md)
    // If you would like to add additional developer properties, you can add them in the 'properties' in the metadata.
    // You need to define your content metadata class based on this one as seen in /Assets/Rawrshak/Sample/Metadata/ContentMetadataSample.cs
    [Serializable]
    public class ImageMetadataBase : PublicAssetMetadataBase
    {
        public static float SQUARE_ASPECT_RATIO = 1.0f;
        public static float HORIZONTAL_BANNER_ASPECT_RATIO = 2.0f;
        public static float VERTICAL_BANNER_ASPECT_RATIO = 0.5f;
        
        public ImageProperties[] assetProperties;

        public static ImageMetadataBase CreateFromJSON(string jsonString)
        {
            return JsonUtility.FromJson<ImageMetadataBase>(jsonString);
        }

        public bool VerifyData()
        {
            if (type != "image") return false;

            // Todo: Load image as a texture and check that the asset matches

            switch (subtype)
            {
                case "square": 
                {
                    foreach(var img in assetProperties)
                    {
                        if (!VerifyAspectRatio((float)img.height, (float)img.width, SQUARE_ASPECT_RATIO))
                            return false;
                    }
                    break;
                }
                case "horizontal-banner":
                {
                    foreach(var img in assetProperties)
                    {
                        if (!VerifyAspectRatio((float)img.height, (float)img.width, HORIZONTAL_BANNER_ASPECT_RATIO))
                            return false;
                    }
                    break;
                }
                case "vertical-banner":
                {
                    foreach(var img in assetProperties)
                    {
                        if (!VerifyAspectRatio((float)img.height, (float)img.width, VERTICAL_BANNER_ASPECT_RATIO))
                            return false;
                    }
                    break;
                }
                case "custom":
                {
                    // Custom image/texture assets allow the developer to create whatever image-based nft they would like
                    // without restrictions.
                    break;  
                }
                default:
                {
                    return false;
                }
            }
            return true;
        }

        private bool VerifyAspectRatio(float height, float width, float aspectRatio)
        {
            return height * aspectRatio == width;
        }
    }

    [Serializable]
    public class ImageProperties
    {
        public string uri;
        public int height;
        public int width;
        public string contentType;
    }
}