using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;

namespace hello.transaction.core.Models
{
    public class NorthwindContext : DbContext
    {
        private const string DEFAULT_SCHEMA = "dbo";


        public NorthwindContext()
        { }

        public NorthwindContext(DbContextOptions<NorthwindContext> options)
            : base(options)
        { }

        public DbSet<Attachment> Attachments { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=EFProviders.InMemory;Trusted_Connection=True;ConnectRetryCount=0");
            }
        }

        //model validate
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Transaction>().ToTable("Transaction");
            modelBuilder.Entity<Attachment>().ToTable("Attachment");

            // keep this the last line
            modelBuilder.HasDefaultSchema(DEFAULT_SCHEMA);
        }
    }
}