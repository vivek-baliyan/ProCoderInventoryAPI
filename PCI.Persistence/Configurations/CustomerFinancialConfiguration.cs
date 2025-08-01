using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PCI.Domain.Models;

namespace PCI.Persistence.Configurations;

public class CustomerFinancialConfiguration : IEntityTypeConfiguration<CustomerFinancial>
{
    public void Configure(EntityTypeBuilder<CustomerFinancial> builder)
    {
        builder.ToTable("CustomerFinancials");

        builder.HasKey(cf => cf.Id);

        builder.Property(cf => cf.CustomerId)
            .IsRequired();

        builder.Property(cf => cf.CurrentBalance)
            .HasColumnType("decimal(18,2)")
            .HasDefaultValue(0);

        builder.Property(cf => cf.CreditLimit)
            .HasColumnType("decimal(18,2)")
            .HasDefaultValue(0);

        builder.Property(cf => cf.OutstandingAmount)
            .HasColumnType("decimal(18,2)")
            .HasDefaultValue(0);

        builder.Property(cf => cf.PaymentTermDays)
            .HasDefaultValue(30);

        builder.Property(cf => cf.PaymentTerms)
            .HasMaxLength(100)
            .HasDefaultValue("Due on Receipt");

        builder.Property(cf => cf.TotalSalesYTD)
            .HasColumnType("decimal(18,2)")
            .HasDefaultValue(0);

        builder.Property(cf => cf.TotalSalesLifetime)
            .HasColumnType("decimal(18,2)")
            .HasDefaultValue(0);

        builder.Property(cf => cf.MinimumOrderValue)
            .HasColumnType("decimal(18,2)")
            .HasDefaultValue(0);

        builder.Property(cf => cf.PreferredPaymentMethod)
            .HasMaxLength(50)
            .HasDefaultValue("BankTransfer");

        builder.Property(cf => cf.IsOnCreditHold)
            .HasDefaultValue(false);

        builder.Property(cf => cf.CreditHoldReason)
            .HasMaxLength(500);

        builder.Property(cf => cf.DefaultDiscountPercentage)
            .HasColumnType("decimal(5,2)")
            .HasDefaultValue(0);

        builder.Property(cf => cf.Notes)
            .HasMaxLength(500);

        // OrganisationId removed - inherited through Customer relationship

        // Relationships
        builder.HasOne(cf => cf.Customer)
            .WithOne(c => c.CustomerFinancial)
            .HasForeignKey<CustomerFinancial>(cf => cf.CustomerId)
            .OnDelete(DeleteBehavior.Cascade);

        // No direct Organisation relationship - inherited through Customer

        // Indexes
        builder.HasIndex(cf => cf.CustomerId)
            .IsUnique();

        // Removed OrganisationId index as field was removed
    }
}