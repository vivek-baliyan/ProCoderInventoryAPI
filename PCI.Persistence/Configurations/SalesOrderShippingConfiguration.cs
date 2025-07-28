using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PCI.Domain.Models;

namespace PCI.Persistence.Configurations;

public class SalesOrderShippingConfiguration : IEntityTypeConfiguration<SalesOrderShipping>
{
    public void Configure(EntityTypeBuilder<SalesOrderShipping> builder)
    {
        builder.ToTable("SalesOrderShippings");

        builder.HasKey(sos => sos.Id);

        builder.Property(sos => sos.SalesOrderId)
            .IsRequired();

        builder.Property(sos => sos.ShippingMethod)
            .HasMaxLength(100);

        builder.Property(sos => sos.TrackingNumber)
            .HasMaxLength(100);

        builder.Property(sos => sos.ShippingCost)
            .HasColumnType("decimal(18,2)")
            .HasDefaultValue(0);

        builder.Property(sos => sos.CarrierName)
            .HasMaxLength(100);

        builder.Property(sos => sos.ShippingAddress)
            .HasMaxLength(500);

        builder.Property(sos => sos.ShippingStatus)
            .HasMaxLength(20)
            .HasDefaultValue("Pending");

        builder.Property(sos => sos.ShippingNotes)
            .HasMaxLength(500);

        builder.Property(sos => sos.IsDropShipment)
            .HasDefaultValue(false);

        // Relationships
        builder.HasOne(sos => sos.SalesOrder)
            .WithOne(so => so.SalesOrderShipping)
            .HasForeignKey<SalesOrderShipping>(sos => sos.SalesOrderId)
            .OnDelete(DeleteBehavior.Cascade);

        // Indexes
        builder.HasIndex(sos => sos.SalesOrderId)
            .IsUnique();

        builder.HasIndex(sos => sos.TrackingNumber);
        builder.HasIndex(sos => sos.ShippingStatus);
        builder.HasIndex(sos => sos.EstimatedDeliveryDate);
    }
}