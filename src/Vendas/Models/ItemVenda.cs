using System;
using System.Text.Json.Serialization;

namespace Vendas.Models
{
    public class ItemVenda
    {
        public int Id { get; set; }

        public int VendaId { get; set; }

        [JsonIgnore]
        public Venda? Venda { get; set; }

        public int ProdutoId { get; set; }        // ID do produto no estoque
        public string NomeProduto { get; set; } = string.Empty;   // Snapshot do nome
        public decimal PrecoUnitario { get; set; } // Snapshot do preço no momento da venda
        public int Quantidade { get; set; }
        public decimal Subtotal { get; set; }
    }
}
