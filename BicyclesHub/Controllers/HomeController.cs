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

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}