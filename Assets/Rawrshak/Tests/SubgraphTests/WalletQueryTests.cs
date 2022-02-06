using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using GraphQlClient.Core;

public static class UnityTestUtils {
    public static void RunAsyncMethodSync(this Func < Task > asyncFunc) {
    Task.Run(async () => await asyncFunc()).GetAwaiter().GetResult();
    }
}

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
    [Test]
    public async void GetAccountInfoTest()
    {
        var data = await Rawrshak.GetAccountInfo.Fetch(accountAddress);

        Assert.AreEqual(data.data.account.id, accountAddress.ToLower());
        Assert.AreEqual(data.data.account.address, accountAddress.ToLower());
    }
    
    [Test]
    public async void GetAssetsInWalletTest()
    {
        var data = await Rawrshak.GetAssetsInWallet.Fetch(accountAddress, pageSize, lastId);

        string expectedResultContractAddress = "0x20af0c7dd43fc91e7e8f449692f26adc2fa69ee4";
        string expectedResultTokenId = "0";
        
        Assert.AreEqual(data.data.account.assetBalances.Length, 1);
        Assert.AreEqual(data.data.account.assetBalances[0].asset.parentContract.id, expectedResultContractAddress);
        Assert.AreEqual(data.data.account.assetBalances[0].asset.tokenId, expectedResultTokenId);
        Assert.AreEqual(data.data.account.assetBalances[0].id, String.Format("{0}-{1}-{2}", expectedResultContractAddress, accountAddress.ToLower(), expectedResultTokenId));
    }
    
    [Test]
    public void GetWalletAssetsInContentTest()
    {
        UnityTestUtils.RunAsyncMethodSync(async() => {
            var data = await Rawrshak.GetWalletAssetsInContent.Fetch(accountAddress, contentAddress, pageSize, lastId);

            string expectedResultContractAddress = "0x20af0c7dd43fc91e7e8f449692f26adc2fa69ee4";
            string expectedResultTokenId = "0";

            Assert.AreEqual(data.data.account.assetBalances.Length, 1);
            Assert.AreEqual(data.data.account.assetBalances[0].asset.parentContract.id, expectedResultContractAddress);
            Assert.AreEqual(data.data.account.assetBalances[0].asset.tokenId, expectedResultTokenId);
            Assert.AreEqual(data.data.account.assetBalances[0].id, String.Format("{0}-{1}-{2}", expectedResultContractAddress, accountAddress.ToLower(), expectedResultTokenId));
        });
    }
    
    [Test]
    public async void GetWalletAssetsOfTypeTest()
    {
        var data = await Rawrshak.GetWalletAssetsOfType.Fetch(accountAddress, type, pageSize, lastId);

        // Assert.AreEqual(data.data.account.id, accountAddress.ToLower());
        // Assert.AreEqual(data.data.account.address, accountAddress.ToLower());
    }
    
    [Test]
    public async void GetWalletAssetsOfSubtypeTest()
    {
        var data = await Rawrshak.GetWalletAssetsOfSubtype.Fetch(accountAddress, subtype, pageSize, lastId);

        // Assert.AreEqual(data.data.account.id, accountAddress.ToLower());
        // Assert.AreEqual(data.data.account.address, accountAddress.ToLower());
    }
}
