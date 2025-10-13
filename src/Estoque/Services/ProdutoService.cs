using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using Estoque.Models;
using Estoque.Repositories.Interfaces;
using Estoque.Services.Interfaces;

namespace Estoque.Services
{
    public class ProdutoService :IProdutoService
    {
        private readonly IProdutoRepository _repository;

        public ProdutoService(IProdutoRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Produto>> ListarProdutosAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Produto?> ObterPorIdAsync (int id)
        {
            return await _repository.GetByIdAsync(id);
        }
        public async Task<Produto> CriarProdutoAsync(Produto produto)
        {
            await _repository.AddAsync(produto);
            await _repository.SaveChangesAsync();

            return produto;
        }

        public async Task<Produto?> AtualizarProdutoAsync(int id, Produto produto)
        {
            var existente = await _repository.GetByIdAsync(id);
            if (existente == null)
                return null;

            existente.Nome = produto.Nome;
            existente.Valor = produto.Valor;
            existente.Quantidade = produto.Quantidade;

            await _repository.UpdateAsync(existente);
            await _repository.SaveChangesAsync();

            return existente;
        }
        
        public async Task<bool> DeletarProdutoAsync(int id)
        {
            var produto = await _repository.GetByIdAsync(id);
            if (produto == null)
                return false;

            await _repository.DeleteAsync(produto);
            await _repository.SaveChangesAsync();

            return true;
        }
    }
}