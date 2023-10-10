using System;
using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class Folha
    {
        public Folha()
        {
            CriadoEm = DateTime.Now;
        }

        public int FolhaID { get; set; }
        public double ValorHora { get; set; }

        public int QuantidadeHoras { get; set; }
        public double SalarioBruto { get; set; }
        public double ImpostoRenda { get; set; }
        public double ImpostoInss { get; set; }
        public double ImpostoFgts { get; set; }
        public double SalarioLiquido { get; set; }

        
        public int Mes { get; set; }


        public int Ano { get; set; }

        public DateTime CriadoEm { get; set; }

        public int FuncionarioId { get; set; }

        public Funcionario Funcionario { get; set; }
    }
}
