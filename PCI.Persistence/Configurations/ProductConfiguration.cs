using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PCI.Domain.Models;

namespace PCI.Persistence.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.Property(e => e.Name).IsRequired().HasMaxLength(100);
        builder.Property(e => e.PageTitle).HasMaxLength(100);
        builder.Property(e => e.UrlIdentifier).HasMaxLength(100);
        builder.Property(e => e.Description).HasMaxLength(5000);
        builder.Property(e => e.Price).HasColumnType("decimal(18,2)").IsRequired();
        builder.Property(e => e.OldPrice).HasColumnType("decimal(18,2)");
        builder.Property(e => e.Coupon).HasMaxLength(50);
        builder.Property(e => e.SKU).HasMaxLength(50);

        builder.HasOne(e => e.Organisation)
            .WithMany()
            .HasForeignKey(e => e.OrganisationId)
            .OnDelete(DeleteBehavior.Cascade);

        // Index for frequently queried fields
        builder.HasIndex(e => e.UrlIdentifier).IsUnique();
        builder.HasIndex(e => e.SKU).IsUnique();
        builder.HasIndex(e => e.Status);
        builder.HasIndex(e => e.PublishDate);
    }
}
