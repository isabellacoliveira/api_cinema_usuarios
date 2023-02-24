using FluentResults;
using Microsoft.AspNetCore.Mvc;
using UsuariosApi.Data;
using UsuariosApi.Data.Requests;
using UsuariosApi.Services;

namespace UsuariosApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private LoginService _loginService;

        public LoginController(LoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpPost]
        // request pois será uma requisição 
        // agora precisamos gerar o token e informar o usuario que foi feito o login 
        public IActionResult LogaUsuario(LoginRequest request)
        {
            Result resultado =_loginService.LogaUsuario(request);
            if(resultado.IsFailed) return Unauthorized(resultado.Errors);
            return Ok(resultado.Successes);
        }
    }
};