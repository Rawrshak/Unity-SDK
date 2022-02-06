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
    string tokenId = "1";
    string tag = "Rawrshak";
    int pageSize = 1;
    string lastId = String.Empty;

    /******************************************/
    /*          Content Query Tests           */
    /******************************************/

    [Test]
    public async void GetContentInfoTest()
    {
        var data = await Rawrshak.GetContentInfo.Fetch(contentAddress);
    }

    [Test]
    public async void GetAssetInfoTest()
    {
        var data = await Rawrshak.GetAssetInfo.Fetch(contentAddress, tokenId);
    }

    [Test]
    public async void GetAssetIdsWithTagTest()
    {
        var data = await Rawrshak.GetAssetIdsWithTag.Fetch(tag, pageSize, lastId);
    }

    [Test]
    public async void GetAssetsInContentContractTest()
    {
        var data = await Rawrshak.GetAssetsInContentContract.Fetch(contentAddress, pageSize, lastId);
    }
}
