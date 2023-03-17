using System;
using FluentResults;
using Microsoft.AspNetCore.Mvc;
using UsuariosApi.Data;
using UsuariosApi.Data.Requests;
using UsuariosApi.Services;

namespace UsuariosApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CadastroController : ControllerBase
    {
        private CadastroService _cadastroService;

        public CadastroController(CadastroService cadastroService)
        {
            _cadastroService = cadastroService;
        }

        [HttpPost]
        public IActionResult CadastroUsuario(CreateUsuarioDto createDto)
        {
            Result resultado =_cadastroService.CadastraUsuario(createDto);
            // se o resultado falhou 
            if(resultado.IsFailed) 
            {
                Console.WriteLine(resultado);
                return StatusCode(500);
            }
            return Ok(resultado.Successes);
        }

        // precisamos do ativa para garantir que a conta esta ativa ou não
        [HttpGet("/ativa")]
        public IActionResult AtivaContaUsuario([FromQuery] AtivaContaRequest request)
        {
            // vamos então precisar de uma nova request 
            Result resultado = _cadastroService.AtivaContaUsuario(request); 
            if(resultado.IsFailed) return StatusCode(500); 
            return Ok(resultado.Successes);
        }
    }
}