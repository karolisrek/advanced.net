using System.Text;
using System.Text.Json;
using Catalog.BLL.Entities;
using Catalog.BLL.Interfaces.Handlers;
using RabbitMQ.Client;


namespace Catalog.BLL.Handlers
{
    public class RabbitMQMessagePublisher : IQueueMessagePublisher
    {
        public void SendMessage<T>(T message)
        {
            var factory = new ConnectionFactory { HostName = "localhost" };
            var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message));
            channel.BasicPublish("", "product", true, null, body);
        }
    }
}
