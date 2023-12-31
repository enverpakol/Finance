﻿using Finance.Domain.Entities;
using Finance.Domain.Entities.Common;
using Finance.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Finance.Persistence.Contexts
{
    public class AppData : IdentityDbContext<AppUser, AppRole, int>
    {

        public AppData(DbContextOptions options) : base(options)
        {

        }
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<AppRole> AppRole { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Customer> Customers { get; set; }
        

        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<InvoiceDetail> InvoiceDetails { get; set; }
        public DbSet<StockTransaction> StockTransactions { get; set; }
        public DbSet<PaymentTransaction> PaymentTransaction { get; set; }




        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var datas = ChangeTracker
                .Entries<BaseEntity>();

            foreach (var data in datas)
            {
                _ = data.State switch
                {
                    EntityState.Added => data.Entity.CreatedDate = DateTime.UtcNow,
                    EntityState.Modified => data.Entity.UpdatedDate = DateTime.UtcNow,
                    _ => DateTime.UtcNow,
                };
            }

            return await base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Invoice>()
             .HasOne(d => d.Customer)
             .WithMany(d => d.Invoices)
             .HasForeignKey(d => d.CustomerId)
             .IsRequired(true)
             .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<InvoiceDetail>()
             .HasOne(d => d.Invoice)
             .WithMany(d => d.InvoiceDetails)
             .HasForeignKey(d => d.InvoiceId)
             .IsRequired(true)
             .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<InvoiceDetail>()
             .HasOne(d => d.Stock)
             .WithMany(d => d.InvoiceDetails)
             .HasForeignKey(d => d.StockId)
             .IsRequired(true)
             .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
