using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Threading.Tasks;
using UnityEngine;

namespace Rawrshak
{
    public class Order
    {
        public BigInteger orderId;
        public string parentContract;
        public BigInteger tokenId;
        public OrderType type;
        public BigInteger price;
        public BigInteger amountOrdered;
        public BigInteger amountFilled;
        public BigInteger amountClaimed;
        public OrderStatus status;
        public BigInteger createdAtTimestamp;
        public BigInteger filledAtTimestamp;
        public BigInteger cancelledAtTimestamp;
        public BigInteger lastClaimedAtTimestamp;

        public async Task Load()
        {
            GetOrderInfo.ReturnData data = await GetOrderInfo.Fetch(orderId);

            if (data.data.order == null)
            {
                Debug.LogError("[Order] Order doesn't exist.");
                return;
            }

            parentContract = data.data.order.asset.parentContract;
            tokenId = BigInteger.Parse(data.data.order.asset.tokenId);
            type = ParseOrderType(data.data.order.type);
            price = BigInteger.Parse(data.data.order.price);
            amountOrdered = BigInteger.Parse(data.data.order.amountOrdered);
            amountFilled = BigInteger.Parse(data.data.order.amountFilled);
            amountClaimed = BigInteger.Parse(data.data.order.amountClaimed);
            status = ParseOrderStatus(data.data.order.status);
            createdAtTimestamp = BigInteger.Parse(data.data.order.createdAtTimestamp);
            filledAtTimestamp = BigInteger.Parse(data.data.order.filledAtTimestamp);
            cancelledAtTimestamp = BigInteger.Parse(data.data.order.cancelledAtTimestamp);
            lastClaimedAtTimestamp = BigInteger.Parse(data.data.order.lastClaimedAtTimestamp);
        }

        public bool IsBuyOrder()
        {
            return type == OrderType.Buy;
        }

        public static OrderType ParseOrderType(string orderType)
        {
            return (orderType == "Buy") ? OrderType.Buy : OrderType.Sell;
        }

        public static OrderStatus ParseOrderStatus(string orderStatus)
        {
            switch (orderStatus)
            {
                case "Ready":
                {
                    return OrderStatus.Ready;
                }
                case "PartiallyFilled":
                {
                    return OrderStatus.PartiallyFilled;
                }
                case "Filled":
                {
                    return OrderStatus.Filled;
                }
                case "Claimed":
                {
                    return OrderStatus.Claimed;
                }
                case "Cancelled":
                default:
                {
                    return OrderStatus.Cancelled;
                }
            }
        }
    }
}