using System;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;
using UsuariosApi.Models;

namespace UsuariosApi.Services
{
    public class EmailService
    {
        private IConfiguration _configuration;
        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration; 
        }
        public void EnviarEmail(string[] destinatario, string assunto, int usuarioId, string code)
        {
            // mensagem que vamos gerar e as informações que vao compor 
            Mensagem mensagem = new Mensagem(destinatario, assunto, usuarioId, code);
            // precisamos converter a mensagem em si na mensagem de um email
            var mensagemDeEmail = CriaCorpoDoEmail(mensagem);
            // aqui vamos enviar a mensagem de email 
            Enviar(mensagemDeEmail);
        }

        private MimeMessage CriaCorpoDoEmail(Mensagem mensagem)
        {
        // vai fazer as requisições e conversões necessarias para que enviemos as mensagens 
        // de forma correta
            var mensagemDeEmail = new MimeMessage();
            // precisamos converter para um mail box address para que não seja puramente uma string
            mensagemDeEmail.From.Add(new MailboxAddress(
                                    _configuration.GetValue<string>("EmailSetting:From")));
            // precisamos adicionar ao destinatario a mensagem 
            mensagemDeEmail.To.AddRange(mensagem.Destinatario);
            mensagemDeEmail.Subject = mensagem.Assunto;
            // o corpo do email (aqui está o tipo dele) { texto especifico do email }
            mensagemDeEmail.Body = new TextPart(MimeKit.Text.TextFormat.Text) {
                Text = mensagem.Conteudo
            };
            return mensagemDeEmail;
        }

        // recebe a mensagem de email que acabamos de fazer
        private void Enviar(MimeMessage mensagemDeEmail)
        {
            // usando um cliente 
            using(var client = new SmtpClient())
            {
                try
                {
                    // vamos tentar conectar o email ao nosso servidor de email
                    // vamos pegar o valor das configurações que eu defini no json
                    // carregamos o valor dessa configuração
                    // o segundo parâmetro é o número da porta  
                    client.Connect(_configuration.GetValue<string>("EmailSetting:SmptServer"), 
                                                        _configuration.GetValue<int>("EmailSettings:Port"), true); 
                    client.AuthenticationMechanisms.Remove("XOUATH2");
                    // aqui vamos autenticar com o nosso email e nossa senha 
                    client.Authenticate(_configuration.GetValue<string>("EmailSetting:From"), 
                                        _configuration.GetValue<string>("EmailSetting:Password"));
                    client.Send(mensagemDeEmail);
                }
                catch 
                {   
                    throw;
                }
                finally
                {
                    client.Disconnect(true);
                    // liberar os recursos 
                    client.Dispose();
                }
            }
        }

    }
}