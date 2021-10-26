using System.Collections;
using System.Numerics;
using System.Collections.Generic;
using UnityEngine;
using Rawrshak;

namespace Rawrshak
{
    public class WalletQueries : MonoBehaviour
    {
        public string privateKey = "";
        public PrivateKeyWallet wallet;
        public Network network;

        // Optimism Kovan Testnet - Sample Content Contract Address
        public string contractAddress = "0x184d723b301c08401f200a4cdf221c5fc93df3e5";

        // Start is called before the first frame update
        async void Start()
        {
            network = Network.Instance;

            wallet = FindObjectOfType<PrivateKeyWallet>();
            if (!wallet) {
                wallet = ScriptableObject.CreateInstance<PrivateKeyWallet>();
            }
            wallet.privateKey = privateKey;
            wallet.Load();

            string addr = wallet.GetPublicAddress();
            Debug.Log("Wallet: " + addr);

            // Check Balance
            BigInteger balance = await Content.BalanceOf(network.chain, network.network, contractAddress, addr, "1", network.httpEndpoint);
            Debug.Log(addr + "'s Balance: " + balance.ToString());
    
            // Check BalanceOfBatch
            string[] accounts = new string[] {addr, addr};
            string[] tokenIds = new string[] {"0", "1"};
            List<BigInteger> balances = await Content.BalanceOfBatch(network.chain, network.network, contractAddress, accounts, tokenIds, network.httpEndpoint);
            balances.ForEach(delegate(BigInteger balance) {
                Debug.Log(addr + "'s Balance: " + balance.ToString());
            });

            /****** Content Contract Calls ******/
            // IsApprovedForAll()
            bool isApproved = await Content.isApprovedForAll(network.chain, network.network, contractAddress, contractAddress, contractAddress, network.httpEndpoint);
            Debug.Log("isApproved: " + isApproved);
            
            // ContractUri()
            string contractUri = await Content.ContractUri(network.chain, network.network, contractAddress, network.httpEndpoint);
            Debug.Log("contractUri: " + contractUri);
            
            // ContractRoyalty()
            Content.RoyaltyResponse contractRoyalty = await Content.ContractRoyalty(network.chain, network.network, contractAddress, network.httpEndpoint);
            Debug.Log("contractRoyalty: " + contractRoyalty.receiver + ": " + contractRoyalty.rate);
            
            // TokenUri()
            string tokenUri = await Content.TokenUri(network.chain, network.network, contractAddress, "1", network.httpEndpoint);
            Debug.Log("tokenUri: " + tokenUri);
            
            // TokenUriWithVersion()
            string tokenUriWithVersion = await Content.TokenUriWithVersion(network.chain, network.network, contractAddress, "1", "0", network.httpEndpoint);
            Debug.Log("tokenUriWithVersion: " + tokenUriWithVersion);
            
            // Total Supply
            BigInteger totalSupply = await Content.TotalSupply(network.chain, network.network, contractAddress, "1", network.httpEndpoint);
            Debug.Log("Token totalSupply: " + totalSupply.ToString());
            
            // Max Supply
            BigInteger maxSupply = await Content.MaxSupply(network.chain, network.network, contractAddress, "1", network.httpEndpoint);
            Debug.Log("Token maxSupply: " + maxSupply.ToString());
            
            // Supports Interface
            bool supportsInterface = await Content.SupportsInterface(network.chain, network.network, contractAddress, "0xd9b67a26", network.httpEndpoint);
            Debug.Log("Supports Interface: " + supportsInterface.ToString());
        }

        // Update is called once per frame
        void Update()
        {
            
        }
    }
}