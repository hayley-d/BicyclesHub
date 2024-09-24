using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BicyclesHub.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public Category(int id, string name)
        {
            Id = id;
            Name = name;
            assignImageUrl();
        }

        private void assignImageUrl()
        {
            if (this.Name == "Children Bicycles") {
                this.ImageUrl = "https://finishlinecycles.co.za/cdn/shop/files/FinishLineCylcesAvalancheBikesStorm_14_pink_1.webp?v=1707418806&width=533";
            } else if (this.Name == "Comfort Bicycles")
            {
                this.ImageUrl = "https://cdn.shopify.com/s/files/1/0611/8037/9293/files/700cMETROH1_Black1_1200x.jpg?v=1714050867";
            }
            else if (this.Name == "Cruisers Bicycles")
            {
                this.ImageUrl = "https://publicbikes.com/cdn/shop/products/PUBLIC-Cruiser-ST-Single-Speed_Sea-Blue_01_1800x1200_2fe9502a-9ca4-433b-bad5-dc198b55c1e4.jpg?v=1673306153";
            } else if(this.Name == "Cyclocross Bicycles")
            {
                this.ImageUrl = "https://dolan-images.s3.eu-west-2.amazonaws.com/block/g_968/Sporco-Matt-Electric-Blue-Bike-1.jpg";
            } else if(this.Name == "Electric Bikes")
            {
                this.ImageUrl = "https://cdn.shopify.com/s/files/1/0614/8826/7429/files/mte-1_LR.jpg?v=1724831830";
            } else if (this.Name == "Mountain Bikes")
            {
                this.ImageUrl = "https://solomonscycles.co.za/wp-content/uploads/2024/07/Apex-A400L-24-Lightweight-Steel-Frame-Junior-Mountain-Bike-2024-1.webp";
            }
            else
            {
                this.ImageUrl = "https://dvillecyclery.co.za/wp-content/uploads/2022/07/YTB46I55MQ_6.jpg";
            }
        }
    }
}