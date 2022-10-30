using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolAPI.Entities;
using SchoolAPI.Models;
using SchoolAPI.Services;

namespace SchoolAPI.Controllers
{
    [Route("api/school")]
    public class SchoolController : ControllerBase
    {
        public ISchoolService _schoolService;

        public SchoolController(ISchoolService schoolService)
        {
            _schoolService = schoolService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<SchoolDTO>> GetAll()
        {
            var schoolsDTO = _schoolService.GetAll();

            return Ok(schoolsDTO);
        }

        [HttpGet("{id}")]
        public ActionResult<SchoolDTO> Get([FromRoute]int id)
        {
            var school = _schoolService.GetByID(id);
            return Ok(school);
        }

        [HttpPost]
        public ActionResult CreateSchool([FromBody] CreateSchoolDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

           var id = _schoolService.Create(dto);


            return Created($"/api/schools/{id}",null);
        }

        
    }
}
