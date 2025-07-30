using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PCI.Domain.Models;
using PCI.Shared.Common.Enums;

namespace PCI.Persistence.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        // Primary properties with constraints
        builder.Property(e => e.SKU)
            .HasMaxLength(100);

        builder.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(200);

        // Product identification codes
        builder.Property(e => e.ManufacturerPartNumber)
            .HasMaxLength(100);

        builder.Property(e => e.UPC)
            .HasMaxLength(50);

        builder.Property(e => e.EAN)
            .HasMaxLength(50);

        builder.Property(e => e.ISBN)
            .HasMaxLength(50);

        // Pricing with precision
        builder.Property(e => e.SellingPrice)
            .HasColumnType("decimal(18,2)");

        builder.Property(e => e.CostPrice)
            .HasColumnType("decimal(18,2)");

        // Enum configurations
        builder.Property(e => e.ProductType)
            .IsRequired()
            .HasConversion<int>()
            .HasDefaultValue(ProductType.Goods);

        builder.Property(e => e.Status)
            .IsRequired()
            .HasConversion<int>()
            .HasDefaultValue(ProductStatus.Active);

        // Boolean defaults
        builder.Property(e => e.IsReturnable)
            .IsRequired()
            .HasDefaultValue(true);

        builder.Property(e => e.TrackInventory)
            .IsRequired()
            .HasDefaultValue(true);

        // Foreign key relationships
        builder.HasOne(e => e.Brand)
            .WithMany(b => b.Products)
            .HasForeignKey(e => e.BrandId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(e => e.Organisation)
            .WithMany()
            .HasForeignKey(e => e.OrganisationId)
            .OnDelete(DeleteBehavior.Restrict);

        // GL Account relationships
        builder.HasOne(e => e.SalesAccount)
            .WithMany(a => a.ProductsSalesAccount)
            .HasForeignKey(e => e.SalesAccountId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(e => e.PurchaseAccount)
            .WithMany(a => a.ProductsPurchaseAccount)
            .HasForeignKey(e => e.PurchaseAccountId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(e => e.InventoryAccount)
            .WithMany(a => a.ProductsInventoryAccount)
            .HasForeignKey(e => e.InventoryAccountId)
            .OnDelete(DeleteBehavior.SetNull);

        // Indexes for performance
        builder.HasIndex(e => e.SKU)
            .HasDatabaseName("IX_Product_SKU");

        builder.HasIndex(e => e.Name)
            .HasDatabaseName("IX_Product_Name");

        builder.HasIndex(e => e.Status)
            .HasDatabaseName("IX_Product_Status");

        builder.HasIndex(e => e.ProductType)
            .HasDatabaseName("IX_Product_ProductType");

        builder.HasIndex(e => e.OrganisationId)
            .HasDatabaseName("IX_Product_OrganisationId");

        builder.HasIndex(e => e.BrandId)
            .HasDatabaseName("IX_Product_BrandId");


        // Composite indexes for common queries
        builder.HasIndex(e => new { e.OrganisationId, e.SKU })
            .IsUnique()
            .HasDatabaseName("IX_Product_OrganisationId_SKU");

        // Check constraints
        builder.ToTable(t => t.HasCheckConstraint(
            "CK_Product_SellingPrice_NonNegative",
            "SellingPrice IS NULL OR SellingPrice >= 0"));

        builder.ToTable(t => t.HasCheckConstraint(
            "CK_Product_CostPrice_NonNegative",
            "CostPrice IS NULL OR CostPrice >= 0"));
    }
}
