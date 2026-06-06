using DomainLayer.Contracts;
using DomainLayer.Models.IdentityModule;
using DomainLayer.Models.OrderModule;
using DomainLayer.Models.ProductModule;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using Persistence.Identity;
using System.Linq.Expressions;
using System.Text.Json;

namespace Persistence
{
    public class DataSeeding(StoreDbContext dbContext 
        , UserManager<AppUser> userManager 
        , RoleManager<IdentityRole> roleManager
        , StoreIdentityDbContext identityDbContext) : IDataSeeding
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

                if (!dbContext.Set<DeliveryMethod>().Any())
                {
                    // Read Data
                    using var DeliveryMethodDataStream =
                        File.OpenRead(@"..\Persistence\Data\DataSeed\delivery.json");

                    // Convert To C# Objects
                    var DeliveryMethods = await JsonSerializer.DeserializeAsync<List<DeliveryMethod>>(DeliveryMethodDataStream);

                    // Save To Db
                    if (DeliveryMethods is not null && DeliveryMethods.Any())
                    {
                        await dbContext.Set<DeliveryMethod>().AddRangeAsync(entities: DeliveryMethods);
                        await dbContext.SaveChangesAsync();
                    }
                }



            }
            catch (Exception ex)
            {
                // TODO: Add logging later
                Console.WriteLine(ex.Message);
            }
        }

        public async Task IdentityDataSeedAsync()
        {
            try
            {
                if (!roleManager.Roles.Any())
                {
                    await roleManager.CreateAsync(new IdentityRole("Admin"));
                    await roleManager.CreateAsync(new IdentityRole("SuperAdmin"));
                }
                if (!userManager.Users.Any())
                {
                    var User01 = new AppUser()
                    {
                        Email = "Mohamed@gmail.com",
                        DisplayName = "Mohamed Tarek",
                        PhoneNumber = "0123456789",
                        UserName = "MohamedTarek"
                    };

                    var User02 = new AppUser()
                    {
                        Email = "Moh@gmail.com",
                        DisplayName = "Moh Tak",
                        PhoneNumber = "0144456789",
                        UserName = "MohTk"
                    };

                    await userManager.CreateAsync(User01, "P@ssw0rd");
                    await userManager.CreateAsync(User02, "P@ssw0rd");

                    await userManager.AddToRoleAsync(User01, "Admin");
                    await userManager.AddToRoleAsync(User02, "SuperAdmin");

                    await identityDbContext.SaveChangesAsync();
                }
                
            }
            catch(Exception ex)
            {

            }
        }
    }
}