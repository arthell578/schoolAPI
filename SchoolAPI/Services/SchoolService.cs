using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using SchoolAPI.Authorization;
using SchoolAPI.Entities;
using SchoolAPI.Exceptions;
using SchoolAPI.Models;
using System.Security.Claims;

namespace SchoolAPI.Services
{
    public interface ISchoolService
    {
        int Create(CreateSchoolDTO dto, int teacherId);
        IEnumerable<SchoolDTO> GetAll();
        SchoolDTO GetByID(int id);
        void Delete(int id, ClaimsPrincipal user);
        void Update(int id, UpdateSchoolDTO dto, ClaimsPrincipal user);

    }

    public class SchoolService : ISchoolService
    {
        private readonly SchoolDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<SchoolService> _logger;
        private readonly IAuthorizationService _authorizationService;

        public SchoolService(SchoolDbContext dbContext,
            IMapper mapper,
            ILogger<SchoolService> logger,
            IAuthorizationService authorizationService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = logger;
            _authorizationService = authorizationService;
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

        public int Create(CreateSchoolDTO dto, int teacherId)
        {
            var school = _mapper.Map<School>(dto);
            school.CreatedById = teacherId;
            _dbContext.Schools.Add(school);
            _dbContext.SaveChanges();

            return school.Id;
        }

        public void Delete(int id, ClaimsPrincipal user)
        {
            _logger.LogError($"School with id {id} DELETE action invoked");

            var school = _dbContext
            .Schools
            .FirstOrDefault(r => r.Id == id);

            var authResult = _authorizationService.AuthorizeAsync(user, school, new ResourceOperationRequirement(ResourceOperation.Update)).Result;

            if (!authResult.Succeeded)
            {
                throw new ForbidException();
            }

            if (school is null)
                throw new NotFoundException("School could not be found");

            _dbContext.Schools.Remove(school);
            _dbContext.SaveChanges();
        }


        public void Update(int id, UpdateSchoolDTO dto, ClaimsPrincipal user)
        {

            var school = _dbContext
                .Schools
                .FirstOrDefault(r => r.Id == id);

            var authResult = _authorizationService.AuthorizeAsync(user, school, new ResourceOperationRequirement(ResourceOperation.Update)).Result;

            if (!authResult.Succeeded)
            {
                throw new ForbidException();
            }

            if (school is null)
                throw new NotFoundException("School could not be found");


            school.Name = dto.Name;
            school.Description = dto.Description;
            school.ContactNumber = dto.ContactNumber;

            _dbContext.SaveChanges();
        }
    }
}
