using System;
using System.Threading.Tasks;
using AutoMapper;
using FluentResults;
using Microsoft.AspNetCore.Identity;
using UsuariosApi.Data;
using UsuariosApi.Models;

namespace UsuariosApi.Services 
{
    public class CadastroService
    {
        // precisamos converter o dto para um usuario do nosso sistema
        private IMapper _mapper;
        // isso vai gerenciar um identity user 
        public UserManager<IdentityUser<int>> _userManager;
        public CadastroService(IMapper mapper, UserManager<IdentityUser<int>> userManager )
        {
            _mapper = mapper;
            _userManager = userManager;
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
            if(resultadoIdentity.Result.Succeeded) return Result.Ok();
            return Result.Fail("Falha ao cadastrar o usuário");
        }
    }
}