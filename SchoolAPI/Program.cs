using NLog.Web;
using SchoolAPI;
using SchoolAPI.Entities;
using SchoolAPI.Middleware;
using SchoolAPI.Services;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

//Nlog
builder.Logging.ClearProviders();
builder.Logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
builder.Host.UseNLog();

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<SchoolDbContext>();
builder.Services.AddScoped<SchoolSeeder>();
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddScoped<ISchoolService, SchoolService>();
builder.Services.AddScoped<ErrorHandlingMiddleware>();

var app = builder.Build();

var scope =app.Services.CreateScope();
var seeder = scope.ServiceProvider.GetRequiredService<SchoolSeeder>();

// Configure the HTTP request pipeline.
seeder.Seed();

app.UseMiddleware<ErrorHandlingMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
