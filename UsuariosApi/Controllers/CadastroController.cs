using FluentResults;
using Microsoft.AspNetCore.Mvc;
using UsuariosApi.Data;
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
            if(resultado.IsFailed) return StatusCode(500);
            return Ok();
        }
    }
};