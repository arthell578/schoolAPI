using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using NLog.Web;
using SchoolAPI;
using SchoolAPI.Entities;
using SchoolAPI.Middleware;
using SchoolAPI.Models;
using SchoolAPI.Models.Validators;
using SchoolAPI.Services;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

//Nlog
builder.Logging.ClearProviders();
builder.Logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
builder.Host.UseNLog();

// Add services to the container.

builder.Services.AddControllers().AddFluentValidation();
builder.Services.AddDbContext<SchoolDbContext>();
builder.Services.AddScoped<SchoolSeeder>();
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddScoped<ISchoolService, SchoolService>();
builder.Services.AddScoped<ICourseService, CourseService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<ErrorHandlingMiddleware>();
builder.Services.AddScoped<IPasswordHasher<Teacher>,PasswordHasher<Teacher>>();
builder.Services.AddScoped<IValidator<RegisterTeacherDTO>, RegisterTeacherDTOValidator>();
builder.Services.AddScoped<RequestExecutionTimeMiddleware>();
builder.Services.AddSwaggerGen();

var app = builder.Build();

var scope =app.Services.CreateScope();
var seeder = scope.ServiceProvider.GetRequiredService<SchoolSeeder>();

// Configure the HTTP request pipeline.
seeder.Seed();

app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseMiddleware<RequestExecutionTimeMiddleware>();

app.UseHttpsRedirection();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json","SchoolAPI");
});

app.UseAuthorization();

app.MapControllers();

app.Run();
