using DomainLayer.Contracts;
using DomainLayer.Models.ProductModule;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using System.Text.Json;

namespace Persistence
{
    public class DataSeeding(StoreDbContext dbContext) : IDataSeeding
    {
        public async Task DataSeedAsync()
        {
            // Apply pending migrations
            if ((await dbContext.Database.GetPendingMigrationsAsync()).Any())
            {
                await dbContext.Database.MigrateAsync();
            }

            try
            {
                #region ProductBrands

                if (!dbContext.ProductBrands.Any())
                {
                    var productsBrandData =
                        File.OpenRead(@"..\Persistence\Data\DataSeed\brands.json");

                    var productsBrands =
                        await JsonSerializer.DeserializeAsync<List<ProductBrand>>(productsBrandData);

                    if (productsBrands is not null && productsBrands.Any())
                    {
                        await dbContext.ProductBrands.AddRangeAsync(productsBrands);

                        await dbContext.SaveChangesAsync();
                    }
                }

                #endregion

                #region ProductTypes

                if (!dbContext.ProductTypes.Any())
                {
                    var productsTypesData =
                        File.OpenRead(@"..\Persistence\Data\DataSeed\types.json");

                    var productsTypes =
                        await JsonSerializer.DeserializeAsync<List<ProductType>>(productsTypesData);

                    if (productsTypes is not null && productsTypes.Any())
                    {
                        await dbContext.ProductTypes.AddRangeAsync(productsTypes);

                        await dbContext.SaveChangesAsync();
                    }
                }

                #endregion

                #region Products

                if (!dbContext.Products.Any())
                {
                    var productsData =
                        File.OpenRead(@"..\Persistence\Data\DataSeed\products.json");

                    var products =
                        await JsonSerializer.DeserializeAsync<List<Product>>(productsData);

                    if (products is not null && products.Any())
                    {
                        await dbContext.Products.AddRangeAsync(products);

                        await dbContext.SaveChangesAsync();
                    }
                }

                #endregion
            }
            catch (Exception ex)
            {
                // TODO: Add logging later
                Console.WriteLine(ex.Message);
            }
        }
    }
}