using FalzoniCSharpRMQ.Common.Config;
using FalzoniCSharpRMQ.Consumer.Consumers.Product;
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
                var c1 = new ProductLogConsumer();

                Console.WriteLine("Cosumindo mensagens");
                c1.Consume(RabbitMQAttributes.EXG_DIRECT_NAME, RabbitMQAttributes.QUEUE_PRODUCT_LOG, ExchangeType.Direct, RabbitMQAttributes.RK_PRODUCT_LOG);
                Console.WriteLine("Processo concluído!");

                var c2 = new ProductDataConsumer();
                Console.WriteLine("Cosumindo mensagens");
                c2.Consume(RabbitMQAttributes.EXG_TOPIC_NAME, RabbitMQAttributes.QUEUE_PRODUCT_DATA, ExchangeType.Topic, RabbitMQAttributes.RK_PRODUCT_DATA);
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
