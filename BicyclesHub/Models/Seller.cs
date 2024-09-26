﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BicyclesHub.Models
{
    public class Seller
    {
        public int StoreId { get; set; }
        public string StoreName { get; set; }
        public Address StoreAddress { get; set; }
        public DateTime OrderDate { get; set; }

        public Product Product { get; set; }

        public Seller(int storeId,string storeName, Address storeAddress, DateTime orderDate, Product product)
        {
            StoreId = storeId;
            StoreName = storeName;
            StoreAddress = storeAddress;
            OrderDate = orderDate;
            Product = product;
        }
    }
}