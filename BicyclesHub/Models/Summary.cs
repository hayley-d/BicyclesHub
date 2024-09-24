using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BicyclesHub.Models
{
    public class Summary
    {
        public int NewStocks { get; set; }
        public int ListedForSale { get; set; }
        public int TotalSold { get; set; }
        public Dictionary<string,int> SalesPerBrand { get; set; }
        public Dictionary<string, int> ListingsTotal { get; set; }
        public Dictionary<string, decimal> AverageSalesPerBrand { get; set; }
        public Dictionary<string, Dictionary<string, int>> TotalsPerBrand { get; set; }
        public Dictionary<string, int> TotalsByStore { get; set; }

        public Summary(int newStocks, int listedForSale, int totalSold, Dictionary<string,int> salesPerBrand, Dictionary<string, int> listingsTotal, Dictionary<string, decimal> averageSalesPerBrand, Dictionary<string, Dictionary<string, int>> totalsPerBrand, Dictionary<string, int> totalsByStore)
        {
            NewStocks = newStocks;
            ListedForSale = listedForSale;
            TotalSold = totalSold;
            SalesPerBrand = salesPerBrand;
            ListingsTotal = listingsTotal;
            AverageSalesPerBrand = averageSalesPerBrand;
            TotalsPerBrand = totalsPerBrand;
            TotalsByStore = totalsByStore;
        }
    }
}