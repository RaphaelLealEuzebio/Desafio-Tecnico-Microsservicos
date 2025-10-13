using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Estoque.Models;
using Microsoft.EntityFrameworkCore;

namespace Estoque.Data
{
    public class EstoqueContext:DbContext
    {
        public EstoqueContext(DbContextOptions<EstoqueContext> options)
            : base(options)
        {
        }

        public DbSet<Produto> Produtos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Produto>().Property(p => p.Nome).IsRequired().HasMaxLength(100);
        }
    }
}