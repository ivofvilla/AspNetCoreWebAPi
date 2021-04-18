using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SmartSchool.API.Models;

namespace SmartSchool.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlunoController : ControllerBase
    {
        public List<Aluno> Alunos = new List<Aluno>(){
            new Aluno() {
                Id = 1,
                Nome = "Marcos",
                Telefone = "123456789"
            },
            new Aluno() {
                Id = 2,
                Nome = "Marta",
                Telefone = "658954541"
            },
            new Aluno() {
                Id = 3,
                Nome = "Laura",
                Telefone = "1456222"
            },
        }; 

        public AlunoController() {}

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(Alunos);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            var aluno = Alunos.FirstOrDefault(w => w.Id == id);

            if(aluno == null)
                return BadRequest("Aluno não encontrado");

            return Ok(aluno);
        }

        [HttpGet("{nome}")]
        public IActionResult GetByName(string nome)
        {
            var aluno = Alunos.FirstOrDefault(w => w.Nome.Contains(nome));

            if(aluno == null)
                return BadRequest("Aluno não encontrado");

            return Ok(aluno);
        }

        [HttpPost]
        public IActionResult Post(Aluno aluno)
        {
            Alunos.Add(aluno);
            return Ok(Alunos);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Aluno aluno)
        {
            var schule = Alunos.FirstOrDefault(w => w.Id == id);
    
            schule.Nome = aluno.Nome;
            schule.Sobrenome = aluno.Sobrenome;
            schule.Telefone = aluno.Telefone;

            return Ok(Alunos);
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(int id, Aluno aluno)
        {
            var schule = Alunos.FirstOrDefault(w => w.Id == id);
    
            schule.Nome = aluno.Nome;
            schule.Sobrenome = aluno.Sobrenome;
            schule.Telefone = aluno.Telefone;

            return Ok(Alunos);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var aluno = Alunos.FirstOrDefault(w => w.Id == id);

            Alunos.Remove(aluno);

            return Ok(Alunos);
        }
    }
}