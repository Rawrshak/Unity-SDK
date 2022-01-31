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
            MP3,
            Ogg,
            Aiff
        }

        public int downloadTimeout = 10;

        protected AudioMetadataBase metadata;
        protected Dictionary<ContentTypes, AudioProperties> audioData;
        protected ContentTypes currentContentType;
        protected AudioClip currentAudioClip;

        public override void Init(PublicAssetMetadataBase baseMetadata)
        {
            metadata = AudioMetadataBase.Parse(baseMetadata.jsonString);
            metadata.jsonString = baseMetadata.jsonString;

            audioData = new Dictionary<ContentTypes, AudioProperties>();
            foreach (var audioProperty in metadata.assetProperties)
            {
                // Filter out non-unity engine assets and unsupported content types
                ContentTypes contentType = ConvertContentTypeFromString(audioProperty.contentType);
                if (contentType != ContentTypes.Invalid)
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
                Debug.LogError("[AudioAssetBase] No audio ContentType supported");
                return null;
            }

            if (currentContentType == type)
            {
                return currentAudioClip;
            }

            AudioProperties data = null;
            foreach (var aData in audioData.Values)
            {
                if (ConvertContentTypeFromString(aData.contentType) == type) {
                    data = aData;
                    break;
                }
            }
            
            if (data == null)
            {
                Debug.LogError("[AudioAssetBase] AudioClip metadata uri is not found");
                return null;
            }

            AudioClip audioClip;

            switch(type)
            {
                case ContentTypes.Wav:
                {
                    audioClip = await Downloader.DownloadAudioClip(data.uri, AudioType.WAV, downloadTimeout);
                    break;
                }
                case ContentTypes.MP3:
                {
                    audioClip = await Downloader.DownloadAudioClip(data.uri, AudioType.MPEG, downloadTimeout);
                    break;
                }
                case ContentTypes.Ogg:
                {
                    audioClip = await Downloader.DownloadAudioClip(data.uri, AudioType.OGGVORBIS, downloadTimeout);
                    break;
                }
                case ContentTypes.Aiff:
                {
                    audioClip = await Downloader.DownloadAudioClip(data.uri, AudioType.AIFF, downloadTimeout);
                    break;
                }
                default:
                {
                    Debug.LogError("[AudioAssetBase] Audio Clip Type is not supported.");
                    return null;
                }
            }

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

        public bool IsContentTypeSupported(ContentTypes type)
        {
            foreach(var data in audioData)
            {
                if (type == data.Key)
                {
                    return true;
                }
            }
            return false;
        }
        
        public int GetDuration(ContentTypes type)
        {
            return audioData[type].duration;
        }

        private bool VerifyAudioClipProperties(AudioClip audioClip, AudioProperties properties)
        {
            // Debug.Log($"Channels: Actual: {audioClip.channels}, Property: {properties.channelCount}");
            // Debug.Log($"Length: Actual: {Mathf.RoundToInt(audioClip.length * 1000)}, Property: {properties.duration}");
            // Debug.Log($"Frequency: Actual: {audioClip.frequency}, Property: {properties.sampleRate}");

            if (Mathf.RoundToInt(audioClip.length * 1000) != properties.duration )
            {
                return false;
            }
            return true;
        }
        
        private ContentTypes ConvertContentTypeFromString(string contentType)
        {
            switch(contentType)
            {
                case "audio/wav":
                {
                    return ContentTypes.Wav;
                }
                case "audio/mp3":
                {
                    return ContentTypes.MP3;
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
