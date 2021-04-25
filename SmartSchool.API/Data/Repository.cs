using Microsoft.EntityFrameworkCore;
using SmartSchool.API.Data;
using SmartSchool.API.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartSchool.Api.Data
{
    public class Repository : IRepository
    {
        private readonly SmartContext _context = null; 
        public Repository(SmartContext context)
        {
            _context = context;
        }
        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }
        public void Update<T>(T entity) where T : class
        {
            _context.Update(entity);
        }
        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }
        public bool SaveChanges()
        {
            return _context.SaveChanges() > 0 ? true : false;
        }

        public Aluno[] GetAllAlunos(bool incluirProfessor = false)
        {
            IQueryable<Aluno> query = _context.Alunos;

            if(incluirProfessor)
            {
                query = query.Include(ad => ad.AlunosDisciplinas)
                             .ThenInclude(d => d.Disciplina)
                             .ThenInclude(p => p.Professor);
            }

            query = query.AsNoTracking().OrderBy(o => o.Id);

            return query.ToArray();
        }

        public Aluno[] GetAllAlunosByDisciplinaId(int disciplinaId, bool incluirProfessor)
        {
            IQueryable<Aluno> query = _context.Alunos;

            if (incluirProfessor)
            {
                query = query.Include(ad => ad.AlunosDisciplinas)
                             .ThenInclude(d => d.Disciplina)
                             .ThenInclude(p => p.Professor);
            }

            query = query.AsNoTracking()
                         .OrderBy(o => o.Id)
                         .Where(w => w.AlunosDisciplinas.Any(a => a.DisciplinaId == disciplinaId));

            return query.ToArray();
        }

        public Aluno[] GetAlunoById(int alunoId, bool incluirProfessor = false)
        {
            IQueryable<Aluno> query = _context.Alunos;

            if (incluirProfessor)
            {
                query = query.Include(ad => ad.AlunosDisciplinas)
                             .ThenInclude(d => d.Disciplina)
                             .ThenInclude(p => p.Professor);
            }

            query = query.AsNoTracking()
                         .OrderBy(o => o.Id)
                         .Where(w => w.Id == alunoId);

            return query.ToArray();
        }

        public Professor[] GetAllProfessores(bool incluirAlunos = false)
        {
            IQueryable<Professor> query = _context.Professores;

            if (incluirAlunos)
            {
                query = query.Include(d => d.Disciplinas)
                             .ThenInclude(ad => ad.AlunosDisciplinas)
                             .ThenInclude(a => a.Aluno);
            }

            query = query.AsNoTracking().OrderBy(o => o.Id);

            return query.ToArray();
        }

        public Professor[] GetAllProfessoresByDisciplina(int idDisciplina, bool incluirAlunos = false)
        {
            IQueryable<Professor> query = _context.Professores;

            if (incluirAlunos)
            {
                query = query.Include(d => d.Disciplinas)
                             .ThenInclude(ad => ad.AlunosDisciplinas)
                             .ThenInclude(a => a.Aluno);
            }

            query = query.AsNoTracking()
                         .OrderBy(o => o.Id)
                         .Where(w => w.Disciplinas.Any(
                                    a => a.AlunosDisciplinas.Any(ada => ada.DisciplinaId == idDisciplina)
                                ) 
                         );

            return query.ToArray();
        }

        public Professor[] GetAllProfessoresById(int idProfessor, bool incluirAlunos = false)
        {
            IQueryable<Professor> query = _context.Professores;

            if (incluirAlunos)
            {
                query = query.Include(d => d.Disciplinas)
                             .ThenInclude(ad => ad.AlunosDisciplinas)
                             .ThenInclude(a => a.Aluno);
            }

            query = query.AsNoTracking().Where(w => w.Id == idProfessor);

            return query.ToArray();
        }
    }
}
