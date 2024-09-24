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
        public int SalesPerBrand { get; set; }
        public int ListingsTotal { get; set; }
        public int AverageSalesPerBrand { get; set; }
        public int TotalsPerBrand { get; set; }
        public int TotalsByStore { get; set; }

        public Summary(int newStocks, int listedForSale, int totalSold, int salesPerBrand, int listingsTotal, int averageSalesPerBrand, int totalsPerBrand, int totalsByStore)
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