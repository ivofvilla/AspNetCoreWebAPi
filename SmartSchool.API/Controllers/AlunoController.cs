using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SmartSchool.API.Models;
using SmartSchool.API.Data;
using Microsoft.EntityFrameworkCore;

namespace SmartSchool.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlunoController : ControllerBase
    {
        private readonly SmartContext _context;

        public AlunoController(SmartContext context) 
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_context.Alunos);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            var aluno = _context.Alunos.FirstOrDefault(w => w.Id == id);

            if(aluno == null)
                return BadRequest("Aluno não encontrado");

            return Ok(aluno);
        }

        [HttpGet("{nome}")]
        public IActionResult GetByName(string nome)
        {
            var aluno = _context.Alunos.FirstOrDefault(w => w.Nome.Contains(nome));

            if(aluno == null)
                return BadRequest("Aluno não encontrado");

            return Ok(aluno);
        }

        [HttpPost]
        public IActionResult Post(Aluno aluno)
        {
            _context.Add(aluno);
            _context.SaveChanges();
            return Ok(_context.Alunos);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Aluno aluno)
        {
            var alu = _context.Alunos.AsNoTracking().FirstOrDefault(f => f.Id == id);           
            if(alu == null)
            {
                return BadRequest("Aluno não encontrado");
            }

            _context.Update(aluno);
            _context.SaveChanges();

            return Ok(_context.Alunos);
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(int id, Aluno aluno) 
        {
            var alu = _context.Alunos.AsNoTracking().FirstOrDefault(f => f.Id == id);           
            if(alu == null)
            {
                return BadRequest("Aluno não encontrado");
            }

            _context.Update(aluno);
            _context.SaveChanges();

            return Ok(_context.Alunos);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var aluno = _context.Alunos.FirstOrDefault(f => f.Id == id);           
            if(aluno == null)
            {
                return BadRequest("Aluno não encontrado");
            }

            _context.Remove(aluno);
            _context.SaveChanges();

            return Ok(_context.Alunos);
        }
    }
}