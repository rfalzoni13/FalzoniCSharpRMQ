using FalzoniCSharpRMQ.Common.Config;
using System;
using System.Collections;
using System.Collections.Generic;

namespace FalzoniCSharpRMQ.Consumer.Workers
{
    internal static class MessageWorker
    {
        internal static void ConsumeSingleMessage(ConsumerWorker consumer, string queueName)
        {
            List<string> messages = new List<string>();

            uint messageCount = consumer.GetMessageCount(queueName);

            for(int i = 0; i < messageCount; i++)
            {
                messages.AddRange(consumer.Consume(queueName));
            }

            PrintMessages(messages);
        }

        internal static void ConsumeAllMessages(ConsumerWorker consumer, string[] queues)
        {
            List<string> messages = new List<string>();

            for(int i = 0; i < queues.Length; i++)
            {
                messages.AddRange(consumer.ConsumeAll(queues[i]));
            }

            PrintMessages(messages);
        }

        internal static async void ConsumeAllMessagesAsync(ConsumerWorker consumer, string[] queues)
        {
            List<string> messages = new List<string>();

            for (int i = 0; i < queues.Length; i++)
            {
                messages.AddRange(await consumer.ConsumeAllAsync(queues[i]));
            }

            PrintMessages(messages);
        }

        private static void PrintMessages(List<string> messages)
        {
            foreach (var message in messages) 
            {
                Console.WriteLine("Exibida a seguinte mensagem: " + (!string.IsNullOrEmpty(message) ? message : "Vazia"));
            }
        }
    }
}
