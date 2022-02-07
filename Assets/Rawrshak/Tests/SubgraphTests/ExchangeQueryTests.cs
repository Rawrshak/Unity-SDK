using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using GraphQlClient.Core;

public class ExchangeQueryTests
{
    BigInteger orderId = 0;
    string contentAddress = "0x899753a7055093b1dc32422cffd55186a5c18198";
    string tokenId = "0";
    string accountAddress = "0xB796BCe3db9A9DFb3F435A375f69f43a104b4caF";
    // string tag = "Rawrshak";
    int pageSize = 1;
    string lastOrderId = String.Empty;

    /******************************************/
    /*          Content Query Tests           */
    /******************************************/

    [UnityTest]
    public IEnumerator GetOrderInfoTest()
    {
        yield return Rawrshak.GetOrderInfo.Fetch(orderId).AsIEnumerator<Rawrshak.GetOrderInfo.ReturnData>();

        var data = Rawrshak.GetOrderInfo.LastFetchData;
        
        Assert.AreNotEqual(data.data.order, null);
        Assert.AreEqual(data.data.order.id, "0x0");
        Assert.AreEqual(data.data.order.asset.parentContract, "0x899753a7055093b1dc32422cffd55186a5c18198");
        Assert.AreEqual(data.data.order.asset.tokenId, "3");
        Assert.AreEqual(data.data.order.type, "Sell");
        Assert.AreEqual(data.data.order.price, "2000000000000000000");
        Assert.AreEqual(data.data.order.amountOrdered, "1");
        Assert.AreEqual(data.data.order.amountFilled, "1");
        Assert.AreEqual(data.data.order.amountClaimed, "0");
        Assert.AreEqual(data.data.order.status, "Filled");
        Assert.AreEqual(data.data.order.createdAtTimestamp, "1639869064");
        Assert.AreEqual(data.data.order.filledAtTimestamp, "1639869064");
        Assert.AreEqual(data.data.order.cancelledAtTimestamp, "0");
        Assert.AreEqual(data.data.order.lastClaimedAtTimestamp, "0");
    }

    [UnityTest]
    public IEnumerator GetAssetExchangeInfoTest()
    {
        yield return Rawrshak.GetAssetExchangeInfo.Fetch(contentAddress, tokenId).AsIEnumerator<Rawrshak.GetAssetExchangeInfo.ReturnData>();

        var data = Rawrshak.GetAssetExchangeInfo.LastFetchData;
        
        Assert.AreNotEqual(data.data.asset, null);
        Assert.IsTrue(BigInteger.Parse(data.data.asset.assetVolumeTransacted) > 100);
    }

    [UnityTest]
    public IEnumerator GetAccountExchangeInfoTest()
    {
        yield return Rawrshak.GetAccountExchangeInfo.Fetch(accountAddress).AsIEnumerator<Rawrshak.GetAccountExchangeInfo.ReturnData>();

        var data = Rawrshak.GetAccountExchangeInfo.LastFetchData;
        
        Assert.AreNotEqual(data.data.account, null);
        Assert.AreEqual(data.data.account.id, accountAddress.ToLower());
        Assert.AreEqual(data.data.account.address, accountAddress.ToLower());
    }

    [UnityTest]
    public IEnumerator GetAccountOrdersTest()
    {
        yield return Rawrshak.GetAccountOrders.Fetch(accountAddress, pageSize, lastOrderId).AsIEnumerator<Rawrshak.GetAccountOrders.ReturnData>();

        var data = Rawrshak.GetAccountOrders.LastFetchData;
    }

    [UnityTest]
    public IEnumerator GetAccountOrderFillsTest()
    {
        yield return Rawrshak.GetAccountOrderFills.Fetch(accountAddress, pageSize, lastOrderId).AsIEnumerator<Rawrshak.GetAccountOrderFills.ReturnData>();

        var data = Rawrshak.GetAccountOrderFills.LastFetchData;
    }

    [UnityTest]
    public IEnumerator GetAssetActiveBuyOrdersTest()
    {
        yield return Rawrshak.GetAssetActiveOrders.Fetch(contentAddress, tokenId, pageSize, lastOrderId, Rawrshak.OrderType.Buy).AsIEnumerator<Rawrshak.GetAssetActiveOrders.ReturnData>();

        var data = Rawrshak.GetAssetActiveOrders.LastFetchData;
    }

    [UnityTest]
    public IEnumerator GetAssetActiveSellOrdersTest()
    {
        yield return Rawrshak.GetAssetActiveOrders.Fetch(contentAddress, tokenId, pageSize, lastOrderId, Rawrshak.OrderType.Sell).AsIEnumerator<Rawrshak.GetAssetActiveOrders.ReturnData>();

        var data = Rawrshak.GetAssetActiveOrders.LastFetchData;
    }

    [UnityTest]
    public IEnumerator GetAccountActiveBuyOrdersTest()
    {
        yield return Rawrshak.GetAccountActiveOrders.Fetch(accountAddress, pageSize, lastOrderId, Rawrshak.OrderType.Buy).AsIEnumerator<Rawrshak.GetAccountActiveOrders.ReturnData>();

        var data = Rawrshak.GetAccountActiveOrders.LastFetchData;
    }

    [UnityTest]
    public IEnumerator GetAccountActiveSellOrdersTest()
    {
        yield return Rawrshak.GetAccountActiveOrders.Fetch(accountAddress, pageSize, lastOrderId, Rawrshak.OrderType.Sell).AsIEnumerator<Rawrshak.GetAccountActiveOrders.ReturnData>();

        var data = Rawrshak.GetAccountActiveOrders.LastFetchData;
    }
}
