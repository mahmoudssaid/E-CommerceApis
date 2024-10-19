using Microsoft.AspNetCore.Identity;
using Persistence.Identity;
using System.Text.Json;

namespace Persistence
{
    public class DbInitializer : IDbInitializer
    {
        private readonly StoreContext _storeContext;
        private readonly StoreIdentityContext _identityContext;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DbInitializer(StoreContext storeContext,
            StoreIdentityContext identityContext,
            RoleManager<IdentityRole> roleManager,
            UserManager<User> userManager)
        {
            _storeContext = storeContext;
            _identityContext = identityContext;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task InitializeAsync()
        {
            try
            {
                // Create DataBase IF It doesn't Exist & Applying Any Pending Migrations
                if (_storeContext.Database.GetPendingMigrations().Any())
                    await _storeContext.Database.MigrateAsync();

                // Apply Data Seeding 
                if (!_storeContext.ProductTypes.Any())
                {
                    // Read Types From File  as string 
                    var typesData = await File.ReadAllTextAsync(@"..\Infrastructure\Persistence\Data\Seeding\types.json");


                    // Transform into C# Objects
                    var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);


                    //Add to DB & save Changes 
                    if (types is not null && types.Any())
                    {
                        await _storeContext.ProductTypes.AddRangeAsync(types);
                        await _storeContext.SaveChangesAsync();
                    }
                }

                if (!_storeContext.ProductBrands.Any())
                {
                    // Read Types From File  as string 
                    var brandsData = await File.ReadAllTextAsync(@"..\Infrastructure\Persistence\Data\Seeding\brands.json");


                    // Transform into C# Objects
                    var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);


                    //Add to DB & save Changes 
                    if (brands is not null && brands.Any())
                    {
                        await _storeContext.ProductBrands.AddRangeAsync(brands);
                        await _storeContext.SaveChangesAsync();
                    }
                }

                if (!_storeContext.Products.Any())
                {
                    // Read Types From File  as string 
                    var productsData = await File.ReadAllTextAsync(@"..\Infrastructure\Persistence\Data\Seeding\products.json");


                    // Transform into C# Objects
                    var products = JsonSerializer.Deserialize<List<Product>>(productsData);


                    //Add to DB & save Changes 
                    if (products is not null && products.Any())
                    {
                        await _storeContext.Products.AddRangeAsync(products);
                        await _storeContext.SaveChangesAsync();
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task InitializeIdentityAsync()
        {

            if (!_roleManager.Roles.Any())
            {
                await _roleManager.CreateAsync(new IdentityRole("Admin"));
                await _roleManager.CreateAsync(new IdentityRole("SuperAdmin"));
            }



            if (!_userManager.Users.Any())
            {
                var superAdminUser = new User
                {
                    DisplayName = "Super Admin",
                    Email = "SuperAdmin@Gmail.com",
                    UserName = "SuperAdmin",
                    PhoneNumber = "1234567890",
                };
                var adminUser = new User
                {
                    DisplayName = "Admin",
                    Email = "Admin@Gmail.com",
                    UserName = "Admin",
                    PhoneNumber = "1234567890"
                };

                await _userManager.CreateAsync(superAdminUser, "Passw0rd");
                await _userManager.CreateAsync(adminUser, "Passw0rd");


                await _userManager.AddToRoleAsync(superAdminUser, "SuperAdmin");
                await _userManager.AddToRoleAsync(adminUser, "Admin");
            }
        }
    }
}
