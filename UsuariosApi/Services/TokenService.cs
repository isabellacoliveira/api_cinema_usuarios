// classe responsavel pela criação do token
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using UsuariosApi.Models;

namespace UsuariosApi.Services
{
    public class TokenService
    {
        // qual usuario estamos recebendo para criar um token para ele 
        public Token CreateToken(IdentityUser<int> usuario)
        {
            // vamos gerar um array de "reclamações"
            Claim[] direitosUsuario = new Claim[]
            {
                // o username é escrito assim devido ao banco de dados 
                new Claim("username", usuario.UserName),
                new Claim("id", usuario.Id.ToString()),
            };
            // precisamos gerar uma chave para criptografar o nosso token
            // a chave vai ser gerada através do 
            // vamos usar uma chave bem doida 
            var chave = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes("0asdjas09djsa09djasdjsadajsd09asjd09sajcnzxn"));
            // vamos gerar as crendenciais a partir da nossa chave 
            var credenciais = new SigningCredentials(chave, SecurityAlgorithms.HmacSha256);
            // vamos gerar o token 
            var token = new JwtSecurityToken(
                claims: direitosUsuario,
            // quais são nossas partes de segurança 
                signingCredentials: credenciais, 
            // quando o token vai expirar (1h)
            expires: DateTime.UtcNow.AddHours(1)
            );

            // precisamos tornar o token uma string 
            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
            return new Token(tokenString);
            // estamos gerando nosso modelo de token gerando o nosso token string 
            
        }
    }
}