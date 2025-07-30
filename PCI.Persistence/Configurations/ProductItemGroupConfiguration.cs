using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PCI.Domain.Models;

namespace PCI.Persistence.Configurations;

public class ProductItemGroupConfiguration : IEntityTypeConfiguration<ProductItemGroup>
{
    public void Configure(EntityTypeBuilder<ProductItemGroup> builder)
    {
        // Configure the many-to-many relationship
        builder.HasKey(pig => pig.Id);

        builder.HasOne(pig => pig.Product)
            .WithMany(p => p.ProductItemGroups)
            .HasForeignKey(pig => pig.ProductId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(pig => pig.ItemGroup)
            .WithMany(ig => ig.ProductItemGroups)
            .HasForeignKey(pig => pig.ItemGroupId)
            .OnDelete(DeleteBehavior.Cascade);

        // Boolean default
        builder.Property(pig => pig.IsPrimary)
            .IsRequired()
            .HasDefaultValue(false);

        // Indexes for performance
        builder.HasIndex(pig => pig.ProductId)
            .HasDatabaseName("IX_ProductItemGroup_ProductId");

        builder.HasIndex(pig => pig.ItemGroupId)
            .HasDatabaseName("IX_ProductItemGroup_ItemGroupId");

        // Composite unique index to prevent duplicate entries
        builder.HasIndex(pig => new { pig.ProductId, pig.ItemGroupId })
            .IsUnique()
            .HasDatabaseName("IX_ProductItemGroup_ProductId_ItemGroupId");

        // Index for finding primary item group
        builder.HasIndex(pig => new { pig.ProductId, pig.IsPrimary })
            .HasDatabaseName("IX_ProductItemGroup_ProductId_IsPrimary");
    }
}