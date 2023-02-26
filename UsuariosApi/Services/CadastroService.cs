using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using AutoMapper;
using FluentResults;
using Microsoft.AspNetCore.Identity;
using UsuariosApi.Data;
using UsuariosApi.Data.Requests;
using UsuariosApi.Models;

namespace UsuariosApi.Services 
{
    public class CadastroService
    {
        // precisamos converter o dto para um usuario do nosso sistema
        private IMapper _mapper;
        // isso vai gerenciar um identity user 
        public UserManager<IdentityUser<int>> _userManager;
        private EmailService _emailService;

        public CadastroService(IMapper mapper, UserManager<IdentityUser<int>> userManager,EmailService emailService)
        {
            _mapper = mapper;
            _userManager = userManager;
            _emailService = emailService;
        }

        public Result CadastraUsuario(CreateUsuarioDto createDto)
        {
            Usuario usuario = _mapper.Map<Usuario>(createDto);
            // vamos converter o usuario para um identity user
            IdentityUser<int> usuarioIdentity = _mapper.Map<IdentityUser<int>>(usuario);
            // precisamos agora cadastrar ele no banco para que consigamos enviar a requisição 
            // preciamos ter algum gerenciador de usuários que faça essa tarefa para nós 
            // esse gerenciador de usuarios vai gerar um resultado no identity 
            // ou seja, vai criar de maneira assincrona esse usuario 
            // o usuario identity é o que acabamos de mapear e ele terá uma senha
            // vinda do nosso dto 
            // isso será uma tarefa 
            Task<IdentityResult> resultadoIdentity = _userManager.CreateAsync(usuarioIdentity, createDto.Password);
            // vamos ver se o resultado que acabamos de executar deu sucesso 
            // retornamos um result ok 
            if (resultadoIdentity.Result.Succeeded)
            {
                // precisamos aguardar esse codigo ser preenchido para continuar 
                string code = _userManager.GenerateEmailConfirmationTokenAsync(usuarioIdentity).Result;
                var encoded = HttpUtility.UrlEncode(code);
                // o primeiro parametro é nosso destinatario e o assunto do email 
                _emailService.EnviarEmail(new[] {usuarioIdentity.Email }, "Link de ativação da conta", 
                                                usuarioIdentity.Id, encoded);
                return Result.Ok().WithSuccess(code);
            }
            return Result.Fail("Falha ao cadastrar usuário");

        }

        public Result AtivaContaUsuario(AtivaContaRequest request)
        {
            // vamos recuperar o identity user aqui dentro 
            var identityUser = _userManager
                                        .Users  
                                        // recuperar usuario que o identificador é igual o 
                                        // identificador da request 
                                        .FirstOrDefault(u => u.Id == request.UsuarioId);
            var identityResult = _userManager
                                .ConfirmEmailAsync(identityUser, request.CodigoDeAtivacao);
            if (identityResult.Result.Succeeded)
            {
                return Result.Ok();
            }
            return Result.Fail("Falha ao ativar conta do usuário"); 
        }
    }
}