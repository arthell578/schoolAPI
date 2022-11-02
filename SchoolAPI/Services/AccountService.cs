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

        public AccountService(SchoolDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void RegiserUser(RegisterTeacherDTO dto)
        {
            var newTeacher = new Teacher()
            {
                Email = dto.Email,
                TeachingStartDate = dto.TeachingStartDate,
                Specialization = dto.Specialization,
                RoleId = dto.RoleId
            };

            _dbContext.Teachers.Add(newTeacher);
            _dbContext.SaveChanges();
        }
    }
}
