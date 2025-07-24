using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PCI.Domain.Models;

namespace PCI.Persistence.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.Property(e => e.SKU).HasMaxLength(100);
        builder.Property(e => e.Name).IsRequired().HasMaxLength(200);
        builder.Property(e => e.Description).HasMaxLength(1000);
        builder.Property(e => e.ManufacturerPartNumber).HasMaxLength(100);
        builder.Property(e => e.UPC).HasMaxLength(50);
        builder.Property(e => e.EAN).HasMaxLength(50);
        builder.Property(e => e.ISBN).HasMaxLength(50);

        // Pricing
        builder.Property(e => e.SellingPrice).HasColumnType("decimal(18,2)");
        builder.Property(e => e.CostPrice).HasColumnType("decimal(18,2)");

        // Enum configurations
        builder.Property(e => e.ProductType).HasConversion<int>();
        builder.Property(e => e.Status).HasConversion<int>();

        // Foreign key relationships
        builder.HasOne(e => e.ItemGroup)
            .WithMany(ig => ig.Products)
            .HasForeignKey(e => e.ItemGroupId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(e => e.Brand)
            .WithMany(b => b.Products)
            .HasForeignKey(e => e.BrandId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(e => e.Organisation)
            .WithMany()
            .HasForeignKey(e => e.OrganisationId)
            .OnDelete(DeleteBehavior.Restrict);

        // Indexes for performance
        builder.HasIndex(e => e.SKU);
        builder.HasIndex(e => e.Status);
        builder.HasIndex(e => e.ProductType);
        builder.HasIndex(e => e.IsActive);
        builder.HasIndex(e => e.OrganisationId);
    }
}
