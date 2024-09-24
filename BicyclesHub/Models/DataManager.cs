using BicyclesHub.Controllers;
using System;
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



    }
}