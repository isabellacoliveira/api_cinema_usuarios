using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace UsuariosApi.Data
{
    // precisamos definir qual contexto vamos usar 
    // estamos usando um identity user 
    // temos um identificador inteiro <int>
    // e ele tem um papel dentro do sistema (uma role)
    // e a chave usada para identificar ele em nosso sistema é um inteiro 
    public class UserDbContext : IdentityDbContext<IdentityUser<int>, IdentityRole<int>, int>
    {
        // essas opções farão referencia ao nosso user db context
        public UserDbContext(DbContextOptions<UserDbContext> opt) : base(opt)
        {

        }
    }
}