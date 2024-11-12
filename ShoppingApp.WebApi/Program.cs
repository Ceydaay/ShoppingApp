using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ShoppingApp.Business.DataProtection;
using ShoppingApp.Business.Operations.Order;
using ShoppingApp.Business.Operations.Product.Dtos;
using ShoppingApp.Business.Operations.Setting;
using ShoppingApp.Business.Operations.User;
using ShoppingApp.Data.Context;
using ShoppingApp.Data.Repositories;
using ShoppingApp.Data.UnitOfWork;
using ShoppingApp.WebApi.Middlewares;
using System.Text;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{

    var jwtSecurityScheme = new OpenApiSecurityScheme
    {
        Scheme = "Bearer",
        BearerFormat = "JWT",
        Name = "Jwt Authentication",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Description = "Put **_ONLY_ your JWT Bearer Token on Texbox below!",

        Reference = new OpenApiReference     // JWT doðrulama için referans
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };

    options.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);   // Güvenlik þemasý ekleniyor
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        { jwtSecurityScheme, Array.Empty<string>() }
    });
}); 

builder.Services.AddScoped<IDataProtection, DataProtection>(); // Veritabaný için þifreleme iþlemleri yapacak sýnýfý servis olarak ekliyoruz.

var keysDirectory = new DirectoryInfo(Path.Combine(builder.Environment.ContentRootPath, "App_Data", "Keys")); // Uygulama içindeki þifreleme anahtarlarýnýn saklanacaðý dizin

builder.Services.AddDataProtection()
    .SetApplicationName("ShoppingApp")
    .PersistKeysToFileSystem(keysDirectory);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],

            ValidateAudience = true,
            ValidAudience = builder.Configuration["Jwt:Audience"],

            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"]!))
        };
    });

var connectionString = builder.Configuration.GetConnectionString("default");

builder.Services.AddDbContext<ShoppingAppDbContext>(options => options.UseSqlServer(connectionString)); // DbContext ve Entity Framework ile veritabaný baðlantýsý saðlanýyor

// Servislerin sýnýflara baðlanmasý
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IUserService, UserManager>();
builder.Services.AddScoped<IProductService, ProductManager>();
builder.Services.AddScoped<IOrderService, OrderManager>();
builder.Services.AddScoped<ISettingService, SettingManager>();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
//app.UseMiddleware<MaintenenceMiddleware>();

app.UseMaintenanceMode();
app.UseExceptionHandling();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
