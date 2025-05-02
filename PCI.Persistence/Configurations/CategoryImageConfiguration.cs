using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PCI.Domain.Models;

namespace PCI.Persistence.Configurations;

public class CategoryImageConfiguration : IEntityTypeConfiguration<CategoryImage>
{
    public void Configure(EntityTypeBuilder<CategoryImage> builder)
    {
        builder.Property(e => e.ImagePath).IsRequired().HasMaxLength(255);
        builder.Property(e => e.CategoryId).IsRequired();
        builder.Property(e => e.IsPrimary).IsRequired();

        // Configure relationships
        builder.HasOne(e => e.Category)
            .WithMany(e => e.CategoryImages)
            .HasForeignKey(e => e.CategoryId)
            .OnDelete(DeleteBehavior.Cascade);
    }

}
