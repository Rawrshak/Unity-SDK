using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Rawrshak
{
    public class ImageAssetBase : MonoBehaviour
    {
        protected ImageMetadataBase metadata;
        protected Texture2D currentTextureAsset;

        public void Init(PublicAssetMetadataBase baseMetadata)
        {
            metadata = ImageMetadataBase.Parse(baseMetadata.jsonString);
            metadata.jsonString = baseMetadata.jsonString;
        }

        public async Task<Texture2D> GetTexture2D(int width, int height)
        {
            // Set Texture2D
            if (metadata.assetProperties.Length == 0)
            {
                Debug.LogError("No image asset uri available");
                return null;
            }

            // Check if the currently loaded asset has the same resolution
            if (currentTextureAsset != null &&
                currentTextureAsset.width == width &&
                currentTextureAsset.height == height)
            {
                return null;
            }

            // Use 1st texture as the default
            string uri = String.Empty;
            for (int i = 0; i < metadata.assetProperties.Length; ++i)
            {
                if (width == metadata.assetProperties[i].width && 
                    height == metadata.assetProperties[i].height)
                {
                    uri = metadata.assetProperties[i].uri;
                }
            }

            // resolution doesn't exist
            if (String.IsNullOrEmpty(uri))
            {
                return currentTextureAsset;
            }
            
            // resolution found
            // Todo: error checking
            currentTextureAsset = await Downloader.DownloadTexture(uri);
            return currentTextureAsset;
        }
        
        public List<List<int>> GetAvailableResolutions()
        {
            List<List<int>> resolutions = new List<List<int>>();
            for (int i = 0; i < metadata.assetProperties.Length; ++i)
            {
                List<int> resolution = new List<int>();
                resolution.Add(metadata.assetProperties[i].width);
                resolution.Add(metadata.assetProperties[i].height);
                resolutions.Add(resolution);
            }
            return resolutions;
        }

        protected bool VerifyAspectRatio(float height, float width, float aspectRatio)
        {
            return height * aspectRatio == width;
        }
    }
}
