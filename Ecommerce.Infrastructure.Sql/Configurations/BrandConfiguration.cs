using Ecommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.Infrastructure.Sql.Configurations
{
    public class BrandConfiguration : IEntityTypeConfiguration<Brand>
    {
        public void Configure(EntityTypeBuilder<Brand> builder)
        {
            // Configure primary key
            builder.HasKey(x => x.Id);

            // Configure Name property
            builder.Property(x => x.Name)
                .HasMaxLength(30)
                .IsRequired()
                .HasColumnName("Name") // Optional: specify column name
                .HasComment("The name of the brand"); // Optional: add a comment

            // Configure Description property
            builder.Property(x => x.Description)
                .HasMaxLength(500) // Optional: set a maximum length
                .HasColumnName("Description"); // Optional: specify column name

            // Configure Status property
            builder.Property(x => x.IsActive)
                .HasColumnName("IsActive") // Optional: specify column name
                .HasComment("Status of the Brand (active/inactive)");

            // Optional: Configure table name
            builder.ToTable("Brands");
        }


    }
}
