using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace Vendas.MessageBus
{
    public class RabbitMQService : IRabbitMQService
    {
        private readonly ConnectionFactory _factory;

        public RabbitMQService(IConfiguration configuration)
        {
            _factory = new ConnectionFactory
            {
                HostName = configuration["RabbitMQ:HostName"] ?? "localhost",
                UserName = configuration["RabbitMQ:UserName"] ?? "guest",
                Password = configuration["RabbitMQ:Password"] ?? "guest",
            };
        }

        public async Task PublishAsync(string queueName, string message)
        {
            await using var connection = await _factory.CreateConnectionAsync();
            await using var channel = await connection.CreateChannelAsync();

            await channel.QueueDeclareAsync(queue: queueName,
                durable: true,
                exclusive: false,
                autoDelete: false);

            var body = Encoding.UTF8.GetBytes(message);

            var props = new BasicProperties
            {
                ContentType = "application/json"
            };

            await channel.BasicPublishAsync(
                exchange: "",
                routingKey: queueName,
                mandatory: false,
                basicProperties: props,
                body: body);
        }
    }
}