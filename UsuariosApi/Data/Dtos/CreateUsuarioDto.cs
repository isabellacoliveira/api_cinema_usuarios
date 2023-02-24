using System.ComponentModel.DataAnnotations;

namespace UsuariosApi.Data
{
    public class CreateUsuarioDto
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        // esse campo só vai ser valido se a comparação com o outro campo 
        // também for válida 
        [Compare("Password")]
        public string RePassword { get; set; }
    }
}