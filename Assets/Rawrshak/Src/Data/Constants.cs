using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

namespace Rawrshak
{
    public class Constants
    {
        public static string RAWRSHAK_ICONTENT_INTERFACE_ID = "0x6a3af2b5";
        public static string RAWRSHAK_IEXCHANGE_INTERFACE_ID = "0x581b76ff";
        public static string IPFS_QUERY_FORMAT = "https://gateway.pinata.cloud/ipfs/{0}";
        
        public static string GET_ASSET_INFO_QUERY_STRING_LOCATION = "ContentInfo/GetAssetInfo";
        public static string GET_ASSET_IDS_WITH_TAG_QUERY_STRING_LOCATION = "ContentInfo/GetAssetIdsWithTag";
        public static string GET_ASSETS_IN_CONTENT_CONTRACT_QUERY_STRING_LOCATION = "ContentInfo/GetAssetsInContentContract";
        public static string GET_CONTENT_INFO_QUERY_STRING_LOCATION = "ContentInfo/GetContentInfo";
        public static string GET_ACCOUNT_INFO_QUERY_STRING_LOCATION = "WalletInfo/GetAccountInfo";
        public static string GET_ASSETS_IN_WALLET_QUERY_STRING_LOCATION = "WalletInfo/GetAssetsInWallet";
        public static string GET_WALLET_ASSETS_FROM_CONTRACT_QUERY_STRING_LOCATION = "WalletInfo/GetWalletAssetsInContent";
        public static string GET_WALLET_ASSETS_OF_SUBTYPE_QUERY_STRING_LOCATION = "WalletInfo/GetWalletAssetsOfSubtype";
        public static string GET_WALLET_ASSETS_OF_TYPE_QUERY_STRING_LOCATION = "WalletInfo/GetWalletAssetsOfType";
        
        public static string GET_ASSET_EXCHANGE_INFO_QUERY_STRING_LOCATION = "ExchangeInfo/GetAssetExchangeInfo";
        public static string GET_ACCOUNT_ACTIVE_ORDERS_QUERY_STRING_LOCATION = "ExchangeInfo/GetAccountActiveOrders";
        public static string GET_ACCOUNT_EXCHANGE_INFO_QUERY_STRING_LOCATION = "ExchangeInfo/GetAccountExchangeInfo";
        public static string GET_ACCOUNT_ORDER_FILLS_QUERY_STRING_LOCATION = "ExchangeInfo/GetAccountOrderFills";
        public static string GET_ACCOUNT_ORDERS_QUERY_STRING_LOCATION = "ExchangeInfo/GetAccountOrders";
        public static string GET_ASSET_ACTIVE_ORDERS_QUERY_STRING_LOCATION = "ExchangeInfo/GetAssetActiveOrders";
        public static string GET_ORDER_INFO_QUERY_STRING_LOCATION = "ExchangeInfo/GetOrderInfo";

        // Static Variables
        public static int statusCheckSleepDuration = 1000;
        public static BigInteger MaxApproveAmount = BigInteger.Pow(2, 256) - 1;
    }
}