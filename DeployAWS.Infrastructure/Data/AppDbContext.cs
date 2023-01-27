using Microsoft.EntityFrameworkCore;
using DeployAWS.Domain.Entitys;
using System;
using System.Linq;

namespace DeployAWS.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Client> Clientes { get; set; }
        public DbSet<Product> Produtos { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Client>()
                .Property(p => p.Nome)
                .HasMaxLength(80);

            builder.Entity<Client>()
                .Property(p => p.Sobrenome)
                .HasMaxLength(80);

            builder.Entity<Client>()
                .Property(p => p.Email)
                .HasMaxLength(80);

            builder.Entity<Client>()
                .HasData(
                    new Client { Id = 1, Nome = "Cliente 1", Sobrenome = "Teste 1", Email = "cliente1@teste.com", DataCadastro = DateTime.Now, IsAtivo = true },
                    new Client { Id = 2, Nome = "Cliente 2", Sobrenome = "Teste 2", Email = "cliente2@teste.com", DataCadastro = DateTime.Now, IsAtivo = true },
                    new Client { Id = 3, Nome = "Cliente 3", Sobrenome = "Teste 3", Email = "cliente3@teste.com", DataCadastro = DateTime.Now, IsAtivo = true },
                    new Client { Id = 4, Nome = "Cliente 4", Sobrenome = "Teste 4", Email = "cliente4@teste.com", DataCadastro = DateTime.Now, IsAtivo = true },
                    new Client { Id = 5, Nome = "Cliente 5", Sobrenome = "Teste 5", Email = "cliente5@teste.com", DataCadastro = DateTime.Now, IsAtivo = true }
                );

            builder.Entity<Product>()
                .Property(p => p.Nome)
                .HasMaxLength(100);

            builder.Entity<Product>()
                .Property(p => p.Valor)
                .HasColumnType("decimal(10, 2)");

            builder.Entity<Product>()
                .HasData(
                    new Product { Id = 1, Nome = "Lápis", IsDisponivel = true },
                    new Product { Id = 2, Nome = "Caderno", IsDisponivel = true },
                    new Product { Id = 3, Nome = "Borracha", IsDisponivel = true },
                    new Product { Id = 4, Nome = "Caneta", IsDisponivel = true },
                    new Product { Id = 5, Nome = "Apontador", IsDisponivel = true }
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