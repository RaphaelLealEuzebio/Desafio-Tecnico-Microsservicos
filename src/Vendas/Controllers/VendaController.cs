using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Vendas.Interfaces;
using Vendas.Models;

namespace Vendas.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VendaController : ControllerBase
    {
        private readonly IVendaService _service;

        public VendaController(IVendaService service)
        {
            _service = service;
        }

        // 🔹 GET: api/venda
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var vendas = await _service.ObterTodasAsync();
            return Ok(vendas);
        }

        // 🔹 GET: api/venda/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var venda = await _service.ObterPorIdAsync(id);
            if (venda == null)
                return NotFound($"Venda com ID {id} não encontrada.");

            return Ok(venda);
        }

        // 🔹 POST: api/venda
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Venda venda)
        {
            if (venda == null || venda.Itens == null || !venda.Itens.Any())
                return BadRequest("Venda deve conter ao menos um item.");

            var novaVenda = await _service.CriarVendaAsync(venda);
            return CreatedAtAction(nameof(GetById), new { id = novaVenda.Id }, novaVenda);
        }

        // 🔹 PUT: api/venda/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Venda venda)
        {
            if (id != venda.Id)
                return BadRequest("ID da URL e da venda não coincidem.");

            var existente = await _service.ObterPorIdAsync(id);
            if (existente == null)
                return NotFound($"Venda com ID {id} não encontrada.");

            await _service.AtualizarVendaAsync(venda);
            return NoContent();
        }

        // 🔹 DELETE: api/venda/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existente = await _service.ObterPorIdAsync(id);
            if (existente == null)
                return NotFound($"Venda com ID {id} não encontrada.");

            await _service.RemoverVendaAsync(id);
            return NoContent();
        }
    }
}