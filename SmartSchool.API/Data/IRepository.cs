using SmartSchool.API.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartSchool.Api.Data
{
    public interface IRepository
    {
        void Add<T>(T Entity) where T : class;
        void Update<T>(T Entity) where T : class;
        void Delete<T>(T Entity) where T : class;
        bool SaveChanges();
        Aluno[] GetAllAlunos(bool incluirProfessor = false);
        Aluno[] GetAllAlunosByDisciplinaId(int disciplinaId, bool incluirProfessor);
        Aluno[] GetAlunoById(int alunoId, bool incluirProfessor = false);
        Professor[] GetAllProfessores(bool incluirAlunos = false);
        Professor[] GetAllProfessoresByDisciplina(int idDisciplina, bool incluirAlunos = false);
        Professor[] GetAllProfessoresById(int idProfessor, bool incluirAlunos = false);


    }
}
