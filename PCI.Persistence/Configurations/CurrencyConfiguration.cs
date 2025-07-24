using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PCI.Domain.Models;

namespace PCI.Persistence.Configurations;

public class CurrencyConfiguration : IEntityTypeConfiguration<Currency>
{
    public void Configure(EntityTypeBuilder<Currency> builder)
    {
        builder.Property(e => e.Code).IsRequired().HasMaxLength(3);
        builder.Property(e => e.Name).IsRequired().HasMaxLength(100);
        builder.Property(e => e.Symbol).HasMaxLength(10);
        builder.Property(e => e.ExchangeRate).HasColumnType("decimal(18,6)");

        builder.HasOne(e => e.Organisation)
            .WithMany()
            .HasForeignKey(e => e.OrganisationId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(e => e.Code);
        builder.HasIndex(e => e.OrganisationId);
        builder.HasIndex(e => e.IsBaseCurrency);
        builder.HasIndex(e => e.IsActive);
    }
}