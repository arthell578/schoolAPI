using Microsoft.AspNetCore.Mvc;
using SchoolAPI.Entities;

namespace SchoolAPI.Controllers
{
    [Route("api/school")]
    public class SchoolController : ControllerBase
    {
        private readonly SchoolDbContext _dbContext;

        public SchoolController(SchoolDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public ActionResult<IEnumerable<School>> GetAll()
        {
            var schools = _dbContext.Schools.ToList();
            return Ok(schools);
        }

        [HttpGet("{id}")]
        public ActionResult<School> Get([FromRoute]int id)
        {
            var school = _dbContext.Schools.FirstOrDefault(r => r.Id==id);
            if (school == null)
            {
                return NotFound();
            }
            return Ok(school);
        }

    }
}
