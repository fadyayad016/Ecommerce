using Ecommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Infrastructure.Sql.Configurations
{
    public class CurrencyConfiguration : IEntityTypeConfiguration<Currency>
    {
        public void Configure(EntityTypeBuilder<Currency> builder)
        {
            // Configure primary key
            builder.HasKey(x => x.Id);

            // Configure Name property
            builder.Property(x => x.Name)
                .HasMaxLength(30)
                .IsRequired()
                .HasColumnName("Name") // Optional: specify column name
                .HasComment("The name of the Currency"); // Optional: add a comment

            // Configure IsActive property
            builder.Property(x => x.IsActive)
                .HasColumnName("IsActive") // Optional: specify column name
                .HasComment("Status of the Currency (active/inactive)");

            // Optional: Configure table name
            builder.ToTable("Currencies");
        }
    }
}
