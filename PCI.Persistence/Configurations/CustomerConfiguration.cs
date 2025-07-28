using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PCI.Domain.Models;
using PCI.Shared.Common.Enums;

namespace PCI.Persistence.Configurations;

public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        // Primary properties with constraints
        builder.Property(e => e.CustomerCode)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(e => e.CustomerName)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(e => e.CompanyName)
            .HasMaxLength(200);

        builder.Property(e => e.WebsiteUrl)
            .HasMaxLength(200);

        // Business information
        builder.Property(e => e.CustomerType)
            .IsRequired()
            .HasConversion<int>()
            .HasDefaultValue(CustomerType.Individual);

        // Status
        builder.Property(e => e.IsActive)
            .IsRequired()
            .HasDefaultValue(true);

        builder.Property(e => e.Notes)
            .HasMaxLength(1000);

        // Foreign key relationships
        builder.HasOne(e => e.Organisation)
            .WithMany()
            .HasForeignKey(e => e.OrganisationId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.Currency)
            .WithMany()
            .HasForeignKey(e => e.CurrencyId)
            .OnDelete(DeleteBehavior.SetNull);

        // Indexes for performance
        builder.HasIndex(e => e.CustomerCode)
            .HasDatabaseName("IX_Customer_CustomerCode");

        builder.HasIndex(e => e.CustomerName)
            .HasDatabaseName("IX_Customer_CustomerName");

        builder.HasIndex(e => e.CustomerType)
            .HasDatabaseName("IX_Customer_CustomerType");

        builder.HasIndex(e => e.IsActive)
            .HasDatabaseName("IX_Customer_IsActive");

        builder.HasIndex(e => e.OrganisationId)
            .HasDatabaseName("IX_Customer_OrganisationId");

        builder.HasIndex(e => e.CurrencyId)
            .HasDatabaseName("IX_Customer_CurrencyId");

        // Composite indexes for common queries
        builder.HasIndex(e => new { e.OrganisationId, e.IsActive })
            .HasDatabaseName("IX_Customer_OrganisationId_IsActive");

        builder.HasIndex(e => new { e.OrganisationId, e.CustomerCode })
            .IsUnique()
            .HasDatabaseName("IX_Customer_OrganisationId_CustomerCode");

        builder.HasIndex(e => new { e.OrganisationId, e.CustomerType })
            .HasDatabaseName("IX_Customer_OrganisationId_CustomerType");

        // No check constraints needed for core Customer entity
    }
}