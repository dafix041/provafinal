using API.Data;
using API.Formulas;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace API.Controllers
{
    [ApiController]
    [Route("api/folha")]
    public class FolhaPagamentoController : ControllerBase
    {
        private readonly AppDataContext _context;

        public FolhaPagamentoController(AppDataContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("cadastrar")]
        public IActionResult Cadastrar([FromBody] Folha folha)
        {
            try
            {
                folha.SalarioBruto = Calculos.CalcularSalarioBruto(folha.QuantidadeHoras, folha.ValorHora);
                folha.ImpostoRenda = Calculos.CalcularImpostoRenda(folha.SalarioBruto);
                folha.ImpostoInss = Calculos.CalcularInss(folha.SalarioBruto);
                folha.ImpostoFgts = Calculos.CalcularFgts(folha.SalarioBruto);
                folha.SalarioLiquido = Calculos.CalcularSalarioLiquido(folha.SalarioBruto, folha.ImpostoRenda, folha.ImpostoInss);

                _context.Folhas.Add(folha);
                _context.SaveChanges();

                return Created("", folha);
            }
            catch (Exception ex)
            {
                return BadRequest($"Ocorreu um erro ao cadastrar a folha de pagamento: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("listar")]
        public IActionResult Listar()
        {
            try
            {
                List<Folha> folhas = _context.Folhas.Include(f => f.Funcionario).ToList();

                if (folhas.Count == 0)
                {
                    return NotFound("Nenhuma folha de pagamento encontrada.");
                }

                return Ok(folhas);
            }
            catch (Exception ex)
            {
                return BadRequest($"Ocorreu um erro ao listar as folhas de pagamento: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("buscar/{cpf}/{mes}/{ano}")]
        public IActionResult Buscar([FromRoute] string cpf, int mes, int ano)
        {
            try
            {
                var folha = _context.Folhas
                    .Include(f => f.Funcionario)
                    .FirstOrDefault(f =>
                        f.CriadoEm.Month == mes &&
                        f.CriadoEm.Year == ano &&
                        f.Funcionario.Cpf.Equals(cpf, StringComparison.OrdinalIgnoreCase)); // Use StringComparison para evitar problemas de maiúsculas/minúsculas

                if (folha == null)
                {
                    return NotFound("Folha de pagamento não encontrada.");
                }

                return Ok(folha);
            }
            catch (Exception ex)
            {
                return BadRequest($"Ocorreu um erro ao buscar a folha de pagamento: {ex.Message}");
            }
        }
    }
}
