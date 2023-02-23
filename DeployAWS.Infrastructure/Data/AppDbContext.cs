using DeployAWS.Domain.Entitys;
using Microsoft.EntityFrameworkCore;

namespace DeployAWS.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            #region Customer

            builder.Entity<Customer>()
                .Property(p => p.Id)
                .HasMaxLength(37);

            builder.Entity<Customer>()
                .Property(p => p.UserName)
                .HasMaxLength(20);

            builder.Entity<Customer>()
                .Property(p => p.FirstName)
                .HasMaxLength(20);

            builder.Entity<Customer>()
                .Property(p => p.LastName)
                .HasMaxLength(20);

            builder.Entity<Customer>()
                .Property(p => p.EmailAddress)
                .HasMaxLength(40);

            builder.Entity<Customer>()
                .Property(p => p.Profile)
                .HasMaxLength(20);

            builder.Entity<Customer>()
                .Property(p => p.Password)
                .HasMaxLength(16);

            builder.Entity<Customer>()
                .Property(p => p.CreateDate);

            builder.Entity<Customer>()
                .Property(p => p.ModifiedDate);

            builder.Entity<Customer>()
                .Property(p => p.IsActive);

            #endregion Customer

            #region Order

            //builder.Entity<Order>()
            //    .Property(p => p.Id)
            //    .HasMaxLength(37);

            //builder.Entity<Order>()
            //    .Property(p => p.Customer);

            //builder.Entity<Order>()
            //    .Property(p => p.Items);

            //builder.Entity<Order>()
            //    .Property(p => p.Status);

            //builder.Entity<Order>()
            //    .Property(p => p.CreateDate);

            //builder.Entity<Order>()
            //    .Property(p => p.ModifiedDate);

            #endregion Order
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