using System;
using System.Linq;
using API.Data;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/funcionario")]
    public class FuncionarioController : ControllerBase
    {
        private readonly AppDataContext _ctx;

        public FuncionarioController(AppDataContext ctx)
        {
            _ctx = ctx;
        }

        [HttpGet]
        [Route("listar")]
        public IActionResult Listar()
        {
            try
            {
                var funcionarios = _ctx.Funcionarios.ToList();
                if (funcionarios.Count == 0)
                {
                    return NotFound("Nenhum funcionário encontrado.");
                }
                return Ok(funcionarios);
            }
            catch (Exception ex)
            {
                return BadRequest($"Ocorreu um erro ao listar os funcionários: {ex.Message}");
            }
        }

        [HttpPost]
        [Route("cadastrar")]
        public IActionResult Cadastrar([FromBody] Funcionario funcionario)
        {
            try
            {
                _ctx.Add(funcionario);
                _ctx.SaveChanges();
                return Created("", funcionario);
            }
            catch (Exception ex)
            {
                return BadRequest($"Ocorreu um erro ao cadastrar o funcionário: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("buscar/{id}")]
        public IActionResult Buscar([FromRoute] int id)
        {
            try
            {
                var funcionarioCadastrado = _ctx.Funcionarios.FirstOrDefault(x => x.FuncionarioId == id);
                if (funcionarioCadastrado != null)
                {
                    return Ok(funcionarioCadastrado);
                }
                return NotFound("Funcionário não encontrado.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Ocorreu um erro ao buscar o funcionário: {ex.Message}");
            }
        }

        [HttpDelete]
        [Route("deletar/{id}")]
        public IActionResult Deletar([FromRoute] int id)
        {
            try
            {
                var funcionarioCadastrado = _ctx.Funcionarios.Find(id);
                if (funcionarioCadastrado != null)
                {
                    _ctx.Funcionarios.Remove(funcionarioCadastrado);
                    _ctx.SaveChanges();
                    return Ok();
                }
                return NotFound("Funcionário não encontrado.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Ocorreu um erro ao deletar o funcionário: {ex.Message}");
            }
        }
    }
}
