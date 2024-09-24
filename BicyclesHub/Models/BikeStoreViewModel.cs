using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BicyclesHub.Models
{
    public class BikeStoreViewModel
    {
        private DataManager dataManager = new DataManager();
        public List<Brand> Brands { get; set; }
        public List<Order> Orders { get; set; }
        public List<Product> Products { get; set; }
        public List<OrderItem> OrderItems { get; set; }
        public List<Stock> Stocks { get; set; }
        public List<Store> Stores { get; set; }
        public List<Staff> Staff { get; set; }
        public List<Category> Categories { get; set; }
        public List<Customer> Customers { get; set; }
        public Summary Summary { get; set; }

        public BikeStoreViewModel()
        {
            setBrands();
            setOrders();
            setOrderItems();
            setProducts();
            setStocks();
            setStores();
            setCategory();
            setSummary();
            setStaff();
            setCustomers();
        }

        public void setBrands()
        {
            Brands = dataManager.GetAllBrands();
        }

        public void setOrders()
        {
            Orders = dataManager.GetAllOrders();
        }

        public void setProducts()
        {
            Products = dataManager.GetAllProducts();
        }

        public void setOrderItems()
        {
            OrderItems = dataManager.GetAllOrderItems();
        }

        public void setStocks()
        {
            Stocks = dataManager.GetAllStock();
        }

        public void setStaff()
        {
            Staff = dataManager.GetAllStaff();
        }

        public void setStores()
        {
            Stores = dataManager.GetAllStores();
        }
        public void setCategory()
        {
            Categories = dataManager.GetAllCategories();
        }

        public void setSummary()
        {
            Summary = dataManager.GetSummary();
        }

        public void setCustomers()
        {
            Customers = dataManager.GetAllCustomers();
        }
    }
}