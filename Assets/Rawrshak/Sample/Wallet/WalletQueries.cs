using System.Collections;
using System.Numerics;
using System.Collections.Generic;
using UnityEngine;
using Rawrshak;

namespace Rawrshak
{
    public class WalletQueries : MonoBehaviour
    {
        public Network network;

        // Optimism Kovan Testnet - Sample ContentManager Contract Address
        public string contractAddress;
        public string playerAddress;
        public string oper;

        // Start is called before the first frame update
        async void Start()
        {
            network = Network.Instance;
            
            Debug.Log("Player Wallet: " + playerAddress);
            Debug.Log("Contract Address: " + contractAddress);

            Debug.Log("Network chain: " + network.chain);
            Debug.Log("Network network: " + network.network);
            Debug.Log("Network endpoint: " + network.httpEndpoint);

            // Check Balance
            BigInteger balance = await ContentManager.BalanceOf(network.chain, network.network, contractAddress, playerAddress, "3", network.httpEndpoint);
            Debug.Log(playerAddress + "'s Balance: " + balance.ToString());
    
            // Check BalanceOfBatch
            string[] accounts = new string[] {playerAddress, playerAddress};
            string[] tokenIds = new string[] {"3", "4"};
            List<BigInteger> balances = await ContentManager.BalanceOfBatch(network.chain, network.network, contractAddress, accounts, tokenIds, network.httpEndpoint);
            balances.ForEach(delegate(BigInteger balance) {
                Debug.Log(playerAddress + "'s Balance: " + balance.ToString());
            });

            /****** ContentManager Contract Calls ******/
            // IsApprovedForAll()
            bool isApproved = await ContentManager.isApprovedForAll(network.chain, network.network, contractAddress, playerAddress, oper, network.httpEndpoint);
            Debug.Log("isApproved: " + isApproved);
            
            // ContractUri()
            string contractUri = await ContentManager.ContractUri(network.chain, network.network, contractAddress, network.httpEndpoint);
            Debug.Log("contractUri: " + contractUri);
            
            // ContractRoyalty()
            ContentManager.RoyaltyResponse contractRoyalty = await ContentManager.ContractRoyalty(network.chain, network.network, contractAddress, network.httpEndpoint);
            Debug.Log("contractRoyalty: " + contractRoyalty.receiver + ": " + contractRoyalty.rate);
            
            // TokenUri()
            string tokenUri = await ContentManager.TokenUri(network.chain, network.network, contractAddress, "1", network.httpEndpoint);
            Debug.Log("tokenUri: " + tokenUri);
            
            // TokenUriWithVersion()
            string tokenUriWithVersion = await ContentManager.TokenUriWithVersion(network.chain, network.network, contractAddress, "1", "0", network.httpEndpoint);
            Debug.Log("tokenUriWithVersion: " + tokenUriWithVersion);
            
            // Total Supply
            BigInteger totalSupply = await ContentManager.TotalSupply(network.chain, network.network, contractAddress, "1", network.httpEndpoint);
            Debug.Log("Token totalSupply: " + totalSupply.ToString());
            
            // Max Supply
            BigInteger maxSupply = await ContentManager.MaxSupply(network.chain, network.network, contractAddress, "1", network.httpEndpoint);
            Debug.Log("Token maxSupply: " + maxSupply.ToString());
            
            // Supports Interface
            bool supportsInterface = await ContentManager.SupportsInterface(network.chain, network.network, contractAddress, "0xd9b67a26", network.httpEndpoint);
            Debug.Log("Supports Interface: " + supportsInterface.ToString());
        }

        // Update is called once per frame
        void Update()
        {
            
        }
    }
}