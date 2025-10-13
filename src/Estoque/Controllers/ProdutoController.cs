using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Estoque.Models;
using Estoque.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Estoque.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProdutoController:ControllerBase
    {
        private readonly IProdutoService _service;

        public ProdutoController(IProdutoService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var produtos = await _service.ListarProdutosAsync();
            return Ok(produtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var produto = await _service.ObterPorIdAsync(id);
            if (produto == null)
                return NotFound("Produto Não encontrado");
            return Ok(produto);
        }

        [HttpPost]
        public async Task<IActionResult> Create (int id, [FromBody] Produto produto)
        {
            var novo = await _service.CriarProdutoAsync(produto);
            return CreatedAtAction(nameof(GetById), new { id = novo.Id }, novo);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Produto produto)
        {
            var atualizado = await _service.AtualizarProdutoAsync(id, produto);
            if (atualizado == null)
                return NotFound("Produto nao encontrado para atualização");
            return Ok(atualizado);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var sucesso = await _service.DeletarProdutoAsync(id);

            if (!sucesso)
                return NotFound("Produto nao encontrado para ser Deletado");
            return NoContent();
        }
    }
}