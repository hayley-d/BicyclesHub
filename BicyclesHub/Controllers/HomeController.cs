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

        public ActionResult About()
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
    }
}