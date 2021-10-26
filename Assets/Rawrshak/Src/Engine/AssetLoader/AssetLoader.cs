using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Events;

namespace Rawrshak
{
    public class AssetLoader
    {
        public static async Task<PublicAssetMetadataBase> LoadMetadata(RawrshakAsset asset, Network network)
        {
            if (asset == null || network == null)
            {
                Debug.LogError("Invalid LoadMetadata input.");
                return null;
            }

            // 1. Get the Asset Uri from the Content Contract
            string uri = await Content.TokenUri(network.chain, network.network, asset.contract, asset.tokenId.ToString(), network.httpEndpoint);

            if (String.IsNullOrEmpty(uri))
            {
                Debug.LogError("Invalid Token ID: Token uri not found.");
                return null;
            }

            // Download the metadata
            string metadataJson = await Downloader.DownloadMetadata(uri);
            if (String.IsNullOrEmpty(metadataJson))
            {
                Debug.LogError("Invalid URI: Metadata from URI not found.");
                return null;
            }

            // Parse the metadata
            PublicAssetMetadataBase metadata = (PublicAssetMetadataBase)PublicMedatadataParser.Parse(metadataJson);
            if (metadata == null)
            {
                Debug.LogError("Invalid metadata: Unable to parse public metadata object.");
                return null;
            }
            
            return metadata;
        }
    }
}