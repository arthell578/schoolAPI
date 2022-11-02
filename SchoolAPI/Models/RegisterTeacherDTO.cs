using SchoolAPI.Entities;
using System.ComponentModel.DataAnnotations;

namespace SchoolAPI.Models
{
    public class RegisterTeacherDTO
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Specialization { get; set; }
        public DateTime? TeachingStartDate { get; set; }
        public int RoleId { get; set; } = 1;
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
