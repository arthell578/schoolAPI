using Microsoft.AspNetCore.Mvc;
using SchoolAPI.Models;
using SchoolAPI.Services;

namespace SchoolAPI.Controllers
{
    [Route("/api/school/{schoolId}/course")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ICourseService courseService;

        public CourseController(ICourseService courseService)
        {
            this.courseService = courseService;
        }

        [HttpPost]
        public ActionResult Post([FromRoute] int schoolId,[FromBody] CreateCourseDTO dto)
        {
           var newCourseId =  courseService.Create(schoolId,dto);

            return Created($"api/school/{schoolId}/course/{newCourseId}", null);
        }

        [HttpGet("{dishId}")]
        public ActionResult<CourseDTO> Get([FromRoute] int schoolId, [FromRoute] int courseId)
        {
            var course = courseService.GetByID(schoolId, courseId);
            return Ok(course);
        }

        [HttpGet]
        public ActionResult<List<CourseDTO>> Get([FromRoute] int schoolId)
        {
            var courses = courseService.GetAll(schoolId);
            return Ok(courses);
        }

        [HttpDelete]
        public ActionResult Delete([FromRoute] int schoolId)
        {
            courseService.RemoveAll(schoolId);

            return NoContent();
        }
    }
}
