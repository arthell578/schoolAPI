namespace SchoolAPI.Models
{
    public class SchoolDTO
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public string ContactNumber { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string PostalCode { get; set; }
        public List<CourseDTO> Courses { get; set; }

    }
}
