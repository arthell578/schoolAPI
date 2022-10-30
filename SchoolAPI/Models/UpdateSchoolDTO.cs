using System.ComponentModel.DataAnnotations;

namespace SchoolAPI.Models
{
    public class UpdateSchoolDTO
    {
        [Required]
        [MaxLength(25)]
        public string Name { get; set; }
        public string Description { get; set; }
        public string ContactNumber { get; set; }

    }
}
