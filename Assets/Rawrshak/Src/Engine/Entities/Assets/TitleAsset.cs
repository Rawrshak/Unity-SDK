using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rawrshak
{
    public class TitleAsset : MonoBehaviour
    {
        public TextMetadataBase metadata;

        // Start is called before the first frame update
        void Start()
        {
        }

        public bool Init(PublicAssetMetadataBase baseMetadata)
        {
            metadata = TextMetadataBase.Parse(baseMetadata.jsonString);
            metadata.jsonString = baseMetadata.jsonString;

            if (!metadata.VerifyData()) {
                Debug.LogError("Invalid TitleAsset.");
                return false;
            }
            return true;
        }
    }
}
