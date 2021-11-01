using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Numerics;
using Newtonsoft.Json;
using UnityEngine;
using Rawrshak;
using WalletConnectSharp.Core.Models.Ethereum;
using WalletConnectSharp.Unity;

public class TransactionCreator : MonoBehaviour
{
    public PrivateKeyWallet devWallet;

    private static string FUNCTION_SELECTOR = "0x84547d25";
    public string to = "90f79bf6eb2c4f870365e785982e1f101e93b906";
    public List<BigInteger> tokenIds;
    public List<BigInteger> amounts;
    public BigInteger nonce = 10;
    public string signer = "90f79bf6eb2c4f870365e785982e1f101e93b906";
    public string signature = "";
    public MintTransactionData transaction;

    // Start is called before the first frame update
    async void Start()
    {
        tokenIds = new List<BigInteger>();
        tokenIds.Add(7);
        tokenIds.Add(8);
        tokenIds.Add(9);
        
        amounts = new List<BigInteger>();
        amounts.Add(100);
        amounts.Add(200);
        amounts.Add(300);

        devWallet = FindObjectOfType<PrivateKeyWallet>();
        if (!devWallet) {
            devWallet = ScriptableObject.CreateInstance<PrivateKeyWallet>();
            devWallet.privateKey = "7c852118294e51e653712a81e05800f419141751be58f605c371e15141b007a6";
        }
        devWallet.Load();

        Debug.Log("Wallet: " + devWallet.GetPublicAddress());
        Debug.Log("Wallet: " + to);

        BigInteger chainId = 31337;

        transaction = new MintTransactionData();
        transaction.to = "0x" + to;
        transaction.nonce = nonce;
        transaction.signer = "0x" + signer;
        transaction.tokenIds = tokenIds;
        transaction.amounts = amounts;
        transaction.signature = devWallet.SignEIP712MintTransaction(transaction, chainId, "0x0165878A594ca255338adfa4d48449f69242Eb8F");

        signature = transaction.signature;
        Debug.Log("Signature: " + signature);
        
        // object[][] obj = { transaction.GenerateArgsForCreateContractData() };
        // string args = JsonConvert.SerializeObject(obj);
        Debug.Log("Args: " + transaction.GenerateArgsForCreateContractData());
        
    }

    private string ConvertListOfBigIntToTransactionString(List<BigInteger> array) {
        BigInteger len = array.Count;
        Debug.Log("Length: " + len.ToString("X64"));

        string arrayString = len.ToString("X64");

        List<string> arrayStr = new List<string>();
        for (int i = 0; i < array.Count; i++) {
            Debug.Log(String.Format("[{0}]: {1}", i, array[i].ToString("X64")));
            arrayStr.Add(array[i].ToString("X64"));
        }

        string arrayConcat = String.Concat(arrayStr);
        arrayString = String.Concat(arrayString, arrayConcat);

        return arrayString;
    }

    public async void Update()
    {
        if (Input.GetKeyDown("space")) {
            Debug.Log("Space Was pressed");
            string AbiFileLocation = "Abis/Content";
            string abi = Resources.Load<TextAsset>(AbiFileLocation).text;

            string data = await EVM.CreateContractData(abi, "mintBatch", transaction.GenerateArgsForCreateContractData());
            Debug.Log("Contract Data: " + data);

            string address = WalletConnect.ActiveSession.Accounts[0];

            Debug.Log("Active Session Address: " + address);

            var transactionData = new TransactionData()
            {
                from = address,
                to = "0x0165878A594ca255338adfa4d48449f69242Eb8F",
                data = data
            };
            string response = await WalletConnect.ActiveSession.EthSendTransaction(transactionData);
            Debug.Log("Response: " + response);
        }
    }
}
