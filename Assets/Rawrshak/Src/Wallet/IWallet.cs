using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Nethereum
using Nethereum.Web3.Accounts;


namespace Rawrshak
{
    public interface IWallet
    {
        Account Load(); // Loads wallet
        bool VerifyPermissionsForAll(); // Verify Wallet permissions for all smart contract objects attached
        bool VerifyPermissions(ContentContract contract); // Verify Wallet permission of specific content contract attached
        string SignTransaction(MintTransactionData data); // Sign mint transaction data
    }
}