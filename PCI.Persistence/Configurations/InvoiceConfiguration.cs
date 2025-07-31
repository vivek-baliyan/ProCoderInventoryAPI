using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PCI.Domain.Models;

namespace PCI.Persistence.Configurations;

public class InvoiceConfiguration : IEntityTypeConfiguration<Invoice>
{
    public void Configure(EntityTypeBuilder<Invoice> builder)
    {
        builder.ToTable("Invoices");

        builder.HasKey(i => i.Id);

        builder.Property(i => i.Id)
            .IsRequired()
            .ValueGeneratedOnAdd();

        builder.Property(i => i.InvoiceNumber)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(i => i.InvoiceDate)
            .IsRequired();

        builder.Property(i => i.Status)
            .HasMaxLength(20)
            .HasDefaultValue("Draft");

        builder.Property(i => i.CustomerId)
            .IsRequired();

        builder.Property(i => i.OrganisationId)
            .IsRequired();

        builder.Property(i => i.SubTotal)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(i => i.TaxAmount)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(i => i.TotalAmount)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(i => i.AmountPaid)
            .HasColumnType("decimal(18,2)")
            .HasDefaultValue(0);

        builder.Property(i => i.AmountDue)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(i => i.Notes)
            .HasMaxLength(1000);

        builder.Property(i => i.PaymentTerms)
            .HasMaxLength(100);

        // Indexes
        builder.HasIndex(i => i.InvoiceNumber)
            .IsUnique()
            .HasDatabaseName("IX_Invoices_InvoiceNumber");

        builder.HasIndex(i => i.InvoiceDate)
            .HasDatabaseName("IX_Invoices_InvoiceDate");

        builder.HasIndex(i => i.Status)
            .HasDatabaseName("IX_Invoices_Status");

        builder.HasIndex(i => i.CustomerId)
            .HasDatabaseName("IX_Invoices_CustomerId");

        builder.HasIndex(i => i.OrganisationId)
            .HasDatabaseName("IX_Invoices_OrganisationId");

        builder.HasIndex(i => i.SalesOrderId)
            .HasDatabaseName("IX_Invoices_SalesOrderId");

        // Relationships
        builder.HasOne(i => i.Customer)
            .WithMany()
            .HasForeignKey(i => i.CustomerId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(i => i.Organisation)
            .WithMany()
            .HasForeignKey(i => i.OrganisationId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(i => i.SalesOrder)
            .WithMany()
            .HasForeignKey(i => i.SalesOrderId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(i => i.PriceList)
            .WithMany()
            .HasForeignKey(i => i.PriceListId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasMany(i => i.InvoiceItems)
            .WithOne(ii => ii.Invoice)
            .HasForeignKey(ii => ii.InvoiceId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}