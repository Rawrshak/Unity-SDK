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
        Assert.IsTrue(BigInteger.Parse(data.data.account.ordersCount) >= 142);
        Assert.IsTrue(BigInteger.Parse(data.data.account.orderFillsCount) >= 65);
        Assert.IsTrue(BigInteger.Parse(data.data.account.activeBuyOrders) >= 1);
        Assert.IsTrue(BigInteger.Parse(data.data.account.activeSellOrders) >= 52);
        Assert.IsTrue(BigInteger.Parse(data.data.account.activeOrdersCount) >= 53);
        Assert.IsTrue(BigInteger.Parse(data.data.account.filledOrdersCount) >= 56);
        Assert.IsTrue(BigInteger.Parse(data.data.account.volume) >= BigInteger.Parse("36859150000000000000000"));
        Assert.IsTrue(BigInteger.Parse(data.data.account.volumeAsBuyer) >= BigInteger.Parse("185500000000000000000"));
        Assert.IsTrue(BigInteger.Parse(data.data.account.volumeAsSeller) >= BigInteger.Parse("36673650000000000000000"));
        Assert.IsTrue(BigInteger.Parse(data.data.account.daysActive) >= 32);
    }

    [UnityTest]
    public IEnumerator GetAccountOrdersTest()
    {
        yield return Rawrshak.GetAccountOrders.Fetch(accountAddress, pageSize, lastOrderId).AsIEnumerator<Rawrshak.GetAccountOrders.ReturnData>();

        var data = Rawrshak.GetAccountOrders.LastFetchData;
        
        Assert.AreNotEqual(data.data.account, null);
        Assert.AreEqual(data.data.account.orders.Length, 1);
        Assert.AreEqual(data.data.account.orders[0].id, "0xc");
    }

    [UnityTest]
    public IEnumerator GetAccountOrderFillsTest()
    {
        yield return Rawrshak.GetAccountOrderFills.Fetch(accountAddress, pageSize, lastOrderId).AsIEnumerator<Rawrshak.GetAccountOrderFills.ReturnData>();

        var data = Rawrshak.GetAccountOrderFills.LastFetchData;
        
        Assert.AreNotEqual(data.data.account, null);
        Assert.AreEqual(data.data.account.orderFills.Length, 1);
        Assert.AreEqual(data.data.account.orderFills[0].order.id, "0x2c");
        Assert.AreEqual(data.data.account.orderFills[0].amount, "1");
        Assert.AreEqual(data.data.account.orderFills[0].pricePerItem, "2500000000000000000");
        Assert.AreEqual(data.data.account.orderFills[0].totalPrice, "2500000000000000000");
        Assert.AreEqual(data.data.account.orderFills[0].token.address, "0xda10009cbd5d07dd0cecc66161fc93d7c9000da1");
        Assert.AreEqual(data.data.account.orderFills[0].createdAtTimestamp, "1641596584");
    }

    [UnityTest]
    public IEnumerator GetAssetActiveBuyOrdersTest()
    {
        yield return Rawrshak.GetAssetActiveOrders.Fetch(contentAddress, tokenId, pageSize, lastOrderId, Rawrshak.OrderType.Buy).AsIEnumerator<Rawrshak.GetAssetActiveOrders.ReturnData>();

        var data = Rawrshak.GetAssetActiveOrders.LastFetchData;
        
        Assert.AreNotEqual(data.data.asset, null);
        Assert.AreEqual(data.data.asset.orders.Length, 1);
        Assert.AreEqual(data.data.asset.orders[0].id, "0x18a");
    }

    [UnityTest]
    public IEnumerator GetAssetActiveSellOrdersTest()
    {
        yield return Rawrshak.GetAssetActiveOrders.Fetch(contentAddress, tokenId, pageSize, lastOrderId, Rawrshak.OrderType.Sell).AsIEnumerator<Rawrshak.GetAssetActiveOrders.ReturnData>();

        var data = Rawrshak.GetAssetActiveOrders.LastFetchData;
        
        Assert.AreNotEqual(data.data.asset, null);
        Assert.AreEqual(data.data.asset.orders.Length, 1);
        Assert.AreEqual(data.data.asset.orders[0].id, "0x249");
    }

    [UnityTest]
    public IEnumerator GetAccountActiveBuyOrdersTest()
    {
        yield return Rawrshak.GetAccountActiveOrders.Fetch(accountAddress, pageSize, lastOrderId, Rawrshak.OrderType.Buy).AsIEnumerator<Rawrshak.GetAccountActiveOrders.ReturnData>();

        var data = Rawrshak.GetAccountActiveOrders.LastFetchData;
        
        Assert.AreNotEqual(data.data.account, null);
        Assert.AreEqual(data.data.account.orders.Length, 1);
        Assert.AreEqual(data.data.account.orders[0].id, "0x69");
    }

    [UnityTest]
    public IEnumerator GetAccountActiveSellOrdersTest()
    {
        yield return Rawrshak.GetAccountActiveOrders.Fetch(accountAddress, pageSize, lastOrderId, Rawrshak.OrderType.Sell).AsIEnumerator<Rawrshak.GetAccountActiveOrders.ReturnData>();

        var data = Rawrshak.GetAccountActiveOrders.LastFetchData;
        
        Assert.AreNotEqual(data.data.account, null);
        Assert.AreEqual(data.data.account.orders.Length, 1);
        Assert.AreEqual(data.data.account.orders[0].id, "0xc");
    }
}
