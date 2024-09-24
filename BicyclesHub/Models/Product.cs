using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BicyclesHub.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int BrandId { get; set; }
        public int CategoryId { get; set; }
        public short ModelYear { get; set; }
        public decimal ListPrice { get; set; }


        public Product(int id, string name, int brandId,int categoryId, short modelYear, decimal listPrice)
        {
            Id = id;
            Name = name;
            BrandId = brandId;
            CategoryId = categoryId;
            ModelYear = modelYear;
            ListPrice = listPrice;
        }
    }
}