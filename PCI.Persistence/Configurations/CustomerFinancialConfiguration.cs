using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PCI.Domain.Models;
using PCI.Shared.Common.Enums;

namespace PCI.Persistence.Configurations;

public class CustomerFinancialConfiguration : IEntityTypeConfiguration<CustomerFinancial>
{
    public void Configure(EntityTypeBuilder<CustomerFinancial> builder)
    {
        builder.ToTable("CustomerFinancials");

        builder.HasKey(cf => cf.Id);

        builder.Property(cf => cf.CustomerId)
            .IsRequired();

        builder.Property(cf => cf.CreditLimit)
            .HasColumnType("decimal(18,2)")
            .HasDefaultValue(0);

        builder.Property(cf => cf.PaymentTerms)
            .HasConversion<int>()
            .HasDefaultValue(PaymentTerms.Net30);

        builder.Property(cf => cf.CustomPaymentTermDays)
            .IsRequired(false);

        builder.Property(cf => cf.PreferredPaymentMethod)
            .HasConversion<int>()
            .HasDefaultValue(CustomerPaymentMethod.BankTransfer);

        builder.Property(cf => cf.IsOnCreditHold)
            .HasDefaultValue(false);

        builder.Property(cf => cf.CreditHoldReason)
            .HasMaxLength(500);

        // Relationships
        builder.HasOne(cf => cf.Customer)
            .WithOne(c => c.CustomerFinancial)
            .HasForeignKey<CustomerFinancial>(cf => cf.CustomerId)
            .OnDelete(DeleteBehavior.Cascade);

        // Indexes
        builder.HasIndex(cf => cf.CustomerId)
            .IsUnique();
    }
}