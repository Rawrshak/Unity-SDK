using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rawrshak
{
    public class SquareAsset : ImageAssetBase
    {
        public static float SquareAspectRatio = 1.0f;
        public bool IsValidAsset()
        {
            foreach(var img in metadata.assetProperties)
            {
                if (!VerifyAspectRatio((float)img.height, (float)img.width, SquareAspectRatio))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
