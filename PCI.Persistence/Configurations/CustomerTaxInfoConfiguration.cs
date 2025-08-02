using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PCI.Domain.Models;
using PCI.Shared.Common.Enums;

namespace PCI.Persistence.Configurations;

public class CustomerTaxInfoConfiguration : IEntityTypeConfiguration<CustomerTaxInfo>
{
    public void Configure(EntityTypeBuilder<CustomerTaxInfo> builder)
    {
        builder.ToTable("CustomerTaxInfos");

        builder.Property(e => e.CustomerId)
            .IsRequired();

        builder.Property(e => e.TaxType)
            .IsRequired()
            .HasConversion<int>()
            .HasDefaultValue(TaxType.TaxIdentificationNumber);

        builder.Property(e => e.TaxNumber)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(e => e.IsPrimary)
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(e => e.IsActive)
            .IsRequired()
            .HasDefaultValue(true);

        // Relationship
        builder.HasOne(e => e.Customer)
            .WithMany(c => c.CustomerTaxInfos)
            .HasForeignKey(e => e.CustomerId)
            .OnDelete(DeleteBehavior.Cascade);

        // Indexes
        builder.HasIndex(e => e.CustomerId)
            .HasDatabaseName("IX_CustomerTaxInfo_CustomerId");

        builder.HasIndex(e => e.TaxType)
            .HasDatabaseName("IX_CustomerTaxInfo_TaxType");

        builder.HasIndex(e => e.TaxNumber)
            .HasDatabaseName("IX_CustomerTaxInfo_TaxNumber");

        builder.HasIndex(e => e.IsPrimary)
            .HasDatabaseName("IX_CustomerTaxInfo_IsPrimary");

        builder.HasIndex(e => e.IsActive)
            .HasDatabaseName("IX_CustomerTaxInfo_IsActive");

        builder.HasIndex(e => new { e.CustomerId, e.TaxType })
            .HasDatabaseName("IX_CustomerTaxInfo_Customer_TaxType");

        builder.HasIndex(e => new { e.CustomerId, e.IsPrimary })
            .HasDatabaseName("IX_CustomerTaxInfo_Customer_IsPrimary");
    }
}