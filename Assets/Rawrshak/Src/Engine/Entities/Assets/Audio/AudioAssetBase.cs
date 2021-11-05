using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rawrshak
{
    public class AudioAssetBase : MonoBehaviour
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

        public void Init(PublicAssetMetadataBase baseMetadata)
        {
            metadata = AudioMetadataBase.Parse(baseMetadata.jsonString);
            metadata.jsonString = baseMetadata.jsonString;

            foreach (var audioProperty in metadata.assetProperties)
            {
                // Filter out non-unity engine assets and unsupported content types
                ContentTypes contentType = ConvertFromString(audioProperty.contentType);
                if (audioProperty.engine == Engine && contentType != ContentTypes.Invalid)
                {
                    audioData.Add(contentType, audioProperty);
                }
            }
        }

        // Todo: Download Audio and get piece
        // public Audio GetAudio()
        // {
        //     return metadata.assetProperties.title;
        // }
        
        // public Audio GetAudioFromContentType(ContentTypes type)
        // {
        // }

        // public List<ContentType> GetAvailableContentTypes()
        // {
        // }
        
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
