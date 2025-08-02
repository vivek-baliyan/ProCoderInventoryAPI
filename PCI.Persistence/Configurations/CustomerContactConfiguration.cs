using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PCI.Domain.Models;
using PCI.Shared.Common.Enums;

namespace PCI.Persistence.Configurations;

public class CustomerContactConfiguration : IEntityTypeConfiguration<CustomerContact>
{
    public void Configure(EntityTypeBuilder<CustomerContact> builder)
    {
        builder.ToTable("CustomerContacts");

        builder.Property(e => e.CustomerId)
            .IsRequired();

        builder.Property(e => e.ContactType)
            .IsRequired()
            .HasConversion<int>()
            .HasDefaultValue(ContactType.Primary);

        // Contact Person Details
        builder.Property(e => e.Salutation)
            .HasMaxLength(20);

        builder.Property(e => e.FirstName)
            .HasMaxLength(100);

        builder.Property(e => e.LastName)
            .HasMaxLength(100);

        builder.Property(e => e.Email)
            .HasMaxLength(100);

        builder.Property(e => e.PhoneNumber)
            .HasMaxLength(20);

        builder.Property(e => e.MobileNumber)
            .HasMaxLength(20);

        builder.Property(e => e.IsPrimary)
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(e => e.IsActive)
            .IsRequired()
            .HasDefaultValue(true);

        // Relationship
        builder.HasOne(e => e.Customer)
            .WithMany(c => c.CustomerContacts)
            .HasForeignKey(e => e.CustomerId)
            .OnDelete(DeleteBehavior.Cascade);

        // Indexes
        builder.HasIndex(e => e.CustomerId)
            .HasDatabaseName("IX_CustomerContact_CustomerId");

        builder.HasIndex(e => e.Email)
            .HasDatabaseName("IX_CustomerContact_Email");

        builder.HasIndex(e => e.ContactType)
            .HasDatabaseName("IX_CustomerContact_ContactType");

        builder.HasIndex(e => e.IsPrimary)
            .HasDatabaseName("IX_CustomerContact_IsPrimary");

        builder.HasIndex(e => e.IsActive)
            .HasDatabaseName("IX_CustomerContact_IsActive");

        builder.HasIndex(e => new { e.CustomerId, e.ContactType })
            .HasDatabaseName("IX_CustomerContact_Customer_ContactType");

        builder.HasIndex(e => new { e.CustomerId, e.IsPrimary })
            .HasDatabaseName("IX_CustomerContact_Customer_IsPrimary");
    }
}