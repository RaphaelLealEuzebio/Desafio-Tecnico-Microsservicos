using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Estoque.DTOs
{
    public class VendaDto
    {
        public int VendaId { get; set; }
        public List<VendaItemDto> Itens { get; set; } = new();
    }
}