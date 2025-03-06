using Ecommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.Infrastructure.Sql.Configurations
{
    class BoxesCurrenciesConfiguration : IEntityTypeConfiguration<BoxesCurrencies>
    {
        public void Configure(EntityTypeBuilder<BoxesCurrencies> builder)
        {
            builder.HasKey(bc => bc.Id);
            builder.Property(bc => bc.StartValue).HasDefaultValue(0);
            builder.Property(bc => bc.IsActive).HasDefaultValue(true);
            builder.HasOne(bc => bc.Boxes)
                   .WithMany(b => b.BoxesCurrencies)
                   .HasForeignKey(bc => bc.BoxId);
            builder.HasOne(bc => bc.Currencies)
                   .WithMany()
                   .HasForeignKey(bc => bc.CurrencyId);
        }
    }
}
