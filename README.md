# UnityLibraries
Rawrshak Unity Libraries for Game Developers

These libraries give the game developer the following functionality:
    Wallet Functionality
        - Log in User Wallet via WalletConnect or private key
        - Query assets in user wallet
        - filter these assets by asset type and subtype
    NFT Assets
        - download assets from NFT
        - Load assets into game
    Game Developer functionality
        - Store game developer wallet
        - Mint assets for user

## Dependencies
- Unity: 2020.3.12f1
- graphQL-client-unity: https://github.com/Gazuntype/graphQL-client-unity
- Chainsafe SDK: https://github.com/ChainSafe/web3.unity 
  - Web3.Unity: v1.0.14
- WalletConnectUnity: https://github.com/WalletConnect/WalletConnectUnity
  - Sync date: 1/10/22
  - Last Commit Synced: 3c2cac53f33253051e255c91026fc596cf672d04

## Tests
Tests aren't included in the Unity SDK package. 
Tests also have to run in play mode. 
Subgraph Tests require the Content and Exchange subgraphs to be running. (The subgraphs will get archived in a month with no activity. Make sure this is reasonable.)

### Dependencies
- Unity Test Framework (from Package Manager): v.1.1.30

### Instructions: 
1. In the menu, click: Window -> General -> Test Runner
2. Select: Play Mode tab
3. Press "Run All" button