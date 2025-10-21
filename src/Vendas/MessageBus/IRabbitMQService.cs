using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vendas.MessageBus
{
    public interface IRabbitMQService
    {
        Task PublishAsync(string queueName, string message);
    }
}