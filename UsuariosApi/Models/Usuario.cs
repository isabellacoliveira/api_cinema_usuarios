namespace UsuariosApi.Models 
{
    // o usuário não tem a senha exposta
    // por isso não colocamos aqui 
    public class Usuario
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
    }
}