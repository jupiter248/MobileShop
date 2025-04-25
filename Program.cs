
using MainApi.Data;
using MainApi.Interfaces;
using MainApi.Models.User;
using MainApi.Services;
using Microsoft.AspNetCore.Identity;



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
}

app.UseMiddleware<RequestTimingMiddleware>();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();


app.UseStaticFiles();

app.UseCors("HelloConnection");

app.MapControllers();

app.Run();
