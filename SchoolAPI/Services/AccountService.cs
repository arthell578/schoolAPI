using Microsoft.AspNetCore.Identity;
using SchoolAPI.Entities;
using SchoolAPI.Models;

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

        public AccountService(SchoolDbContext dbContext, IPasswordHasher<Teacher> passwordHasher)
        {
            _dbContext = dbContext;
            _passwordHasher = passwordHasher;
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
    }
}
