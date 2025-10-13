using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Estoque.Models
{
    [Table("Produtos")]
    public class Produto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome do produto é obrigatio")]
        [StringLength(100)]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "A quantidade precisa ser informada ")]
        public int Quantidade { get; set; }

        [Required(ErrorMessage = "O valor precisa ser informado ")]
        [Column(TypeName = "Decimal(18,2)")]
        public decimal Valor { get; set; }

        public DateTime DataCadastro { get; set; } = DateTime.Now;
    }
}