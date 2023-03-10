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


        public void sendEmailParaVender(double valor_atual, double valor_de_venda_referenca, string moedaOrigem, string moedaDestino){
            smtpClient.Send(emailOrigem, emailDestino, "Aconselhamento sobre a venda do ativo ", $@"
                Conforme o valor de venda de referência inserido no sistema e o valor da cotação {moedaOrigem} para {moedaDestino}, é recomendado efetuar a venda do ativo.

                A motivação disso é que o valor referente de venda inserido, {valor_de_venda_referenca}, é menor que o valor atual do ativo, {valor_atual}.

                Atenciosamente,

                Projeto Teste

                Envio automático. Favor não responder este e-mail. 

            ");
        }

        public void sendEmailParaCompra(double valor_atual, double valor_de_compra_referenca, string moedaOrigem, string moedaDestino){
            smtpClient.Send(emailOrigem, emailDestino, "Aconselhamento sobre a compra do ativo", $@"
                Conforme o valor de compra de referência inserido no sistema e o valor da cotação {moedaOrigem} para {moedaDestino}, é recomendado efetuar a compra do ativo.

                A motivação disso é que o valor referente de compra inserido, {valor_de_compra_referenca}, é maior que o valor atual do ativo, {valor_atual}.

                Atenciosamente,

                Projeto Teste

                Envio automático. Favor não responder este e-mail. 

            ");
        }


    }

}