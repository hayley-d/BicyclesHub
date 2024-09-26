using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BicyclesHub.Models
{
    public class Seller
    {
        public int CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string StoreName { get; set; }
        public Address StoreAddress { get; set; }
        public DateTime OrderDate { get; set; }

        public Product Product { get; set; }

        public Seller(int customerId, string firstName, string lastName, string storeName, Address storeAddress, DateTime orderDate, Product product)
        {
            CustomerId = customerId;
            FirstName = firstName;
            LastName = lastName;
            StoreName = storeName;
            StoreAddress = storeAddress;
            OrderDate = orderDate;
            Product = product;
        }
    }
}