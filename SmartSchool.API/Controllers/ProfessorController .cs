using Microsoft.AspNetCore.Mvc;

namespace SmartSchool.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfessorController : ControllerBase
    {
        public ProfessorController() {}

        public IActionResult Get()
        {
            return Ok("Professor: Marta, Paula, Lucas, Rafa");
        }
    }
}