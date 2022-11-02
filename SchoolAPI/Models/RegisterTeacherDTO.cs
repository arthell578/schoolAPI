using SchoolAPI.Entities;
using System.ComponentModel.DataAnnotations;

namespace SchoolAPI.Models
{
    public class RegisterTeacherDTO
    {
        [Required]
        public string Email { get; set; }
        [Required]
        [MinLength(6)]
        public string Password { get; set; }
        public string Specialization { get; set; }
        public DateTime? TeachingStartDate { get; set; }
        public int RoleId { get; set; } = 1;
    }
}
