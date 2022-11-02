using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolAPI.Entities;
using SchoolAPI.Models;
using SchoolAPI.Services;

namespace SchoolAPI.Controllers
{
    [Route("api/school")]
    [ApiController]
    public class SchoolController : ControllerBase
    {
        public ISchoolService _schoolService;

        public SchoolController(ISchoolService schoolService)
        {
            _schoolService = schoolService;
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult<IEnumerable<SchoolDTO>> GetAll()
        {
            var schoolsDTO = _schoolService.GetAll();

            return Ok(schoolsDTO);
        }

        [Authorize]
        [HttpGet("{id}")]
        public ActionResult<SchoolDTO> Get([FromRoute]int id)
        {
            var school = _schoolService.GetByID(id);
            return Ok(school);
        }

        [HttpPost]
        public ActionResult CreateSchool([FromBody] CreateSchoolDTO dto)
        {

           var id = _schoolService.Create(dto);


            return Created($"/api/schools/{id}",null);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteSchool([FromRoute] int id)
        {
            _schoolService.Delete(id);

            return NoContent();
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Principal")]
        public ActionResult UpdateSchool([FromBody] UpdateSchoolDTO dto, [FromRoute]int id)
        {
            _schoolService.Update(id, dto);
            return Ok();
        }
    }
}
