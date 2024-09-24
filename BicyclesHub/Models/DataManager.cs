using BicyclesHub.Controllers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Web;

namespace BicyclesHub.Models
{
    public class DataManager
    {
        private SqlConnection myConnection = new SqlConnection(Globals.ConnectionString);

        public Summary GetSummary()
        {
            
            List<OrderItem> orders = GetAllOrderItems();
            List<Product> products = GetAllProducts();
            List<Brand> brands = GetAllBrands();
            List<Stock> stocks = GetAllStock();
            List<Store> stores = GetAllStores();
            List<Category> categories = GetAllCategories();

            int listed_for_sale = stocks.Sum(stock => stock.Quantity);
            int total_sold = GetAllOrders().Count;

            //int totalSoldForBrandA = sales_per_brand["BrandA"];
            Dictionary<string,int> sales_per_brand = orders
                .Join(products, oi => oi.ProductId, p => p.Id, (oi, p) => new { oi, p })
                .Join(brands, op => op.p.BrandId, b => b.Id, (op, b) => new { op.oi.Quantity, b.Name })
                .GroupBy(x => x.Name)
                .Select(g => new { BrandName = g.Key, TotalQuantitySold = g.Sum(x => x.Quantity) })
                .ToDictionary(g => g.BrandName, g => g.TotalQuantitySold);

            Dictionary<string,int> listings_per_brand = stocks
                .Join(products, stock => stock.ProductId, product => product.Id, (stock, product) => new { stock.Quantity, product.BrandId })
                .Join(brands, sp => sp.BrandId, brand => brand.Id, (sp, brand) => new { sp.Quantity, brand.Name })
                .GroupBy(x => x.Name)
                .Select(g => new { BrandName = g.Key, TotalQuantityAvailable = g.Sum(x => x.Quantity) })
                .ToDictionary(g => g.BrandName, g => g.TotalQuantityAvailable);

            Dictionary<string, decimal> average_sale_per_brand = orders
                 .Join(products, oi => oi.ProductId, p => p.Id, (oi, p) => new { oi.ListPrice, p.BrandId })
                 .Join(brands, op => op.BrandId, b => b.Id, (op, b) => new { op.ListPrice, b.Name })
                 .GroupBy(x => x.Name)
                 .Select(g => new
                 {
                     BrandName = g.Key,
                     AveragePrice = Math.Round(g.Average(x => x.ListPrice), 2) 
                 })
                 .ToDictionary(g => g.BrandName, g => g.AveragePrice);


            Dictionary<string, Dictionary<string, int>> total_per_brand_category = CountBicyclesPerCategoryForEachBrand(products, brands, categories);
            Dictionary<string, int> total_per_store = stores
                .Join(stocks, store => store.Id, stock => stock.StoreId, (store, stock) => new { store, stock })
                .GroupBy(g => g.store)
                .ToDictionary(
                    g => g.Key.Name,
                    g => g.Sum(x => x.stock.Quantity)
                );

            return new Summary(0, listed_for_sale, total_sold, sales_per_brand, listings_per_brand, average_sale_per_brand, total_per_brand_category, total_per_store);
        }

        public Dictionary<string, Dictionary<string, int>> CountBicyclesPerCategoryForEachBrand(List<Product> products, List<Brand> brands, List<Category> categories)
        {
            // Create a Map to hold the count of bicycles per category for each brand
            var result = new Dictionary<string, Dictionary<string, int>>();

            // Group the products by brand and category
            var productGroups = products
                .GroupBy(p => new { p.BrandId, p.CategoryId })
                .Select(g => new
                {
                    BrandId = g.Key.BrandId,
                    CategoryId = g.Key.CategoryId,
                    Count = g.Count()
                });

            // Iterate over the results to populate the map
            foreach (var group in productGroups)
            {
                // Get the brand name
                var brand = brands.FirstOrDefault(b => b.Id == group.BrandId)?.Name;
                // Get the category name
                var category = categories.FirstOrDefault(c => c.Id == group.CategoryId)?.Name;

                if (brand != null && category != null)
                {
                    // Check if brand is already in the result dictionary
                    if (!result.ContainsKey(brand))
                    {
                        result[brand] = new Dictionary<string, int>();
                    }

                    // Add or update the count for the specific category
                    if (!result[brand].ContainsKey(category))
                    {
                        result[brand][category] = 0;
                    }
                    result[brand][category] += group.Count;
                }
            }

            return result;
        }



        public List<Brand> GetAllBrands()
        {
            var brands = new List<Brand>();
            using (myConnection = new SqlConnection(Globals.ConnectionString))
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
            using (myConnection = new SqlConnection(Globals.ConnectionString))
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
            using (myConnection = new SqlConnection(Globals.ConnectionString))
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
            using (myConnection = new SqlConnection(Globals.ConnectionString))
            {
                myConnection.Open();
                string query = "SELECT * FROM [production].[stocks]";

                using (SqlCommand command = new SqlCommand(query, myConnection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Boolean isQuantity = reader.IsDBNull(2) ? false : true;
                            if(isQuantity)
                            {
                                stocks.Add(new Stock(reader.GetInt32(0), reader.GetInt32(1), reader.GetInt32(2)));
                            } else
                            {
                                stocks.Add(new Stock(reader.GetInt32(0), reader.GetInt32(1),0));
                            }
                            
                        }
                    }
                }
            }
            return stocks;
        }

        public List<Customer> GetAllCustomers()
        {
            var customers = new List<Customer>();
            using (myConnection = new SqlConnection(Globals.ConnectionString))
            {
                myConnection.Open();
                string query = "SELECT * FROM [sales].[customers]";

                using (SqlCommand command = new SqlCommand(query, myConnection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            
                            var number = reader.IsDBNull(3) ? "" : reader.GetString(3);
                            var street = reader.IsDBNull(5) ? "" : reader.GetString(5);
                            var city = reader.IsDBNull(6) ? "" : reader.GetString(6);
                            var state = reader.IsDBNull(7) ? "" : reader.GetString(7);
                            var zip = reader.IsDBNull(8) ? "" : reader.GetString(8);

                            customers.Add(new Customer(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), number, reader.GetString(4), street, city, state, zip));
                        }
                    }
                }
            }
            return customers;
        }

        public List<OrderItem> GetAllOrderItems()
        {
            var order_items = new List<OrderItem>();
            using (myConnection = new SqlConnection(Globals.ConnectionString))
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
            using (myConnection = new SqlConnection(Globals.ConnectionString))
            {
                myConnection.Open();
                string query = "SELECT * FROM [sales].[orders]";

                using (SqlCommand command = new SqlCommand(query, myConnection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Boolean isShipDate = reader.IsDBNull(5) ? false : true;
                            Boolean hasCustomer = reader.IsDBNull(1) ? false : true;
                            if (isShipDate && hasCustomer)
                            {
                                orders.Add(new Order(reader.GetInt32(0), reader.GetInt32(1), reader.GetByte(2), reader.GetDateTime(3), reader.GetDateTime(4), reader.GetDateTime(5), reader.GetInt32(6), reader.GetInt32(7)));
                            } else if(!isShipDate && hasCustomer)
                            {
                                orders.Add(new Order(reader.GetInt32(0), reader.GetInt32(1), reader.GetByte(2), reader.GetDateTime(3), reader.GetDateTime(4), reader.GetInt32(6), reader.GetInt32(7)));
                            } else if(isShipDate && !hasCustomer)
                            {
                                orders.Add(new Order(reader.GetInt32(0), reader.GetByte(2), reader.GetDateTime(3), reader.GetDateTime(4), reader.GetDateTime(5), reader.GetInt32(6), reader.GetInt32(7)));
                            }
                            else
                            {
                                orders.Add(new Order(reader.GetInt32(0), reader.GetByte(2), reader.GetDateTime(3), reader.GetDateTime(4), reader.GetInt32(6), reader.GetInt32(7)));
                            }
                        }
                    }
                }
            }
            return orders;
        }

        public List<Staff> GetAllStaff()
        {
            var staff = new List<Staff>();
            using (myConnection = new SqlConnection(Globals.ConnectionString))
            {
                myConnection.Open();
                string query = "SELECT * FROM [sales].[staffs]";

                using (SqlCommand command = new SqlCommand(query, myConnection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var managerId = reader.IsDBNull(7) ? (-1) : reader.GetInt32(7);
                            var phone = reader.IsDBNull(4) ? "" : reader.GetString(4);
                            staff.Add(new Staff(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), phone, reader.GetByte(5), reader.GetInt32(6), managerId));
                        }
                    }
                }
            }
            return staff;
        }

        public List<Store> GetAllStores()
        {
            var stores = new List<Store>();
            using (myConnection = new SqlConnection(Globals.ConnectionString))
            {
                myConnection.Open();
                string query = "SELECT * FROM [sales].[stores]";

                using (SqlCommand command = new SqlCommand(query, myConnection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var phone = reader.IsDBNull(2) ? "" : reader.GetString(2);
                            var email = reader.IsDBNull(3) ? "" : reader.GetString(3);
                            var street = reader.IsDBNull(4) ? "" : reader.GetString(4);
                            var city = reader.IsDBNull(5) ? "" : reader.GetString(5);
                            var state = reader.IsDBNull(6) ? "" : reader.GetString(6);
                            var zip = reader.IsDBNull(7) ? "" : reader.GetString(7);

                            stores.Add(new Store(reader.GetInt32(0), reader.GetString(1), phone, email,street,city, state, zip));
                        }
                    }
                }
            }
            return stores;
        }

    }
}