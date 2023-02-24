using FluentResults;
using Microsoft.AspNetCore.Identity;

namespace UsuariosApi.Services
{
    public class LogoutService
    {
        private SignInManager<IdentityUser<int>> _signInManager;

        public LogoutService(SignInManager<IdentityUser<int>> signInManager)
        {
            _signInManager = signInManager;
        }

        public Result DeslogaUsuario()
        {
            // vamos chamar o signOut async 
            var resultadoIdentity = _signInManager.SignOutAsync();
            if(resultadoIdentity.IsCompletedSuccessfully) return Result.Ok();
            return Result.Fail("Falha ao deslogar");
        }
    }
}