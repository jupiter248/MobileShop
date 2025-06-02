using MainApi.Persistence.Data;
using MainApi.Domain.Models.User;
using MainApi.Persistence.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
// using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using MainApi.Infrastructure.Services.Generators;
using MainApi.Application.Interfaces.Services;
using MainApi.Infrastructure.Services.External;
using MainApi.Application.Interfaces.Repositories;
using MainApi.Infrastructure.Services.Internal;
using Microsoft.IdentityModel.Tokens;

namespace MainApi.Infrastructure.Services.Internal
{
    public static class ConfigureService
    {

        public static void AddSwaggerDocumentation(this IServiceCollection services)
        {
            services.AddSwaggerGen(option =>
            {

                option.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo API", Version = "v1" });
                option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter a valid token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });
                option.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type=ReferenceType.SecurityScheme,
                                Id="Bearer"
                            }
                        },
                        new string[]{}
                    }
                });
            });
        }
        public static void AddIdentityService(this IServiceCollection services)
        {
            services.AddIdentity<AppUser, IdentityRole>(option =>
            {
                option.Password.RequireDigit = true;
                option.Password.RequireNonAlphanumeric = true;
                option.Password.RequireUppercase = true;
                option.Password.RequiredLength = 8;
                option.Password.RequiredUniqueChars = 0;
            }).AddEntityFrameworkStores<ApplicationDbContext>()
             .AddDefaultTokenProviders();
        }
        public static void AddJwtAuthentication(this IServiceCollection services, IConfiguration config)
        {
            // var jwtSettings = config.GetSection("JWT");
            string signingKeyString = Environment.GetEnvironmentVariable("JWT_SigningKey") ?? string.Empty;
            if (string.IsNullOrWhiteSpace(signingKeyString))
                throw new InvalidOperationException("JWT key is not configured properly.");

            var signingKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(signingKeyString));

            services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme =
                option.DefaultScheme =
                option.DefaultChallengeScheme =
                option.DefaultForbidScheme =
                option.DefaultSignInScheme =
                option.DefaultSignOutScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(option =>
            {
                option.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = true,
                    ValidateAudience = true,
                    ValidateIssuer = true,
                    ValidateLifetime = true,
                    ValidAudience = Environment.GetEnvironmentVariable("JWT_Audience"),
                    ValidIssuer = Environment.GetEnvironmentVariable("JWT_Issuer"),
                    IssuerSigningKey = signingKey,
                };
            });
        }
        public static void AddCustomServices(this IServiceCollection services)
        {
            //Services
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IXmlService, XmlService>();
            services.AddScoped<ISKUService, SKUService>();
            services.AddScoped<IPaymentService, PaymentService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IAddressService, AddressService>();
            services.AddScoped<ICartItemService, CartItemService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ICommentService, CommentService>();
            services.AddScoped<IImageService, ImageService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IOrderShipmentService, OrderShipmentService>();
            services.AddScoped<IProductAttributeService, ProductAttributeService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ISpecificationAttributesService, SpecificationAttributesService>();
            services.AddScoped<IWishListService, WishListService>();










            //Repositories
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IImageRepository, ImageRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<ICommentRepository, CommentRepository>();
            services.AddScoped<IAddressRepository, AddressRepository>();
            services.AddScoped<IWishListRepository, WishListRepository>();
            services.AddScoped<ISpecificationAttributesRepository, SpecificationAttributesRepository>();
            services.AddScoped<IProductAttributeRepository, ProductAttributeRepository>();
            services.AddScoped<ICartItemRepository, CartItemRepository>();
            services.AddScoped<IOrderShipmentRepository, OrderShipmentRepository>();
            services.AddScoped<IPaymentRepository, PaymentRepository>();
        }

    }
}