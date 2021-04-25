using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SmartSchool.API.Models;
using SmartSchool.API.Data;
using Microsoft.EntityFrameworkCore;
using SmartSchool.Api.Data;

namespace SmartSchool.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlunoController : ControllerBase
    {
        private readonly IRepository _repository;

        public AlunoController(IRepository repository) 
        {
            _repository = repository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var result = _repository.GetAllAlunos(true);
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            var aluno = _repository.GetAlunoById(id, false);
            if (aluno == null)
                BadRequest("Aluno não encontrado!");

            return Ok(aluno);
        }

        [HttpPost]
        public IActionResult Post(Aluno aluno)
        {
            _repository.Add(aluno);
            if(_repository.SaveChanges())
            {
                return Ok(aluno);
            }

            return BadRequest("Aluno não cadastrado!");
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Aluno aluno)
        {
            var schuler = _repository.GetAlunoById(id, false);
            if (schuler == null)
            {
                return BadRequest("Aluno não encontrado");
            }

            _repository.Update(aluno);
            if (_repository.SaveChanges())
            {
                return Ok(aluno);
            }

            return BadRequest("Aluno não atualizado!");
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(int id, Aluno aluno) 
        {
            var schuler = _repository.GetAlunoById(id, false);
            if (schuler == null)
            {
                return BadRequest("Aluno não encontrado");
            }

            _repository.Update(aluno);
            if (_repository.SaveChanges())
            {
                return Ok(aluno);
            }

            return BadRequest("Aluno não atualizado!");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var aluno = _repository.GetAlunoById(id, false);           
            if(aluno == null)
            {
                return BadRequest("Aluno não encontrado");
            }

            _repository.Delete(aluno);
            if (_repository.SaveChanges())
            {
                return Ok("Aluno apagado");
            }

            return BadRequest("Aluno não excluido!");
        }
    }
}