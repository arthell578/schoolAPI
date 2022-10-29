using SchoolAPI.Entities;

namespace SchoolAPI
{
    public class SchoolSeeder
    {
        private readonly SchoolDbContext _dbContext;

        public SchoolSeeder(SchoolDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Seed()
        {
            if (_dbContext.Database.CanConnect())
            {
                if (!_dbContext.Schools.Any())
                {
                    var schools = GetSchools();
                    _dbContext.Schools.AddRange(schools);
                    _dbContext.SaveChanges();
                }
            }
        }

        private IEnumerable<School> GetSchools()
        {
            var schools = new List<School>() {
                new School()
                {
                    Name = "Giganci Wolomin",
                    Type = "Private",
                    Description = "Giganci Programowania to miejsce, w którym kursy programowania dla dzieci i młodzieży łączą się ze świetną zabawą i nauką nowoczesnych technologii.",
                    ContactNumber = "48000111222",
                    Address = new Address()
                    {
                        City = "Wolomin",
                        Street = "Wilenska",
                        PostalCode = "05-200"
                    },
                    courses = new List<Course>()
                    {
                        new Course()
                        {
                            Name = "Hacking",
                            Description = "Hacking i cyberbezpieczenstwo, 13-18lat",
                            MaxCapacity = 12,
                            Price = 1200,
                        },
                        new Course()
                        {
                            Name = "Scratch",
                            Description = "Scratch, 7-9lat",
                            MaxCapacity = 10,
                            Price = 800,
                        }
                    }
                },
                new School()
                {
                    Name = "Giganci Raddzymin",
                    Type = "Private",
                    Description = "Giganci Programowania to miejsce, w którym kursy programowania dla dzieci i młodzieży łączą się ze świetną zabawą i nauką nowoczesnych technologii.",
                    ContactNumber = "48000222333",
                    Address = new Address()
                    {
                        City = "Radzymin",
                        Street = "Jana Pawła 2",
                        PostalCode = "05-250"
                    },
                    courses = new List<Course>()
                    {
                        new Course()
                        {
                            Name = "Python",
                            Description = "Python w Minecraft 10-13lat",
                            MaxCapacity = 12,
                            Price = 900,
                        },
                        new Course()
                        {
                            Name = "Scratch",
                            Description = "Scratch, 7-9lat",
                            MaxCapacity = 10,
                            Price = 800,
                        }
                    }
                }
            };
            return schools;
        }
    }
}
