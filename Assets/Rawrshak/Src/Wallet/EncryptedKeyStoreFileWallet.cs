using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Nethereum
using Nethereum.Web3.Accounts;

// For signing
using Nethereum.Web3;
using Nethereum.Util;
using System.Text;
using Nethereum.Signer;
using Nethereum.Hex.HexConvertors.Extensions;
using Nethereum.ABI.Encoders;   

namespace Rawrshak
{
    [CreateAssetMenu(fileName="EncryptedKeyStoreWalletFromFile", menuName="Rawrshak/Create Encrypted Key Store Object from File")]
    public class EncryptedKeyStoreFileWallet : WalletBase
    {
        // Public properties
        public string keyStoreFileLocation;
        public string password;

        public override void Load()
        {
            account = Nethereum.Web3.Accounts.Account.LoadFromKeyStoreFile(keyStoreFileLocation, password);
            if (account == null)
            {
                Debug.LogError("Invalid Key Store File");
                return;
            }
            
            // Get the Eth ECKey for signing
            eCKey = new EthECKey(account.PrivateKey);
        }
    }
}