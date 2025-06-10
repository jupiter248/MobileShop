
using MainApi.Application.Interfaces;
using MainApi.Application.Interfaces.Services;
using MainApi.Domain.Models.Orders;
using MainApi.Domain.Models.Payments;
using MainApi.Domain.Models.Products;
using MainApi.Domain.Models.Products.ProductAttributes;
using MainApi.Domain.Models.Products.SpecificationAttributes;
using MainApi.Domain.Models.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace MainApi.Persistence.Data
{
    public static class DbInitializer
    {
        public static async Task UserInitializerAsync(ApplicationDbContext _context, UserManager<AppUser> _userManager, RoleManager<IdentityRole> _roleManager)
        {

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
                var roles = new[] { "Admin", "User" };
                foreach (var role in roles)
                {
                    if (!await _roleManager.RoleExistsAsync(role))
                    {
                        await _roleManager.CreateAsync(new IdentityRole(role));
                    }
                }
                //Add a user and an admin
                AppUser appUser = new AppUser()
                {
                    UserName = "Admin",
                    Email = "mmazimifar7@gmail.com"
                };
                await _userManager.CreateAsync(appUser, "@Admin248" ?? string.Empty);
                await _userManager.AddToRoleAsync(appUser, "Admin");

                AppUser appUser1 = new AppUser()
                {
                    UserName = "User",
                    Email = "User@gmail.com",
                };
                await _userManager.CreateAsync(appUser1, "@User248" ?? string.Empty);
                await _userManager.AddToRoleAsync(appUser1, "User");

                // Address
                Address? address = new Address()
                {
                    appUser = appUser1,
                    UserId = appUser1.Id,
                    Country = "Iran",
                    City = "Mashhad",
                    State = "Tabadkan",
                    Street = "Fadak 17",
                    Plate = "22",
                    PostalCode = "123456987547",
                };
                await _context.Addresses.AddAsync(address);


                await _context.SaveChangesAsync();
                transaction.Commit();
                Console.WriteLine("Database has been seeded successfully.");
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                System.Console.WriteLine($"Error during seeding: {ex.Message}");
            }
        }
        public static async Task ProductInitializerAsync(ApplicationDbContext _context, ISKUService sKUService)
        {

            // Check if the database has been seeded
            if (_context.Products.Any() || _context.Categories.Any() || _context.PredefinedProductAttributeValues.Any() || _context.ProductAttributes.Any()
            || _context.ProductCombinations.Any() || _context.ProductCombinationsAttribute.Any()
            || _context.SpecificationAttributes.Any() || _context.SpecificationAttributeOptions.Any() || _context.SpecificationAttributeMappings.Any()
            )
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
                        CategoryName = "موبایل",
                        Description = "TestTestTestTestTestTest"
                    },
                    new Category
                    {
                        CategoryName = "شارژر",
                        Description = "TestTestTestTestTest"
                    }
                };
                await _context.Categories.AddRangeAsync(categories);

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
                    },
                    new ProductAttribute
                    {
                        Name = "Color",
                        Description = "The storage of device"
                    }
                };
                await _context.ProductAttributes.AddRangeAsync(productAttributes);

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
                        Name = "8GB",
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
                    },
                    new PredefinedProductAttributeValue
                    {
                        Name = "64GB",
                        ProductAttribute = productAttributes.FirstOrDefault(a => a.Name == "Storage"),
                        ProductAttributeId = productAttributes.Where(a => a.Name == "Storage")
                                             .Select(a => a.Id).FirstOrDefault()
                    },
                    new PredefinedProductAttributeValue
                    {
                        Name = "Black",
                        ProductAttribute = productAttributes.FirstOrDefault(a => a.Name == "Color"),
                        ProductAttributeId = productAttributes.Where(a => a.Name == "Color")
                                             .Select(a => a.Id).FirstOrDefault()
                    },
                    new PredefinedProductAttributeValue
                    {
                        Name = "Blue",
                        ProductAttribute = productAttributes.FirstOrDefault(a => a.Name == "Color"),
                        ProductAttributeId = productAttributes.Where(a => a.Name == "Color")
                                             .Select(a => a.Id).FirstOrDefault()
                    }
                };
                await _context.PredefinedProductAttributeValues.AddRangeAsync(predefinedProductAttributeValues);


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
                await _context.SpecificationAttributes.AddRangeAsync(specificationAttributes);

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
                await _context.SpecificationAttributeOptions.AddRangeAsync(specificationAttributeOptions);


                //Add Product
                List<Product> products = new List<Product>
                {
                    new Product
                    {
                        Brand = "Samsung",
                        ProductName = "Samsung A15",
                        Model = "2023",
                        Quantity = 5,
                        Description = "Test Test Test Test Test",
                        Price = 125000000,
                        Category = categories.FirstOrDefault(c => c.CategoryName == "موبایل"),
                        CategoryId = categories.Where(item => item.CategoryName == "موبایل")
                        .Select(item => (int)item.Id)
                        .FirstOrDefault()
                    },
                    new Product
                    {
                        Brand = "Samsung",
                        ProductName = "Samsung A10s",
                        Model = "2019",
                        Quantity = 2,
                        Description = "Test Test Test Test Test",
                        Price = 50000000,
                        Category = categories.FirstOrDefault(c => c.CategoryName == "موبایل"),
                        CategoryId = categories.Where(item => item.CategoryName == "موبایل")
                        .Select(item => (int)item.Id)
                        .FirstOrDefault()
                    }
                };
                await _context.Products.AddRangeAsync(products);

                //Add Product Attribute Mapping
                List<ProductAttributeMapping> product_ProductAttribute_Mappings = new List<ProductAttributeMapping>
                {
                    new ProductAttributeMapping
                    {
                        Product = products[0],
                        ProductId = products[0].Id,
                        ProductAttribute = productAttributes.FirstOrDefault(a => a.Name == "RAM"),
                        ProductAttributeId = productAttributes.FirstOrDefault(a => a.Name == "RAM").Id
                    },
                    new ProductAttributeMapping
                    {
                        Product = products[0],
                        ProductId = products[0].Id,
                        ProductAttribute = productAttributes.FirstOrDefault(a => a.Name == "Storage"),
                        ProductAttributeId = productAttributes.FirstOrDefault(a => a.Name == "Storage").Id
                    },
                    new ProductAttributeMapping
                    {
                        Product = products[0],
                        ProductId = products[0].Id,
                        ProductAttribute = productAttributes.FirstOrDefault(a => a.Name == "Color"),
                        ProductAttributeId = productAttributes.FirstOrDefault(a => a.Name == "Color").Id
                    }
                };
                await _context.ProductAttributeMappings.AddRangeAsync(product_ProductAttribute_Mappings);
                //Add Product Combination Attribute
                List<ProductCombination> productCombinations = new List<ProductCombination>
                {
                    new ProductCombination
                    {
                        FinalPrice = 130000000,
                        Product = products[0],
                        ProductId = products[0].Id,
                        Quantity = 5,
                        CombinationAttributes =  new List<ProductCombinationAttribute>
                        {
                            new ProductCombinationAttribute
                            {
                                AttributeValueId = predefinedProductAttributeValues[0].Id,
                                AttributeValue = predefinedProductAttributeValues[0],
                            },
                            new ProductCombinationAttribute
                            {
                                AttributeValueId = predefinedProductAttributeValues[4].Id,
                                AttributeValue = predefinedProductAttributeValues[4],
                            }
                        },
                        Sku = sKUService.GenerateSKU(products[0].ProductName , new List<string>
                        {
                            predefinedProductAttributeValues[0].Name,
                            predefinedProductAttributeValues[4].Name
                        }),
                    },
                    new ProductCombination
                    {
                        FinalPrice = 130000000,
                        Product = products[0],
                        ProductId = products[0].Id,
                        Quantity = 5,
                        CombinationAttributes =  new List<ProductCombinationAttribute>
                        {
                            new ProductCombinationAttribute
                            {
                                AttributeValueId = predefinedProductAttributeValues[1].Id,
                                AttributeValue = predefinedProductAttributeValues[1],
                            },
                            new ProductCombinationAttribute
                            {
                                AttributeValueId = predefinedProductAttributeValues[5].Id,
                                AttributeValue = predefinedProductAttributeValues[5],
                            }
                        },
                        Sku = sKUService.GenerateSKU(products[0].ProductName , new List<string>
                        {
                            predefinedProductAttributeValues[1].Name,
                            predefinedProductAttributeValues[5].Name
                        }),
                    },
                    new ProductCombination
                    {
                        FinalPrice = 140000000,
                        Product = products[1],
                        ProductId = products[1].Id,
                        Quantity = 5,
                        CombinationAttributes =  new List<ProductCombinationAttribute>
                        {
                            new ProductCombinationAttribute
                            {
                                AttributeValueId = predefinedProductAttributeValues[1].Id,
                                AttributeValue = predefinedProductAttributeValues[1],
                            },
                            new ProductCombinationAttribute
                            {
                                AttributeValueId = predefinedProductAttributeValues[5].Id,
                                AttributeValue = predefinedProductAttributeValues[5],
                            }
                        },
                        Sku = sKUService.GenerateSKU(products[1].ProductName , new List<string>
                        {
                            predefinedProductAttributeValues[1].Name,
                            predefinedProductAttributeValues[5].Name
                        }),
                    },
                    new ProductCombination
                    {
                        FinalPrice = 150000000,
                        Product = products[1],
                        ProductId = products[1].Id,
                        Quantity = 4,
                        CombinationAttributes =  new List<ProductCombinationAttribute>
                        {
                            new ProductCombinationAttribute
                            {
                                AttributeValueId = predefinedProductAttributeValues[2].Id,
                                AttributeValue = predefinedProductAttributeValues[2],
                            },
                            new ProductCombinationAttribute
                            {
                                AttributeValueId = predefinedProductAttributeValues[5].Id,
                                AttributeValue = predefinedProductAttributeValues[5],
                            }
                        },
                        Sku = sKUService.GenerateSKU(products[1].ProductName , new List<string>
                        {
                            predefinedProductAttributeValues[2].Name,
                            predefinedProductAttributeValues[5].Name
                        }),
                    }
                };
                await _context.ProductCombinations.AddRangeAsync(productCombinations);


                //Add  Product_SpecificationAttribute_Mapping

                List<Product_SpecificationAttribute_Mapping> product_Specifications = new List<Product_SpecificationAttribute_Mapping>
                {
                    new Product_SpecificationAttribute_Mapping
                    {
                        Product = products[0],
                        ProductId = products[0].Id,
                        AllowFiltering = true,
                        ShowOnProductPage = true,
                        SpecificationAttributeOption = specificationAttributeOptions.FirstOrDefault(o => o.Name == "6.1Inc"),
                        SpecificationAttributeOptionId = specificationAttributeOptions.FirstOrDefault(o => o.Name == "6.1Inc").Id,
                    },
                    new Product_SpecificationAttribute_Mapping
                    {
                        Product = products[0],
                        ProductId = products[0].Id,
                        AllowFiltering = true,
                        ShowOnProductPage = true,
                        SpecificationAttributeOption = specificationAttributeOptions.FirstOrDefault(o => o.Name == "5000MA"),
                        SpecificationAttributeOptionId = specificationAttributeOptions.FirstOrDefault(o => o.Name == "5000MA").Id,
                    },
                    new Product_SpecificationAttribute_Mapping
                    {
                        Product = products[0],
                        ProductId = products[0].Id,
                        AllowFiltering = true,
                        ShowOnProductPage = true,
                        SpecificationAttributeOption = specificationAttributeOptions.FirstOrDefault(o => o.Name == "SnapDragon"),
                        SpecificationAttributeOptionId = specificationAttributeOptions.FirstOrDefault(o => o.Name == "SnapDragon").Id,
                    }
                };
                await _context.SpecificationAttributeMappings.AddRangeAsync(product_Specifications);

                await _context.SaveChangesAsync();
                transaction.Commit();
                Console.WriteLine("Database has been seeded successfully.");
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                System.Console.WriteLine($"Error during seeding: {ex.Message}");
            }
        }
        public static async Task StatusInitializerAsync(ApplicationDbContext _context)
        {
            // Ensure the database is created

            // Check if the database has been seeded
            if (_context.ShippingStatuses.Any() || _context.OrderStatuses.Any() || _context.PaymentStatuses.Any())
            {
                Console.WriteLine("Database already seeded.");
                return;
            }
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                List<OrderStatus> orderStatuses = new List<OrderStatus>
                {
                    new OrderStatus
                    {
                        StatusName = "Pending",
                        Description = "Order placed but not yet processed"
                    },
                    new OrderStatus
                    {
                        StatusName = "Processing",
                        Description = "Order is being prepared"
                    },
                    new OrderStatus
                    {
                        StatusName = "Complete",
                        Description = "Order fully processed and shipped"
                    },
                    new OrderStatus
                    {
                        StatusName = "Cancelled	",
                        Description = "Order was cancelled"
                    },
                    new OrderStatus
                    {
                        StatusName = "OnHold",
                        Description = "Order is temporarily on hold"
                    },
                    new OrderStatus
                    {
                        StatusName = "Failed",
                        Description = "Order failed due to some error"
                    },
                    new OrderStatus
                    {
                        StatusName = "Refunded",
                        Description = "Customer was refunded"
                    }
                };
                await _context.OrderStatuses.AddRangeAsync(orderStatuses);

                List<PaymentStatus> paymentStatuses = new List<PaymentStatus>
                {
                    new PaymentStatus
                    {
                        Name = "Pending",
                        Description = "Payment has not been completed yet"
                    },
                    new PaymentStatus
                    {
                        Name = "Authorized",
                        Description = "Payment authorized but not captured"
                    },
                    new PaymentStatus
                    {
                        Name = "Paid",
                        Description = "Payment was successfully completed"
                    },
                    new PaymentStatus
                    {
                        Name = "PartiallyRefunded",
                        Description = "Part of the amount was refunded"
                    },
                    new PaymentStatus
                    {
                        Name = "Refunded",
                        Description = "Full amount was refunded"
                    },
                    new PaymentStatus
                    {
                        Name = "Voided",
                        Description = "Payment was voided before completion"
                    },
                    new PaymentStatus
                    {
                        Name = "Failed",
                        Description = "Payment attempt failed"
                    }
                };
                await _context.PaymentStatuses.AddRangeAsync(paymentStatuses);

                List<ShippingStatus> shippingStatuses = new List<ShippingStatus>
                {
                    new ShippingStatus
                    {
                        Name = "NotShipped",
                        Description = "Item not yet shipped"
                    },
                    new ShippingStatus
                    {
                        Name = "PartiallyShipped",
                        Description = "Some items shipped, others not"
                    },
                    new ShippingStatus
                    {
                        Name = "Shipped",
                        Description = "Item has been shipped"
                    },
                    new ShippingStatus
                    {
                        Name = "Delivered",
                        Description = "Item delivered to customer"
                    },
                    new ShippingStatus
                    {
                        Name = "Returned",
                        Description = "Item returned by customer"
                    },
                    new ShippingStatus
                    {
                        Name = "Cancelled",
                        Description = "Shipment cancelled"
                    }
                };
                await _context.ShippingStatuses.AddRangeAsync(shippingStatuses);

                await _context.SaveChangesAsync();
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