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

        builder.Property(e => e.ContactPerson)
            .HasMaxLength(100);

        // Contact information
        builder.Property(e => e.PhoneNumber)
            .HasMaxLength(20);

        builder.Property(e => e.MobileNumber)
            .HasMaxLength(20);

        builder.Property(e => e.Email)
            .HasMaxLength(100);

        // Address information
        builder.Property(e => e.BillingAddress)
            .HasMaxLength(500);

        builder.Property(e => e.ShippingAddress)
            .HasMaxLength(500);

        builder.Property(e => e.City)
            .HasMaxLength(100);

        builder.Property(e => e.State)
            .HasMaxLength(100);

        builder.Property(e => e.PostalCode)
            .HasMaxLength(20);

        builder.Property(e => e.Country)
            .HasMaxLength(100);

        builder.Property(e => e.WebsiteUrl)
            .HasMaxLength(200);

        // Business information
        builder.Property(e => e.CustomerType)
            .IsRequired()
            .HasConversion<int>()
            .HasDefaultValue(CustomerType.Individual);

        builder.Property(e => e.PaymentTermDays)
            .IsRequired()
            .HasDefaultValue(30);

        builder.Property(e => e.CreditLimit)
            .HasColumnType("decimal(18,2)");

        // Tax information
        builder.Property(e => e.TaxNumber)
            .HasMaxLength(50);

        builder.Property(e => e.GSTNumber)
            .HasMaxLength(50);

        builder.Property(e => e.PANNumber)
            .HasMaxLength(50);

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

        builder.HasIndex(e => e.Email)
            .HasDatabaseName("IX_Customer_Email");

        builder.HasIndex(e => e.PhoneNumber)
            .HasDatabaseName("IX_Customer_PhoneNumber");

        builder.HasIndex(e => e.CustomerType)
            .HasDatabaseName("IX_Customer_CustomerType");

        builder.HasIndex(e => e.IsActive)
            .HasDatabaseName("IX_Customer_IsActive");

        builder.HasIndex(e => e.OrganisationId)
            .HasDatabaseName("IX_Customer_OrganisationId");

        builder.HasIndex(e => e.CurrencyId)
            .HasDatabaseName("IX_Customer_CurrencyId");

        builder.HasIndex(e => e.City)
            .HasDatabaseName("IX_Customer_City");

        builder.HasIndex(e => e.Country)
            .HasDatabaseName("IX_Customer_Country");

        // Composite indexes for common queries
        builder.HasIndex(e => new { e.OrganisationId, e.IsActive })
            .HasDatabaseName("IX_Customer_OrganisationId_IsActive");

        builder.HasIndex(e => new { e.OrganisationId, e.CustomerCode })
            .IsUnique()
            .HasDatabaseName("IX_Customer_OrganisationId_CustomerCode");

        builder.HasIndex(e => new { e.OrganisationId, e.Email })
            .IsUnique()
            .HasFilter("Email IS NOT NULL")
            .HasDatabaseName("IX_Customer_OrganisationId_Email");

        builder.HasIndex(e => new { e.OrganisationId, e.CustomerType })
            .HasDatabaseName("IX_Customer_OrganisationId_CustomerType");

        // Check constraints
        builder.ToTable(t => t.HasCheckConstraint(
            "CK_Customer_CreditLimit_NonNegative",
            "CreditLimit IS NULL OR CreditLimit >= 0"));

        builder.ToTable(t => t.HasCheckConstraint(
            "CK_Customer_PaymentTermDays_NonNegative",
            "PaymentTermDays >= 0"));
    }
}