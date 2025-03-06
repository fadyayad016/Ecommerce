using Ecommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Infrastructure.Sql.Configurations
{
    public class BarcodeConfiguration : IEntityTypeConfiguration<Barcode>
    {
        public void Configure(EntityTypeBuilder<Barcode> builder)
        {
            // Table Name
            builder.ToTable("Barcodes");

            // Primary Key
            builder.HasKey(b => b.Id);

            // Properties
            builder.Property(b => b.BarcodeName)
                .IsRequired()
                .HasColumnName("BarcodeName") // Optional: specify column name
                .HasMaxLength(200) // Optional: define maximum length
                .HasComment("Name of the Barcode");

            builder.Property(b => b.IsActive)
                .HasDefaultValue(true) // Default value set to true
                .HasColumnName("IsActive")
                .HasComment("Status of the BarCode (active/inactive)");

            // Configure ProductId property
            builder.Property(x => x.ProductId)
                .IsRequired()
                .HasColumnName("ProductId")
                .HasComment("Foreign key for Product");

            // Indexes
            builder.HasIndex(b => b.BarcodeName).IsUnique();
           
        }
    }
}
