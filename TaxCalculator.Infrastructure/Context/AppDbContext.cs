using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxCalculator.Domain.Core.Entities;
using TaxCalculator.Domain.Core.Enums;

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
                    .HasColumnType("varchar")
                    .HasMaxLength(5)
                    .IsRequired();

                entity.Property(e => e.Type)
                    .HasConversion(
                        v => v.ToString(),
                        v => (TaxTypeEnum)Enum.Parse(typeof(TaxTypeEnum), v))
                    .HasMaxLength(15)
                    .IsRequired();
            });

            modelBuilder.Entity<TaxRate>(entity => 
            {
                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Rate)
                    .HasPrecision(5,2)
                    .IsRequired();

                entity.Property(e => e.MinValue)
                    .HasPrecision(19, 4)
                    .IsRequired();

                entity.Property(e => e.MaxValue)
                    .HasPrecision(19, 4)
                    .IsRequired();

                entity.Property(e => e.TaxLevel)
                    .HasConversion(
                        v => v.ToString(),
                        v => (TaxLevelEnum)Enum.Parse(typeof(TaxLevelEnum), v))
                    .HasMaxLength(15)
                    .IsRequired();
            });

            modelBuilder.Entity<FlatValueTax>(entity => 
            {
                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.FlatValue)
                    .HasPrecision(19, 4)
                    .IsRequired();

                entity.Property(e => e.Threshold)
                    .HasPrecision(19, 4)
                    .IsRequired();

                entity.Property(e => e.ThresholdRate)
                    .HasPrecision(5, 2)
                    .IsRequired();
            });

            modelBuilder.Entity<FlatRateTax>(entity => 
            {
                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.FlatRate)
                    .HasPrecision(5, 2)
                    .IsRequired();
            });

            modelBuilder.Entity<CalculatedTax>(entity => 
            {
                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.AnnualIncome)
                    .HasPrecision(19, 4)
                    .IsRequired();

                entity.Property(e => e.PostalCode)
                    .HasColumnType("varchar")
                    .HasMaxLength(4)
                    .IsRequired();

                entity.Property(e => e.TaxAmount)
                    .HasPrecision(19, 4)
                    .IsRequired();

                entity.Property(e => e.Created)
                    .HasColumnType("date")
                    .IsRequired();
            });
        }

        #endregion
    }
}
