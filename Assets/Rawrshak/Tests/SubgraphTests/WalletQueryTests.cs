using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using GraphQlClient.Core;

public class WalletQueryTests
{
    string accountAddress = "0xB796BCe3db9A9DFb3F435A375f69f43a104b4caF";
    string contentAddress = "0x20af0c7dd43fc91e7e8f449692f26adc2fa69ee4";
    // string tokenId = "1";
    int pageSize = 1;
    string lastId = String.Empty;
    string type = "text";
    string subtype = "title";

    /******************************************/
    /*          Wallet Query Tests            */
    /******************************************/
    [UnityTest]
    public IEnumerator GetAccountInfoTest()
    {
        // AsIEnumerator allows us to run Async Tasks that need to run in the main thread to be used 
        // in a coroutine
        yield return Rawrshak.GetAccountInfo.Fetch(accountAddress).AsIEnumerator<Rawrshak.GetAccountInfo.ReturnData>();

        var data = Rawrshak.GetAccountInfo.LastFetchData;

        Assert.AreEqual(data.data.account.id, accountAddress.ToLower());
        Assert.AreEqual(data.data.account.address, accountAddress.ToLower());
    }
    
    [UnityTest]
    public IEnumerator GetAssetsInWalletTest()
    {
        yield return Rawrshak.GetAssetsInWallet.Fetch(accountAddress, pageSize, lastId).AsIEnumerator<Rawrshak.GetAssetsInWallet.ReturnData>();

        string expectedResultContractAddress = "0x20af0c7dd43fc91e7e8f449692f26adc2fa69ee4";
        string expectedResultTokenId = "0";
        
        var data = Rawrshak.GetAssetsInWallet.LastFetchData;
        
        Assert.AreEqual(data.data.account.assetBalances.Length, 1);
        Assert.AreEqual(data.data.account.assetBalances[0].asset.parentContract.id, expectedResultContractAddress);
        Assert.AreEqual(data.data.account.assetBalances[0].asset.tokenId, expectedResultTokenId);
        Assert.AreEqual(data.data.account.assetBalances[0].id, String.Format("{0}-{1}-{2}", expectedResultContractAddress, accountAddress.ToLower(), expectedResultTokenId));
    }
    
    [UnityTest]
    public IEnumerator GetWalletAssetsInContentTest()
    {
        yield return Rawrshak.GetWalletAssetsInContent.Fetch(accountAddress, contentAddress, pageSize, lastId).AsIEnumerator<Rawrshak.GetWalletAssetsInContent.ReturnData>();

        string expectedResultContractAddress = "0x20af0c7dd43fc91e7e8f449692f26adc2fa69ee4";
        string expectedResultTokenId = "0";
        
        var data = Rawrshak.GetWalletAssetsInContent.LastFetchData;

        Assert.AreEqual(data.data.account.assetBalances.Length, 1);
        Assert.AreEqual(data.data.account.assetBalances[0].asset.parentContract.id, expectedResultContractAddress);
        Assert.AreEqual(data.data.account.assetBalances[0].asset.tokenId, expectedResultTokenId);
        Assert.AreEqual(data.data.account.assetBalances[0].id, String.Format("{0}-{1}-{2}", expectedResultContractAddress, accountAddress.ToLower(), expectedResultTokenId));
    }
    
    [UnityTest]
    public IEnumerator GetWalletAssetsOfTypeTest()
    {
        yield return Rawrshak.GetWalletAssetsOfType.Fetch(accountAddress, type, pageSize, lastId).AsIEnumerator<Rawrshak.GetWalletAssetsOfType.ReturnData>();
        
        string expectedResultContractAddress = "0x899753a7055093b1dc32422cffd55186a5c18198";
        string expectedResultTokenId = "0";

        var data = Rawrshak.GetWalletAssetsOfType.LastFetchData;

        Assert.AreEqual(data.data.account.assetBalances.Length, 1);
        Assert.AreEqual(data.data.account.assetBalances[0].asset.parentContract.id, expectedResultContractAddress);
        Assert.AreEqual(data.data.account.assetBalances[0].asset.tokenId, expectedResultTokenId);
        Assert.AreEqual(data.data.account.assetBalances[0].id, String.Format("{0}-{1}-{2}", expectedResultContractAddress, accountAddress.ToLower(), expectedResultTokenId));
    }
    
    [UnityTest]
    public IEnumerator GetWalletAssetsOfSubtypeTest()
    {
        yield return Rawrshak.GetWalletAssetsOfSubtype.Fetch(accountAddress, subtype, pageSize, lastId).AsIEnumerator<Rawrshak.GetWalletAssetsOfSubtype.ReturnData>();

        var data = Rawrshak.GetWalletAssetsOfSubtype.LastFetchData;
        
        string expectedResultContractAddress = "0x899753a7055093b1dc32422cffd55186a5c18198";
        string expectedResultTokenId = "0";

        Assert.AreEqual(data.data.account.assetBalances.Length, 1);
        Assert.AreEqual(data.data.account.assetBalances[0].asset.parentContract.id, expectedResultContractAddress);
        Assert.AreEqual(data.data.account.assetBalances[0].asset.tokenId, expectedResultTokenId);
        Assert.AreEqual(data.data.account.assetBalances[0].id, String.Format("{0}-{1}-{2}", expectedResultContractAddress, accountAddress.ToLower(), expectedResultTokenId));
    }
}
