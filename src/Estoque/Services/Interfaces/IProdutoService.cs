using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Estoque.Models;

namespace Estoque.Services.Interfaces
{
    public interface IProdutoService
    {
        Task<IEnumerable<Produto>> ListarProdutosAsync();
        Task<Produto?> ObterPorIdAsync(int id);
        Task<Produto> CriarProdutoAsync(Produto produto);
        Task<Produto?> AtualizarProdutoAsync(int id, Produto produto);
        Task<bool> DeletarProdutoAsync(int id);
    }
}