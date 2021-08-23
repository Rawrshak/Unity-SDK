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
        void Load(); // Loads wallet
        bool VerifyPermissionsForAll(); // Verify Wallet permissions for all smart contract objects attached
        bool VerifyPermissions(ContentContract contract); // Verify Wallet permission of specific content contract attached
        string SignEIP712MintTransaction(MintTransactionData data, BigInteger chainId, string verifyingContract); // Sign mint transaction data
        string SignTransaction(string msg); // Sign mint transaction data
    }
}