using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PCI.Domain.Models;

namespace PCI.Persistence.Configurations;

public class ChartOfAccountsConfiguration : IEntityTypeConfiguration<ChartOfAccounts>
{
    public void Configure(EntityTypeBuilder<ChartOfAccounts> builder)
    {
        // Primary properties with constraints
        builder.Property(e => e.AccountCode)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(e => e.AccountName)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(e => e.Description)
            .HasMaxLength(1000);

        // Enum configurations with defaults
        builder.Property(e => e.AccountType)
            .IsRequired()
            .HasConversion<int>();

        builder.Property(e => e.SubType)
            .IsRequired()
            .HasConversion<int>();

        builder.Property(e => e.NormalBalanceType)
            .IsRequired()
            .HasConversion<int>();

        // Boolean properties with defaults
        builder.Property(e => e.IsActive)
            .IsRequired()
            .HasDefaultValue(true);

        builder.Property(e => e.IsSystemAccount)
            .IsRequired()
            .HasDefaultValue(false);

        // Decimal precision with default
        builder.Property(e => e.CurrentBalance)
            .IsRequired()
            .HasColumnType("decimal(18,2)")
            .HasDefaultValue(0);

        // External system integration fields
        builder.Property(e => e.ExternalAccountId)
            .HasMaxLength(50);

        builder.Property(e => e.ExternalSystemName)
            .HasMaxLength(100);

        // Foreign key relationships
        builder.HasOne(e => e.ParentAccount)
            .WithMany(e => e.SubAccounts)
            .HasForeignKey(e => e.ParentAccountId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.Organisation)
            .WithMany()
            .HasForeignKey(e => e.OrganisationId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.Currency)
            .WithMany()
            .HasForeignKey(e => e.CurrencyId)
            .OnDelete(DeleteBehavior.SetNull);

        // Performance indexes
        builder.HasIndex(e => e.AccountCode)
            .HasDatabaseName("IX_ChartOfAccounts_AccountCode");

        builder.HasIndex(e => e.AccountName)
            .HasDatabaseName("IX_ChartOfAccounts_AccountName");

        builder.HasIndex(e => e.OrganisationId)
            .HasDatabaseName("IX_ChartOfAccounts_OrganisationId");

        builder.HasIndex(e => e.AccountType)
            .HasDatabaseName("IX_ChartOfAccounts_AccountType");

        builder.HasIndex(e => e.SubType)
            .HasDatabaseName("IX_ChartOfAccounts_SubType");

        builder.HasIndex(e => e.IsActive)
            .HasDatabaseName("IX_ChartOfAccounts_IsActive");

        builder.HasIndex(e => e.ParentAccountId)
            .HasDatabaseName("IX_ChartOfAccounts_ParentAccountId");

        builder.HasIndex(e => e.ExternalAccountId)
            .HasDatabaseName("IX_ChartOfAccounts_ExternalAccountId");

        builder.HasIndex(e => e.IsSystemAccount)
            .HasDatabaseName("IX_ChartOfAccounts_IsSystemAccount");

        // Composite indexes for common queries
        builder.HasIndex(e => new { e.OrganisationId, e.AccountCode })
            .IsUnique()
            .HasDatabaseName("IX_ChartOfAccounts_OrganisationId_AccountCode");

        builder.HasIndex(e => new { e.OrganisationId, e.IsActive })
            .HasDatabaseName("IX_ChartOfAccounts_OrganisationId_IsActive");

        builder.HasIndex(e => new { e.OrganisationId, e.AccountType, e.IsActive })
            .HasDatabaseName("IX_ChartOfAccounts_OrganisationId_AccountType_IsActive");

        // Check constraints for data integrity
        builder.ToTable(t => t.HasCheckConstraint(
            "CK_ChartOfAccounts_AccountCode_Format",
            "AccountCode - '^[0-9A-Z-]+$'"));

        builder.ToTable(t => t.HasCheckConstraint(
            "CK_ChartOfAccounts_AccountCode_NotEmpty",
            "LENGTH(TRIM(AccountCode)) > 0"));

        builder.ToTable(t => t.HasCheckConstraint(
            "CK_ChartOfAccounts_AccountName_NotEmpty",
            "LENGTH(TRIM(AccountName)) > 0"));

        builder.ToTable(t => t.HasCheckConstraint(
            "CK_ChartOfAccounts_CurrentBalance_Valid",
            "CurrentBalance IS NOT NULL"));

        // Ensure sub-account hierarchy doesn't exceed 2 levels (Zoho standard)
        builder.ToTable(t => t.HasCheckConstraint(
            "CK_ChartOfAccounts_MaxDepth",
            @"CASE 
                WHEN ParentAccountId IS NULL THEN 0
                ELSE 1
              END <= 1"));

        // Prevent self-referencing parent accounts
        builder.ToTable(t => t.HasCheckConstraint(
            "CK_ChartOfAccounts_NoSelfReference",
            "ParentAccountId IS NULL OR ParentAccountId != Id"));
    }
}