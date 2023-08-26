using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxCalculator.Domain.Core.Entities;

namespace TaxCalculator.Infrastructure.Context
{
    public class AppDbContext : DbContext
    {
        #region Fields
        #endregion

        #region Constructors

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        { 
        
        }

        #endregion

        #region Properties

        public virtual DbSet<TaxType> TaxTypes { get; set; }
        public virtual DbSet<TaxRate> TaxRates { get; set; }
        public virtual DbSet<FlatValueTax> FlatValueTax { get; set; }
        public virtual DbSet<FlatRateTax> FlatRateTax { get; set; }
        public virtual DbSet<CalculatedTax> CalculatedTaxes { get; set; }

        #endregion

        #region Public Methods

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<TaxType>(entity => 
            {
                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Code)
                    .HasMaxLength(5)
                    .IsRequired();

                entity.Property(e => e.Type)
                    .HasMaxLength(15)
                    .IsRequired();
            });

            modelBuilder.Entity<TaxRate>(entity => 
            {
                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Rate)
                    .IsRequired();

                entity.Property(e => e.MinValue)
                    .IsRequired();

                entity.Property(e => e.MaxValue)
                    .IsRequired();
            });

            modelBuilder.Entity<FlatValueTax>(entity => 
            {
                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.FlatValue)
                    .IsRequired();

                entity.Property(e => e.Threshold)
                    .IsRequired();

                entity.Property(e => e.ThresholdRate)
                    .IsRequired();
            });

            modelBuilder.Entity<FlatRateTax>(entity => 
            {
                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.FlatRate)
                    .IsRequired();
            });

            modelBuilder.Entity<CalculatedTax>(entity => 
            {
                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.AnnualIncome)
                    .IsRequired();

                entity.Property(e => e.PostalCode)
                    .IsRequired();

                entity.Property(e => e.TaxAmount)
                    .IsRequired();

                entity.Property(e => e.Created)
                    .IsRequired();
            });
        }

        #endregion
    }
}
