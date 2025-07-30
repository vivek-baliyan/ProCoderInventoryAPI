using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PCI.Domain.Models;

namespace PCI.Persistence.Configurations;

public class GLAccountConfiguration : IEntityTypeConfiguration<GLAccount>
{
    public void Configure(EntityTypeBuilder<GLAccount> builder)
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

        // Foreign key relationships
        builder.HasOne(e => e.AccountSubType)
            .WithMany(e => e.GLAccounts)
            .HasForeignKey(e => e.AccountSubTypeId)
            .OnDelete(DeleteBehavior.Restrict);

        // Boolean properties with defaults
        builder.Property(e => e.IsActive)
            .IsRequired()
            .HasDefaultValue(true);

        builder.Property(e => e.IsSystemAccount)
            .IsRequired()
            .HasDefaultValue(false);

        // Foreign key relationships
        builder.HasOne(e => e.ParentAccount)
            .WithMany(e => e.SubAccounts)
            .HasForeignKey(e => e.ParentAccountId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.Organisation)
            .WithMany()
            .HasForeignKey(e => e.OrganisationId)
            .OnDelete(DeleteBehavior.Restrict);

        // Performance indexes
        builder.HasIndex(e => e.AccountCode)
            .HasDatabaseName("IX_GLAccount_AccountCode");

        builder.HasIndex(e => e.AccountName)
            .HasDatabaseName("IX_GLAccount_AccountName");

        builder.HasIndex(e => e.OrganisationId)
            .HasDatabaseName("IX_GLAccount_OrganisationId");

        builder.HasIndex(e => e.AccountType)
            .HasDatabaseName("IX_GLAccount_AccountType");

        builder.HasIndex(e => e.AccountSubTypeId)
            .HasDatabaseName("IX_GLAccount_AccountSubTypeId");

        builder.HasIndex(e => e.IsActive)
            .HasDatabaseName("IX_GLAccount_IsActive");

        builder.HasIndex(e => e.ParentAccountId)
            .HasDatabaseName("IX_GLAccount_ParentAccountId");


        builder.HasIndex(e => e.IsSystemAccount)
            .HasDatabaseName("IX_GLAccount_IsSystemAccount");


        // Composite indexes for common queries
        builder.HasIndex(e => new { e.OrganisationId, e.AccountCode })
            .IsUnique()
            .HasDatabaseName("IX_GLAccount_OrganisationId_AccountCode");

        builder.HasIndex(e => new { e.OrganisationId, e.IsActive })
            .HasDatabaseName("IX_GLAccount_OrganisationId_IsActive");

        builder.HasIndex(e => new { e.OrganisationId, e.AccountType, e.IsActive })
            .HasDatabaseName("IX_GLAccount_OrganisationId_AccountType_IsActive");

        // Check constraints for data integrity
        builder.ToTable(t => t.HasCheckConstraint(
            "CK_GLAccount_AccountCode_Format",
            "AccountCode - '^[0-9A-Z-]+$'"));

        builder.ToTable(t => t.HasCheckConstraint(
            "CK_GLAccount_AccountCode_NotEmpty",
            "LENGTH(TRIM(AccountCode)) > 0"));

        builder.ToTable(t => t.HasCheckConstraint(
            "CK_GLAccount_AccountName_NotEmpty",
            "LENGTH(TRIM(AccountName)) > 0"));

        // Ensure sub-account hierarchy doesn't exceed 2 levels (Zoho standard)
        builder.ToTable(t => t.HasCheckConstraint(
            "CK_GLAccount_MaxDepth",
            @"CASE 
                WHEN ParentAccountId IS NULL THEN 0
                ELSE 1
              END <= 1"));

        // Prevent self-referencing parent accounts
        builder.ToTable(t => t.HasCheckConstraint(
            "CK_GLAccount_NoSelfReference",
            "ParentAccountId IS NULL OR ParentAccountId != Id"));
    }
}