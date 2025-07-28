using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PCI.Domain.Models;
using PCI.Shared.Common.Enums;

namespace PCI.Persistence.Configurations;

public class VendorConfiguration : IEntityTypeConfiguration<Vendor>
{
    public void Configure(EntityTypeBuilder<Vendor> builder)
    {
        // Primary properties with constraints
        builder.Property(e => e.VendorCode)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(e => e.VendorName)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(e => e.CompanyName)
            .HasMaxLength(200);

        builder.Property(e => e.WebsiteUrl)
            .HasMaxLength(200);

        // Vendor Classification
        builder.Property(e => e.VendorType)
            .IsRequired()
            .HasConversion<int>()
            .HasDefaultValue(VendorType.Supplier);

        builder.Property(e => e.Category)
            .IsRequired()
            .HasConversion<int>()
            .HasDefaultValue(VendorCategory.RawMaterials);

        builder.Property(e => e.Status)
            .IsRequired()
            .HasConversion<int>()
            .HasDefaultValue(VendorStatus.Active);

        builder.Property(e => e.Industry)
            .HasMaxLength(100);

        // Boolean defaults
        builder.Property(e => e.IsManufacturer)
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(e => e.IsDropshipVendor)
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(e => e.HasPortalAccess)
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(e => e.RequiresPOApproval)
            .IsRequired()
            .HasDefaultValue(false);

        // Text fields with length constraints
        builder.Property(e => e.PortalAccessEmail)
            .HasMaxLength(100);

        builder.Property(e => e.PreferredCommunicationMethod)
            .HasMaxLength(50)
            .HasDefaultValue("Email");

        builder.Property(e => e.StatusChangeReason)
            .HasMaxLength(500);

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

        builder.HasOne(e => e.ParentVendor)
            .WithMany(v => v.ChildVendors)
            .HasForeignKey(e => e.ParentVendorId)
            .OnDelete(DeleteBehavior.SetNull);

        // Indexes for performance
        builder.HasIndex(e => e.VendorCode)
            .HasDatabaseName("IX_Vendor_VendorCode");

        builder.HasIndex(e => e.VendorName)
            .HasDatabaseName("IX_Vendor_VendorName");

        builder.HasIndex(e => e.VendorType)
            .HasDatabaseName("IX_Vendor_VendorType");

        builder.HasIndex(e => e.Category)
            .HasDatabaseName("IX_Vendor_Category");

        builder.HasIndex(e => e.Status)
            .HasDatabaseName("IX_Vendor_Status");

        builder.HasIndex(e => e.Industry)
            .HasDatabaseName("IX_Vendor_Industry");

        builder.HasIndex(e => e.OrganisationId)
            .HasDatabaseName("IX_Vendor_OrganisationId");

        builder.HasIndex(e => e.CurrencyId)
            .HasDatabaseName("IX_Vendor_CurrencyId");

        builder.HasIndex(e => e.ParentVendorId)
            .HasDatabaseName("IX_Vendor_ParentVendorId");

        builder.HasIndex(e => e.IsManufacturer)
            .HasDatabaseName("IX_Vendor_IsManufacturer");

        // Composite indexes for common queries
        builder.HasIndex(e => new { e.OrganisationId, e.Status })
            .HasDatabaseName("IX_Vendor_OrganisationId_Status");

        builder.HasIndex(e => new { e.OrganisationId, e.VendorCode })
            .IsUnique()
            .HasDatabaseName("IX_Vendor_OrganisationId_VendorCode");

        builder.HasIndex(e => new { e.OrganisationId, e.VendorType })
            .HasDatabaseName("IX_Vendor_OrganisationId_VendorType");

        builder.HasIndex(e => new { e.OrganisationId, e.Category })
            .HasDatabaseName("IX_Vendor_OrganisationId_Category");

        // No check constraints needed for core Vendor entity
    }
}