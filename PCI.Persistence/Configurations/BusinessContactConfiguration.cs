using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PCI.Domain.Models;
using PCI.Shared.Common.Enums;

namespace PCI.Persistence.Configurations;

public class BusinessContactConfiguration : IEntityTypeConfiguration<BusinessContact>
{
    public void Configure(EntityTypeBuilder<BusinessContact> builder)
    {
        builder.Property(e => e.EntityType)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(e => e.EntityId)
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

        // Indexes
        builder.HasIndex(e => new { e.EntityType, e.EntityId })
            .HasDatabaseName("IX_BusinessContact_Entity");

        builder.HasIndex(e => e.Email)
            .HasDatabaseName("IX_BusinessContact_Email");

        builder.HasIndex(e => e.ContactType)
            .HasDatabaseName("IX_BusinessContact_ContactType");

        builder.HasIndex(e => e.IsPrimary)
            .HasDatabaseName("IX_BusinessContact_IsPrimary");

        builder.HasIndex(e => e.IsActive)
            .HasDatabaseName("IX_BusinessContact_IsActive");

        builder.HasIndex(e => new { e.EntityType, e.EntityId, e.ContactType })
            .HasDatabaseName("IX_BusinessContact_Entity_ContactType");

        builder.HasIndex(e => new { e.EntityType, e.EntityId, e.IsPrimary })
            .HasDatabaseName("IX_BusinessContact_Entity_IsPrimary");
    }
}