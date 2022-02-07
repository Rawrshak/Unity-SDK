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
        string query = MockQueryBase.LoadQuery("TestQuery");

        string expectedQuery = @"query TestQuery {{ asset ( id: ""{0}-{1}"" ) {{ id }} }}";

        Assert.AreEqual(query, expectedQuery);
    }
    
    [UnityTest]
    public IEnumerator PostAsyncTests()
    {
        string query = MockQueryBase.LoadQuery("TestQuery");

        string address = "0x899753a7055093b1dc32422cffd55186a5c18198";
        string tokenId = "1";
        
        string queryWithArgs = String.Format(query, address.ToLower(), tokenId.ToLower());

        yield return MockQueryBase.PostAsync(TestConstants.ContentSubgraphUri, queryWithArgs).AsIEnumerator<string>();

        string expectedResult = @"{""data"":{""asset"":{""id"":""0x899753a7055093b1dc32422cffd55186a5c18198-1""}}}";

        Assert.AreEqual(MockQueryBase.LastPostResult, expectedResult);
    }
}
