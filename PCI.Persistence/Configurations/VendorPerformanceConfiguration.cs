using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PCI.Domain.Models;

namespace PCI.Persistence.Configurations;

public class VendorPerformanceConfiguration : IEntityTypeConfiguration<VendorPerformance>
{
    public void Configure(EntityTypeBuilder<VendorPerformance> builder)
    {
        builder.Property(e => e.VendorId)
            .IsRequired();

        // Performance ratings with precision
        builder.Property(e => e.PerformanceRating)
            .HasColumnType("decimal(3,2)")
            .HasDefaultValue(0);

        builder.Property(e => e.OnTimeDeliveryPercentage)
            .HasDefaultValue(0);

        builder.Property(e => e.QualityRating)
            .HasDefaultValue(0);

        builder.Property(e => e.CommunicationRating)
            .HasDefaultValue(0);

        builder.Property(e => e.PriceCompetitivenessRating)
            .HasDefaultValue(0);

        // Review information
        builder.Property(e => e.ReviewPeriodStart)
            .IsRequired();

        builder.Property(e => e.ReviewPeriodEnd)
            .IsRequired();

        builder.Property(e => e.ReviewedBy)
            .HasMaxLength(100);

        // Performance metrics
        builder.Property(e => e.TotalOrdersInPeriod)
            .HasDefaultValue(0);

        builder.Property(e => e.OnTimeDeliveries)
            .HasDefaultValue(0);

        builder.Property(e => e.LateDeliveries)
            .HasDefaultValue(0);

        builder.Property(e => e.QualityIssues)
            .HasDefaultValue(0);

        builder.Property(e => e.ResolvedComplaints)
            .HasDefaultValue(0);

        builder.Property(e => e.UnresolvedComplaints)
            .HasDefaultValue(0);

        // Status flags
        builder.Property(e => e.IsPreferredVendor)
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(e => e.IsBlacklisted)
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(e => e.BlacklistReason)
            .HasMaxLength(500);

        builder.Property(e => e.BlacklistedBy)
            .HasMaxLength(100);

        // Improvement areas
        builder.Property(e => e.StrengthsNoted)
            .HasMaxLength(1000);

        builder.Property(e => e.AreasForImprovement)
            .HasMaxLength(1000);

        builder.Property(e => e.ActionPlan)
            .HasMaxLength(1000);

        builder.Property(e => e.Notes)
            .HasMaxLength(500);

        // Foreign key relationships
        builder.HasOne(e => e.Vendor)
            .WithMany(v => v.VendorPerformances)
            .HasForeignKey(e => e.VendorId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(e => e.Organisation)
            .WithMany()
            .HasForeignKey(e => e.OrganisationId)
            .OnDelete(DeleteBehavior.Restrict);

        // Indexes
        builder.HasIndex(e => e.VendorId)
            .HasDatabaseName("IX_VendorPerformance_VendorId");

        builder.HasIndex(e => e.ReviewPeriodStart)
            .HasDatabaseName("IX_VendorPerformance_ReviewPeriodStart");

        builder.HasIndex(e => e.ReviewPeriodEnd)
            .HasDatabaseName("IX_VendorPerformance_ReviewPeriodEnd");

        builder.HasIndex(e => e.PerformanceRating)
            .HasDatabaseName("IX_VendorPerformance_PerformanceRating");

        builder.HasIndex(e => e.IsPreferredVendor)
            .HasDatabaseName("IX_VendorPerformance_IsPreferredVendor");

        builder.HasIndex(e => e.IsBlacklisted)
            .HasDatabaseName("IX_VendorPerformance_IsBlacklisted");

        builder.HasIndex(e => new { e.VendorId, e.ReviewPeriodStart, e.ReviewPeriodEnd })
            .HasDatabaseName("IX_VendorPerformance_Vendor_Period");

        builder.HasIndex(e => new { e.OrganisationId, e.IsPreferredVendor })
            .HasDatabaseName("IX_VendorPerformance_Organisation_IsPreferredVendor");

        // Check constraints
        builder.ToTable(t => t.HasCheckConstraint(
            "CK_VendorPerformance_PerformanceRating_Range",
            "PerformanceRating >= 0 AND PerformanceRating <= 5"));

        builder.ToTable(t => t.HasCheckConstraint(
            "CK_VendorPerformance_OnTimeDeliveryPercentage_Range",
            "OnTimeDeliveryPercentage >= 0 AND OnTimeDeliveryPercentage <= 100"));

        builder.ToTable(t => t.HasCheckConstraint(
            "CK_VendorPerformance_QualityRating_Range",
            "QualityRating >= 0 AND QualityRating <= 100"));

        builder.ToTable(t => t.HasCheckConstraint(
            "CK_VendorPerformance_CommunicationRating_Range",
            "CommunicationRating >= 0 AND CommunicationRating <= 100"));

        builder.ToTable(t => t.HasCheckConstraint(
            "CK_VendorPerformance_PriceCompetitivenessRating_Range",
            "PriceCompetitivenessRating >= 0 AND PriceCompetitivenessRating <= 100"));

        builder.ToTable(t => t.HasCheckConstraint(
            "CK_VendorPerformance_ReviewPeriod_Valid",
            "ReviewPeriodEnd >= ReviewPeriodStart"));
    }
}