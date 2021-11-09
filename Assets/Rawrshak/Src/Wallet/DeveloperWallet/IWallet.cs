using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

// Nethereum
using Nethereum.Web3.Accounts;


namespace Rawrshak
{
    public interface IWallet
    {
        string GetPublicAddress();
        void Load();
        bool IsLoaded();
        string SignEIP712MintTransaction(MintTransactionData data, BigInteger chainId, string verifyingContract); // Sign mint transaction data
        string SignTransaction(string msg); // Sign mint transaction data
    }
}