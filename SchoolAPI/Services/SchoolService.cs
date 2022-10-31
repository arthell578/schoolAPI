using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SchoolAPI.Entities;
using SchoolAPI.Exceptions;
using SchoolAPI.Models;

namespace SchoolAPI.Services
{
    public interface ISchoolService
    {
        int Create(CreateSchoolDTO dto);
        IEnumerable<SchoolDTO> GetAll();
        SchoolDTO GetByID(int id);
        void Delete(int id);
        void Update(int id, UpdateSchoolDTO dto);

    }

    public class SchoolService : ISchoolService
    {
        private readonly SchoolDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<SchoolService> _logger;

        public SchoolService(SchoolDbContext dbContext, IMapper mapper, ILogger<SchoolService> logger)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = logger;
        }

        public SchoolDTO GetByID(int id)
        {
            var school = _dbContext
              .Schools
              .Include(s => s.Address)
              .Include(s => s.courses)
              .FirstOrDefault(r => r.Id == id);

            if (school is null)
                throw new NotFoundException("School could not be found");

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

        public void Delete(int id)
        {
            _logger.LogError($"School with id {id} DELETE action invoked");

            var school = _dbContext
            .Schools
            .FirstOrDefault(r => r.Id == id);

            if (school is null)
                throw new NotFoundException("School could not be found");

            _dbContext.Schools.Remove(school);
            _dbContext.SaveChanges();
        }


        public void Update(int id, UpdateSchoolDTO dto)
        {
            var school = _dbContext
                .Schools
                .FirstOrDefault(r => r.Id == id);

            if (school is null)
                throw new NotFoundException("School could not be found");


            school.Name = dto.Name;
            school.Description = dto.Description;
            school.ContactNumber = dto.ContactNumber;

            _dbContext.SaveChanges();
        }
    }
}
