using BicyclesHub.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;

namespace BicyclesHub.Controllers
{
    public class HomeController : Controller
    {
        SqlConnection myConnection = new SqlConnection(Globals.ConnectionString);
        
        private static BikeStoreViewModel bike_store = new BikeStoreViewModel();

        public ActionResult Index()
        {
            return View(bike_store);
        }

        public ActionResult Sell()
        {
            // Check if the session is set if not then take them to login page
            if (Session["StoreName"] == null)
            {
                return RedirectToAction("Login");
            } 
            ViewBag.StoreName = Session["StoreName"];
            return View(bike_store);
        }

        public ActionResult Sellers()
        {
            // Check if the session is set if not then take them to login page
            if (Session["user"] == null)
            {
                return RedirectToAction("Login");
            }
            return View(bike_store);
        }

        public ActionResult Buyers()
        {
            // Check if the session is set if not then take them to login page
            if (Session["user"] == null)
            {
                return RedirectToAction("Login");
            }
            return View(bike_store);
        }

        

        public ActionResult MyBikes()
        {
            // Check if the session is set if not then take them to login page
            if (Session["user"] == null)
            {
                return RedirectToAction("Login");
            }
            return View(bike_store);
        }

        public ActionResult Register()
        {
            

            return View();
        }

        public ActionResult ViewBikes()
        {
            ViewBag.BrandNames = bike_store.Brands.Select(b => b.Name).ToList();
            ViewBag.CategoryNames = bike_store.Categories.Select(b => b.Name).ToList();

            return View(bike_store);
        }

        public ActionResult Login()
        {
            

            return View();
        }

        public ActionResult AddBike()
        {
            if (Session["StoreName"] == null)
            {
                return RedirectToAction("Login");
            }
            ViewBag.StoreName = Session["StoreName"];
            Store store = bike_store.Stores.FirstOrDefault(s => s.Name == ViewBag.StoreName);
            if(store == null)
            {
                return RedirectToAction("Login");
            }
            ViewBag.BrandNames = bike_store.Brands.Select(b => b.Name).ToList();
            ViewBag.CategoryNames = bike_store.Categories.Select(b => b.Name).ToList();
            return View(store);
        }

        public ActionResult UpdateBike(int productId)
        {
            if (Session["StoreName"] == null)
            {
                return RedirectToAction("Login");
            }
            Product product = bike_store.Products.FirstOrDefault(s => s.Id == productId);
            product.getBrandName();
            product.getCategoryName();
            product.getImageUrl();
            ViewBag.BrandNames = bike_store.Brands.Select(b => b.Name).ToList();
            ViewBag.CategoryNames = bike_store.Categories.Select(b => b.Name).ToList();
            ViewBag.Quantity = bike_store.Stocks
                .Where(s => s.ProductId == productId)
                .Select(s => s.Quantity)
                .FirstOrDefault();

            if (product == null)
            {
                return RedirectToAction("Login");
            }
            return View(product);
        }

        public ActionResult Product(int id)
        {
            var product = bike_store.GetProductById(id);
            product.getBrandName();
            product.getCategoryName();
            product.getImageUrl();

            if (product == null)
            {
                product = new Product(-1, "Not Found", -1, -1, 000, 0);
                product.setBrandName("Not Found");
                product.setCategoryName("Not Found");
                product.setImageUrl("https://pbs.twimg.com/media/C5OTOt3UEAAExIk.jpg");
            }

            return View(product);
        }

        [HttpPost]
        public ActionResult RegisterBuyerSeller(string email, string phoneNumber, string firstName, string lastName,
                      string storeName, string streetAddress, string city,
                      string country, string province, string state, string zip)
        {
            string territory = state == "default" ? province : state;
            string customerQuery = "INSERT INTO [sales].[customers] " +
                                   "([first_name], [last_name], [phone], [email], [street], [city], [state], [zip_code]) " +
                                   "VALUES (@FirstName, @LastName, @Phone, @Email, @Street, @City, @State, @ZipCode)";

            string storeQuery = "INSERT INTO [sales].[stores] " +
                                "([store_name], [phone], [email], [street], [city], [state], [zip_code]) " +
                                "VALUES (@StoreName, @Phone, @Email, @Street, @City, @State, @ZipCode)";

            using (SqlConnection myConnection = new SqlConnection(Globals.ConnectionString))
            {
                myConnection.Open();
                using (SqlCommand command = new SqlCommand(customerQuery, myConnection))
                {
                    command.Parameters.AddWithValue("@FirstName", firstName);
                    command.Parameters.AddWithValue("@LastName", lastName);
                    command.Parameters.AddWithValue("@Phone", phoneNumber);
                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("@Street", streetAddress);
                    command.Parameters.AddWithValue("@City", city);
                    command.Parameters.AddWithValue("@State", territory);
                    command.Parameters.AddWithValue("@ZipCode", zip);
                    try
                    {
                        command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        return View("Index");
                    }
                }
                using (SqlCommand command = new SqlCommand(storeQuery, myConnection))
                {
                    command.Parameters.AddWithValue("@StoreName", storeName);
                    command.Parameters.AddWithValue("@Phone", phoneNumber);
                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("@Street", streetAddress);
                    command.Parameters.AddWithValue("@City", city);
                    command.Parameters.AddWithValue("@State", state);
                    command.Parameters.AddWithValue("@ZipCode", zip);
                    try
                    {
                        command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        return RedirectToAction("Index");
                    }
                }
            }
            return RedirectToAction("Login");
        }


        [HttpPost]
        public ActionResult RegisterBuyer(string email, string phoneNumber, string firstName, string lastName,
                              string streetAddress, string city,
                              string country, string province, string state, string zip)
        {

            string territory = state == "default" ? province : state;
            string query = "INSERT INTO [sales].[customers] " +
                           "([first_name], [last_name], [phone], [email], [street], [city], [state], [zip_code]) " +
                           "VALUES (@FirstName, @LastName, @Phone, @Email, @Street, @City, @State, @ZipCode)";

            using (SqlConnection myConnection = new SqlConnection(Globals.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, myConnection))
                {
                    command.Parameters.AddWithValue("@FirstName", firstName);
                    command.Parameters.AddWithValue("@LastName", lastName);
                    command.Parameters.AddWithValue("@Phone", phoneNumber);
                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("@Street", streetAddress);
                    command.Parameters.AddWithValue("@City", city);
                    command.Parameters.AddWithValue("@State", territory);
                    command.Parameters.AddWithValue("@ZipCode", zip);

                    try
                    {
                        myConnection.Open();
                        command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        return RedirectToAction("Index");
                    }
                }
            }
            return RedirectToAction("Login");
        }

        [HttpPost]
        public ActionResult RegisterSeller(string email, string phoneNumber, string storeName, string streetAddress, string city,
                              string country, string province, string state, string zip)
        {
            string territory = state == "default" ? province : state;
            string query = "INSERT INTO [sales].[stores] " +
                       "([store_name], [phone], [email], [street], [city], [state], [zip_code]) " +
                       "VALUES (@StoreName, @Phone, @Email, @Street, @City, @State, @ZipCode)";

            using (SqlConnection myConnection = new SqlConnection(Globals.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, myConnection))
                {
                    command.Parameters.AddWithValue("@StoreName", storeName);
                    command.Parameters.AddWithValue("@Phone", phoneNumber);
                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("@Street", streetAddress);
                    command.Parameters.AddWithValue("@City", city);
                    command.Parameters.AddWithValue("@State", state);
                    command.Parameters.AddWithValue("@ZipCode", zip);
                    try
                    {
                        myConnection.Open();
                        command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        return RedirectToAction("Index");
                    }
                }
            }
            return RedirectToAction("Login");
        }

        [HttpPost]
        public ActionResult Login(string email, string phone)
        {
            using (SqlConnection myConnection = new SqlConnection(Globals.ConnectionString))
            {
                Session["IsOwner"] = false; 
                Session["IsCustomer"] = false; 

                string customerQuery = "SELECT * FROM [sales].[customers] WHERE [email] = @Email AND [phone] = @Phone";
                using (SqlCommand customerCommand = new SqlCommand(customerQuery, myConnection))
                {
                    customerCommand.Parameters.AddWithValue("@Email", email);
                    customerCommand.Parameters.AddWithValue("@Phone", phone);

                    myConnection.Open();
                    SqlDataReader customerReader = customerCommand.ExecuteReader();

                    if (customerReader.Read())
                    {
                        // Store customer session variables
                        Session["user"] = customerReader["customer_id"];
                        Session["Email"] = customerReader["email"];
                        Session["IsCustomer"] = true;
                    }
                    customerReader.Close();
                }

                // Check in stores table
                if (true)
                {
                    string storeQuery = "SELECT * FROM [sales].[stores] WHERE [email] = @Email AND [phone] = @Phone";
                    using (SqlCommand storeCommand = new SqlCommand(storeQuery, myConnection))
                    {
                        storeCommand.Parameters.AddWithValue("@Email", email);
                        storeCommand.Parameters.AddWithValue("@Phone", phone);
                        SqlDataReader storeReader = storeCommand.ExecuteReader();
                        if (storeReader.Read())
                        {
                            // Store store owner session variables
                            Session["user"] = storeReader["store_id"];
                            Session["StoreName"] = storeReader["store_name"];
                            Session["Email"] = storeReader["email"];
                            Session["IsOwner"] = true;
                        }
                    }
                }
            }
            Debug.WriteLine(Session["StoreName"] + " is the name");
            // Pass the session variables to the bike_store
            try
            {
                bike_store.setUser(Convert.ToInt32(Session["user"]), Session["Email"].ToString(), Convert.ToBoolean(Session["IsCustomer"]), Convert.ToBoolean(Session["IsOwner"]));

            } catch (Exception ex)
            {
                return RedirectToAction("Login");
            }
            return RedirectToAction("Index");
        }

        public ActionResult Logout()
        {
            Session.Clear(); // Clear all session variables
            return RedirectToAction("Login");
        }

        [HttpPost]
        public ActionResult DeleteProduct(int productId)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login");
            }
            int storeId = Convert.ToInt32(Session["user"]);
            try
            {
                using (SqlConnection myConnection = new SqlConnection(Globals.ConnectionString))
                {
                    string deleteQuery = "DELETE FROM [production].[stocks] WHERE [product_id] = @ProductId AND [store_id] = @StoreId";
                    using (SqlCommand deleteCommand = new SqlCommand(deleteQuery, myConnection))
                    {
                        deleteCommand.Parameters.AddWithValue("@ProductId", productId);
                        deleteCommand.Parameters.AddWithValue("@StoreId", storeId);
                        myConnection.Open();
                        int rowsAffected = deleteCommand.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            Debug.WriteLine(rowsAffected + " rows affected");
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            return RedirectToAction("Sell");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
            bike_store.setStocks();
            bike_store.setProducts();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult UpdateProduct(int productId)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login");
            }
            return RedirectToAction("UpdateBike", new { productId = productId });
        }

        [HttpPost]
        public ActionResult AddNewProduct(string productName, string brandName, string categoryName, short modelYear, decimal listPrice, int quantity)
        {
            int id = -1;
            string connectionString = Globals.ConnectionString;
            // Fetch the brand and category objects
            var brand = bike_store.Brands.FirstOrDefault(b => b.Name == brandName);
            var category = bike_store.Categories.FirstOrDefault(c => c.Name == categoryName);
            if (brand == null || category == null)
            {
                return RedirectToAction("AddBike");
            }
            string insertStockQuery = @"INSERT INTO [production].[stocks] ([store_id],[product_id] ,[quantity])VALUES (@StoreId, @ProductId, @Quantity);";
            string insertProductQuery = @"INSERT INTO [production].[products] ([product_name],[brand_id],[category_id],[model_year],[list_price]) VALUES(@ProductName, @BrandId, @CategoryId, @ModelYear, @ListPrice);  SELECT SCOPE_IDENTITY();";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand insertCommand = new SqlCommand(insertProductQuery, connection);
                insertCommand.Parameters.AddWithValue("@ProductName", productName);
                insertCommand.Parameters.AddWithValue("@BrandId", brand.Id);
                insertCommand.Parameters.AddWithValue("@CategoryId", category.Id);
                insertCommand.Parameters.AddWithValue("@ModelYear", modelYear);
                insertCommand.Parameters.AddWithValue("@ListPrice", listPrice);
                connection.Open();
                int productId = Convert.ToInt32(insertCommand.ExecuteScalar());
                id = productId;

                using (SqlCommand stockCommand = new SqlCommand(insertStockQuery, connection))
                {
                    stockCommand.Parameters.AddWithValue("@StoreId", Convert.ToInt32(Session["user"]));
                    stockCommand.Parameters.AddWithValue("@ProductId", productId);
                    stockCommand.Parameters.AddWithValue("@Quantity", quantity);
                    stockCommand.ExecuteNonQuery();
                }
            }
            bike_store.setStocks();
            bike_store.setProducts();
            return RedirectToAction("Product", new { id = id });
        }

        [HttpPost]
        public ActionResult UpdateBikeProduct(int productId, string productName, string brandName, string categoryName, short modelYear, decimal listPrice, int quantity)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login");
            }
            var brand = bike_store.Brands.FirstOrDefault(b => b.Name == brandName);
            var category = bike_store.Categories.FirstOrDefault(c => c.Name == categoryName);
            if (brand == null || category == null)
            {
                return RedirectToAction("UpdateBike", new { productId = productId });
            }
            string connectionString = Globals.ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string updateProductQuery = @" UPDATE [production].[products] SET [product_name] = @ProductName,[brand_id] = @BrandId, [category_id] = @CategoryId, [model_year] = @ModelYear, [list_price] = @ListPrice WHERE [product_id] = @ProductId;";

                using (SqlCommand updateCommand = new SqlCommand(updateProductQuery, connection))
                {
                    updateCommand.Parameters.AddWithValue("@ProductName", productName);
                    updateCommand.Parameters.AddWithValue("@BrandId", brand.Id);
                    updateCommand.Parameters.AddWithValue("@CategoryId", category.Id);
                    updateCommand.Parameters.AddWithValue("@ModelYear", modelYear);
                    updateCommand.Parameters.AddWithValue("@ListPrice", listPrice);
                    updateCommand.Parameters.AddWithValue("@ProductId", productId);
                    updateCommand.ExecuteNonQuery();
                }
                string updateStockQuery = @"UPDATE [production].[stocks] SET [quantity] = @Quantity WHERE [store_id] = @StoreId AND [product_id] = @ProductId;";
                using (SqlCommand stockCommand = new SqlCommand(updateStockQuery, connection))
                {
                    stockCommand.Parameters.AddWithValue("@Quantity", quantity);
                    stockCommand.Parameters.AddWithValue("@StoreId", Convert.ToInt32(Session["user"])); 
                    stockCommand.Parameters.AddWithValue("@ProductId", productId);
                    stockCommand.ExecuteNonQuery();
                }
            }
            Debug.WriteLine("Updated " + productId);
            bike_store.setStocks();
            bike_store.setProducts();
            return RedirectToAction("Product", new { id = productId });
        }

        [HttpPost]
        public ActionResult ViewSimilarBikes(int productId)
        {
            var product = bike_store.Products.FirstOrDefault(p => p.Id == productId);

            if (product != null)
            {
                var price = product.ListPrice;
                var category = product.CategoryName;
                return RedirectToAction("SimilarBikes", new { price = price, category = category });
            }
            return RedirectToAction("Sell");
        }

        public ActionResult SimilarBikes(decimal price, string category)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login");
            }
            var filteredProducts = bike_store.Products
               .Where(p => Math.Abs(p.ListPrice - price) <= 500 && p.CategoryName == category).ToList();
            return View(filteredProducts);
        }

        public ActionResult Stores(int storeId = -1)
        {
            bike_store.setStores();
            ViewBag.Stores = bike_store.Stores;
            var products = new List<Product>();
            if(storeId == -1)
            {
                ViewBag.SelectedStore = bike_store.Stores[0].Name;
                products = bike_store.GetProductsByStoreId(1);
            }
            else
            {
                ViewBag.SelectedStore = bike_store.Stores.FirstOrDefault(s => s.Id == storeId).Name;
                products = bike_store.GetProductsByStoreId(storeId);
            }
            return View(products);
        }

        [HttpPost]
        public ActionResult SortByStore(string storeId)
        {
            return RedirectToAction("Stores", new {storeId = storeId}); 
        }

        [HttpPost]
        public ActionResult FilterByPrice(decimal selectedPrice, string currency)
        {
            return RedirectToAction("Prices", new {selectedPrice = selectedPrice, currency=currency }); 
        }

        public ActionResult Prices(decimal selectedPrice = 5000, string currency = "usd")
        {
            ViewBag.Currency = currency;
            ViewBag.SelectedPrice = selectedPrice;
            if (currency == "zar")
            {
                selectedPrice = Math.Round(selectedPrice / (decimal)17.21, 2); ;
            }
            Debug.WriteLine(selectedPrice + " in " + currency);

            var products = bike_store.Products;
            var filteredProducts = products
                .Where(p => p.ListPrice <= selectedPrice)
                .OrderByDescending(p => p.ListPrice)
                .ToList();



            return View(filteredProducts);
        }


    }
}