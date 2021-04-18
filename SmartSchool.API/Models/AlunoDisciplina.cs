namespace SmartSchool.API.Models
{
    public class AlunoDisciplina
    {
        public AlunoDisciplina() { }

        public AlunoDisciplina(int alunoId, int disciplinaId, Aluno aluno, Disciplina disciplina)
        {
            this.AlunoId = alunoId;
            this.DisciplinaId = disciplinaId;
        }
        public int AlunoId { get; set; }
        public int DisciplinaId { get; set; }

        public Aluno Aluno { get; set; }
        public Disciplina Disciplina { get; set; }
    }
}