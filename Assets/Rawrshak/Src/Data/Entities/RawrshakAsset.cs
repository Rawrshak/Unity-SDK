using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Threading.Tasks;
using UnityEngine;

namespace Rawrshak
{
    [CreateAssetMenu(fileName="RawrshakAsset", menuName="Rawrshak/Create Rawrshak Asset Object")]
    public class RawrshakAsset : ScriptableObject
    {
        // This is a string because RawrshakAssets received from the GraphQL may be from other contracts that do
        // not belong to this developer.
        public string contractAddress;
        public string tokenId;
        public string name;
        public string type;
        public string subtype;
        public string imageUri;
        public string currentSupply;
        public string maxSupply;
        public string latestPublicUriVersion;
        public string latestHiddenUriVersion;
        public string latestPublicUri;
        public List<string> tags;
        
        private Network network;

        public void OnEnable()
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
            type = data.data.asset.type;
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

            return true;
        }

        // Todo:
        // 2. Load Asset Metadata
        // 3. Download Asset 
        // 4. Load Asset ?
    }
}