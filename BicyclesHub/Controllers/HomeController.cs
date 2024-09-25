using BicyclesHub.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BicyclesHub.Controllers
{
    public class HomeController : Controller
    {
        SqlConnection myConnection = new SqlConnection(Globals.ConnectionString);
        
        private BikeStoreViewModel bike_store = new BikeStoreViewModel();

        public ActionResult Index()
        {
            return View(bike_store);
        }

        public ActionResult Register()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Login()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Product(int id)
        {
            var product = bike_store.GetProductById(id);

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
                        return View("Index");
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
                        return View("Index");
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
                        return View("Index");
                    }
                }
            }
            return RedirectToAction("Login");
        }
    }
}