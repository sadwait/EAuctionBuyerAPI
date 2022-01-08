using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuyerAPI.MessageBroker
{
    public class RabbitMqProducer:IRabbitMqProducer
    {
        private readonly ConnectionFactory _connectionFactory;
        private IConnection _connection;
        private IModel channel;
        private string exchangeName = "BidQueue";

        public RabbitMqProducer(ConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
            CreateChannel();
        }

        private void CreateChannel()
        {
            if (_connection == null || _connection.IsOpen == false)
                _connection = _connectionFactory.CreateConnection();

            if (channel == null || channel.IsOpen == false)
            {
                channel = _connection.CreateModel();

                channel.QueueDeclare(queue: exchangeName,
                                  durable: false,
                                  exclusive: false,
                                  autoDelete: false,
                                  arguments: null);
            }
        }

        public void Publish(string message)
        {
            var body = Encoding.UTF8.GetBytes(message);

            channel.BasicPublish(exchange: "",
                                 routingKey: exchangeName,
                                 basicProperties: null,
                                 body: body);
        }
    }
}
