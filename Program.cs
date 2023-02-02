// See https://aka.ms/new-console-template for more information

using System.Text.Json;
using System.Net.Mail;
using System.Net;
using EmailService;

namespace TrabalhoInoa {
   
    
    
    public class Program {
        public static void Main(){

            string fileName = "input.json";
            string jsonString = File.ReadAllText(fileName);


            Credencial  credencial = JsonSerializer.Deserialize<Credencial>(jsonString);
            EmailSender emailSender = new EmailSender(credencial, "lucastavasousa@gmail.com");

            emailSender.sendEmailParaVender(1,1, "BRL","USD");
        }


        
    }
}


