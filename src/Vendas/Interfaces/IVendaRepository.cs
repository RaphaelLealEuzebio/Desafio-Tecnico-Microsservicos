using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vendas.Models;

namespace Vendas.Interfaces
{
    public interface IVendaRepository
    {
        Task<IEnumerable<Venda>> ObterTodasAsync();
        Task<Venda?> ObterPorIdAsync(int id);
        Task<Venda> AdicionarAsync(Venda venda);
        Task AtualizarAsync(Venda venda);
        Task RemoverAsync(int id);
    }
}