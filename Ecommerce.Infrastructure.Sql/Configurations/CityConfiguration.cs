using Ecommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.Infrastructure.Sql.Configurations
{
    public class CityConfiguration : IEntityTypeConfiguration<City>
    {
        public void Configure(EntityTypeBuilder<City> builder)
        {
            // Configure primary key
            builder.HasKey(x => x.Id);

            // Configure Name property
            builder.Property(x => x.Name)
                .HasMaxLength(30)
                .IsRequired()
                .HasColumnName("Name") // Optional: specify column name
                .HasComment("The name of the city"); // Optional: add a comment

            // Configure Description property
            builder.Property(x => x.Description)
                .HasMaxLength(500) // Optional: set a maximum length
                .HasColumnName("Description"); // Optional: specify column name

            // Configure Status property
            builder.Property(x => x.Status)
                .HasColumnName("Status") // Optional: specify column name
                .HasComment("Status of the city (active/inactive)");

            // Optional: Configure table name
            builder.ToTable("Cities");
        }
    }
}
