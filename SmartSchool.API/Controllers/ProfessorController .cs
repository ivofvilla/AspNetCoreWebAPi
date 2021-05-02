using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartSchool.Api.Data;
using SmartSchool.Api.Dtos;
using SmartSchool.API.Data;
using SmartSchool.API.Models;
using System.Collections.Generic;
using System.Linq;

namespace SmartSchool.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfessorController : ControllerBase
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;

        public ProfessorController(IRepository repository, IMapper mapper) 
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var professores = _repository.GetAllProfessores(true);
            return Ok(_mapper.Map<IEnumerable<ProfessorDto>>(professores));
        }

        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            var professor = _repository.GetAllProfessoresById(id);

            if (professor == null)
                return BadRequest("Professor não encontrado!");

            var professorDto = _mapper.Map<IEnumerable<ProfessorDto>>(professor);

            return Ok(professorDto);
        }

        [HttpPost]
        public IActionResult Post(ProfessorRegistroDto model)
        {
            var professor = _mapper.Map<Professor>(model);

            _repository.Add(professor);
            if (_repository.SaveChanges())
            {
                return Created($"/api/aluno/{model.Id}", _mapper.Map<IEnumerable<ProfessorDto>>(professor));
            }

            return BadRequest("Professor não cadastrado!");
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, ProfessorRegistroDto model)
        {
            var professor = _repository.GetAllProfessoresById(id);

            if (professor == null)
                return BadRequest("Professor não encontrado");

            _mapper.Map(model, professor);

            _repository.Update(professor);
            if (_repository.SaveChanges())
            {
                Created($"/api/professor/{model.Id}", _mapper.Map<IEnumerable<ProfessorDto>>(professor));
            }

            return BadRequest("Professor não atualizado!");
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(int id, ProfessorRegistroDto model)
        {
            var professor = _repository.GetAllProfessoresById(id);

            if (professor == null)
                return BadRequest("Professor não encontrado");

            _mapper.Map(model, professor);

            _repository.Update(professor);
            if (_repository.SaveChanges())
            {
                return Created($"/api/professor/{model.Id}", _mapper.Map<ProfessorDto>(professor));
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