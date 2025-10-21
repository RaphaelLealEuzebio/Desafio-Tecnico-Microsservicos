using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Estoque.Services.Interfaces
{
    public interface IRabbitMQConsumerService
    {
        Task StartConsumingAsync();
    }
}