using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SchoolAPI.Entities;
using SchoolAPI.Exceptions;
using SchoolAPI.Models;

namespace SchoolAPI.Services
{
    public interface ICourseService
    {
        int Create(int schoolId, CreateCourseDTO dto);
        List<CourseDTO> GetAll(int schoolId);
        CourseDTO GetByID(int schoolId, int courseId);
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

        public CourseDTO GetByID(int schoolId, int courseId)
        {
            var school = _dbContext.Schools.FirstOrDefault(s => s.Id == schoolId);

            if (school is null)
                throw new NotFoundException("School not found");

            var course = _dbContext.Courses.FirstOrDefault(c => c.Id == courseId);

            if (course is null || course.SchoolId != schoolId)
                throw new NotFoundException("Course not found");

            var courseDTO = _mapper.Map<CourseDTO>(course);
            return courseDTO;
        }

        public List<CourseDTO> GetAll(int schoolId)
        {
            var school = _dbContext.Schools
                .Include(s => s.courses)
                .FirstOrDefault(s => s.Id == schoolId);

            if (school is null)
                throw new NotFoundException("School not found");

            var coursesDTO = _mapper.Map<List<CourseDTO>>(school.courses);
            return coursesDTO;
        }
    }
}
