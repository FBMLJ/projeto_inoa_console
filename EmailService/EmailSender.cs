using System.Net.Mail;
using System.Net;
namespace EmailService {
    public class EmailSender {


        public string emailDestino;
        public string emailOrigem;
        private SmtpClient smtpClient;
        public EmailSender(Credencial credencial, string EmailDestino){
            emailOrigem = credencial.email;
            string password =  credencial.password;
            emailDestino = EmailDestino; 
            smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(emailOrigem,password),
                EnableSsl = true,
            };
        }


        public void sendEmailParaVender(float valor_atual, float valor_de_venda_referenca, string moedaOrigem, string moedaDestino){
            smtpClient.Send(emailOrigem, emailDestino, "Aconselhamento sobre a venda do ativo ", $@"
                Conforme o valor de venda de referencia inserido no sistema e o valor da contação {moedaOrigem} para {moedaDestino}, é recomendado efetuar a venda do ativo.

                O resultado desse conselho é motivado que o valor de venda atual, {valor_de_venda_referenca}, ser menor que o {valor_atual}.

                Atenciosamente,

                Projeto Teste

                Envio automático. Favor não responder este e-mail. 

            ");
        }

        public void sendEmailParaCompra(float valor_atual, float valor_de_venda_referenca, string moedaOrigem, string moedaDestino){
            smtpClient.Send(emailOrigem, emailDestino, "Aconselhamento sobre a compra do ativo", $@"
                Conforme o valor de compra de referencia inserido no sistema e o valor da contação {moedaOrigem} para {moedaDestino}, é recomendado efetuar a compra do ativo.

                O resultado desse conselho é motivado que o valor de venda atual, {valor_de_venda_referenca}, ser maior que o {valor_atual}.

                Atenciosamente,

                Projeto Teste

                Envio automático. Favor não responder este e-mail. 

            ");
        }


    }

}