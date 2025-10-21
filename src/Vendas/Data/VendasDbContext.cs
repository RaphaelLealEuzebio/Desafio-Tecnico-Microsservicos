using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Vendas.Models;

namespace Vendas.Data
{
    public class VendasDbContext: DbContext
    {
        public VendasDbContext(DbContextOptions<VendasDbContext> options)
            : base(options)
        {
        }
        
        public DbSet<Venda> Vendas { get; set; }
        public DbSet<ItemVenda> ItensVenda { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Venda>(entity =>
            {

                entity.ToTable("Vendas");
                entity.HasKey(v => v.Id);
                entity.Property(v => v.Total)
                    .HasPrecision(18, 2);
                entity.Property(v => v.Status)
                    .HasMaxLength(50)
                    .IsRequired();
                entity.HasMany(v => v.Itens)
                    .WithOne(i => i.Venda)
                    .HasForeignKey(i => i.VendaId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<ItemVenda>(entity =>
            {
                entity.ToTable("ItensVenda");
                entity.HasKey(i => i.Id);
                entity.Property(i => i.PrecoUnitario)
                    .HasPrecision(18, 2);
                entity.Property(i => i.Subtotal)
                    .HasPrecision(18, 2);

                // entity.HasOne(i => i.Produto)
                //     .WithMany()
                //     .HasForeignKey(i => i.ProdutoId)
                //     .OnDelete(DeleteBehavior.NoAction);
            });
        }
    }
}