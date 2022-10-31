using System.ComponentModel.DataAnnotations;

namespace SchoolAPI.Models
{
    public class CreateCourseDTO
    {

        [Required]
        [MaxLength(40)]
        public string Name { get; set; }
        public int MaxCapacity { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public int SchoolId { get; set; }
    }
}