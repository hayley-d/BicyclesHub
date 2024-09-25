using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BicyclesHub.Models
{
    public class BikeStoreViewModel
    {
        private DataManager dataManager = new DataManager();
        public List<Brand> Brands { get; set; }
        public List<Order> Orders { get; set; }
        public List<Product> Products { get; set; }
        public List<OrderItem> OrderItems { get; set; }
        public List<Stock> Stocks { get; set; }
        public List<Store> Stores { get; set; }
        public List<Staff> Staff { get; set; }
        public List<Category> Categories { get; set; }
        public List<Customer> Customers { get; set; }
        public Summary Summary { get; set; }

        public CurrentUser CurrentUser  { get; set; } 

        public BikeStoreViewModel()
        {
            setBrands();
            setCategory();
            setOrders();
            setOrderItems();

            setProducts();

            //set product details
            foreach (Product p in Products)
            {
                var brand = Brands.FirstOrDefault(b => b.Id == p.BrandId);
                var category = Categories.FirstOrDefault(b => b.Id == p.CategoryId);
                p.setBrandName(brand?.Name);
                p.setCategoryName(category?.Name);
                p.setImageUrl(category?.ImageUrl);
            }

            setStocks();
            setStores();
            
            setSummary();
            setStaff();
            setCustomers();

            setUser(-1, "", false, false);
        }

        public void setBrands()
        {
            Brands = dataManager.GetAllBrands();
        }

        public void setOrders()
        {
            Orders = dataManager.GetAllOrders();
        }

        public void setProducts()
        {
            Products = dataManager.GetAllProducts();
        }

        public void setOrderItems()
        {
            OrderItems = dataManager.GetAllOrderItems();
        }

        public void setStocks()
        {
            Stocks = dataManager.GetAllStock();
        }

        public void setStaff()
        {
            Staff = dataManager.GetAllStaff();
        }

        public void setStores()
        {
            Stores = dataManager.GetAllStores();
        }
        public void setCategory()
        {
            Categories = dataManager.GetAllCategories();
        }

        public void setSummary()
        {
            Summary = dataManager.GetSummary();
        }

        public void setCustomers()
        {
            Customers = dataManager.GetAllCustomers();
        }

        public Boolean isLoggedIn()
        {
            return CurrentUser.Id != -1 ? true : false;
        }

        public void setUser(int id, string email, bool isCustomer, bool isOwner)
        {
            this.CurrentUser = new CurrentUser(id, email, isCustomer, isOwner);
        }

        public Dictionary<string, Dictionary<string, (string ImageUrl, int ProductId)>> GetCategoriesPerBrand()
        {
            // Create a Map to hold the categories for each brand
            var result = new Dictionary<string, Dictionary<string, (string ImageUrl, int ProductId)>>();

            // Group the products by brand and category
            var productGroups = Products
                .GroupBy(p => new { p.BrandId, p.CategoryId })
                .Select(g => new
                {
                    BrandId = g.Key.BrandId,
                    CategoryId = g.Key.CategoryId,
                    ProductId = g.First().Id
                });

            // Iterate over the results to populate the map
            foreach (var group in productGroups)
            {
                // Get the brand name
                var brand = Brands.FirstOrDefault(b => b.Id == group.BrandId)?.Name;
                // Get the category info
                var category = Categories.FirstOrDefault(c => c.Id == group.CategoryId);

                if (brand != null && category != null)
                {
                    if (!result.ContainsKey(brand))
                    {
                        result[brand] = new Dictionary<string, (string ImageUrl,int ProductId)>();
                    }

                    // Add the category name and image URL
                    result[brand].Add(category.Name, (category.ImageUrl,group.ProductId));
                }
            }

            return result;
        }

        
        public Dictionary<string, string> GetCategoriesByBrand(string brandName)
        {
            var result = new Dictionary<string, string>();
            var categoriesPerBrand = GetCategoriesPerBrand();
            if (categoriesPerBrand.ContainsKey(brandName))
            {
                foreach (var category in categoriesPerBrand[brandName])
                {
                    result.Add(category.Key, category.Value.ImageUrl);
                }
            }
            return result;
        }

        public Product GetProductById(int id)
        {
            return Products.FirstOrDefault(p => p.Id == id);
        }

        public int GetProductFromBrand(string brandName, string categoryName)
        {
            var brand = Brands.FirstOrDefault(b => b.Name == brandName);
            var category = Categories.FirstOrDefault(b => b.Name == categoryName);

            Product product = Products.FirstOrDefault(p => p.BrandId == brand.Id && p.CategoryId == category.Id);
            return product.Id;
        }

        public Dictionary<string, (Store, List<Product>)> GetProductsInStockByStore()
        {
            var storeProductMap = new Dictionary<string, (Store, List<Product>)>();
            foreach (var store in Stores)
            {
                // Find the products in stock for the current store
                var productsInStock = (from stock in Stocks
                                       join product in Products on stock.ProductId equals product.Id
                                       where stock.StoreId == store.Id && stock.Quantity > 0
                                       select product).ToList();
                if (productsInStock.Any())
                {
                    if (!storeProductMap.ContainsKey(store.Name))
                    {
                        // Initialize the tuple with the store and products
                        storeProductMap[store.Name] = (store, new List<Product>());
                    }
                    storeProductMap[store.Name].Item2.AddRange(productsInStock);
                }
            }

            return storeProductMap;
        }

        public List<Buyer> GetBuyers()
        {
            return Orders
                .Where(order => Customers.Any(c => c.Id == order.CustomerId) &&
                                Stores.Any(s => s.Id == order.StoreId) &&
                                Staff.Any(staff => staff.Id == order.StaffId))
                .Select(order =>
                {
                    var customer = Customers.First(c => c.Id == order.CustomerId);
                    var store = Stores.First(s => s.Id == order.StoreId);
                    var staff = Staff.First(_staff => _staff.Id == order.StaffId);

                    return new Buyer(
                        customer.Id,
                        customer.FirstName,
                        customer.LastName,
                        store.Name,
                        store.Address,
                        staff,
                        order.OrderDate
                    );
                }).ToList();
        }





    }
}