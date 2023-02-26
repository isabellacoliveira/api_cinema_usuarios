using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MimeKit;

namespace UsuariosApi.Models
{
    public class Mensagem
    {
        // vamos criar a estrutura que compoe uma mensagem 
        // o destinatario vai passar a ser de um tipo especial 
        // que identifica o endereço de email 
        public List<MailboxAddress> Destinatario { get; set; }
        public string Assunto { get; set; }
        public string Conteudo { get; set; }

        // vamos receber uma lista de string que é nosso destinatario 
        public Mensagem(IEnumerable<string> destinatario, string assunto, 
                        int usuarioId, string codigo)
        {
            Destinatario = new List<MailboxAddress>();
            // adicionando um elemento ao final dessa coleção 
            // vamos adicionar o novo destinatario a lista 
            // que é um destinatario/string que esta na nossa lista de destinatarios 
            // recebida por parametro 
            Destinatario.AddRange(destinatario.Select(d => new MailboxAddress(d)));
            Assunto = assunto;
            Conteudo = $"http://localhost:5001/ativa?UsuarioId={usuarioId}&CodigoDeAtivacao={codigo}";

            // como vamos garantir que nossa mensagem foi enviada corretamente ?
        }
    }
}