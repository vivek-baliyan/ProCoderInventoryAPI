using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PCI.Domain.Models;

namespace PCI.Persistence.Configurations;

public class VendorDocumentConfiguration : IEntityTypeConfiguration<VendorDocument>
{
    public void Configure(EntityTypeBuilder<VendorDocument> builder)
    {
        builder.Property(e => e.DocumentName)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(e => e.DocumentType)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(e => e.Description)
            .HasMaxLength(500);

        builder.Property(e => e.FilePath)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(e => e.FileExtension)
            .HasMaxLength(10);

        builder.Property(e => e.FileSizeBytes)
            .IsRequired();

        builder.Property(e => e.IsActive)
            .IsRequired()
            .HasDefaultValue(true);

        builder.Property(e => e.IsConfidential)
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(e => e.UploadedBy)
            .HasMaxLength(100);

        builder.Property(e => e.UploadedOn)
            .IsRequired();

        builder.Property(e => e.Notes)
            .HasMaxLength(500);

        // Foreign key relationship
        builder.HasOne(e => e.Vendor)
            .WithMany(v => v.VendorDocuments)
            .HasForeignKey(e => e.VendorId)
            .OnDelete(DeleteBehavior.Cascade);

        // Indexes
        builder.HasIndex(e => e.VendorId)
            .HasDatabaseName("IX_VendorDocument_VendorId");

        builder.HasIndex(e => e.DocumentType)
            .HasDatabaseName("IX_VendorDocument_DocumentType");

        builder.HasIndex(e => e.ExpiryDate)
            .HasDatabaseName("IX_VendorDocument_ExpiryDate");

        builder.HasIndex(e => e.IsActive)
            .HasDatabaseName("IX_VendorDocument_IsActive");

        builder.HasIndex(e => new { e.VendorId, e.DocumentType })
            .HasDatabaseName("IX_VendorDocument_VendorId_DocumentType");

        builder.HasIndex(e => new { e.VendorId, e.IsActive })
            .HasDatabaseName("IX_VendorDocument_VendorId_IsActive");
    }
}