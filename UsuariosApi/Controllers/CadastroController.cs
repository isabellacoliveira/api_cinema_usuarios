using Microsoft.AspNetCore.Mvc;
using UsuariosApi.Data;


namespace UsuariosApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CadastroController : ControllerBase
    {
        [HttpPost]
        public IActionResult CadastroUsuario(CreateUsuarioDto createDto)
        {
            return Ok();
        }
    }
}