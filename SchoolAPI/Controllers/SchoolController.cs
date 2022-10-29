using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolAPI.Entities;
using SchoolAPI.Models;

namespace SchoolAPI.Controllers
{
    [Route("api/school")]
    public class SchoolController : ControllerBase
    {
        private readonly SchoolDbContext _dbContext;
        private readonly IMapper _mapper;

        public SchoolController(SchoolDbContext dbContext,IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<SchoolDTO>> GetAll()
        {
            var schools = _dbContext
                .Schools
                .Include(s=>s.Address)
                .Include(s=>s.courses)
                .ToList();

            var schoolsDTO = _mapper.Map<List<SchoolDTO>>(schools);

            return Ok(schoolsDTO);
        }

        [HttpGet("{id}")]
        public ActionResult<SchoolDTO> Get([FromRoute]int id)
        {
            var school = _dbContext
                .Schools
                .Include(s => s.Address)
                .Include(s => s.courses)
                .FirstOrDefault(r => r.Id == id);


            if (school is null)
            {
                return NotFound();
            }

            var schoolDTO = _mapper.Map<SchoolDTO>(school);

            return Ok(schoolDTO);
        }

        
    }
}
