using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using GraphQlClient.Core;

public class ContentQueryTests
{
    string contentAddress = "0x899753a7055093b1dc32422cffd55186a5c18198";
    string tokenId = "0";
    string tag = "Rawrshak";
    int pageSize = 1;
    string lastId = String.Empty;

    /******************************************/
    /*          Content Query Tests           */
    /******************************************/

    [UnityTest]
    public IEnumerator GetContentInfoTest()
    {
        yield return Rawrshak.GetContentInfo.Fetch(contentAddress).AsIEnumerator<Rawrshak.GetContentInfo.ReturnData>();

        var data = Rawrshak.GetContentInfo.LastFetchData;
        
        Assert.AreNotEqual(data.data.content, null);
        Assert.AreEqual(data.data.content.id, contentAddress.ToLower());
        Assert.AreEqual(data.data.content.name, "Rawrshak Default Assets");
        Assert.AreEqual(data.data.content.game, String.Empty);
        Assert.AreEqual(data.data.content.creator, "Rawrshak Dev Team");
        Assert.AreEqual(data.data.content.creatorAddress, "0xb796bce3db9a9dfb3f435a375f69f43a104b4caf");
        Assert.AreEqual(data.data.content.contractAddress, contentAddress.ToLower());
        Assert.AreEqual(data.data.content.contractUri, "QmcMTRsv1Yt8PMoQpViids7qB2fnezzTRWXAAsDm35S3Pp");
        Assert.AreEqual(data.data.content.royaltyRate, 10000);
        Assert.AreEqual(data.data.content.tags.Length, 2);
    }

    [UnityTest]
    public IEnumerator GetAssetInfoTest()
    {        
        yield return Rawrshak.GetAssetInfo.Fetch(contentAddress, tokenId).AsIEnumerator<Rawrshak.GetAssetInfo.ReturnData>();

        var data = Rawrshak.GetAssetInfo.LastFetchData;
        
        Assert.AreNotEqual(data.data.asset, null);
        Assert.AreEqual(data.data.asset.tokenId, tokenId);
        Assert.AreEqual(data.data.asset.name, "Rawr Apprentice Title");
        Assert.AreEqual(data.data.asset.type, "text");
        Assert.AreEqual(data.data.asset.subtype, "title");
        Assert.AreEqual(data.data.asset.imageUri, "https://arweave.net/TsBCV3HyMssIZQk0S7MqZht_zR3e9pBXDWUo0VYAXW4");
        Assert.AreEqual(data.data.asset.maxSupply, "115792089237316195423570985008687907853269984665640564039457584007913129639935");
        Assert.AreEqual(data.data.asset.latestPublicUriVersion, "0");
        Assert.AreEqual(data.data.asset.latestHiddenUriVersion, "0");
        Assert.AreEqual(data.data.asset.latestPublicUri, "QmNQecdZ5CkwnmCZXhaZQCHzcVofZiuDCMHUKQnDfyqZ9i");
    }

    [UnityTest]
    public IEnumerator GetAssetIdsWithTagTest()
    {
        yield return Rawrshak.GetAssetIdsWithTag.Fetch(tag, pageSize, lastId).AsIEnumerator<Rawrshak.GetAssetIdsWithTag.ReturnData>();

        var data = Rawrshak.GetAssetIdsWithTag.LastFetchData;
        
        Assert.AreNotEqual(data.data.tag, null);
        Assert.AreEqual(data.data.tag.id, tag);
        Assert.AreEqual(data.data.tag.assets.Length, 1);
        Assert.AreEqual(data.data.tag.assets[0].tokenId, "0");
        Assert.AreEqual(data.data.tag.assets[0].parentContract.id, "0x20af0c7dd43fc91e7e8f449692f26adc2fa69ee4");
    }

    [UnityTest]
    public IEnumerator GetAssetsInContentContractTest()
    {
        yield return Rawrshak.GetAssetsInContentContract.Fetch(contentAddress, pageSize, lastId).AsIEnumerator<Rawrshak.GetAssetsInContentContract.ReturnData>();

        var data = Rawrshak.GetAssetsInContentContract.LastFetchData;
        
        Assert.AreNotEqual(data.data.content, null);
        Assert.AreEqual(data.data.content.assets.Length, 1);
        Assert.AreEqual(data.data.content.assets[0].tokenId, tokenId);
        Assert.AreEqual(data.data.content.assets[0].name, "Rawr Apprentice Title");
        Assert.AreEqual(data.data.content.assets[0].type, "text");
        Assert.AreEqual(data.data.content.assets[0].subtype, "title");
        Assert.AreEqual(data.data.content.assets[0].imageUri, "https://arweave.net/TsBCV3HyMssIZQk0S7MqZht_zR3e9pBXDWUo0VYAXW4");
        Assert.AreEqual(data.data.content.assets[0].maxSupply, "115792089237316195423570985008687907853269984665640564039457584007913129639935");
        Assert.AreEqual(data.data.content.assets[0].latestPublicUriVersion, "0");
        Assert.AreEqual(data.data.content.assets[0].latestHiddenUriVersion, "0");
        Assert.AreEqual(data.data.content.assets[0].latestPublicUri, "QmNQecdZ5CkwnmCZXhaZQCHzcVofZiuDCMHUKQnDfyqZ9i");
        Assert.AreEqual(data.data.content.assets[0].tags.Length, 0);
    }
}
