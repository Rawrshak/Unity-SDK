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
    public abstract class WalletBase : ScriptableObject, IWallet
    {
        // Public properties
        public List<ContentContract> contract;

        // Private properties
        protected Account account;
        protected EthereumMessageSigner signer;
        protected EthECKey eCKey;

        public void OnEnable()
        {
            account = null;
            signer = new EthereumMessageSigner();
            eCKey = null;
        }

        public abstract void Load();

        public bool VerifyPermissionsForAll()
        {
            // Todo: Make a ContentContract call similar to ERC1155 calls specifically for all content contracts
            return true;
        }

        public bool VerifyPermissions(ContentContract contract)
        {
            // Todo: Make a ContentContract call similar to ERC1155 calls specifically for all content contracts
            return true;
        }

        public string SignTransaction(MintTransactionData data)
        {
            if (eCKey == null)
            {
                Debug.LogError("Account has not been set.");
            }
            // Convert to json string
            string json = JsonUtility.ToJson(data);

            // return signed transaction data
            return signer.EncodeUTF8AndSign(json, eCKey);
        }
    }
}