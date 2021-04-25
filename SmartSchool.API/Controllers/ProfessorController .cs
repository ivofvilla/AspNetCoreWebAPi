using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartSchool.API.Data;
using SmartSchool.API.Models;
using System.Linq;

namespace SmartSchool.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfessorController : ControllerBase
    {
        private readonly SmartContext _context;

        public ProfessorController(SmartContext context) 
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_context.Professores);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            var professor = _context.Professores.FirstOrDefault(f => f.Id == id);

            if (professor == null)
                return BadRequest("Professor não encontrado!");

            return Ok(professor);
        }

        [HttpGet("{nome}")]
        public IActionResult GetByName(string nome)
        {
            var professor = _context.Professores.FirstOrDefault(f => f.Nome.Contains(nome));

            if (professor == null)
                return BadRequest("Professor não encontrado");

            return Ok(professor);
        }

        [HttpPost]
        public IActionResult Post(Professor professor)
        {
            _context.Add(professor);
            _context.SaveChanges();
            return Ok(_context.Professores);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Professor professor)
        {
            var leher = _context.Professores.AsNoTracking().FirstOrDefault(f => f.Id == id);

            if (leher == null)
                return BadRequest("Professor não encontrado");

            _context.Update(leher);
            _context.SaveChanges();

            return Ok(_context.Professores);
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(int id, Professor professor)
        {
            var leher = _context.Professores.AsNoTracking().FirstOrDefaultAsync(f => f.Id == id);

            if (leher == null)
                return BadRequest("Professor não encontrado");

            _context.Update(professor);
            _context.SaveChanges();

            return Ok(_context.Professores);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var leher = _context.Professores.FirstOrDefaultAsync(f => f.Id == id);

            if (leher == null)
                return BadRequest("Professor não encontrado");

            _context.Remove(leher);
            _context.SaveChanges();

            return Ok(_context.Professores);
        }
    }
}