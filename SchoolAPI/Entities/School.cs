namespace SchoolAPI.Entities
{
    public class School
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public string ContactNumber { get; set; }
        public int AddressId { get; set; }

        public int? CreatedById { get; set; }
        public virtual Teacher CreatedBy { get; set; }

        public virtual Address Address { get; set; }

        public virtual List<Course> courses { get; set; }


    }
}
