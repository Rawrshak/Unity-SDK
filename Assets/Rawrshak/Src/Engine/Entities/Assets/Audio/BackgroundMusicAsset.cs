using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rawrshak
{
    public class BackgroundMusicAsset : AudioAssetBase
    {
        public bool IsValidAsset()
        {
            foreach(var audio in audioData)
            {
                if (audio.Value.duration > (int)MaxDurationMs.BackgroundMusic)
                {
                    return false;
                }   
            }
            return true;
        }
    }
}
