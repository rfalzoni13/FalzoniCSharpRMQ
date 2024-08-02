using FalzoniCSharpRMQ.Common.Config;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Threading.Tasks;

namespace FalzoniCSharpRMQ.Consumer.Workers
{
    public class ConsumerWorker
    {
        public virtual string Consume(string exchangeName, string exchangeType, string queueName, string routingKey)
        {
            string message = null;

            IModel channel = RabbitMQConfig.GetChannel(exchangeName, exchangeType, queueName, routingKey);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (ch, ea) =>
            {
                var body = ea.Body.ToArray();
                // copy or deserialise the payload
                // and process the message
                // ...

                message = Encoding.UTF8.GetString(body);

                channel.BasicAck(ea.DeliveryTag, false);
            };

            // this consumer tag identifies the subscription
            // when it has to be cancelled
            channel.BasicConsume(queueName, false, consumer);

            return message;
        }

        public virtual string AsyncConsume(string exchangeName, string exchangeType, string queueName, string routingKey)
        {
            string message = null;

            IModel channel = RabbitMQConfig.GetChannel(exchangeName, exchangeType, queueName, routingKey);
            
            var consumer = new AsyncEventingBasicConsumer(channel);
            consumer.Received += async (ch, ea) =>
            {
                var body = ea.Body.ToArray();
                // copy or deserialise the payload
                // and process the message
                // ...

                message = Encoding.UTF8.GetString(body);

                channel.BasicAck(ea.DeliveryTag, false);
                await Task.Yield();
            };

            // this consumer tag identifies the subscription
            // when it has to be cancelled
            string consumerTag = channel.BasicConsume(queueName, false, consumer);

            return message;
        }
    }
}
