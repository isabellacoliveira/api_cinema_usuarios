using System.ComponentModel.DataAnnotations;

namespace UsuariosApi.Data
{
    public class CreateUsuarioDto
    {
        [Required]
        public int Username { get; set; }
        [Required]
        public int Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public int Password { get; set; }
        [Required]
        // esse campo só vai ser valido se a comparação com o outro campo 
        // também for válida 
        [Compare("Password")]
        public int RePassword { get; set; }
    }
}