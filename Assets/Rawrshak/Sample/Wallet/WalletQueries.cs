using System.Collections;
using System.Numerics;
using System.Collections.Generic;
using UnityEngine;
using Rawrshak;

namespace Rawrshak
{
    public class WalletQueries : MonoBehaviour
    {
        public NetworkManager networkManager;

        // Optimism Kovan Testnet - Sample ContentManager Contract Address
        public string contractAddress;
        public string playerAddress;
        public string oper;

        // Start is called before the first frame update
        async void Start()
        {
            networkManager = NetworkManager.Instance;
            
            Debug.Log("Player Wallet: " + playerAddress);
            Debug.Log("Contract Address: " + contractAddress);

            Debug.Log("Network chain: " + networkManager.chain);
            Debug.Log("Network network: " + networkManager.network);
            Debug.Log("Network endpoint: " + networkManager.httpEndpoint);

            // Check Balance
            BigInteger balance = await ContentManager.BalanceOf(networkManager.chain, networkManager.network, contractAddress, playerAddress, "3", networkManager.httpEndpoint);
            Debug.Log(playerAddress + "'s Balance: " + balance.ToString());
    
            // Check BalanceOfBatch
            string[] accounts = new string[] {playerAddress, playerAddress};
            string[] tokenIds = new string[] {"3", "4"};
            List<BigInteger> balances = await ContentManager.BalanceOfBatch(networkManager.chain, networkManager.network, contractAddress, accounts, tokenIds, networkManager.httpEndpoint);
            balances.ForEach(delegate(BigInteger balance) {
                Debug.Log(playerAddress + "'s Balance: " + balance.ToString());
            });

            /****** ContentManager Contract Calls ******/
            // IsApprovedForAll()
            bool isApproved = await ContentManager.isApprovedForAll(networkManager.chain, networkManager.network, contractAddress, playerAddress, oper, networkManager.httpEndpoint);
            Debug.Log("isApproved: " + isApproved);
            
            // ContractUri()
            string contractUri = await ContentManager.ContractUri(networkManager.chain, networkManager.network, contractAddress, networkManager.httpEndpoint);
            Debug.Log("contractUri: " + contractUri);
            
            // ContractRoyalty()
            ContentManager.RoyaltyResponse contractRoyalty = await ContentManager.ContractRoyalty(networkManager.chain, networkManager.network, contractAddress, networkManager.httpEndpoint);
            Debug.Log("contractRoyalty: " + contractRoyalty.receiver + ": " + contractRoyalty.rate);
            
            // TokenUri()
            string tokenUri = await ContentManager.TokenUri(networkManager.chain, networkManager.network, contractAddress, "1", networkManager.httpEndpoint);
            Debug.Log("tokenUri: " + tokenUri);
            
            // TokenUriWithVersion()
            string tokenUriWithVersion = await ContentManager.TokenUriWithVersion(networkManager.chain, networkManager.network, contractAddress, "1", "0", networkManager.httpEndpoint);
            Debug.Log("tokenUriWithVersion: " + tokenUriWithVersion);
            
            // Total Supply
            BigInteger totalSupply = await ContentManager.TotalSupply(networkManager.chain, networkManager.network, contractAddress, "1", networkManager.httpEndpoint);
            Debug.Log("Token totalSupply: " + totalSupply.ToString());
            
            // Max Supply
            BigInteger maxSupply = await ContentManager.MaxSupply(networkManager.chain, networkManager.network, contractAddress, "1", networkManager.httpEndpoint);
            Debug.Log("Token maxSupply: " + maxSupply.ToString());
            
            // Supports Interface
            bool supportsInterface = await ContentManager.SupportsInterface(networkManager.chain, networkManager.network, contractAddress, "0xd9b67a26", networkManager.httpEndpoint);
            Debug.Log("Supports Interface: " + supportsInterface.ToString());
        }

        // Update is called once per frame
        void Update()
        {
            
        }
    }
}