using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PCI.Domain.Models;

namespace PCI.Persistence.Configurations;

public class ProductTagAssignmentConfiguration : IEntityTypeConfiguration<ProductTagAssignment>
{
    public void Configure(EntityTypeBuilder<ProductTagAssignment> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasOne(x => x.Product)
            .WithMany(p => p.ProductTagAssignments)
            .HasForeignKey(x => x.ProductId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.ProductTag)
            .WithMany(pt => pt.ProductTagAssignments)
            .HasForeignKey(x => x.ProductTagId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(x => new { x.ProductId, x.ProductTagId })
            .IsUnique();
    }
}