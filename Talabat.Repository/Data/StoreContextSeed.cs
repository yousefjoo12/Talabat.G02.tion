using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.Core.Entites;
using Talabat.Core.Order_Aggregrate;

namespace Talabat.Repository.Data
{
    public static class StoreContextSeed
    {
        public async static Task SeedAsync(StoreContext _dbcontext)
        {
            var brandsData = File.ReadAllText("../Talabat.Repository/Data/DataSeeding/brands.json");
            var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);

            if (brands.Count() > 0)
            {
                if (_dbcontext.productBrands.Count() == 0)
                {
                    brands = brands.Select(b => new ProductBrand()
                    {
                        Name = b.Name,
                    }).ToList();

                    foreach (var brand in brands)
                    {
                        _dbcontext.Set<ProductBrand>().Add(brand);
                    }
                    await _dbcontext.SaveChangesAsync();
                }
            }
            //***************************************************************************************
            var categorieData = File.ReadAllText("../Talabat.Repository/Data/DataSeeding/categories.json");
            var categories = JsonSerializer.Deserialize<List<ProductCategory>>(categorieData);

            if (categories.Count() > 0)
            {
                if (_dbcontext.productCategories.Count() == 0)
                {
                    foreach (var category in categories)
                    {
                        _dbcontext.Set<ProductCategory>().Add(category);
                    }
                    await _dbcontext.SaveChangesAsync();
                }
            }
            //***************************************************************************************
            var productsData = File.ReadAllText("../Talabat.Repository/Data/DataSeeding/products.json");
            var products = JsonSerializer.Deserialize<List<Product>>(productsData);

            if (products.Count() > 0)
            {
                if (_dbcontext.products.Count() == 0)
                {
                    foreach (var product in products)
                    {
                        _dbcontext.Set<Product>().Add(product);
                    }
                    await _dbcontext.SaveChangesAsync();
                }
            }
            //***************************************************************************************
            var deliverysData = File.ReadAllText("../Talabat.Repository/Data/DataSeeding/delivery.json");
            var DeliveryMethods = JsonSerializer.Deserialize<List<DeliveryMethod>>(deliverysData);
            if (DeliveryMethods.Count() > 0)
            {
                if (_dbcontext.DeliveryMethods.Count() == 0)
                {
                    foreach (var Methods in DeliveryMethods)
                    {
                        _dbcontext.Set<DeliveryMethod>().Add(Methods);
                    }
                    await _dbcontext.SaveChangesAsync();
                }
            }
        }
    }
}
