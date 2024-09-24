using BicyclesHub.Controllers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Web;

namespace BicyclesHub.Models
{
    public class DataManager
    {
        SqlConnection myConnection = new SqlConnection(Globals.ConnectionString);

        public Summary GetSummary()
        {
            return new Summary(0, 0, 0, 0, 0, 0, 0, 0);
        }

        public List<Brand> GetAllBrands()
        {
            var brands = new List<Brand>();
            using (myConnection)
            {
                myConnection.Open();
                string query = "SELECT * FROM [production].[brands]";

                using (SqlCommand command = new SqlCommand(query, myConnection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            brands.Add(new Brand(reader.GetInt32(0), reader.GetString(1)));
                        }
                    }
                }
            }
            return brands;
        }

        public List<Category> GetAllCategories()
        {
            var categories = new List<Category>();
            using (myConnection)
            {
                myConnection.Open();
                string query = "SELECT * FROM [production].[categories]";

                using (SqlCommand command = new SqlCommand(query, myConnection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            categories.Add(new Category(reader.GetInt32(0), reader.GetString(1)));
                        }
                    }
                }
            }
            return categories;
        }

        public List<Product> GetAllProducts()
        {
            var products = new List<Product>();
            using (myConnection)
            {
                myConnection.Open();
                string query = "SELECT * FROM [production].[products]";

                using (SqlCommand command = new SqlCommand(query, myConnection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            products.Add(new Product(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2), reader.GetInt32(3), reader.GetInt16(4), reader.GetDecimal(5)));
                        }
                    }
                }
            }
            return products;
        }

        public List<Stock> GetAllStock()
        {
            var stocks = new List<Stock>();
            using (myConnection)
            {
                myConnection.Open();
                string query = "SELECT * FROM [production].[stocks]";

                using (SqlCommand command = new SqlCommand(query, myConnection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            stocks.Add(new Stock(reader.GetInt32(0), reader.GetInt32(1), reader.GetInt32(2)));
                        }
                    }
                }
            }
            return stocks;
        }

        public List<Customer> GetAllCustomers()
        {
            var customers = new List<Customer>();
            using (myConnection)
            {
                myConnection.Open();
                string query = "SELECT * FROM [sales].[customers]";

                using (SqlCommand command = new SqlCommand(query, myConnection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            customers.Add(new Customer(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5), reader.GetString(6), reader.GetString(7), reader.GetString(8)));
                        }
                    }
                }
            }
            return customers;
        }

        public List<OrderItem> GetAllOrderItems()
        {
            var order_items = new List<OrderItem>();
            using (myConnection)
            {
                myConnection.Open();
                string query = "SELECT * FROM [sales].[order_items]";

                using (SqlCommand command = new SqlCommand(query, myConnection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            order_items.Add(new OrderItem(reader.GetInt32(0), reader.GetInt32(1), reader.GetInt32(2), reader.GetInt32(3), reader.GetDecimal(4), reader.GetDecimal(5)));
                        }
                    }
                }
            }
            return order_items;
        }

        public List<Order> GetAllOrders()
        {
            var orders = new List<Order>();
            using (myConnection)
            {
                myConnection.Open();
                string query = "SELECT * FROM [sales].[orders]";

                using (SqlCommand command = new SqlCommand(query, myConnection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            orders.Add(new Order(reader.GetInt32(0), reader.GetInt32(1), reader.GetByte(2), reader.GetDateTime(3), reader.GetDateTime(4), reader.GetDateTime(5), reader.GetInt32(6), reader.GetInt32(7)));
                        }
                    }
                }
            }
            return orders;
        }

        public List<Staff> GetAllStaff()
        {
            var staff = new List<Staff>();
            using (myConnection)
            {
                myConnection.Open();
                string query = "SELECT * FROM [sales].[staff]";

                using (SqlCommand command = new SqlCommand(query, myConnection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            staff.Add(new Staff(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetByte(5), reader.GetInt32(6), reader.GetInt32(7)));
                        }
                    }
                }
            }
            return staff;
        }

        public List<Store> GetAllStores()
        {
            var stores = new List<Store>();
            using (myConnection)
            {
                myConnection.Open();
                string query = "SELECT * FROM [sales].[stores]";

                using (SqlCommand command = new SqlCommand(query, myConnection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            stores.Add(new Store(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5), reader.GetString(6), reader.GetString(7)));
                        }
                    }
                }
            }
            return stores;
        }

    }
}