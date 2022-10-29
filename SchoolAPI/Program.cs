using SchoolAPI;
using SchoolAPI.Entities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<SchoolDbContext>();
builder.Services.AddScoped<SchoolSeeder>();

var app = builder.Build();

var scope =app.Services.CreateScope();
var seeder = scope.ServiceProvider.GetRequiredService<SchoolSeeder>();

// Configure the HTTP request pipeline.
seeder.Seed();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
