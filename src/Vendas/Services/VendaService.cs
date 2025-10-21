using System.Text.Json;
using Vendas.Models;
using Vendas.Interfaces;
using Vendas.MessageBus;

namespace Vendas.Services
{
    public class VendaService : IVendaService
    {
        private readonly IVendaRepository _repository;
        private readonly IRabbitMQService _rabbitMQService;

        public VendaService(IVendaRepository repository, IRabbitMQService rabbitMQService)
        {
            _repository = repository;
            _rabbitMQService = rabbitMQService;
        }

        public async Task<IEnumerable<Venda>> ObterTodasAsync()
        {
            return await _repository.ObterTodasAsync();
        }

        public async Task<Venda?> ObterPorIdAsync(int id)
        {
            return await _repository.ObterPorIdAsync(id);
        }

        public async Task<Venda> CriarVendaAsync(Venda venda)
        {
            // 🧮 Regra de negócio: calcular total automaticamente
            venda.Total = venda.Itens.Sum(i => i.Subtotal);

            // 🕒 Define a data de criação
            venda.DataVenda = DateTime.Now;

            // 🏷️ Status inicial
            venda.Status = "Pendente";

            // 💾 Persiste a venda
            var novaVenda = await _repository.AdicionarAsync(venda);

            // 📦 Envia mensagem para o RabbitMQ
            var message = new
            {
                VendaId = novaVenda.Id,
                Itens = novaVenda.Itens.Select(i => new { i.ProdutoId, i.Quantidade }).ToList()
            };
            var mensagemJson = JsonSerializer.Serialize(message);
            await _rabbitMQService.PublishAsync("venda_realizada", mensagemJson);

            return novaVenda;
        }

        public async Task AtualizarVendaAsync(Venda venda)
        {
            venda.Total = venda.Itens.Sum(i => i.Subtotal);
            await _repository.AtualizarAsync(venda);
        }

        public async Task RemoverVendaAsync(int id)
        {
            await _repository.RemoverAsync(id);
        }
    }
}
