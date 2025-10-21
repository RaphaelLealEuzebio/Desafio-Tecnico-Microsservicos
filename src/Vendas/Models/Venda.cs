using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vendas.Models
{
    public class Venda
    {
        public int Id { get; set; }
        public DateTime DataVenda { get; set; } = DateTime.Now;
        public decimal Total { get; set; }
        public string Status { get; set; } = "pendente";

        public ICollection<ItemVenda> Itens { get; set; }
    }
}