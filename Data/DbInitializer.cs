using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MainApi.Models.Products;
using MainApi.Models.Products.ProductAttributes;
using MainApi.Models.Products.SpecificationAttributes;
using MainApi.Models.User;
using Microsoft.AspNetCore.Identity;

namespace MainApi.Data
{
    public static class DbInitializer
    {
        public static void UserInitializer(ApplicationDbContext _context, UserManager<AppUser> _userManager)
        {
            // Ensure the database is created
            _context.Database.EnsureCreated();

            // Check if the database has been seeded
            if (_context.Roles.Any() || _context.Users.Any() || _context.Addresses.Any())
            {
                Console.WriteLine("Database already seeded.");
                return;
            }
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                // Add roles
                List<IdentityRole> roles = new List<IdentityRole>
                {
                    new IdentityRole
                    {
                        Name = "Admin",
                        NormalizedName = "ADMIN"
                    },
                    new IdentityRole
                    {
                        Name = "User",
                        NormalizedName = "USER"
                    }
                };
                _context.Roles.AddRangeAsync(roles);
                _context.SaveChangesAsync();

                //Add a user and an admin
                AppUser appUser = new AppUser()
                {
                    UserName = "Admin",
                    Email = "mmazimifar7@gmail.com"
                };
                _userManager.CreateAsync(appUser, "@Admin248" ?? string.Empty);
                _userManager.AddToRoleAsync(appUser, "Admin");

                AppUser appUser1 = new AppUser()
                {
                    UserName = "User",
                    Email = "User@gmail.com",
                };
                _userManager.CreateAsync(appUser, "@User248" ?? string.Empty);
                _userManager.AddToRoleAsync(appUser, "User");

                // Address
                Address? address = new Address()
                {
                    appUser = appUser1,
                    UserId = appUser.Id,
                    Country = "Iran",
                    City = "Mashhad",
                    State = "Tabadkan",
                    Street = "Fadak 17",
                    Plate = "22",
                    PostalCode = "123456987547",
                };
                _context.Addresses.Add(address);
                _context.SaveChangesAsync();

                transaction.Commit();
                Console.WriteLine("Database has been seeded successfully.");
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                System.Console.WriteLine($"Error during seeding: {ex.Message}");
            }
        }
        public static void ProductInitializer(ApplicationDbContext _context)
        {
            // Ensure the database is created
            _context.Database.EnsureCreated();

            // Check if the database has been seeded
            if (_context.Products.Any() || _context.Categories.Any() || _context.PredefinedProductAttributeValues.Any() || _context.ProductAttributes.Any())
            {
                Console.WriteLine("Database already seeded.");
                return;
            }
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                //Add Category
                List<Category> categories = new List<Category>
                {
                    new Category
                    {
                        CategoryName = "Mobiles",
                        Description = "TestTestTestTestTestTest"
                    },
                    new Category
                    {
                        CategoryName = "Chargers",
                        Description = "TestTestTestTestTest"
                    }
                };
                _context.Categories.AddRange(categories);
                _context.SaveChangesAsync();

                //Add Product Attribute
                List<ProductAttribute> productAttributes = new List<ProductAttribute>
                {
                    new ProductAttribute
                    {
                        Name = "RAM",
                        Description = "The RAM of device"
                    },
                    new ProductAttribute
                    {
                        Name = "Storage",
                        Description = "The storage of device"
                    }
                };
                _context.ProductAttributes.AddRange(productAttributes);
                _context.SaveChangesAsync();

                List<PredefinedProductAttributeValue> predefinedProductAttributeValues = new List<PredefinedProductAttributeValue>
                {
                    new PredefinedProductAttributeValue
                    {
                        Name = "16GB",
                        ProductAttribute = productAttributes.FirstOrDefault(a => a.Name == "RAM"),
                        ProductAttributeId = productAttributes.Where(a => a.Name == "RAM")
                                             .Select(a => a.Id).FirstOrDefault()
                    },
                    new PredefinedProductAttributeValue
                    {
                        Name = "128GB",
                        ProductAttribute = productAttributes.FirstOrDefault(a => a.Name == "Storage"),
                        ProductAttributeId = productAttributes.Where(a => a.Name == "Storage")
                                             .Select(a => a.Id).FirstOrDefault()
                    }
                };
                _context.PredefinedProductAttributeValues.AddRange(predefinedProductAttributeValues);
                _context.SaveChangesAsync();


                //Add Specification Attribute
                List<SpecificationAttribute> specificationAttributes = new List<SpecificationAttribute>
                {
                    new SpecificationAttribute
                    {
                        Name = "Size",
                    },
                    new SpecificationAttribute
                    {
                        Name = "Processor",
                    },
                    new SpecificationAttribute
                    {
                        Name = "Battery"
                    }
                };
                _context.SpecificationAttributes.AddRange(specificationAttributes);
                _context.SaveChangesAsync();

                List<SpecificationAttributeOption> specificationAttributeOptions = new List<SpecificationAttributeOption>
                {
                    new SpecificationAttributeOption
                    {
                        Name = "6.1Inc",
                        SpecificationAttribute = specificationAttributes.FirstOrDefault(c => c.Name == "Size"),
                        SpecificationAttributeId = specificationAttributes.Where(item => item.Name == "Size")
                        .Select(item => (int)item.Id)
                        .FirstOrDefault()
                    },
                    new SpecificationAttributeOption
                    {
                        Name = "SnapDragon",
                        SpecificationAttribute = specificationAttributes.FirstOrDefault(c => c.Name == "Processor"),
                        SpecificationAttributeId = specificationAttributes.Where(item => item.Name == "Processor")
                        .Select(item => (int)item.Id)
                        .FirstOrDefault()
                    },
                    new SpecificationAttributeOption
                    {
                        Name = "5000MA",
                        SpecificationAttribute = specificationAttributes.FirstOrDefault(c => c.Name == "Battery"),
                        SpecificationAttributeId = specificationAttributes.Where(item => item.Name == "Battery")
                        .Select(item => (int)item.Id)
                        .FirstOrDefault()
                    },
                };
                _context.SpecificationAttributeOptions.AddRange(specificationAttributeOptions);
                _context.SaveChangesAsync();


                //Add Product
                List<Product> products = new List<Product>
                {
                    new Product
                    {
                        Brand = "Samsung",
                        ProductName = "Samsung A15",
                        Model = "A15",
                        Quantity = 5,
                        Description = "Test Test Test Test Test",
                        Price = 125000000,
                        Category = categories.FirstOrDefault(c => c.CategoryName == "Mobiles"),
                        CategoryId = categories.Where(item => item.CategoryName == "Mobiles")
                        .Select(item => (int)item.Id)
                        .FirstOrDefault()
                    },
                    new Product
                    {
                        Brand = "Samsung",
                        ProductName = "Samsung A10s",
                        Model = "A10s",
                        Quantity = 2,
                        Description = "Test Test Test Test Test",
                        Price = 50000000,
                        Category = categories.FirstOrDefault(c => c.CategoryName == "Mobiles"),
                        CategoryId = categories.Where(item => item.CategoryName == "Mobiles")
                        .Select(item => (int)item.Id)
                        .FirstOrDefault()
                    }
                };


                transaction.Commit();
                Console.WriteLine("Database has been seeded successfully.");
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                System.Console.WriteLine($"Error during seeding: {ex.Message}");
            }
        }

    }
}