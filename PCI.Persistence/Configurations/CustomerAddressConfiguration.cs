using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PCI.Domain.Models;
using PCI.Shared.Common.Enums;

namespace PCI.Persistence.Configurations;

public class CustomerAddressConfiguration : IEntityTypeConfiguration<CustomerAddress>
{
    public void Configure(EntityTypeBuilder<CustomerAddress> builder)
    {
        builder.ToTable("CustomerAddresses");

        builder.Property(e => e.CustomerId)
            .IsRequired();

        builder.Property(e => e.AddressType)
            .IsRequired()
            .HasConversion<int>()
            .HasDefaultValue(AddressType.Billing);

        builder.Property(e => e.AddressLine1)
            .HasMaxLength(200);

        builder.Property(e => e.AddressLine2)
            .HasMaxLength(200);

        builder.Property(e => e.City)
            .HasMaxLength(100);

        builder.Property(e => e.StateId)
            .HasMaxLength(100);

        builder.Property(e => e.PostalCode)
            .HasMaxLength(20);

        builder.Property(e => e.CountryId)
            .HasMaxLength(100);

        builder.Property(e => e.IsPrimary)
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(e => e.IsActive)
            .IsRequired()
            .HasDefaultValue(true);

        // Relationship
        builder.HasOne(e => e.Customer)
            .WithMany(c => c.CustomerAddresses)
            .HasForeignKey(e => e.CustomerId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(e => e.State)
            .WithMany()
            .HasForeignKey(e => e.StateId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.Country)
            .WithMany()
            .HasForeignKey(e => e.CountryId)
            .OnDelete(DeleteBehavior.Restrict);

        // Indexes
        builder.HasIndex(e => e.CustomerId)
            .HasDatabaseName("IX_CustomerAddress_CustomerId");

        builder.HasIndex(e => e.AddressType)
            .HasDatabaseName("IX_CustomerAddress_AddressType");

        builder.HasIndex(e => e.IsPrimary)
            .HasDatabaseName("IX_CustomerAddress_IsPrimary");

        builder.HasIndex(e => e.IsActive)
            .HasDatabaseName("IX_CustomerAddress_IsActive");

        builder.HasIndex(e => new { e.CustomerId, e.AddressType })
            .HasDatabaseName("IX_CustomerAddress_Customer_AddressType");

        builder.HasIndex(e => new { e.CustomerId, e.IsPrimary })
            .HasDatabaseName("IX_CustomerAddress_Customer_IsPrimary");
    }
}