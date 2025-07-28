using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PCI.Domain.Models;

namespace PCI.Persistence.Configurations;

public class SalesOrderConfiguration : IEntityTypeConfiguration<SalesOrder>
{
    public void Configure(EntityTypeBuilder<SalesOrder> builder)
    {
        builder.ToTable("SalesOrders");

        builder.HasKey(so => so.Id);

        // Primary properties
        builder.Property(so => so.OrderNumber)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(so => so.OrderDate)
            .IsRequired();

        builder.Property(so => so.Status)
            .HasMaxLength(20)
            .HasDefaultValue("Draft");

        // Reference fields
        builder.Property(so => so.ReferenceNumber)
            .HasMaxLength(100);

        builder.Property(so => so.QuoteNumber)
            .HasMaxLength(100);

        // Required foreign keys
        builder.Property(so => so.CustomerId)
            .IsRequired();

        builder.Property(so => so.OrganisationId)
            .IsRequired();

        // Financial fields
        builder.Property(so => so.SubTotal)
            .HasColumnType("decimal(18,2)")
            .HasDefaultValue(0);

        builder.Property(so => so.TaxAmount)
            .HasColumnType("decimal(18,2)")
            .HasDefaultValue(0);

        builder.Property(so => so.TotalAmount)
            .HasColumnType("decimal(18,2)")
            .HasDefaultValue(0);

        // Text fields
        builder.Property(so => so.Notes)
            .HasMaxLength(1000);

        builder.Property(so => so.BillingAddress)
            .HasMaxLength(500);

        // Relationships
        builder.HasOne(so => so.Customer)
            .WithMany(c => c.SalesOrders)
            .HasForeignKey(so => so.CustomerId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(so => so.Organisation)
            .WithMany()
            .HasForeignKey(so => so.OrganisationId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(so => so.PriceList)
            .WithMany()
            .HasForeignKey(so => so.PriceListId)
            .OnDelete(DeleteBehavior.SetNull);

        // Indexes for performance
        builder.HasIndex(so => so.OrderNumber)
            .IsUnique()
            .HasDatabaseName("IX_SalesOrder_OrderNumber");

        builder.HasIndex(so => so.CustomerId)
            .HasDatabaseName("IX_SalesOrder_CustomerId");

        builder.HasIndex(so => so.OrganisationId)
            .HasDatabaseName("IX_SalesOrder_OrganisationId");

        builder.HasIndex(so => so.Status)
            .HasDatabaseName("IX_SalesOrder_Status");

        builder.HasIndex(so => so.OrderDate)
            .HasDatabaseName("IX_SalesOrder_OrderDate");

        builder.HasIndex(so => so.ReferenceNumber)
            .HasDatabaseName("IX_SalesOrder_ReferenceNumber");

        // Composite indexes for common queries
        builder.HasIndex(so => new { so.OrganisationId, so.CustomerId })
            .HasDatabaseName("IX_SalesOrder_OrganisationId_CustomerId");

        builder.HasIndex(so => new { so.OrganisationId, so.Status })
            .HasDatabaseName("IX_SalesOrder_OrganisationId_Status");

        builder.HasIndex(so => new { so.OrganisationId, so.OrderDate })
            .HasDatabaseName("IX_SalesOrder_OrganisationId_OrderDate");

        // Check constraints
        builder.ToTable(t => t.HasCheckConstraint(
            "CK_SalesOrder_SubTotal_NonNegative",
            "SubTotal >= 0"));

        builder.ToTable(t => t.HasCheckConstraint(
            "CK_SalesOrder_TaxAmount_NonNegative",
            "TaxAmount >= 0"));

        builder.ToTable(t => t.HasCheckConstraint(
            "CK_SalesOrder_TotalAmount_NonNegative",
            "TotalAmount >= 0"));
    }
}