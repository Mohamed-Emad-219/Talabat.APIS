using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Repositroy.Data
{
    public class StoreContextSeed
    {
        public static async Task SeedAsync(StoreContext context)
        {
                     #region brands
            var BrandsData = File.ReadAllText("../Talabat.Repositroy\\Data\\DataSeeding\\brands.json");
            var Brands = JsonSerializer.Deserialize<List<ProductBrand>>(BrandsData);
            if (Brands.Count > 0)
            {
                Brands = Brands.Select(b => new ProductBrand()
                {
                    Name = b.Name
                }).ToList();
                if (!(context.ProductBrands.Count() > 0))
                {
                    foreach (var brand in Brands)
                    {
                        context.Set<ProductBrand>().Add(brand);
                    }
                    await context.SaveChangesAsync();
                }
                #endregion

                     #region category
                var CategoryData = File.ReadAllText("../Talabat.Repositroy\\Data\\DataSeeding\\categories.json");
                var categories = JsonSerializer.Deserialize<List<ProductCategory>>(CategoryData);
                if (categories.Count > 0)
                {
                    categories = categories.Select(c => new ProductCategory()
                    {
                        Name = c.Name
                    }).ToList();
                    if (!(context.ProductCategories.Count() > 0))
                    {
                        foreach (var category in categories)
                        {
                            context.Set<ProductCategory>().Add(category);
                        }
                        await context.SaveChangesAsync();
                    }
                }
            }
                    #endregion  
             

            //*********************************//
            var ProductData = File.ReadAllText("../Talabat.Repositroy\\Data\\DataSeeding\\products.json");
            var products = JsonSerializer.Deserialize<List<Product>>(ProductData);
            if (products.Count > 0)
            {
                //products = products.Select(p => new Product()
                //{
                //    Name = p.Name,
                //    Description = p.Description,
                //    PictureUrl = p.PictureUrl,
                //    Price = p.Price,
                //    Brand = p.Brand,
                //    BrandId = p.BrandId,
                //   Category = p.Category,
                //    CategoryId = p.CategoryId
                //}).ToList();
                if ((context.Products.Count() == 0))
                {
                    foreach (var product in products)
                    {
                        context.Set<Product>().Add(product);
                    }
                    await context.SaveChangesAsync();
                }
            }
        }
    }
}