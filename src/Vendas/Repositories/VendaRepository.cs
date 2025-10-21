using Microsoft.EntityFrameworkCore;
using Vendas.Data;
using Vendas.Models;
using Vendas.Interfaces;

namespace Vendas.Repositories
{
    public class VendaRepository : IVendaRepository
    {
        private readonly VendasDbContext _context;

        public VendaRepository(VendasDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Venda>> ObterTodasAsync()
        {
            return await _context.Vendas
                .Include(v => v.Itens)
                .ToListAsync();
        }

        public async Task<Venda?> ObterPorIdAsync(int id)
        {
            return await _context.Vendas
                .Include(v => v.Itens)
                .FirstOrDefaultAsync(v => v.Id == id);
        }

        public async Task<Venda> AdicionarAsync(Venda venda)
        {
            _context.Vendas.Add(venda);
            await _context.SaveChangesAsync();
            return venda;
        }

        public async Task AtualizarAsync(Venda venda)
        {
            _context.Entry(venda).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task RemoverAsync(int id)
        {
            var venda = await _context.Vendas.FindAsync(id);
            if (venda != null)
            {
                _context.Vendas.Remove(venda);
                await _context.SaveChangesAsync();
            }
        }
    }
}
