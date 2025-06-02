
using MainApi.Persistence.Data;
using MainApi.Application.Interfaces;
using MainApi.Domain.Models.User;
using MainApi.Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using MainApi.Persistence.Services;
using MainApi.Application.Interfaces.Services;
using MainApi.Middlewares;
using Microsoft.IdentityModel.Logging;
using MainApi.Infrastructure.Services.Internal;


DotNetEnv.Env.Load(); // This loads .env into Environment variables

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerDocumentation();

builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
});

builder.Services.AddDatabase(builder.Configuration);
builder.Services.AddIdentityService();
builder.Services.AddJwtAuthentication(builder.Configuration);

builder.Services.AddCustomServices();


builder.Services.AddCors(opt =>
{
    opt.AddPolicy("HelloConnection", policy =>
    {
        policy.AllowAnyMethod()
        .AllowAnyHeader()
        .AllowAnyOrigin();
    });
});
builder.Services.AddHttpClient();



var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var dbContext = services.GetRequiredService<ApplicationDbContext>();
    var userManager = services.GetRequiredService<UserManager<AppUser>>();
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
    var sKUService = services.GetRequiredService<ISKUService>();
    await DbInitializer.UserInitializerAsync(dbContext, userManager, roleManager);
    await DbInitializer.ProductInitializerAsync(dbContext, sKUService);
    await DbInitializer.StatusInitializerAsync(dbContext);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    IdentityModelEventSource.ShowPII = true;
}

app.UseMiddleware<RequestTimingMiddleware>();
app.UseMiddleware<ErrorHandlingMiddleware>();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();


app.UseStaticFiles();

app.UseCors("HelloConnection");

app.MapControllers();

app.Run();
