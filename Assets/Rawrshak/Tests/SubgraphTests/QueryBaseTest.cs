using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using GraphQlClient.Core;

public class QueryBaseTests
{

    /******************************************/
    /*          Query Base Tests              */
    /******************************************/
    [Test]
    public void LoadQueryTests()
    {
        MockQueryBase queryBase = new MockQueryBase();

        string query = queryBase.LoadQuery("TestQuery");

        string expectedQuery = @"query TestQuery {{ asset ( id: ""{0}-{1}"" ) {{ id }} }}";

        Assert.AreEqual(query, expectedQuery);
    }
    
    [Test]
    public async void PostAsyncTests()
    {
        // Note: Because this makes UnityWebRequests, this test needs to be in the Play Mode to pass.
        MockQueryBase queryBase = new MockQueryBase();

        string query = queryBase.LoadQuery("TestQuery");

        string address = "0x899753a7055093b1dc32422cffd55186a5c18198";
        string tokenId = "1";
        
        string queryWithArgs = String.Format(query, address.ToLower(), tokenId.ToLower());

        string result = await queryBase.PostAsync(TestConstants.ContentSubgraphUri, queryWithArgs);

        string expectedResult = @"{""data"":{""asset"":{""id"":""0x899753a7055093b1dc32422cffd55186a5c18198-1""}}}";

        Assert.AreEqual(result, expectedResult);
    }
}
