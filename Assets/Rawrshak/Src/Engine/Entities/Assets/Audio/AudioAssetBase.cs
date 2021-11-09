using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Rawrshak
{
    public abstract class AudioAssetBase : AssetBase
    {
        public enum MaxDurationMs {
            SoundEffect = 1000,
            Shout = 2000,
            CharacterLine = 10000,
            BackgroundMusic = 300000
        };

        public enum ContentTypes {
            Invalid,
            Wav,
            Mpeg,
            Ogg,
            Aiff
        }

        private static string Engine = "unity";

        protected AudioMetadataBase metadata;
        protected Dictionary<ContentTypes, AudioProperties> audioData;
        protected ContentTypes currentContentType;
        protected AudioClip currentAudioClip;
        protected AssetBundle currentAssetBundle;

        void OnDisabled()
        {
            // Unload completely before disabling object
            if (currentAssetBundle)
            {
                currentAssetBundle.Unload(true);
            }
        }

        public override void Init(PublicAssetMetadataBase baseMetadata)
        {
            metadata = AudioMetadataBase.Parse(baseMetadata.jsonString);
            metadata.jsonString = baseMetadata.jsonString;

            foreach (var audioProperty in metadata.assetProperties)
            {
                // Filter out non-unity engine assets and unsupported content types
                ContentTypes contentType = ConvertFromString(audioProperty.contentType);
                if (audioProperty.engine == Engine && contentType != ContentTypes.Invalid)
                {
                    // Note: Overwrite duplicates. Does not throw an exception
                    audioData[contentType] = audioProperty;
                }
            }

            currentContentType = ContentTypes.Invalid;
        }
        
        public async Task<AudioClip> LoadAndSetAudioClipFromContentType(ContentTypes type)
        {
            if (!audioData.ContainsKey(type) || type == ContentTypes.Invalid)
            {
                Debug.LogError("No audio ContentType supported");
                return null;
            }

            if (currentContentType == type)
            {
                return currentAudioClip;
            }

            AudioProperties data = audioData[type];
            
            if (!String.IsNullOrEmpty(data.uri))
            {
                Debug.LogError("AudioClip metadata uri is not found");
                return null;
            }

            AssetBundle assetBundle = currentAssetBundle;
            if (data.uri != audioData[currentContentType].uri)
            {
                // Download the assetbundle
                assetBundle = await Downloader.DownloadAssetBundle(data.uri);
                if (assetBundle == null)
                {
                    Debug.LogError("AssetBundle not found");
                    return null;
                }
            }

            AudioClip audioClip = assetBundle.LoadAsset<AudioClip>(data.filename);

            if (audioClip == null)
            {
                Debug.LogError("AudioClip doesn't exist in AssetBundle");
                return null;
            }
            
            // Compare AudioClip data to audio properties metadata
            if (!VerifyAudioClipProperties(audioClip, data))
            {
                Debug.LogError("AudioClip does not have the correct audio properties");
                return null;
            }

            // If a new AssetBundle was loaded that's different from the current bundle
            if (assetBundle != currentAssetBundle)
            {
                currentAssetBundle.Unload(false);
            }

            currentAssetBundle = assetBundle;
            currentAudioClip = audioClip;
            currentContentType = type;
            return currentAudioClip;
        }

        public AudioClip GetCurrentAudioClip()
        {
            return currentAudioClip;
        }

        public AudioProperties GetCurrentAudioProperties()
        {
            return audioData[currentContentType];
        }

        public ContentTypes GetCurrentContentType()
        {
            return currentContentType;
        }

        public List<ContentTypes> GetAvailableContentTypes()
        {
            List<ContentTypes> types = new List<ContentTypes>();
            foreach(var data in audioData)
            {
                types.Add(data.Key);
            }
            return types;
        }
        
        public int GetDuration(ContentTypes type)
        {
            return audioData[type].duration;
        }

        public int GetChannelCount(ContentTypes type)
        {
            return audioData[type].channelCount;
        }

        public int GetSampleRate(ContentTypes type)
        {
            return audioData[type].sampleRate;
        }

        private bool VerifyAudioClipProperties(AudioClip audioClip, AudioProperties properties)
        {
            if (audioClip.channels != properties.channelCount ||
                (audioClip.length * 1000) != properties.duration || 
                audioClip.frequency != properties.sampleRate)
            {
                return false;
            }
            return true;
        }
        
        private ContentTypes ConvertFromString(string contentType)
        {
            switch(contentType)
            {
                case "audio/wav":
                {
                    return ContentTypes.Wav;
                }
                case "audio/mpeg":
                {
                    return ContentTypes.Mpeg;
                }
                case "audio/ogg":
                {
                    return ContentTypes.Ogg;
                }
                case "audio/x-aiff":
                {
                    return ContentTypes.Aiff;
                }
                default:
                {
                    return ContentTypes.Invalid;
                }
            }
        }
    }
}