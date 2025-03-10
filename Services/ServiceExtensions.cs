using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using MainApi.Data;
using MainApi.Interfaces;
using MainApi.Models;
using MainApi.Models.User;
using MainApi.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace MainApi.Services
{
    public static class ServiceExtensions
    {
        public static void AddDatabase(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(config.GetConnectionString("DefaultConnection"));
            });
        }
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
            }).AddEntityFrameworkStores<ApplicationDbContext>();
        }
        public static void AddJwtAuthentication(this IServiceCollection services, IConfiguration config)
        {
            var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
            var logger = loggerFactory.CreateLogger<Program>();
            var jwtSettings = config.GetSection("JWT");
            string signingKeyString = jwtSettings["SigningKey"];
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
                    ValidAudience = jwtSettings["Audience"],
                    ValidIssuer = jwtSettings["Issuer"],
                    IssuerSigningKey = signingKey,

                };
            });
        }
        public static void AddCustomServices(this IServiceCollection services)
        {
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IImageRepository, ImageRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<ICommentRepository, CommentRepository>();
        }
    }
}