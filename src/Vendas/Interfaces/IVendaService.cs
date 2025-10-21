using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vendas.Models;
namespace Vendas.Interfaces
{
public interface IVendaService
    {
        Task<IEnumerable<Venda>> ObterTodasAsync();
        Task<Venda?> ObterPorIdAsync(int id);
        Task<Venda> CriarVendaAsync(Venda venda);
        Task AtualizarVendaAsync(Venda venda);
        Task RemoverVendaAsync(int id);
    }
}