using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PCI.Domain.Models;
using PCI.Shared.Common.Enums;

namespace PCI.Persistence.Configurations;

public class BusinessTaxInfoConfiguration : IEntityTypeConfiguration<BusinessTaxInfo>
{
    public void Configure(EntityTypeBuilder<BusinessTaxInfo> builder)
    {
        builder.Property(e => e.EntityType)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(e => e.EntityId)
            .IsRequired();

        builder.Property(e => e.TaxType)
            .IsRequired()
            .HasConversion<int>();

        builder.Property(e => e.TaxNumber)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(e => e.IssuingAuthority)
            .HasMaxLength(200);

        builder.Property(e => e.TaxCategory)
            .HasMaxLength(100);

        builder.Property(e => e.IsActive)
            .IsRequired()
            .HasDefaultValue(true);

        builder.Property(e => e.IsPrimary)
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(e => e.Notes)
            .HasMaxLength(500);

        // Foreign key relationship
        builder.HasOne(e => e.Organisation)
            .WithMany()
            .HasForeignKey(e => e.OrganisationId)
            .OnDelete(DeleteBehavior.Restrict);

        // Indexes
        builder.HasIndex(e => new { e.EntityType, e.EntityId })
            .HasDatabaseName("IX_BusinessTaxInfo_Entity");

        builder.HasIndex(e => e.TaxType)
            .HasDatabaseName("IX_BusinessTaxInfo_TaxType");

        builder.HasIndex(e => e.TaxNumber)
            .HasDatabaseName("IX_BusinessTaxInfo_TaxNumber");

        builder.HasIndex(e => e.IsActive)
            .HasDatabaseName("IX_BusinessTaxInfo_IsActive");

        builder.HasIndex(e => e.IsPrimary)
            .HasDatabaseName("IX_BusinessTaxInfo_IsPrimary");

        builder.HasIndex(e => new { e.EntityType, e.EntityId, e.TaxType })
            .IsUnique()
            .HasDatabaseName("IX_BusinessTaxInfo_Entity_TaxType_Unique");

        builder.HasIndex(e => new { e.OrganisationId, e.EntityType })
            .HasDatabaseName("IX_BusinessTaxInfo_Organisation_EntityType");

        builder.HasIndex(e => new { e.TaxType, e.TaxNumber })
            .HasDatabaseName("IX_BusinessTaxInfo_TaxType_TaxNumber");
    }
}