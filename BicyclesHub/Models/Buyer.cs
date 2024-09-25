using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BicyclesHub.Models
{
    public class Buyer
    {
        public int CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string StoreName { get; set; }
        public Address StoreAddress { get; set; }
        public Staff Staff { get; set; }
        public DateTime OrderDate { get; set; }

        public Buyer(int customerId, string firstName, string lastName, string storeName, Address storeAddress, Staff staff, DateTime orderDate)
        {
            CustomerId = customerId;
            FirstName = firstName;
            LastName = lastName;
            StoreName = storeName;
            StoreAddress = storeAddress;
            Staff = staff;
            OrderDate = orderDate;
        }
    }
}