using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PCI.Domain.Models;

namespace PCI.Persistence.Configurations;

public class SalesOrderPaymentConfiguration : IEntityTypeConfiguration<SalesOrderPayment>
{
    public void Configure(EntityTypeBuilder<SalesOrderPayment> builder)
    {
        builder.ToTable("SalesOrderPayments");

        builder.HasKey(sop => sop.Id);

        builder.Property(sop => sop.SalesOrderId)
            .IsRequired();

        builder.Property(sop => sop.PaymentStatus)
            .HasMaxLength(20)
            .HasDefaultValue("Pending");

        builder.Property(sop => sop.PaymentTerms)
            .HasMaxLength(50)
            .HasDefaultValue("Net30");

        builder.Property(sop => sop.PaidAmount)
            .HasColumnType("decimal(18,2)")
            .HasDefaultValue(0);

        builder.Property(sop => sop.BalanceAmount)
            .HasColumnType("decimal(18,2)")
            .HasDefaultValue(0);

        builder.Property(sop => sop.PaymentMethod)
            .HasMaxLength(100);

        builder.Property(sop => sop.PaymentReference)
            .HasMaxLength(100);

        builder.Property(sop => sop.PaymentNotes)
            .HasMaxLength(500);

        // Relationships
        builder.HasOne(sop => sop.SalesOrder)
            .WithOne(so => so.SalesOrderPayment)
            .HasForeignKey<SalesOrderPayment>(sop => sop.SalesOrderId)
            .OnDelete(DeleteBehavior.Cascade);

        // Indexes
        builder.HasIndex(sop => sop.SalesOrderId)
            .IsUnique();

        builder.HasIndex(sop => sop.PaymentStatus);
        builder.HasIndex(sop => sop.DueDate);
    }
}