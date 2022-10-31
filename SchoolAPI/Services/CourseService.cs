using AutoMapper;
using SchoolAPI.Entities;
using SchoolAPI.Exceptions;
using SchoolAPI.Models;

namespace SchoolAPI.Services
{
    public interface ICourseService
    {
        int Create(int schoolId, CreateCourseDTO dto);
    }

    public class CourseService : ICourseService
    {
        private readonly SchoolDbContext _dbContext;
        private readonly IMapper _mapper;

        public CourseService(SchoolDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public int Create(int schoolId, CreateCourseDTO dto)
        {
            var school = _dbContext.Schools.FirstOrDefault(s => s.Id == schoolId);

            if (school is null)
                throw new NotFoundException("School not found");

            var courseEntity = _mapper.Map<Course>(dto);

            courseEntity.SchoolId = schoolId;

            _dbContext.Courses.Add(courseEntity);
            _dbContext.SaveChanges();

            return courseEntity.Id;
        }

    }
}
