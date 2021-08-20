using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
// using System.Threading;
using System.Threading.Tasks;

// QR Code 
using ZXing;
using ZXing.QrCode;

// Wallet Connect
using WalletConnectSharp.Desktop;
using WalletConnectSharp.Core.Models;
using WalletConnectSharp.NEthereum;

// Nethereum
using Nethereum.Web3;
using Nethereum.JsonRpc.Client;

namespace Rawrshak
{
    public class WalletManager : ScriptableObject
    {
        // Singleton
        static WalletManager _instance; 

        // Public Properties
        public ClientMeta CurrentClientMeta;
        public Texture2D LastQRCode;

        // Connection Data
        public string PublicKey;
        public int ChainId;
        
        // Todo: the following doesn't exist in WalletConnectSharp v 0.1; Must update when v0.2 comes out
        // public int NetworkId;
        public bool ConnectionSucceeded;
        
        // Private Properties
        private WalletConnect mWalletConnect;
        private WCSessionData mSessionData;

        public WalletManager Instance
        {
            get
            {
                if (!_instance)
                    _instance = FindObjectOfType<WalletManager>();
                if (!_instance)
                    _instance = ScriptableObject.CreateInstance<WalletManager>();
                return _instance;
            }
        }

        private void OnEnabled()
        {
            // Set Default Rawrshak ClientMeta
            CurrentClientMeta = new ClientMeta()
            {
                Description = "Rawrshak Unity SDK Connection",
                Icons = new[] {"https://rawrshak.io/favicon.ico"},
                Name = "Rawrshak",
                URL = "https://rawrshak.io"
            };
            hideFlags = HideFlags.HideAndDontSave;
        }

        private void OnDisabled()
        {
            // Todo: Clean up Wallet Connect
            LastQRCode = null;
            CurrentClientMeta = null;
            mWalletConnect = null;
        }

        public string GenerateUri(ClientMeta metadata)
        {
            if (metadata != null)
                CurrentClientMeta = metadata;

            mWalletConnect = new WalletConnect(CurrentClientMeta);

            return mWalletConnect.URI;
        }

        public Texture2D GenerateQRCode(int height, int width, ClientMeta metadata)
        {
            string uri = GenerateUri(metadata);

            // BarcodeWriter examples: https://csharp.hotexamples.com/examples/ZXing/BarcodeWriter/-/php-barcodewriter-class-examples.html
            var writer = new BarcodeWriter()
            {
                Format = BarcodeFormat.QR_CODE,
                Options = new QrCodeEncodingOptions
                {
                    Height = height,
                    Width = width
                }
            };

            LastQRCode = new Texture2D(height, width);
            LastQRCode.SetPixels32(writer.Write(uri));
            LastQRCode.Apply();

            return LastQRCode;
        }


        public async void Connect() {
            // Once it's shown to the user, call walletConnect.Connect(). 
            // This will block.
            var metadata = new ClientMeta()
            {
                Description = "Rawrshak Unity SDK Connection",
                Icons = new[] {"https://rawrshak.io/favicon.ico"},
                Name = "Rawrshak",
                URL = "https://rawrshak.io"
            };
            var walletConnect = new WalletConnect(metadata);
            mSessionData = await walletConnect.Connect();

            // Debug.Log(mSessionData.accounts[0]);
            // Debug.Log(mSessionData.chainId);
            ConnectionSucceeded = mSessionData.approved;
            if (ConnectionSucceeded)
            {
                PublicKey = mSessionData.accounts[0];
                
                ChainId = mSessionData.chainId;
                
                // Todo: the following doesn't exist in WalletConnectSharp v 0.1
                // NetworkId = mSessionData.networkId; 
            }
        }

        // public async Task Connect() {
        //     // Once it's shown to the user, call walletConnect.Connect(). 
        //     // This will block.
        //     mSessionData = await mWalletConnect.Connect();

        //     // Debug.Log(mSessionData.accounts[0]);
        //     // Debug.Log(mSessionData.chainId);
        //     ConnectionSucceeded = mSessionData.approved;
        //     if (ConnectionSucceeded)
        //     {
        //         PublicKey = mSessionData.accounts[0];
        //         ChainId = mSessionData.chainId;
                
        //         // Todo: the following doesn't exist in WalletConnectSharp v 0.1
        //         // NetworkId = mSessionData.networkId; 
        //     }
        // }
        
        public Web3 GetWeb3(string uri)
        {
            // Bug: This currently fails with the following compiler error
            //      error CS0012: The type 'IClient' is defined in an assembly that is 
            //      not referenced. You must add a reference to assembly 'Nethereum.JsonRpc.Client,
            //      Version=3.8.0.0, Culture=neutral, PublicKeyToken=8768a594786aba4e'.
            // For some reason, it doesn't like Nethereum.JsonRpc.Client that has the PublicKeyToken 
            // equal to null.
            
            // "https://mainnet.infura.io/v3/<infruaId>"
            var provider = mWalletConnect.CreateProvider(new Uri(uri));

            return new Web3(provider);
            // return null;
        }
    }
}
