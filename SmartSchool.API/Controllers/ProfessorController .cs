using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartSchool.Api.Data;
using SmartSchool.API.Data;
using SmartSchool.API.Models;
using System.Linq;

namespace SmartSchool.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfessorController : ControllerBase
    {
        private readonly IRepository _repository;

        public ProfessorController(IRepository repository) 
        {
            _repository = repository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_repository.GetAllProfessores(true));
        }

        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            var professor = _repository.GetAllProfessoresById(id);

            if (professor == null)
                return BadRequest("Professor não encontrado!");

            return Ok(professor);
        }

        [HttpPost]
        public IActionResult Post(Professor professor)
        {
            _repository.Add(professor);
            if (_repository.SaveChanges())
            {
                return Ok(professor);
            }

            return BadRequest("Professor não cadastrado!");
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Professor professor)
        {
            var leher = _repository.GetAllProfessoresById(id);

            if (leher == null)
                return BadRequest("Professor não encontrado");

            _repository.Update(professor);
            if (_repository.SaveChanges())
            {
                return Ok(professor);
            }

            return BadRequest("Professor não atualizado!");
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(int id, Professor professor)
        {
            var leher = _repository.GetAllProfessoresById(id);

            if (leher == null)
                return BadRequest("Professor não encontrado");

            _repository.Update(professor);
            if (_repository.SaveChanges())
            {
                return Ok(professor);
            }

            return BadRequest("Professor não atualizado!");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var leher = _repository.GetAllProfessoresById(id);

            if (leher == null)
                return BadRequest("Professor não encontrado");

            _repository.Delete(leher);
            if (_repository.SaveChanges())
            {
                return Ok("Professor removido com sucesso!");
            }

            return BadRequest("Professor não atualizado!");
        }
    }
}