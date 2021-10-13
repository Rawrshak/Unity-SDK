using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using GraphQlClient.Core;

namespace Rawrshak
{
    [Serializable]
    public class ContentData 
    {
        public string id;
        public string name;
        public string game;
        public string creator;
        public string owner;
        public string contractAddress;
        public string contractUri;
        public int royaltyRate;
        public AssetData assets;
        public TagData[] tags;
    }

    [Serializable]
    public class AssetData 
    {
        public string id;
        public string tokenId;
        public string currentSupply;
        public string maxSupply;
        public string latestPublicUriVersion;
        public string name;
        public string type;
        public string subtype;
        public TagData[] tags;
    }

    [Serializable]
    public class AssetIdData 
    {
        public string id;
        public ContentIdData parentContract;
        public string tokenId;
    }

    [Serializable]
    public class ContentIdData 
    {
        public string id;
    }

    [Serializable]
    public class TagData 
    {
        public string id;
        public AssetIdData[] assets;
    }
}
