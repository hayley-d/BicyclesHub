using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BicyclesHub.Models
{
    public class OrderItem
    {
        public int OrderId { get; set; }
        public int ItemId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal ListPrice { get; set; }
        public decimal Discount { get; set; }

        public OrderItem(int orderId, int itemId, int productId, int quantity, decimal listPrice, decimal discount)
        {
            OrderId = orderId;
            ItemId = itemId;
            ProductId = productId;
            Quantity = quantity;
            ListPrice = listPrice;
            Discount = discount;
        }
    }
}