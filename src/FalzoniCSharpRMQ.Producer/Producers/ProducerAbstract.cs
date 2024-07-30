using FalzoniCSharpRMQ.Common.Config;
using RabbitMQ.Client;

namespace FalzoniCSharpRMQ.Producer.Producers
{
    internal abstract class ProducerAbstract
    {
        internal virtual void ProduceDirect(string message, string queueName, string routingKey)
        {
            Produce(message, RabbitMQAttributes.EXG_DIRECT_NAME, queueName, ExchangeType.Direct, routingKey);
        }

        internal virtual void ProduceTopic(string message, string queueName, string routingKey)
        {
            Produce(message, RabbitMQAttributes.EXG_TOPIC_NAME, queueName, ExchangeType.Topic, routingKey);
        }

        internal virtual void ProduceFanout(string message, string queueName)
        {
            Produce(message, RabbitMQAttributes.EXG_FANOUT_NAME, queueName, ExchangeType.Fanout, null);
        }

        private void Produce(string message, string exchangeName, string queueName, string exchangeType, string routingKey)
        {
            IModel channel = RabbitMQConfig.GetChannel(exchangeName, exchangeType, queueName, routingKey);

            byte[] messageBodyBytes = System.Text.Encoding.UTF8.GetBytes(message);

            //Opções adicionais
            //IBasicProperties props = channel.CreateBasicProperties();
            //props.ContentType = "text/plain";
            //props.DeliveryMode = 2;
            //props.Headers = new Dictionary<string, object>();
            //props.Headers.Add("parameter1", "teste");
            //props.Headers.Add("parameter2", "teste2");
            //props.Expiration = "36000000";
            //channel.BasicPublish(exchangeName, routingKey, props, messageBodyBytes);

            channel.BasicPublish(exchangeName, routingKey, null, messageBodyBytes);
        }
    }
}
