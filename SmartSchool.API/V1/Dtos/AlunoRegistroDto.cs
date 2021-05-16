using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartSchool.Api.V1.Dtos
{
    /// <summary>
    /// Dto Aluno para cadastro V1
    /// </summary>
    public class AlunoRegistroDto
    {
        /// <summary>
        /// Id do aluno
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// numero da matricula
        /// </summary>
        public int Matricula { get; set; }
        /// <summary>
        /// nome do aluno
        /// </summary>
        public string Nome { get; set; }
        /// <summary>
        /// sobrenome do aluno
        /// </summary>
        public string Sobrenome { get; set; }
        /// <summary>
        /// telefone do aluno
        /// </summary>
        public string Telefone { get; set; }
        /// <summary>
        /// Data de nascimento
        /// </summary>
        public DateTime DataNascimento { get; set; }
        /// <summary>
        /// Data de inicio
        /// </summary>
        public DateTime DataInicio { get; set; } = DateTime.Now;
        /// <summary>
        /// Dat de fim 
        /// </summary>
        public DateTime? DataFim { get; set; } = null;
        /// <summary>
        /// aluno esta ativo
        /// </summary>
        public bool Ativo { get; set; } = true;
    }
}
