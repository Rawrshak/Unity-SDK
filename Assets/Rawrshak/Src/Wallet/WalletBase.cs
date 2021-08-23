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
using Nethereum.Signer.EIP712;
using Nethereum.Hex.HexConvertors.Extensions;
using Nethereum.ABI.Encoders;


namespace Rawrshak
{
    public abstract class WalletBase : ScriptableObject, IWallet
    {
        // Static property
        private readonly Eip712TypedDataSigner eip712Signer = new Eip712TypedDataSigner();

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

        public string SignEIP712MintTransaction(MintTransactionData data, BigInteger chainId, string verifyingContract)
        {
            if (eCKey == null)
            {
                Debug.LogError("Account has not been set.");
                return String.Empty;
            }

            // Todo: Clean this up later. I don't know if this works yet
            var typedData = new TypedData
            {
                Domain = new Domain
                {
                    Name = "MintData",
                    Version = "1",
                    ChainId = chainId,
                    VerifyingContract = verifyingContract
                },
                Types = new Dictionary<string, MemberDescription[]>
                {
                    ["EIP712Domain"] = new[]
                    {
                        new MemberDescription {Name = "name", Type = "string"},
                        new MemberDescription {Name = "version", Type = "string"},
                        new MemberDescription {Name = "chainId", Type = "uint256"},
                        new MemberDescription {Name = "verifyingContract", Type = "address"},
                    },
                    ["MintData"] = new[]
                    {
                        new MemberDescription {Name = "to", Type = "address"},
                        new MemberDescription {Name = "tokenIds", Type = "uint256[]"},
                        new MemberDescription {Name = "amounts", Type = "uint256[]"},
                        new MemberDescription {Name = "nonce", Type = "uint256"},
                        new MemberDescription {Name = "signer", Type = "address"},
                    }
                },
                PrimaryType = "MintData",
                Message = new[]
                {
                    new MemberValue {TypeName = "address", Value = data.to},
                    new MemberValue {TypeName = "uint256[]", Value = data.tokenIds.ToArray()},
                    new MemberValue {TypeName = "uint256[]", Value = data.amounts.ToArray()},
                    new MemberValue {TypeName = "uint256", Value = data.nonce},
                    new MemberValue {TypeName = "address", Value = data.signer}
                }
            };

            var signature = eip712Signer.SignTypedData(typedData, eCKey);
            data.signature = signature;

            // return signed transaction data
            return signature;
        }

        public string SignTransaction(string msg)
        {
            if (eCKey == null)
            {
                Debug.LogError("Account has not been set.");
                return String.Empty;
            }

            // return signed transaction data
            return signer.EncodeUTF8AndSign(msg, eCKey);
        }
    }
}