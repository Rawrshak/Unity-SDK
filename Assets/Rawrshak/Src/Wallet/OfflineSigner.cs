using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
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
    public class OfflineSigner : ScriptableObject
    {
        public static string SignMessage(string privateKey, string msg)
        {
            // Get the Eth ECKey for signing
            var eCKey = new EthECKey(privateKey);
            if (eCKey == null)
            {
                Debug.LogError("OfflineSigner: Invalid Private Key");
                return String.Empty;
            }

            var signer = new EthereumMessageSigner();

            return signer.EncodeUTF8AndSign(msg, eCKey);
        }
    }
}