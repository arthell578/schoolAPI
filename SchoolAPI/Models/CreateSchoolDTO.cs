using System.ComponentModel.DataAnnotations;

namespace SchoolAPI.Models
{
    public class CreateSchoolDTO
    {
        [Required]
        [MaxLength(25)]
        public string Name { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        [Required]
        [MaxLength(12)]
        // [Phone]
        public string ContactNumber { get; set; }
        [Required]
        [MaxLength(40)]
        public string City { get; set; }
        public string Street { get; set; }
        public string PostalCode { get; set; }




    }
}
