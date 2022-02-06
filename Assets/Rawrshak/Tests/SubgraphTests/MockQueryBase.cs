using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using GraphQlClient.Core;
using Rawrshak;

public class MockQueryBase : QueryBase
{
    public new string LoadQuery(string queryLocation)
    {
        return QueryBase.LoadQuery(queryLocation);
    }

    public new async Task<String> PostAsync(string uri, string queryWithArgs)
    {
        return await QueryBase.PostAsync(uri, queryWithArgs);
    }
}