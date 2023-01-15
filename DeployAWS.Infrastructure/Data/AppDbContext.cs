using Microsoft.EntityFrameworkCore;
using DeployAWS.Domain.Entitys;
using System;
using System.Linq;

namespace DeployAWS.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Produto> Produtos { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Cliente>()
                .Property(p => p.Nome)
                .HasMaxLength(80);

            builder.Entity<Cliente>()
                .Property(p => p.Sobrenome)
                .HasMaxLength(80);

            builder.Entity<Cliente>()
                .Property(p => p.Email)
                .HasMaxLength(80);

            builder.Entity<Cliente>()
                .HasData(
                    new Cliente { Id = 1, Nome = "Cliente 1", Sobrenome = "Teste 1", Email = "cliente1@teste.com", DataCadastro = DateTime.Now, IsAtivo = true },
                    new Cliente { Id = 2, Nome = "Cliente 2", Sobrenome = "Teste 2", Email = "cliente2@teste.com", DataCadastro = DateTime.Now, IsAtivo = true },
                    new Cliente { Id = 3, Nome = "Cliente 3", Sobrenome = "Teste 3", Email = "cliente3@teste.com", DataCadastro = DateTime.Now, IsAtivo = true },
                    new Cliente { Id = 4, Nome = "Cliente 4", Sobrenome = "Teste 4", Email = "cliente4@teste.com", DataCadastro = DateTime.Now, IsAtivo = true },
                    new Cliente { Id = 5, Nome = "Cliente 5", Sobrenome = "Teste 5", Email = "cliente5@teste.com", DataCadastro = DateTime.Now, IsAtivo = true }
                );

            builder.Entity<Produto>()
                .Property(p => p.Nome)
                .HasMaxLength(100);

            builder.Entity<Produto>()
                .Property(p => p.Valor)
                .HasColumnType("decimal(10, 2)");

            builder.Entity<Produto>()
                .HasData(
                    new Produto { Id = 1, Nome = "Lápis", IsDisponivel = true },
                    new Produto { Id = 2, Nome = "Caderno", IsDisponivel = true },
                    new Produto { Id = 3, Nome = "Borracha", IsDisponivel = true },
                    new Produto { Id = 4, Nome = "Caneta", IsDisponivel = true },
                    new Produto { Id = 5, Nome = "Apontador", IsDisponivel = true }
                );
        }

        public override int SaveChanges()
        {
            foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("DataCadastro") != null))
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Property("DataCadastro").CurrentValue = DateTime.Now;
                }
                if (entry.State == EntityState.Modified)
                {
                    entry.Property("DataCadastro").IsModified = false;
                }
            }
            return base.SaveChanges();
        }
    }
}