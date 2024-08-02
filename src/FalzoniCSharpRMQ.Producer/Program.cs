using FalzoniCSharpRMQ.Common.Config;
using FalzoniCSharpRMQ.Producer.Workers;
using RabbitMQ.Client;
using System;

namespace FalzoniCSharpRMQ.Producer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Criando novo producer do exchange Direct");
                var p1 = new ProducerWorker();

                Console.WriteLine("Enviando mensagem");
                p1.Produce("Teste de mensagem Direct", RabbitMQAttributes.EXG_DIRECT_NAME, RabbitMQAttributes.QUEUE_PRODUCT_DATA, ExchangeType.Direct, RabbitMQAttributes.RK_PRODUCT_DATA);
                Console.WriteLine("Processo concluído!");

                Console.WriteLine("Criando novo producer do exchange Topic");
                var p2 = new ProducerWorker();
                
                Console.WriteLine("Enviando mensagem");
                p2.Produce("Teste de mensagem Topic", RabbitMQAttributes.EXG_TOPIC_NAME, RabbitMQAttributes.QUEUE_PRODUCT_LOG, ExchangeType.Topic, RabbitMQAttributes.RK_PRODUCT_LOG);
                Console.WriteLine("Processo concluído!");

                Console.WriteLine("Criando novo producer do exchange Fanout");
                var p3 = new ProducerWorker();
                
                Console.WriteLine("Enviando mensagem");
                p3.Produce("Teste de mensagem Fanout", RabbitMQAttributes.EXG_FANOUT_NAME, RabbitMQAttributes.QUEUE_PRODUCT_LOG, ExchangeType.Fanout, string.Empty);
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
