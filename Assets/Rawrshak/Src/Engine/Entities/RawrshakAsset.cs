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
        public string assetName;
        public AssetType type;
        public AssetSubtype subtype;
        public string currentSupply;
        public string maxSupply;
        public string latestPublicUriVersion;
        public string latestHiddenUriVersion;
        public string latestPublicUri;
        public List<string> tags;
        public PublicAssetMetadataBase baseMetadata;
        public string imageUri;
        public Texture2D imageTexture;
        public Component assetComponent;
        
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

            assetName = data.data.asset.name;
            type = ParseAssetType(data.data.asset.type);
            subtype = ParseAssetSubtype(data.data.asset.subtype);
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
            await LoadMetadata(latestPublicUri);

            return true;
        }

        public async Task DownloadImage()
        {
            imageTexture = await Downloader.DownloadTexture(imageUri);
        }

        public void CreateAssetComponent()
        {
            // Todo: Optimize this function later
            switch (subtype)
            {
                case AssetSubtype.Custom:
                {
                    switch (type)
                    {
                        case AssetType.Text:
                        {
                            assetComponent = gameObject.AddComponent(typeof(CustomTextAsset));
                            ((CustomTextAsset)assetComponent).Init(baseMetadata);
                            if (!((CustomTextAsset)assetComponent).IsValidAsset())
                            {
                                Destroy(assetComponent);
                            }
                            break;
                        }
                        case AssetType.Image:
                        {
                            assetComponent = gameObject.AddComponent(typeof(CustomImageAsset));
                            ((CustomImageAsset)assetComponent).Init(baseMetadata);
                            if (!((CustomImageAsset)assetComponent).IsValidAsset())
                            {
                                Destroy(assetComponent);
                            }
                            break;
                        }
                        default:
                        {
                            Debug.LogError("Invalid asset component to add.");
                            break;
                        }
                    }
                    break;
                }
                case AssetSubtype.Title:
                {
                    assetComponent = gameObject.AddComponent(typeof(TitleAsset));
                    ((TitleAsset)assetComponent).Init(baseMetadata);
                    if (!((TitleAsset)assetComponent).IsValidAsset())
                    {
                        Destroy(assetComponent);
                    }
                    break;
                }
                case AssetSubtype.Lore:
                {
                    assetComponent = gameObject.AddComponent(typeof(LoreAsset));
                    ((LoreAsset)assetComponent).Init(baseMetadata);
                    if (!((LoreAsset)assetComponent).IsValidAsset())
                    {
                        Destroy(assetComponent);
                    }
                    break;
                }
                case AssetSubtype.Square:
                {
                    assetComponent = gameObject.AddComponent(typeof(SquareAsset));
                    ((SquareAsset)assetComponent).Init(baseMetadata);
                    if (!((SquareAsset)assetComponent).IsValidAsset())
                    {
                        Destroy(assetComponent);
                    }
                    break;
                }
                case AssetSubtype.HorizontalBanner:
                {
                    assetComponent = gameObject.AddComponent(typeof(HorizontalBannerAsset));
                    ((HorizontalBannerAsset)assetComponent).Init(baseMetadata);
                    if (!((HorizontalBannerAsset)assetComponent).IsValidAsset())
                    {
                        Destroy(assetComponent);
                    }
                    break;
                }
                case AssetSubtype.VerticalBanner:
                {
                    assetComponent = gameObject.AddComponent(typeof(VerticalBannerAsset));
                    ((VerticalBannerAsset)assetComponent).Init(baseMetadata);
                    if (!((VerticalBannerAsset)assetComponent).IsValidAsset())
                    {
                        Destroy(assetComponent);
                    }
                    break;
                }
                default:
                {
                    Debug.LogError("Invalid asset component to add.");
                    break;
                }
            }
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

        public static AssetType ParseAssetType(string type)
        {
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

        public static AssetSubtype ParseAssetSubtype(string subtype)
        {
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
                case "vertical-banner":
                {
                    return AssetSubtype.VerticalBanner;
                }
                case "sound-effect":
                {
                    return AssetSubtype.SoundEffect;
                }
                case "shout":
                {
                    return AssetSubtype.Shout;
                }
                case "character-line":
                {
                    return AssetSubtype.CharacterLine;
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