using FalzoniCSharpRMQ.Common.Config;
using FalzoniCSharpRMQ.Consumer.Workers;
using RabbitMQ.Client;
using System;

namespace FalzoniCSharpRMQ.Consumer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Criando novo consumer");
                var c1 = new ConsumerWorker();

                Console.WriteLine("Cosumindo mensagens");
                string returnLog = c1.Consume(RabbitMQAttributes.EXG_DIRECT_NAME, RabbitMQAttributes.QUEUE_PRODUCT_LOG, ExchangeType.Direct, RabbitMQAttributes.RK_PRODUCT_LOG);
                Console.WriteLine("Recebida a mensagem: " + returnLog);
                Console.WriteLine("Processo concluído!");

                var c2 = new ConsumerWorker();
                Console.WriteLine("Cosumindo mensagens");
                string returnData = c2.Consume(RabbitMQAttributes.EXG_TOPIC_NAME, RabbitMQAttributes.QUEUE_PRODUCT_DATA, ExchangeType.Topic, RabbitMQAttributes.RK_PRODUCT_DATA);
                Console.WriteLine("Recebida a mensagem: " + returnData);
                Console.WriteLine("Processo concluído!");

                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
    }
}
