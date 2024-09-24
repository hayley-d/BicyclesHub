using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BicyclesHub.Models
{
    public class Stock
    {
        public int StoreId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }

        public Stock(int storeId, int productId, int quantity)
        {
            StoreId = storeId;
            ProductId = productId;
            Quantity = quantity;
        }
    }
}