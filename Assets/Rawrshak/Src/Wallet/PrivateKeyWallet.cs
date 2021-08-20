using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Nethereum
using Nethereum.Web3.Accounts;

namespace Rawrshak
{
    [CreateAssetMenu(fileName="PrivateKeyWallet", menuName="Rawrshak/Create Private Key Wallet Object")]
    public class PrivateKeyWallet : ScriptableObject, IWallet
    {
        // Public properties
        public string privateKey;
        public List<ContentContract> contract;

        public Account Load()
        {
            var account = new Nethereum.Web3.Accounts.Account(privateKey);
            if (account == null)
            {
                Debug.LogError("Invalid PrivateKey");
            }
            return account;
        }

        public bool VerifyPermissionsForAll()
        {
            // Todo: Make a ContentContract call similar to ERC1155 calls specifically for all content contracts
            return true;
        }

        public bool VerifyPermissions(ContentContract contract)
        {
            // Todo:
            return true;
        }

        public string SignTransaction(MintTransactionData data)
        {
            // Todo:
            return String.Empty;
        }
    }
}