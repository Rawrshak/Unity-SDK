using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;

using System;
using System.IO;
// using System.Collections;
// using System.Collections.Generic;
// using System.Threading;
// using System.Threading.Tasks;

using Nethereum;
using Nethereum.Web3;
using Nethereum.Web3.Accounts;
using Nethereum.Hex.HexConvertors.Extensions;
using Nethereum.KeyStore.Model;

namespace Rawrshak
{
    public class GenerateEthWalletMenu : EditorWindow
    {
        // Static Properties
        public static string RESOURCES_FOLDER = "Assets/Editor/Resources";
        public static string ETHEREUM_WALLET_FOLDER = "EthereumWallets";
        // static string ETHEREUM_WALLET_FILE = "Wallet-";

        public string password;
        public string publicKey;
        public string privateKey;
        public string walletFile;

        private static int currentWalletFileCounter = 0;

        [MenuItem("Rawrshak/Generate Ethereum Wallet")]
        public static void ShowExample()
        {
            GenerateEthWalletMenu wnd = GetWindow<GenerateEthWalletMenu>();
            wnd.titleContent = new GUIContent("GenerateEthereum Wallet");
        }

        public void OnEnable()
        {
            LoadData();
            LoadUXML();
            LoadUSS();

            LoadUI();
            Debug.Log("EthereumWalletMenu Enabled.");
        }

        public void OnDisable()
        {
            Debug.Log("EthereumWalletMenu Disabled.");
        }

        private void LoadData()
        {
            // Create Settings folder if necessary
            if(!Directory.Exists(String.Format("{0}/{1}", RESOURCES_FOLDER, ETHEREUM_WALLET_FOLDER)))
            {
                Directory.CreateDirectory(String.Format("{0}/{1}", RESOURCES_FOLDER, ETHEREUM_WALLET_FOLDER));
            }
        }

        private void LoadUXML()
        {
            var original = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Editor/UXML/GenerateEthWalletMenu.uxml");
            TemplateContainer treeAsset = original.CloneTree();
            rootVisualElement.Add(treeAsset);
        }

        private void LoadUSS()
        {
            var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Editor/USS/GenerateEthWalletMenu.uss");
            rootVisualElement.styleSheets.Add(styleSheet);
        }

        private void LoadUI() 
        {
            SerializedObject so = new SerializedObject(this);
            rootVisualElement.Bind(so);
            
            var newPassword = rootVisualElement.Query<TextField>("new-password").First();
            newPassword.isPasswordField = true;
            newPassword.maskChar = '*';

            // Generate Asset Bundles Button
            var generateWalletButton = rootVisualElement.Query<Button>("generate-ethereum-wallet-button").First();
            generateWalletButton.clicked += () => {
                Debug.Log("Generating Ethereum Wallet");

                GenerateWallet(newPassword.value);
            };
        }

        private void GenerateWallet(string password)
        {
            var ecKey = Nethereum.Signer.EthECKey.GenerateKey();
            var privateKeyHex = ecKey.GetPrivateKeyAsBytes().ToHex();
            var account = new Nethereum.Web3.Accounts.Account(privateKeyHex);

            privateKey = account.PrivateKey.ToString();
            publicKey = account.Address.ToString();
            walletFile = String.Format("{0}/{1}/WalletFile-{2}.json", RESOURCES_FOLDER, ETHEREUM_WALLET_FOLDER, currentWalletFileCounter);

            var keyStoreService = new Nethereum.KeyStore.KeyStoreScryptService();
            var scryptParams = new ScryptParams {Dklen = 32, N = 262144, R = 1, P = 8};
            
            var keyStore = keyStoreService.EncryptAndGenerateKeyStore(password, account.PrivateKey.HexToByteArray(), account.Address, scryptParams);
            var json = keyStoreService.SerializeKeyStoreToJson(keyStore);
            
            // write file 
            StreamWriter writer = new StreamWriter(walletFile, true);
            writer.WriteLine(json);
            writer.Close();
            AssetDatabase.Refresh();
            currentWalletFileCounter++;
        }
    }
}