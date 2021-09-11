using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Threading.Tasks;
using UnityEngine;

namespace Rawrshak
{
    [CreateAssetMenu(fileName="ContentContract", menuName="Rawrshak/Create Content Contract Object")]
    public class ContentContract : ScriptableObject
    {
        enum State
        {
            READY_TO_MINT,
            MINTING
        }

        public string contract;
        public WalletBase devWallet;
        private bool isValid;
        private Dictionary<BigInteger, BigInteger> assetsToMint;
        
        // Network Information
        public string chain;
        public string network;
        public BigInteger chainId;

        public void OnEnable()
        {
            isValid = false;
            assetsToMint = new Dictionary<BigInteger, BigInteger>();
        }
        
        public void OnDisable()
        {
            // Do not save assets to mint
            assetsToMint.Clear();
        }

        public async Task<bool> VerifyContracts()
        {
            return await Content.SupportsContentInterface(chain, network, contract);
        }

        public async Task<bool> MintAssets(string receiver, BigInteger nonce)
        {
            if (devWallet == null || !devWallet.IsLoaded())
            {
                Debug.LogError("Dev wallet is not initialized.");
                return false;
            }

            if (assetsToMint.Count == 0)
            {
                Debug.LogError("No Assets to mint");
                return false;
            }

            // Build the Transaction object
            MintTransactionData transaction = new MintTransactionData();
            transaction.to = receiver;
            transaction.nonce = nonce;
            transaction.signer = devWallet.GetPublicAddress();
            transaction.tokenIds = new List<BigInteger>();
            transaction.amounts = new List<BigInteger>();
            foreach (var asset in assetsToMint)
            {
                transaction.tokenIds.Add(asset.Key);
                transaction.amounts.Add(asset.Value);
            }

            // Developer wallet sign the mint transaction. Developer wallet must have correct access rights.
            transaction.signature = devWallet.SignEIP712MintTransaction(transaction, chainId, contract);

            // Send Mint transaction
            string response = await Content.MintBatch(chain, network, contract, transaction);

            // Response will contain the transaction id 
            Debug.Log(response);

            return true;
        }

        public bool AddToMintList(RawrshakAsset asset, BigInteger amount)
        {
            if (contract != asset.contract)
            {
                Debug.LogError("Asset does not belong to this contract.");
                return false;
            }

            if (assetsToMint.ContainsKey(asset.tokenId))
            {
                assetsToMint[asset.tokenId] += amount;
                return true;
            }
            assetsToMint.Add(asset.tokenId, amount);
            return true;
        }

        public bool RemoveFromMintList(RawrshakAsset asset, BigInteger amount)
        {
            if (contract != asset.contract)
            {
                Debug.LogError("Asset does not belong to this contract.");
                return false;
            }

            if (assetsToMint.ContainsKey(asset.tokenId) && assetsToMint[asset.tokenId] <= amount) {
                assetsToMint.Remove(asset.tokenId);
                return true;
            }
            assetsToMint[asset.tokenId] -= amount;
            return true;
        }

        public bool IsValid()
        {
            return isValid;
        }
    }
}