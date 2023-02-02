// See https://aka.ms/new-console-template for more information

using System.Text.Json;
using System.Net.Mail;
using System.Threading;
using System.Net;
using EmailService;
using Newtonsoft.Json.Linq;

namespace TrabalhoInoa {
    public class Program {
        public static string stringUrl;
        public static string stringMoedaOrigem;
        public static string stringMoedaDestino;

        public static double valorReferenciaVenda;

        public static double valorReferenciaCompra;
        public static EmailSender emailSender;

        public static void Main(){

            string fileName = "input.json";
            string jsonString = File.ReadAllText(fileName);


            Credencial  credencial = JsonSerializer.Deserialize<Credencial>(jsonString);
            emailSender = new EmailSender(credencial, "lucastavasousa@gmail.com");

            Console.WriteLine("Digita o código da moeda de origem da contação escolhida");
            stringMoedaOrigem = Console.ReadLine().ToUpper().Trim();
            
            Console.WriteLine("Digita o código da moeda alvo da contação escolhida");
            stringMoedaDestino = Console.ReadLine().ToUpper().Trim();
            stringUrl = $"https://economia.awesomeapi.com.br/json/last/{stringMoedaOrigem}-{stringMoedaDestino}";
            Console.WriteLine($"O link para consultar a api atual foi definida para  {stringUrl}");
            Console.WriteLine($"O valor da cotação atual pedida é de {getValorAtualCotacao()}");

            Console.WriteLine("\nAgora informe o valor referente de venda: ");
            valorReferenciaVenda = double.Parse(Console.ReadLine());

            Console.WriteLine("Agora informe o valor referente de compra");
            valorReferenciaCompra = double.Parse(Console.ReadLine());


            while(true){
                // valor em milisegundos
                Thread.Sleep(30*1000);
                double valorAtual  =  getValorAtualCotacao();
                if (valorAtual > valorReferenciaVenda) {
                    Console.WriteLine("Enviando email de venda");
                    emailSender.sendEmailParaVender(valorAtual, valorReferenciaVenda,stringMoedaOrigem, stringMoedaDestino);
                } else if (valorAtual < valorReferenciaCompra) {
                    Console.WriteLine("Enviando email de compra");
                    emailSender.sendEmailParaCompra(valorAtual, valorReferenciaCompra,stringMoedaOrigem, stringMoedaDestino);
                }

            }

        }

        public static float getValorAtualCotacao(){
            var request = WebRequest.Create(stringUrl);
            request.Method = "GET";
            using var webResponse = request.GetResponse();
            using var webStream = webResponse.GetResponseStream();

            using var reader = new StreamReader(webStream);
            var data = reader.ReadToEnd();
            dynamic d = JObject.Parse(data);
            // Console.WriteLine(d);
            
            return d[stringMoedaOrigem+stringMoedaDestino]["bid"];


        }
    }
}


