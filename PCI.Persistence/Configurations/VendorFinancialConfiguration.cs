using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PCI.Domain.Models;

namespace PCI.Persistence.Configurations;

public class VendorFinancialConfiguration : IEntityTypeConfiguration<VendorFinancial>
{
    public void Configure(EntityTypeBuilder<VendorFinancial> builder)
    {
        builder.ToTable("VendorFinancials");

        builder.HasKey(vf => vf.Id);

        builder.Property(vf => vf.VendorId)
            .IsRequired();

        builder.Property(vf => vf.CurrentBalance)
            .HasColumnType("decimal(18,2)")
            .HasDefaultValue(0);

        builder.Property(vf => vf.OutstandingAmount)
            .HasColumnType("decimal(18,2)")
            .HasDefaultValue(0);

        builder.Property(vf => vf.PaymentTermDays)
            .HasDefaultValue(30);

        builder.Property(vf => vf.TotalPurchasesYTD)
            .HasColumnType("decimal(18,2)")
            .HasDefaultValue(0);

        builder.Property(vf => vf.TotalPurchasesLifetime)
            .HasColumnType("decimal(18,2)")
            .HasDefaultValue(0);

        builder.Property(vf => vf.MinimumOrderValue)
            .HasColumnType("decimal(18,2)")
            .HasDefaultValue(0);

        builder.Property(vf => vf.PreferredPaymentMethod)
            .HasMaxLength(50)
            .HasDefaultValue("BankTransfer");

        builder.Property(vf => vf.OnTimeDeliveryRate)
            .HasColumnType("decimal(3,2)")
            .HasDefaultValue(0);

        builder.Property(vf => vf.QualityRating)
            .HasColumnType("decimal(3,2)")
            .HasDefaultValue(0);

        builder.Property(vf => vf.EarlyPaymentDiscountPercentage)
            .HasColumnType("decimal(5,2)")
            .HasDefaultValue(0);

        builder.Property(vf => vf.EarlyPaymentDiscountDays)
            .HasDefaultValue(10);

        builder.Property(vf => vf.IsBlacklisted)
            .HasDefaultValue(false);

        builder.Property(vf => vf.BlacklistReason)
            .HasMaxLength(500);

        builder.Property(vf => vf.Notes)
            .HasMaxLength(500);

        // OrganisationId removed - inherited through Vendor relationship

        // Relationships
        builder.HasOne(vf => vf.Vendor)
            .WithOne(v => v.VendorFinancial)
            .HasForeignKey<VendorFinancial>(vf => vf.VendorId)
            .OnDelete(DeleteBehavior.Cascade);

        // No direct Organisation relationship - inherited through Vendor

        // Indexes
        builder.HasIndex(vf => vf.VendorId)
            .IsUnique();

        // Removed OrganisationId index as field was removed
    }
}