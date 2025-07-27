using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PCI.Domain.Models;
using PCI.Shared.Common.Enums;

namespace PCI.Persistence.Configurations;

public class AccountSubTypeConfiguration : IEntityTypeConfiguration<AccountSubType>
{
    public void Configure(EntityTypeBuilder<AccountSubType> builder)
    {
        // Primary properties with constraints
        builder.Property(e => e.Code)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(e => e.Description)
            .HasMaxLength(500);

        // Enum configuration
        builder.Property(e => e.AccountType)
            .IsRequired()
            .HasConversion<int>();

        // Boolean with default
        builder.Property(e => e.IsActive)
            .IsRequired()
            .HasDefaultValue(true);

        builder.Property(e => e.DisplayOrder)
            .IsRequired()
            .HasDefaultValue(0);

        // Indexes for performance
        builder.HasIndex(e => e.Code)
            .IsUnique()
            .HasDatabaseName("IX_AccountSubType_Code");

        builder.HasIndex(e => e.AccountType)
            .HasDatabaseName("IX_AccountSubType_AccountType");

        builder.HasIndex(e => e.IsActive)
            .HasDatabaseName("IX_AccountSubType_IsActive");

        builder.HasIndex(e => new { e.AccountType, e.DisplayOrder })
            .HasDatabaseName("IX_AccountSubType_AccountType_DisplayOrder");

    }
}