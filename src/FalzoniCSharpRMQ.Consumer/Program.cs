using FalzoniCSharpRMQ.Common.Config;
using FalzoniCSharpRMQ.Consumer.Workers;
using System;
using System.Collections.Generic;

namespace FalzoniCSharpRMQ.Consumer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Criando novo consumer");
                var consumer = new ConsumerWorker();

                Console.WriteLine("Cosumindo mensagens");

                // Consumo de todas as mensagens
                MessageWorker.ConsumeAllMessages(consumer, new string[] { RabbitMQAttributes.QUEUE_PRODUCT_DATA, RabbitMQAttributes.QUEUE_PRODUCT_LOG });
                //MessageWorker.ConsumeAllMessagesAsync(consumer, new string[] { RabbitMQAttributes.QUEUE_PRODUCT_DATA, RabbitMQAttributes.QUEUE_PRODUCT_LOG });

                // Consumo de mensagens individuais
                //MessageWorker.ConsumeSingleMessage(consumer, RabbitMQAttributes.QUEUE_PRODUCT_DATA);
                //MessageWorker.ConsumeSingleMessage(consumer, RabbitMQAttributes.QUEUE_PRODUCT_LOG);

                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
    }
}
