using Microsoft.EntityFrameworkCore;

namespace SchoolAPI.Entities
{
    public class SchoolDbContext : DbContext
    {
        private string _connectionString = 
            "Server=(localdb)\\mssqllocaldb;Database=SchoolDb;Trusted_Connection=True;";
        public DbSet<School> Schools { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Role> Roles { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Teacher>()
                .Property(t => t.Email)
                .IsRequired();

            modelBuilder.Entity<Role>()
                .Property(r => r.Name)
                .IsRequired();

            modelBuilder.Entity<School>()
                .Property(r => r.Name)
                .IsRequired()
                .HasMaxLength(25);

            modelBuilder.Entity<Course>()
                .Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(40);

            modelBuilder.Entity<Address>()
                .Property(c => c.City)
                .IsRequired()
                .HasMaxLength(40);

            modelBuilder.Entity<School>()
                .Property(s => s.ContactNumber)
                .IsRequired()
                .HasMaxLength(12);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }
    }
}
