using Microsoft.EntityFrameworkCore;
using DeployAWS.Domain.Entitys;
using System;
using System.Linq;

namespace DeployAWS.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Customer> Customers { get; set; }
        //public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Customer>()
                .Property(p => p.Id)
                .HasMaxLength(37);

            builder.Entity<Customer>()
                .Property(p => p.UserName);

            builder.Entity<Customer>()
                .Property(p => p.FirstName);

            builder.Entity<Customer>()
                .Property(p => p.LastName);

            builder.Entity<Customer>()
                .Property(p => p.EmailAddress);

            builder.Entity<Customer>()
                .Property(p => p.Profile);

            builder.Entity<Customer>()
                .Property(p => p.CreateDate);

            builder.Entity<Customer>()
                .Property(p => p.IsActive);

            //builder.Entity<Customer>()
            //    .HasData(
            //        new Customer { Id = Guid.NewGuid().ToString(), FirstName = "Cliente 1", LastName = "Teste 1", EmailAddress = "cliente1@teste.com", CreateDate = DateTime.Now, IsActive  = true },
            //        new Customer { Id = Guid.NewGuid().ToString(), FirstName = "Cliente 2", LastName = "Teste 2", EmailAddress = "cliente2@teste.com", CreateDate = DateTime.Now, IsActive = true },
            //        new Customer { Id = Guid.NewGuid().ToString(), FirstName = "Cliente 3", LastName = "Teste 3", EmailAddress = "cliente3@teste.com", CreateDate = DateTime.Now, IsActive = true },
            //        new Customer { Id = Guid.NewGuid().ToString(), FirstName = "Cliente 4", LastName = "Teste 4", EmailAddress = "cliente4@teste.com", CreateDate = DateTime.Now, IsActive = true },
            //        new Customer { Id = Guid.NewGuid().ToString(), FirstName = "Cliente 5", LastName = "Teste 5", EmailAddress = "cliente5@teste.com", CreateDate = DateTime.Now, IsActive = true }
            //    );

            //builder.Entity<User>()
            //    .Property(p => p.FirstName);

            //builder.Entity<User>()
            //    .Property(p => p.LastName);

            //builder.Entity<User>()
            //    .Property(p => p.UserName);

            //builder.Entity<User>()
            //    .Property(p => p.EmailAddress);

            //builder.Entity<User>()
            //    .Property(p => p.Profile);

            //builder.Entity<User>()
            //    .Property(p => p.CreateDate);

            //builder.Entity<User>()
            //    .HasData(
            //        new User {Id = Guid.NewGuid().ToString(), FirstName = "Marcos", LastName = "Paulo Silva", UserName="mpaulo.silva" ,EmailAddress = "mpaulo.silva@teste.com", CreateDate = DateTime.Now, Profile = "Admin" },
            //        new User {Id = Guid.NewGuid().ToString(), FirstName = "Pedro", LastName = "Cardoso de Mello", UserName="pcardoso.mello", EmailAddress = "pcardoso.mello@teste.com", CreateDate = DateTime.Now, Profile = "Admin" },
            //        new User {Id = Guid.NewGuid().ToString(), FirstName = "Guilherme", LastName = "Silverio", UserName="guilherme.silverio" ,EmailAddress = "guilherme.silverio@teste.com", CreateDate = DateTime.Now, Profile = "Admin" }
            //    );
        }

        //public override int SaveChanges()
        //{
        //    foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("DataCadastro") != null))
        //    {
        //        if (entry.State == EntityState.Added)
        //        {
        //            entry.Property("CreateDate").CurrentValue = DateTime.Now;
        //        }
        //        if (entry.State == EntityState.Modified)
        //        {
        //            entry.Property("CreateDate").IsModified = false;
        //        }
        //    }
        //    return base.SaveChanges();
        //}
    }
}