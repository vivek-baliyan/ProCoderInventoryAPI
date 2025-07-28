using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PCI.Domain.Models;

namespace PCI.Persistence.Configurations;

public class SalesOrderApprovalConfiguration : IEntityTypeConfiguration<SalesOrderApproval>
{
    public void Configure(EntityTypeBuilder<SalesOrderApproval> builder)
    {
        builder.ToTable("SalesOrderApprovals");

        builder.HasKey(soa => soa.Id);

        builder.Property(soa => soa.SalesOrderId)
            .IsRequired();

        builder.Property(soa => soa.ApprovalStatus)
            .HasMaxLength(20)
            .HasDefaultValue("Pending");

        builder.Property(soa => soa.ApprovedBy)
            .HasMaxLength(100);

        builder.Property(soa => soa.ApprovalNotes)
            .HasMaxLength(1000);

        builder.Property(soa => soa.RejectionReason)
            .HasMaxLength(500);

        // Relationships
        builder.HasOne(soa => soa.SalesOrder)
            .WithMany(so => so.SalesOrderApprovals)
            .HasForeignKey(soa => soa.SalesOrderId)
            .OnDelete(DeleteBehavior.Cascade);

        // Indexes
        builder.HasIndex(soa => soa.SalesOrderId);
        builder.HasIndex(soa => soa.ApprovalStatus);
        builder.HasIndex(soa => soa.ApprovedDate);
    }
}