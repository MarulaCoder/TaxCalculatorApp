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
        }

        #endregion
    }
}
