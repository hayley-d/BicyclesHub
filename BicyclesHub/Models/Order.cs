using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BicyclesHub.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public byte OrderStatus { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime RequiredDate { get; set; }
        public DateTime ShippedDate { get; set; }
        public int StoreId { get; set; }
        public int StaffId { get; set; }

        public Boolean isShipped { get; set; }
        public Boolean hasCustomer { get; set; }

        public Order(int id, int customerId, byte orderStatus, DateTime orderDate, DateTime requiredDate, DateTime shippedDate, int storeId, int staffId)
        {
            Id = id;
            CustomerId = customerId;
            OrderStatus = orderStatus;
            OrderDate = orderDate;
            RequiredDate = requiredDate;
            ShippedDate = shippedDate;
            StoreId = storeId;
            StaffId = staffId;
            isShipped = true;
            hasCustomer = true;
        }

        public Order(int id, int customerId, byte orderStatus, DateTime orderDate, DateTime requiredDate, int storeId, int staffId)
        {
            Id = id;
            CustomerId = customerId;
            OrderStatus = orderStatus;
            OrderDate = orderDate;
            RequiredDate = requiredDate;
            StoreId = storeId;
            StaffId = staffId;
            isShipped = false;
            hasCustomer = true;
        }

        public Order(int id, byte orderStatus, DateTime orderDate, DateTime requiredDate, DateTime shippedDate, int storeId, int staffId)
        {
            Id = id;
            CustomerId = -1;
            OrderStatus = orderStatus;
            OrderDate = orderDate;
            RequiredDate = requiredDate;
            ShippedDate = shippedDate;
            StoreId = storeId;
            StaffId = staffId;
            isShipped = true;
            hasCustomer = false;
        }

        public Order(int id, byte orderStatus, DateTime orderDate, DateTime requiredDate, int storeId, int staffId)
        {
            Id = id;
            CustomerId = -1;
            OrderStatus = orderStatus;
            OrderDate = orderDate;
            RequiredDate = requiredDate;
            StoreId = storeId;
            StaffId = staffId;
            isShipped = false;
            hasCustomer = false;
        }
    }
}