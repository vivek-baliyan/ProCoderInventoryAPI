using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PCI.Domain.Models;

namespace PCI.Persistence.Configurations;

public class CustomerDocumentConfiguration : IEntityTypeConfiguration<CustomerDocument>
{
    public void Configure(EntityTypeBuilder<CustomerDocument> builder)
    {
        builder.ToTable("CustomerDocuments");

        builder.HasKey(cd => cd.Id);

        builder.Property(cd => cd.CustomerId)
            .IsRequired();

        builder.Property(cd => cd.FileName)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(cd => cd.OriginalFileName)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(cd => cd.FileExtension)
            .HasMaxLength(10);

        builder.Property(cd => cd.ContentType)
            .HasMaxLength(100);

        builder.Property(cd => cd.FileSizeBytes)
            .IsRequired();

        builder.Property(cd => cd.FilePath)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(cd => cd.DocumentType)
            .HasMaxLength(50)
            .HasDefaultValue("Other");

        builder.Property(cd => cd.Description)
            .HasMaxLength(500);

        builder.Property(cd => cd.UploadedDate)
            .IsRequired()
            .HasDefaultValueSql("GETUTCDATE()");

        builder.Property(cd => cd.UploadedBy)
            .HasMaxLength(100);

        builder.Property(cd => cd.IsActive)
            .IsRequired()
            .HasDefaultValue(true);

        builder.Property(cd => cd.Notes)
            .HasMaxLength(500);

        // OrganisationId removed - inherited through Customer relationship

        // Relationships
        builder.HasOne(cd => cd.Customer)
            .WithMany(c => c.CustomerDocuments)
            .HasForeignKey(cd => cd.CustomerId)
            .OnDelete(DeleteBehavior.Cascade);

        // No direct Organisation relationship - inherited through Customer

        // Indexes
        builder.HasIndex(cd => cd.CustomerId)
            .HasDatabaseName("IX_CustomerDocument_CustomerId");

        builder.HasIndex(cd => cd.DocumentType)
            .HasDatabaseName("IX_CustomerDocument_DocumentType");

        builder.HasIndex(cd => cd.IsActive)
            .HasDatabaseName("IX_CustomerDocument_IsActive");

        // Removed OrganisationId index as field was removed

        builder.HasIndex(cd => new { cd.CustomerId, cd.DocumentType })
            .HasDatabaseName("IX_CustomerDocument_Customer_DocumentType");

        // Removed OrganisationId composite index as field was removed
    }
}