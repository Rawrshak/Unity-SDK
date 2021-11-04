using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Threading.Tasks;
using UnityEngine;

namespace Rawrshak
{
    public class RawrshakAsset : MonoBehaviour
    {
        // This is a string because RawrshakAssets received from the GraphQL may be from other contracts that do
        // not belong to this developer.
        public string contractAddress;
        public string tokenId;
        public string name;
        public AssetType type;
        public string subtype;
        public string currentSupply;
        public string maxSupply;
        public string latestPublicUriVersion;
        public string latestHiddenUriVersion;
        public string latestPublicUri;
        public List<string> tags;
        public PublicAssetMetadataBase baseMetadata;
        public string imageUri;
        public Texture2D imageTexture;
        
        private Network network;

        void Start()
        {
            network = Network.Instance;
        }

        public async Task<bool> Load()
        {
            GetAssetInfo.ReturnData data = await GetAssetInfo.Fetch(contractAddress, tokenId);

            if (String.IsNullOrEmpty(data.data.asset.id))
            {
                Debug.LogError("Invalid Rawrshak Asset Load. Asset doesn't exist.");
                return false;
            }

            name = data.data.asset.name;
            type = ParseAssetType(data.data.asset.type);
            subtype = data.data.asset.subtype;
            imageUri = data.data.asset.imageUri;
            currentSupply = data.data.asset.currentSupply;
            maxSupply = data.data.asset.maxSupply;
            latestPublicUriVersion = data.data.asset.latestPublicUriVersion;
            latestHiddenUriVersion = data.data.asset.latestHiddenUriVersion;
            latestPublicUri = data.data.asset.latestPublicUri;

            foreach(GetAssetInfo.TagData tag in data.data.asset.tags)
            {
                tags.Add(tag.id);
            }

            // Download Metadata
            LoadMetadata(latestPublicUri);

            return true;
        }

        public async Task DownloadImage()
        {
            imageTexture = await Downloader.DownloadTexture(imageUri);
        }

        public bool IsTextAsset()
        {
            return baseMetadata.type == "text";
        }

        public bool IsAudioAsset()
        {
            return baseMetadata.type == "audio";
        }

        public bool IsImageAsset()
        {
            return baseMetadata.type == "image";
        }

        public bool IsStatic3dObjectAsset()
        {
            return baseMetadata.type == "static3dObject";
        }

        // Internal Functions
        private async Task LoadMetadata(string uri)
        {
            if (String.IsNullOrEmpty(uri))
            {
                Debug.LogError("Invalid Rawrshak Asset Load. Metadata URI doesn't exist");
                return;
            }

            string metadataJson = await Downloader.DownloadMetadata(uri);

            if (String.IsNullOrEmpty(metadataJson)) {
                Debug.LogError("Invalid Rawrshak Asset Load. Metadata doesn't exist");
                return;
            }

            baseMetadata = PublicAssetMetadataBase.Parse(metadataJson);
        }

        public static AssetType ParseAssetType(string type) {
            switch (type)
            {
                case "text":
                {
                    return AssetType.Text;
                }
                case "image":
                {
                    return AssetType.Image;
                }
                case "audio":
                {
                    return AssetType.Audio;
                }
                case "static3dObject":
                {
                    return AssetType.Static3dObject;
                }
                default:
                {
                    return AssetType.Invalid;
                }
            }
        }

        public static AssetSubtype ParseAssetSubtype(string subtype) {
            switch (subtype)
            {
                case "custom":
                {
                    return AssetSubtype.Custom;
                }
                case "title":
                {
                    return AssetSubtype.Title;
                }
                case "lore":
                {
                    return AssetSubtype.Lore;
                }
                case "square":
                {
                    return AssetSubtype.Square;
                }
                case "horizontal-banner":
                {
                    return AssetSubtype.HorizontalBanner;
                }
                case "sound-effect":
                {
                    return AssetSubtype.SoundEffect;
                }
                case "background-music":
                {
                    return AssetSubtype.BackgroundMusic;
                }
                default:
                {
                    return AssetSubtype.Invalid;
                }
            }
        }


        // Todo:
        // - Based on the type of asset, create a new component that handles the Texture, Text, Audio, or 3D asset
    }
}