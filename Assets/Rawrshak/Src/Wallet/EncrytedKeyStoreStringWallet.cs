using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Nethereum
using Nethereum.Web3.Accounts;

namespace Rawrshak
{
    [CreateAssetMenu(fileName="EncryptedKeyStoreWalletFromString", menuName="Rawrshak/Create Encrypted Key Store Object from String")]
    public class EncryptedKeyStoreStringWallet : ScriptableObject, IWallet
    {
        // Public properties
        public string keyStoreJson;
        public string password;
        public List<ContentContract> contract;

        // Private Properties

        public Account Load()
        {
            var account = Nethereum.Web3.Accounts.Account.LoadFromKeyStore(keyStoreJson, password);
            if (account == null)
            {
                Debug.LogError("Invalid PrivateKey");
            }
            return account;
        }

        public bool VerifyPermissionsForAll()
        {
            // Todo:
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