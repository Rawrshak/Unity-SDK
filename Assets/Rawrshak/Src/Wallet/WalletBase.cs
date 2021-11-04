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
        
        public string GetPublicAddress()
        {
            if (account != null)
            {
                return account.Address;
            }
            return String.Empty;
        }

        public abstract void Load();

        public bool IsLoaded()
        {
            return account != null;
        }

        // Todo: Eip712Signer does not support arrays yet. Update this when it is supported. Mint from in-game doesn't
        //       work so far.
        // Issue Tracker: https://github.com/Nethereum/Nethereum/issues/730 
        public string SignEIP712MintTransaction(MintTransactionData data, BigInteger chainId, string verifyingContract)
        {
            throw new NotImplementedException("Array types are not supported");
            // if (eCKey == null)
            // {
            //     Debug.LogError("Account has not been set.");
            //     return String.Empty;
            // }
            // var typedData = new TypedData
            // {
            //     Domain = new Domain
            //     {
            //         Name = "MintData",
            //         Version = "1",
            //         ChainId = chainId,
            //         VerifyingContract = verifyingContract
            //     },
            //     Types = new Dictionary<string, MemberDescription[]>
            //     {
            //         ["EIP712Domain"] = new[]
            //         {
            //             new MemberDescription {Name = "name", Type = "string"},
            //             new MemberDescription {Name = "version", Type = "string"},
            //             new MemberDescription {Name = "chainId", Type = "uint256"},
            //             new MemberDescription {Name = "verifyingContract", Type = "address"},
            //         },
            //         ["MintData"] = new[]
            //         {
            //             new MemberDescription {Name = "to", Type = "address"},
            //             new MemberDescription {Name = "tokenIds", Type = "uint256[]"},
            //             new MemberDescription {Name = "amounts", Type = "uint256[]"},
            //             new MemberDescription {Name = "nonce", Type = "uint256"},
            //             new MemberDescription {Name = "signer", Type = "address"},
            //         }
            //     },
            //     PrimaryType = "MintData",
            //     Message = new[]
            //     {
            //         new MemberValue {TypeName = "address", Value = data.to},
            //         new MemberValue {TypeName = "uint256[]", Value = data.tokenIds.ToArray()},
            //         new MemberValue {TypeName = "uint256[]", Value = data.amounts.ToArray()},
            //         new MemberValue {TypeName = "uint256", Value = data.nonce},
            //         new MemberValue {TypeName = "address", Value = account.Address}
            //     }
            // };

            // // Todo: SignTypedData() is currently broken. This needs to get fixed by Nethereum. 
            // // Issue Tracker: https://github.com/Nethereum/Nethereum/issues/730 
            // // return eip712Signer.SignTypedData(typedData, eCKey);

            // // This is currently a workaround because eip712Signer.SignTypedData() returns the wrong signature.
            // var hashedData = Sha3Keccack.Current.CalculateHash(Eip712TypedDataSigner.Current.EncodeTypedData(typedData));
            // return EthECDSASignature.CreateStringSignature(eCKey.SignAndCalculateV(hashedData));
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