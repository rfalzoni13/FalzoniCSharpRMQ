using RabbitMQ.Client;

namespace FalzoniCSharpRMQ.Common.Config
{
    public class RabbitMQConfig
    {
        public static IModel GetChannel(bool consumersAsync = false)
        {
            IConnection conn = GetConnection(consumersAsync);

            IModel channel = conn.CreateModel();

            return channel;
        }

        public static IModel GetChannel(string exchangeName, string exchangeType, string queueName, string routingKey)
        {
            IConnection conn = GetConnection();

            IModel channel = conn.CreateModel();

            channel.ExchangeDeclare(exchangeName, exchangeType, false, false, null);
            channel.QueueDeclare(queueName, false, false, false, null);
            channel.QueueBind(queueName, exchangeName, routingKey, null);

            //channel.QueueDeclareNoWait(queueName, true, false, false, null);

            return channel;
        }

        private static IConnection GetConnection(bool consumersAsync = false)
        {
            ConnectionFactory factory = new ConnectionFactory();
            // "guest"/"guest" by default, limited to localhost connections
            factory.UserName = "guest";
            factory.Password = "guest";
            factory.VirtualHost = "/";
            factory.HostName = "localhost";
            factory.Port = 5672;

            //factory.ClientProvidedName = "app: falzoni component:event-consumer";
            factory.DispatchConsumersAsync = consumersAsync;

            IConnection conn = factory.CreateConnection();
            return conn;
        }
    }
}
