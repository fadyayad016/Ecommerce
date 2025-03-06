using Ecommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.Infrastructure.Sql.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(prop => prop.Name)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(prop => prop.Slug)
            .HasMaxLength(150)
            .IsRequired();

        builder.Property(prop => prop.ShortDescription)
            .HasMaxLength(200);

        builder.Property(prop => prop.VariableTheme)
            .HasMaxLength(100);

        builder.Property(prop => prop.CategoryId)
            .IsRequired();

        // Configure IsSerial property
        builder.Property(x => x.IsSerial)
            .HasColumnName("IsSerial") // Optional: specify column name
            .HasComment("The product Is Serial (Yes/No)").IsRequired();

        // Configure IsActive property
        builder.Property(x => x.IsActive)
            .HasColumnName("IsActive") // Optional: specify column name
            .HasComment("The Status of the product (active/inactive)").IsRequired();

    }
}
