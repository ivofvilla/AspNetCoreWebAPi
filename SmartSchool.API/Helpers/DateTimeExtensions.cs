using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartSchool.Api.Helpers
{
    public static class DateTimeExtensions
    {
        public static int GetIdadeAtual(this DateTime datetime)
        {
            var idadeAtual = DateTime.UtcNow;
            int idade = idadeAtual.Year - datetime.Year;

            if(idadeAtual < datetime.AddYears(idade))
            {
                idade--;
            }

            return idade;
        }
    }
}
