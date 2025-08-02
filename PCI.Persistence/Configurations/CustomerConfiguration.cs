using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PCI.Domain.Models;
using PCI.Shared.Common.Enums;

namespace PCI.Persistence.Configurations;

public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.ToTable("Customers");

        // Primary properties with constraints
        builder.Property(e => e.CustomerCode)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(e => e.DisplayName)
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

        // Foreign key relationships with parent entities
        builder.HasOne(e => e.Organisation)
            .WithMany()
            .HasForeignKey(e => e.OrganisationId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.Currency)
            .WithMany()
            .HasForeignKey(e => e.CurrencyId)
            .OnDelete(DeleteBehavior.SetNull);

        // Child entity relationships with CASCADE delete
        builder.HasMany(e => e.CustomerContacts)
            .WithOne(cc => cc.Customer)
            .HasForeignKey(cc => cc.CustomerId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(e => e.CustomerAddresses)
            .WithOne(ca => ca.Customer)
            .HasForeignKey(ca => ca.CustomerId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(e => e.CustomerTaxInfos)
            .WithOne(cti => cti.Customer)
            .HasForeignKey(cti => cti.CustomerId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(e => e.CustomerBankInfos)
            .WithOne(cbi => cbi.Customer)
            .HasForeignKey(cbi => cbi.CustomerId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(e => e.CustomerFinancial)
            .WithOne(cf => cf.Customer)
            .HasForeignKey<CustomerFinancial>(cf => cf.CustomerId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(e => e.CustomerDocuments)
            .WithOne(cd => cd.Customer)
            .HasForeignKey(cd => cd.CustomerId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(e => e.CustomerPriceLists)
            .WithOne(cpl => cpl.Customer)
            .HasForeignKey(cpl => cpl.CustomerId)
            .OnDelete(DeleteBehavior.Cascade);

        // Transaction entities - RESTRICT to prevent accidental deletion
        builder.HasMany(e => e.SalesOrders)
            .WithOne(so => so.Customer)
            .HasForeignKey(so => so.CustomerId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(e => e.Invoices)
            .WithOne(i => i.Customer)
            .HasForeignKey(i => i.CustomerId)
            .OnDelete(DeleteBehavior.Restrict);

        // Indexes for performance
        builder.HasIndex(e => e.CustomerCode)
            .HasDatabaseName("IX_Customer_CustomerCode");

        builder.HasIndex(e => e.DisplayName)
            .HasDatabaseName("IX_Customer_DisplayName");

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
    }
}