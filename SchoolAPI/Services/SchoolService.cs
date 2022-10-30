using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SchoolAPI.Entities;
using SchoolAPI.Models;

namespace SchoolAPI.Services
{
    public interface ISchoolService
    {
        int Create(CreateSchoolDTO dto);
        IEnumerable<SchoolDTO> GetAll();
        SchoolDTO GetByID(int id);
    }

    public class SchoolService : ISchoolService
    {
        private readonly SchoolDbContext _dbContext;
        private readonly IMapper _mapper;

        public SchoolService(SchoolDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public SchoolDTO GetByID(int id)
        {
            var school = _dbContext
              .Schools
              .Include(s => s.Address)
              .Include(s => s.courses)
              .FirstOrDefault(r => r.Id == id);

            if (school is null) return null;

            var result = _mapper.Map<SchoolDTO>(school);
            return result;
        }


        public IEnumerable<SchoolDTO> GetAll()
        {
            var schools = _dbContext
              .Schools
              .Include(s => s.Address)
              .Include(s => s.courses)
              .ToList();

            var schoolsDTO = _mapper.Map<List<SchoolDTO>>(schools);

            return schoolsDTO;
        }

        public int Create(CreateSchoolDTO dto)
        {
            var school = _mapper.Map<School>(dto);
            _dbContext.Schools.Add(school);
            _dbContext.SaveChanges();

            return school.Id;
        }
    }
}
