using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;
using Estoque.DTOs;
using Estoque.Services.Interfaces;

namespace Estoque.Services
{
    public class RabbitMQConsumerService : IRabbitMQConsumerService
    {
        private readonly IProdutoService _produtoService;
        private readonly ConnectionFactory _factory;

        public RabbitMQConsumerService(IProdutoService produtoService)
        {
            _produtoService = produtoService;
            _factory = new ConnectionFactory
            {
                HostName = "rabbitmq",
                UserName = "guest",
                Password = "guest"
            };
        }

        public async Task StartConsumingAsync()
        {
            // Criar a conexão assíncrona
            var connection = await _factory.CreateConnectionAsync();

            // Criar o canal assíncrono
            var channel = await connection.CreateChannelAsync();

            // Declarar a fila
            await channel.QueueDeclareAsync("venda_realizada", durable: true, exclusive: false, autoDelete: false);

            // Criar o consumidor assíncrono
            var consumer = new AsyncEventingBasicConsumer(channel);

            // Assinar o evento de recebimento de mensagens
            consumer.ReceivedAsync += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

                var vendaDto = JsonSerializer.Deserialize<VendaDto>(message);

                if (vendaDto != null)
                {
                    await _produtoService.DecrementarEstoqueAsync(vendaDto.Itens);
                }

                // Confirmar o recebimento da mensagem
                await channel.BasicAckAsync(ea.DeliveryTag, multiple: false);
            };

            // Iniciar o consumo da fila
            await channel.BasicConsumeAsync("venda_realizada", autoAck: false, consumer: consumer);
        }
    }
}
