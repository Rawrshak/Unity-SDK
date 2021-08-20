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
    [CreateAssetMenu(fileName="PrivateKeyWallet", menuName="Rawrshak/Create Private Key Wallet Object")]
    public class PrivateKeyWallet : WalletBase
    {
        // Public properties
        public string privateKey;

        public override void Load()
        {
            account = new Nethereum.Web3.Accounts.Account(privateKey);
            if (account == null)
            {
                Debug.LogError("Invalid Private Key");
                return;
            }
            
            // Get the Eth ECKey for signing
            eCKey = new EthECKey(privateKey);
        }
    }
}