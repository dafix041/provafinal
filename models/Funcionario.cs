using System;
using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class Funcionario
    { 
        public int FuncionarioId { get; set; }

       
        public string? Nome { get; set; }

        public string? Cpf { get; set; }

        public string? Nascimento { get; set; }

        public DateTime CriadoEm { get; set; }

        public Funcionario()
        {
            CriadoEm = DateTime.Now;
        }
    }
}
