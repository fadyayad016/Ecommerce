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
    // Boxes Configuration
    public class BoxesConfiguration : IEntityTypeConfiguration<Boxes>
    {
        public void Configure(EntityTypeBuilder<Boxes> builder)
        {
            builder.HasKey(b => b.Id);
            builder.Property(b => b.Name).HasMaxLength(30)
                .IsRequired()
                .HasColumnName("Name") // Optional: specify column name
                .HasComment("The name of the Box"); // Optional: add a comment
            builder.Property(b => b.IsActive)
                .HasColumnName("IsActive") // Optional: specify column name
                .HasComment("Status of the city (active/inactive)");
        }
    }

}
