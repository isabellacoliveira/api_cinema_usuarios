using System;
using FluentResults;
using UsuariosApi.Data.Requests;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using UsuariosApi.Models;

namespace UsuariosApi.Services
{
    public class LoginService
    {
        private SignInManager<IdentityUser<int>> _signInManager;
        private TokenService _tokenService;

        public LoginService(SignInManager<IdentityUser<int>> signInManager, TokenService tokenService)
        {
           _signInManager = signInManager;
           _tokenService = tokenService; 
        }
        public Result LogaUsuario(LoginRequest request)
        {
            // o signInManager vai tentar fazer a autenticação 
            // false pois não queremos fazer persistance
            // e não queremos bloquar caso o login tenha falhado 
            // request pois será uma requisição 
            // agora precisamos gerar o token e informar o usuario que foi feito o login 
            var resultadoIdentity = _signInManager
                .PasswordSignInAsync(request.Username, request.Password, false, false); 
            if(resultadoIdentity.Result.Succeeded) 
            {
                // passamos para ele um usuario pois ele recebe um identityTokenService para gerar o token
                // porem aqui não temos um usuario , somente é feita a requisição 
                // precisamos então recuperar esse usuario 
                // o nome do usuario deve ser normalizado igual ao nome que estamos tentando logar agora 
                var identityUser = _signInManager
                                    .UserManager
                                    .Users
                                    .FirstOrDefault(Usuario =>
                                         Usuario.NormalizedUserName == request.Username.ToUpper());
                Token token = _tokenService.CreateToken(identityUser);
                // a mensagem de sucesso vai conter o valor do nosso token
                return Result.Ok().WithSuccess(token.Value);
            }
            return Result.Fail("Login falhou");
        }
    }
}

        
