using System.Text;
using System.Text.Json;
using Cart.DAL;
using Cart.DAL.Models;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Cart.BLL.BackgroundTasks
{
    public class ProductQueueListener : IDisposable
    {
        private CartRepository _cartRepository;
        private IConnection _connection;
        private IModel _channel;
        private const string QueueName = "product";

        public ProductQueueListener(IConfiguration configuration, CartRepository cartRepository)
        {
            _cartRepository = cartRepository;

            var factory = new ConnectionFactory { HostName = configuration.GetConnectionString("QueueHostName") };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
        }

        public void StartListnening()
        {
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (model, ea) =>
            {
                var product = GetProduct(ea.Body.ToArray());
                _channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);

                if (product is not null)
                {
                    _cartRepository.UpdateItems(product);
                }
            };

            _channel.BasicConsume(QueueName, false, consumer);
        }

        private Product? GetProduct(byte[] body)
        {
            try
            {
                var message = Encoding.UTF8.GetString(body);
                return JsonSerializer.Deserialize<Product>(message);
            }
            catch
            {
                return null;
            }
        }

        public void Dispose()
        {
            _connection?.Dispose();
            _channel?.Dispose();
        }
    }
}
