using UnityEngine;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Rawrshak
{
    // This Audio Asset Metadata is based off of the Rawrshak Metadata Framework (https://github.com/Rawrshak/RawrshakAssetFrameworks/blob/draft-metadata-standards/Drafts/AudioAssetMetadata.md)
    // If you would like to add additional developer properties, you can add them in the 'properties' in the metadata.
    // You need to define your content metadata class based on this one as seen in /Assets/Rawrshak/Sample/Metadata/ContentMetadataSample.cs
    [Serializable]
    public class AudioMetadataBase : PublicAssetMetadataBase
    {
        public enum MAX_DURATION_MS {
            SoundEffect = 1000,
            Shout = 2000,
            CharacterLine = 10000,
            BackgroundMusic = 300000
        };

        public AudioProperties[] assetProperties;

        public static AudioMetadataBase CreateFromJSON(string jsonString)
        {
            return JsonUtility.FromJson<AudioMetadataBase>(jsonString);
        }

        public bool VerifyData()
        {
            if (type != "audio") return false;

            // Todo: Load audio as an audioclip and check that the asset matches

            switch (subtype)
            {
                case "sound-effect": 
                {
                    foreach(var audio in assetProperties)
                    {
                        if (audio.durationMs > (int)MAX_DURATION_MS.SoundEffect)
                            return false;
                    }
                    break;
                }
                case "shout":
                {
                    foreach(var audio in assetProperties)
                    {
                        if (audio.durationMs > (int)MAX_DURATION_MS.Shout)
                            return false;
                    }
                    break;
                }
                case "character-line":
                {
                    foreach(var audio in assetProperties)
                    {
                        if (audio.durationMs > (int)MAX_DURATION_MS.CharacterLine)
                            return false;
                    }
                    break;
                }
                case "background-music":
                {
                    foreach(var audio in assetProperties)
                    {
                        if (audio.durationMs > (int)MAX_DURATION_MS.BackgroundMusic)
                            return false;
                    }
                    break;
                }
                case "custom":
                {
                    // Custom audio assets allow the developer to create whatever image-based nft they would like
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
    }

    [Serializable]
    public class AudioProperties
    {
        public string uri;
        public string engine;
        public string compression;
        public string contentType;
        public int durationMs;
        public int channelCount;
        public int sampleRateHz;
    }
}