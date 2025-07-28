using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PCI.Domain.Models;

namespace PCI.Persistence.Configurations;

public class SalesOrderDocumentConfiguration : IEntityTypeConfiguration<SalesOrderDocument>
{
    public void Configure(EntityTypeBuilder<SalesOrderDocument> builder)
    {
        builder.ToTable("SalesOrderDocuments");

        builder.HasKey(sod => sod.Id);

        builder.Property(sod => sod.SalesOrderId)
            .IsRequired();

        builder.Property(sod => sod.DocumentName)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(sod => sod.DocumentType)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(sod => sod.Description)
            .HasMaxLength(500);

        builder.Property(sod => sod.FilePath)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(sod => sod.FileExtension)
            .HasMaxLength(10);

        builder.Property(sod => sod.FileSizeBytes)
            .IsRequired();

        builder.Property(sod => sod.IsActive)
            .HasDefaultValue(true);

        builder.Property(sod => sod.IsConfidential)
            .HasDefaultValue(false);

        builder.Property(sod => sod.UploadedBy)
            .HasMaxLength(100);

        builder.Property(sod => sod.UploadedOn)
            .IsRequired();

        builder.Property(sod => sod.Notes)
            .HasMaxLength(500);

        // Relationships
        builder.HasOne(sod => sod.SalesOrder)
            .WithMany(so => so.SalesOrderDocuments)
            .HasForeignKey(sod => sod.SalesOrderId)
            .OnDelete(DeleteBehavior.Cascade);

        // Indexes
        builder.HasIndex(sod => sod.SalesOrderId)
            .HasDatabaseName("IX_SalesOrderDocument_SalesOrderId");

        builder.HasIndex(sod => sod.DocumentType)
            .HasDatabaseName("IX_SalesOrderDocument_DocumentType");

        builder.HasIndex(sod => sod.UploadedOn)
            .HasDatabaseName("IX_SalesOrderDocument_UploadedOn");

        builder.HasIndex(sod => sod.IsActive)
            .HasDatabaseName("IX_SalesOrderDocument_IsActive");

        // Check constraints
        builder.ToTable(t => t.HasCheckConstraint(
            "CK_SalesOrderDocument_FileSizeBytes_Positive",
            "FileSizeBytes > 0"));
    }
}