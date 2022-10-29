namespace SchoolAPI.Entities
{
    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int MaxCapacity { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public int SchoolId { get; set; }
        public virtual School School { get; set; }

    }
}
