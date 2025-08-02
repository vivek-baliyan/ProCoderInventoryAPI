using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PCI.Domain.Models;
using PCI.Shared.Common.Enums;

namespace PCI.Persistence.Configurations;

public class BusinessAddressConfiguration : IEntityTypeConfiguration<BusinessAddress>
{
    public void Configure(EntityTypeBuilder<BusinessAddress> builder)
    {
        builder.Property(e => e.EntityType)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(e => e.EntityId)
            .IsRequired();

        builder.Property(e => e.AddressType)
            .IsRequired()
            .HasConversion<int>()
            .HasDefaultValue(AddressType.Billing);

        builder.Property(e => e.AddressLabel)
            .HasMaxLength(100);

        builder.Property(e => e.AddressLine1)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(e => e.AddressLine2)
            .HasMaxLength(500);

        builder.Property(e => e.City)
            .HasMaxLength(100);

        builder.Property(e => e.State)
            .HasMaxLength(100);

        builder.Property(e => e.PostalCode)
            .HasMaxLength(20);

        builder.Property(e => e.Country)
            .HasMaxLength(100);

        builder.Property(e => e.Region)
            .HasMaxLength(50);

        builder.Property(e => e.IsDefault)
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(e => e.IsActive)
            .IsRequired()
            .HasDefaultValue(true);

        builder.Property(e => e.Notes)
            .HasMaxLength(500);

        // Indexes
        builder.HasIndex(e => new { e.EntityType, e.EntityId })
            .HasDatabaseName("IX_BusinessAddress_Entity");

        builder.HasIndex(e => e.AddressType)
            .HasDatabaseName("IX_BusinessAddress_AddressType");

        builder.HasIndex(e => e.IsDefault)
            .HasDatabaseName("IX_BusinessAddress_IsDefault");

        builder.HasIndex(e => e.IsActive)
            .HasDatabaseName("IX_BusinessAddress_IsActive");

        builder.HasIndex(e => new { e.EntityType, e.EntityId, e.AddressType })
            .HasDatabaseName("IX_BusinessAddress_Entity_AddressType");

        builder.HasIndex(e => new { e.EntityType, e.EntityId, e.IsDefault })
            .HasDatabaseName("IX_BusinessAddress_Entity_IsDefault");
    }
}