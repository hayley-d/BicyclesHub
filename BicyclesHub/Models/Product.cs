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

        public string BrandName { get; set; }
        public string CategoryName { get; set; }
        public string ImageUrl { get; set; }


        public Product(int id, string name, int brandId,int categoryId, short modelYear, decimal listPrice)
        {
            Id = id;
            Name = name;
            BrandId = brandId;
            CategoryId = categoryId;
            ModelYear = modelYear;
            ListPrice = listPrice;
        }

        public void setBrandName(string name)
        {
            this.BrandName = name;
        }

        public void setCategoryName(string name)
        {
            this.CategoryName = name;
        }

        public void setImageUrl(string imageUrl)
        {
            this.ImageUrl = imageUrl;
        }

        public string getImageUrl()
        {
            return this.ImageUrl;
        }

        public string getBrandName() { return this.BrandName; }
        public string getCategoryName() { return this.CategoryName; }
    }
}