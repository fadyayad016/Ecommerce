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
    public class StoreConfiguration : IEntityTypeConfiguration<Store>
    {
        public void Configure(EntityTypeBuilder<Store> builder)
        {
            // Configure primary key
            builder.HasKey(x => x.Id);

            // Configure Name property
            builder.Property(x => x.Name)
                .IsRequired()
                .HasColumnName("Name") // Optional: specify column name
                .HasMaxLength(200) // Optional: define maximum length
                .HasComment("Name of the store");

            // Configure CityId property
            builder.Property(x => x.CityId)
                .IsRequired()
                .HasColumnName("CityId")
                .HasComment("Foreign key for City");

            // Configure Address property
            builder.Property(x => x.Address)
                .IsRequired()
                .HasColumnName("Address")
                .HasMaxLength(300) // Optional: define maximum length
                .HasComment("Address of the store");

            // Configure Phone property
            builder.Property(x => x.Phone)
                .HasMaxLength(100)
                .HasColumnName("Phone")
                .HasComment("Mobile phone number of the store");

            // Configure TelPhone property
            builder.Property(x => x.TelPhone)
                .HasMaxLength(100)
                .HasColumnName("TelPhone")
                .HasComment("Landline phone number of the store");

            // Configure Description property
            builder.Property(x => x.Description)
                .HasMaxLength(200)
                .HasColumnName("Description")
                .HasComment("Description of the store");

            // Configure IsActive property
            builder.Property(x => x.IsActive)
                .HasDefaultValue(true) // Default value set to true
                .HasColumnName("IsActive")
                .HasComment("Status of the store (active/inactive)");

            // Configure relationships
            builder.HasOne(x => x.Cities)
                .WithMany()
                .HasForeignKey(x => x.CityId)
                .OnDelete(DeleteBehavior.Restrict) // Prevent cascading deletes
                .HasConstraintName("FK_Store_City");

            // Optional: Configure table name
            builder.ToTable("Stores");
        }
    }
}
