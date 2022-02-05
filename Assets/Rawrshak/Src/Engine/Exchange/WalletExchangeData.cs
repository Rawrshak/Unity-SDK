using System;
using System.Numerics;

namespace Rawrshak
{
    public class WalletExchangeData 
    {
        public string id;
        public string address;
        public BigInteger ordersCount;
        public BigInteger orderFillsCount;
        public BigInteger activeBuyOrders;
        public BigInteger activeSellOrders;
        public BigInteger activeOrdersCount;
        public BigInteger filledOrdersCount;
        public BigInteger volume;
        public BigInteger volumeAsBuyer;
        public BigInteger volumeAsSeller;
        public BigInteger daysActive;
    }
}