using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PCI.Domain.Models;

namespace PCI.Persistence.Configurations;

public class TaxClassificationConfiguration : IEntityTypeConfiguration<TaxClassification>
{
    public void Configure(EntityTypeBuilder<TaxClassification> builder)
    {
        builder.Property(e => e.Code).IsRequired().HasMaxLength(100);
        builder.Property(e => e.Description).IsRequired().HasMaxLength(500);
        builder.Property(e => e.ClassificationType).HasMaxLength(50);
        builder.Property(e => e.Category).HasMaxLength(200);
        builder.Property(e => e.CountryCode).HasMaxLength(10);
        builder.Property(e => e.DefaultTaxRate).HasColumnType("decimal(5,2)");

        builder.HasOne(e => e.Organisation)
            .WithMany()
            .HasForeignKey(e => e.OrganisationId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(e => e.Code);
        builder.HasIndex(e => e.ClassificationType);
        builder.HasIndex(e => e.CountryCode);
        builder.HasIndex(e => e.OrganisationId);
        builder.HasIndex(e => e.IsActive);
    }
}