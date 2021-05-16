using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SmartSchool.API.Models;
using SmartSchool.API.Data;
using Microsoft.EntityFrameworkCore;
using SmartSchool.Api.Data;
using SmartSchool.Api.V2.Dtos;
using AutoMapper;

namespace SmartSchool.API.V2.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [ApiController]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class AlunoController : ControllerBase
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="mapper"></param>
        public AlunoController(IRepository repository, IMapper mapper) 
        {
            _repository = repository;
            _mapper = mapper;
        }

        /// <summary>
        /// Metodo responsavel para retornar todos os alunos
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Get()
        {
            var alunos = _repository.GetAllAlunos(true);
            
            return Ok(_mapper.Map<IEnumerable<AlunoDto>>(alunos)); //funciona
        }

        /// <summary>
        /// Metodo responsavel por retornar um unico aluno
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var aluno = _repository.GetAlunoById(id, false);
            if (aluno == null)
                BadRequest("Aluno não encontrado!");

            var alunoDto = _mapper.Map<AlunoDto>(aluno); // erro

            return Ok(alunoDto);
        }

        /// <summary>
        /// metodo responsavel para salvar um aluno
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Post(AlunoRegistroDto model)
        {
            var aluno = _mapper.Map<Aluno>(model);

            _repository.Add(aluno);
            if(_repository.SaveChanges())
            {
                return Created($"/api/aluno/{model.Id}", _mapper.Map<AlunoDto>(aluno));
            }

            return BadRequest("Aluno não cadastrado!");
        }

        /// <summary>
        /// metodo responsavel para atualizar aluno
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public IActionResult Put(int id, AlunoRegistroDto model)
        {
            var aluno = _repository.GetAlunoById(id, false);
            if (aluno == null)
            {
                return BadRequest("Aluno não encontrado");
            }

            _mapper.Map(model, aluno);

            _repository.Update(aluno);
            if (_repository.SaveChanges())
            {
                return Created($"/api/aluno/{model.Id}", _mapper.Map<AlunoDto>(aluno));
            }

            return BadRequest("Aluno não atualizado!");
        }

        /// <summary>
        /// metodo responsavel para apagar o aluno
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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