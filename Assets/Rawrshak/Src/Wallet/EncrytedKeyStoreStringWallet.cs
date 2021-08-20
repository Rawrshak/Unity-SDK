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
    [CreateAssetMenu(fileName="EncryptedKeyStoreWalletFromString", menuName="Rawrshak/Create Encrypted Key Store Object from String")]
    public class EncryptedKeyStoreStringWallet : WalletBase
    {
        // Public properties
        public string keyStoreJson;
        public string password;

        public override void Load()
        {
            account = Nethereum.Web3.Accounts.Account.LoadFromKeyStore(keyStoreJson, password);
            if (account == null)
            {
                Debug.LogError("Invalid Encrypted Key Store");
                return;
            }
            
            // Get the Eth ECKey for signing
            eCKey = new EthECKey(account.PrivateKey);
        }
    }
}