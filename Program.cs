
using MainApi.Services;



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

builder.Services.AddCors(opt => {
    opt.AddPolicy("HelloConnection", policy =>
    {
        policy.AllowAnyMethod()
        .AllowAnyHeader()
        .AllowAnyOrigin();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();


app.UseStaticFiles();

app.UseCors("HelloConnection");

app.MapControllers();

app.Run();
