using FalzoniCSharpRMQ.Common.Config;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Threading.Tasks;

namespace FalzoniCSharpRMQ.Consumer.Consumers
{
    internal abstract class ConsumerAbstract
    {
        internal virtual void Consume(string exchangeName, string exchangeType, string queueName, string routingKey)
        {
            IModel channel = RabbitMQConfig.GetChannel(exchangeName, exchangeType, queueName, routingKey);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (ch, ea) =>
            {
                var body = ea.Body.ToArray();
                // copy or deserialise the payload
                // and process the message
                // ...

                var message = Encoding.UTF8.GetString(body);

                Console.WriteLine("Recebida a mensagem: " + message);

                channel.BasicAck(ea.DeliveryTag, false);
            };

            // this consumer tag identifies the subscription
            // when it has to be cancelled
            channel.BasicConsume(queueName, false, consumer);
        }

        internal virtual void AsyncConsume(string exchangeName, string exchangeType, string queueName, string routingKey)
        {
            IModel channel = RabbitMQConfig.GetChannel(exchangeName, exchangeType, queueName, routingKey);
            var consumer = new AsyncEventingBasicConsumer(channel);
            consumer.Received += async (ch, ea) =>
            {
                var body = ea.Body.ToArray();
                // copy or deserialise the payload
                // and process the message
                // ...

                channel.BasicAck(ea.DeliveryTag, false);
                await Task.Yield();
            };

            // this consumer tag identifies the subscription
            // when it has to be cancelled
            string consumerTag = channel.BasicConsume(queueName, false, consumer);
        }
    }
}
