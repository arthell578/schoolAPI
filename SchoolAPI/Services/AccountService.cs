using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SchoolAPI.Entities;
using SchoolAPI.Exceptions;
using SchoolAPI.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SchoolAPI.Services
{
    public interface IAccountService
    {
        void RegiserUser(RegisterTeacherDTO dto);
    }

    public class AccountService : IAccountService
    {
        private readonly SchoolDbContext _dbContext;
        private readonly IPasswordHasher<Teacher> _passwordHasher;
        private readonly AuthenticationSettings _authenticationSettings;

        public AccountService(
            SchoolDbContext dbContext, 
            IPasswordHasher<Teacher> passwordHasher,
            AuthenticationSettings authenticationSettings)
        {
            _dbContext = dbContext;
            _passwordHasher = passwordHasher;
            _authenticationSettings = authenticationSettings;
        }

        public void RegiserUser(RegisterTeacherDTO dto)
        {
            var newTeacher = new Teacher()
            {
                Email = dto.Email,
                TeachingStartDate = dto.TeachingStartDate,
                Specialization = dto.Specialization,
                RoleId = dto.RoleId,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
            };

            var hashedPassword = _passwordHasher.HashPassword(newTeacher, dto.Password);
            newTeacher.PasswordHash = hashedPassword;
            _dbContext.Teachers.Add(newTeacher);
            _dbContext.SaveChanges();
        }

        public string GenerateJwt(LoginDTO dto)
        {
            var teacher = _dbContext.Teachers
                .Include(t=> t.Role)
                .FirstOrDefault(t => t.Email == dto.Email);

            if(teacher is null)
            {
                throw new BadRequestException("Invalid email or password");
            }

            var result = _passwordHasher.VerifyHashedPassword(teacher, teacher.PasswordHash, dto.Password);

            if(result == PasswordVerificationResult.Failed)
            {
                throw new BadRequestException("Invalid email or password");
            }

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier,teacher.Id.ToString()),
                new Claim(ClaimTypes.Name,$"{teacher.FirstName} {teacher.LastName}"),
                new Claim(ClaimTypes.Role, $"{teacher.Role}"),
                new Claim("TeachingStartDate",teacher.TeachingStartDate.Value.ToString("yyyy-MM-dd")),
                new Claim("Specialization",teacher.Specialization)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationSettings.JwtKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(_authenticationSettings.JwtExpireDays);

            var token = new JwtSecurityToken(_authenticationSettings.JwtIssuer,
                _authenticationSettings.JwtIssuer,
                claims,
                expires: expires,
                signingCredentials: credentials);

            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(token);
        }
    }
}
