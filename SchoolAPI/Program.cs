using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NLog.Web;
using SchoolAPI;
using SchoolAPI.Authorization;
using SchoolAPI.Entities;
using SchoolAPI.Middleware;
using SchoolAPI.Models;
using SchoolAPI.Models.Validators;
using SchoolAPI.Services;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

//Nlog
builder.Logging.ClearProviders();
builder.Logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
builder.Host.UseNLog();

// Add services to the container.

var authSetting = new AuthenticationSettings();
builder.Configuration.GetSection("Authentication").Bind(authSetting);
builder.Services.AddSingleton(authSetting);
builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = "Bearer";
    option.DefaultScheme = "Bearer";
    option.DefaultChallengeScheme = "Bearer";
}).AddJwtBearer(config =>
{
    config.RequireHttpsMetadata = false;
    config.SaveToken = true;
    config.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = authSetting.JwtIssuer,
        ValidAudience = authSetting.JwtIssuer,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authSetting.JwtKey))
    };
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("HasSpecialization",builder => builder.RequireClaim("Specialization"));
});


builder.Services.AddControllers().AddFluentValidation();
builder.Services.AddDbContext<SchoolDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("SchoolDbConnection")));
builder.Services.AddScoped<SchoolSeeder>();
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddScoped<ISchoolService, SchoolService>();
builder.Services.AddScoped<ICourseService, CourseService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IUserContextService, UserContextService>();
builder.Services.AddScoped<IAuthorizationHandler, ResourceOperationRequirementHandler>();
builder.Services.AddScoped<ErrorHandlingMiddleware>();
builder.Services.AddScoped<IPasswordHasher<Teacher>,PasswordHasher<Teacher>>();
builder.Services.AddScoped<IValidator<RegisterTeacherDTO>, RegisterTeacherDTOValidator>();
builder.Services.AddScoped<IValidator<SchoolQuery>, SchoolQueryValidator>();
builder.Services.AddScoped<RequestExecutionTimeMiddleware>();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options => {
    options.AddPolicy("Client",builder =>
    {
        builder.AllowAnyMethod()
        .AllowAnyHeader()
        .WithOrigins("http://localhost:8080");
    });
});

var app = builder.Build();

var scope =app.Services.CreateScope();
var seeder = scope.ServiceProvider.GetRequiredService<SchoolSeeder>();

// Configure the HTTP request pipeline.
app.UseCors("Client");
seeder.Seed();

app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseMiddleware<RequestExecutionTimeMiddleware>();
app.UseAuthentication();
app.UseHttpsRedirection();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json","SchoolAPI");
});

app.UseAuthorization();

app.MapControllers();

app.Run();
