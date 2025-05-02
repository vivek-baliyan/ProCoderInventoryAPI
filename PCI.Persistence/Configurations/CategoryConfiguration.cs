using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PCI.Domain.Models;

namespace PCI.Persistence.Configurations;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.Property(e => e.Name).IsRequired().HasMaxLength(100);
        builder.Property(e => e.PageTitle).IsRequired().HasMaxLength(100);
        builder.Property(e => e.UrlIdentifier).IsRequired().HasMaxLength(255);
        builder.Property(e => e.Description).HasMaxLength(1000);

        builder.HasMany(e => e.ChildCategories)
            .WithOne(e => e.ParentCategory)
            .HasForeignKey(e => e.ParentCategoryId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(e => e.Status)
            .HasConversion<string>()
            .HasMaxLength(50);

        builder.HasOne(e => e.Organisation)
            .WithMany()
            .HasForeignKey(e => e.OrganisationId)
            .OnDelete(DeleteBehavior.Cascade);

        // Add indexes for frequently queried columns if needed
        builder.HasIndex(e => new { e.Name, e.UrlIdentifier });
    }
}
