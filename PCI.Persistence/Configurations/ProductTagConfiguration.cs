using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PCI.Domain.Models;

namespace PCI.Persistence.Configurations;

public class ProductTagConfiguration : IEntityTypeConfiguration<ProductTag>
{
    public void Configure(EntityTypeBuilder<ProductTag> builder)
    {
        // ProductTag entity configuration
        builder.HasKey(e => e.Id);

        // Configure many-to-many relationship
        builder.HasOne(pt => pt.Product)
            .WithMany(p => p.ProductTags)
            .HasForeignKey(pt => pt.ProductId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(pt => pt.Tag)
                        .WithMany(t => t.ProductTags)
                        .HasForeignKey(pt => pt.TagId)
                        .OnDelete(DeleteBehavior.Cascade);

        // Create composite index
        builder.HasIndex(e => new { e.ProductId, e.TagId }).IsUnique();
    }
}