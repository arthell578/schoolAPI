using AutoMapper;
using SchoolAPI.Entities;
using SchoolAPI.Models;

namespace SchoolAPI
{
    public class SchoolMappingProfile : Profile
    {
        public SchoolMappingProfile()
        {
            CreateMap<School, SchoolDTO>()
                .ForMember(s => s.City, c => c.MapFrom(s => s.Address.City))
                .ForMember(s => s.Street, c => c.MapFrom(s => s.Address.Street))
                .ForMember(s => s.PostalCode, c => c.MapFrom(s => s.Address.PostalCode));

            CreateMap<Course, CourseDTO>();

            CreateMap<CreateSchoolDTO,School>()
                .ForMember(s => s.Address, c=>c.MapFrom(dto =>  new Address() 
                    { City = dto.City, PostalCode = dto.PostalCode, Street = dto.Street}));

            CreateMap<CreateCourseDTO, Course>();
        }
    }
}
