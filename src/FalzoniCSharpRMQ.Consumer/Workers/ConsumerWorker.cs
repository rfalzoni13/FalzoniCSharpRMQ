using FalzoniCSharpRMQ.Common.Config;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace FalzoniCSharpRMQ.Consumer.Workers
{
    public class ConsumerWorker
    {
        private List<string> _messages;

        public virtual uint GetMessageCount(string queueName)
        {
            IModel channel = RabbitMQConfig.GetChannel();

            return channel.MessageCount(queueName);
        }

        public virtual List<string> Consume(string queueName)
        {
            _messages = new List<string>();

            IModel channel = RabbitMQConfig.GetChannel();

            BasicGetResult result = channel.BasicGet(queueName, true);

            if (result == null)
            {
                _messages.Add("Nenhuma mensagem nesta fila");

            }

            IBasicProperties props = result.BasicProperties;
            ReadOnlyMemory<byte> body = result.Body;

            var message = Encoding.UTF8.GetString(body.ToArray());

            _messages.Add(message);

            // acknowledge receipt of the message
            channel.BasicAck(result.DeliveryTag, false);

            return _messages;
        }

        public virtual List<string> ConsumeAll(string queueName)
        {
            _messages = new List<string>();

            IModel channel = RabbitMQConfig.GetChannel();

            using (var signal = new ManualResetEvent(false))
            {
                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (ch, ea) =>
                {
                    var body = ea.Body.ToArray();
                    // copy or deserialise the payload
                    // and process the message
                    // ...

                    var message = Encoding.UTF8.GetString(body);

                    _messages.Add(message);

                    channel.BasicAck(ea.DeliveryTag, false);

                    signal.Set();
                };

                // this consumer tag identifies the subscription
                // when it has to be cancelled
                string consumerTag = channel.BasicConsume(queueName, false, consumer);

                signal.WaitOne(TimeSpan.FromSeconds(30));

                channel.BasicCancel(consumerTag);
            }


            return _messages;
        }

        public virtual async Task<List<string>> ConsumeAllAsync(string queueName)
        {
            _messages = new List<string>();

            IModel channel = RabbitMQConfig.GetChannel(true);

            using (var signal = new ManualResetEvent(false))
            {
                var consumer = new AsyncEventingBasicConsumer(channel);
                consumer.Received += async (ch, ea) =>
                {
                    var body = ea.Body.ToArray();
                    // copy or deserialise the payload
                    // and process the message
                    // ...

                    var message = Encoding.UTF8.GetString(body);

                    _messages.Add(message);

                    channel.BasicAck(ea.DeliveryTag, false);

                    signal.Set();

                    await Task.Yield();
                };

                // this consumer tag identifies the subscription
                // when it has to be cancelled
                string consumerTag = channel.BasicConsume(queueName, false, consumer);

                signal.WaitOne(TimeSpan.FromSeconds(30));

                channel.BasicCancel(consumerTag);
            }


            return await Task.FromResult(_messages);
        }
    }
}
