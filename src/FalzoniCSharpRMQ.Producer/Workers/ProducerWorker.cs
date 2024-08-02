using FalzoniCSharpRMQ.Common.Config;
using RabbitMQ.Client;

namespace FalzoniCSharpRMQ.Producer.Workers
{
    public class ProducerWorker
    {
        public virtual void Produce(string message, string exchangeName, string queueName, string exchangeType, string routingKey)
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
