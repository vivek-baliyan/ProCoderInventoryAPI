using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PCI.Domain.Models;

namespace PCI.Persistence.Configurations;

public class BusinessBankInfoConfiguration : IEntityTypeConfiguration<BusinessBankInfo>
{
    public void Configure(EntityTypeBuilder<BusinessBankInfo> builder)
    {
        builder.Property(e => e.EntityType)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(e => e.EntityId)
            .IsRequired();

        builder.Property(e => e.BankAccountNumber)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(e => e.BankName)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(e => e.BankBranch)
            .HasMaxLength(200);

        builder.Property(e => e.IFSCCode)
            .HasMaxLength(20);

        builder.Property(e => e.SWIFTCode)
            .HasMaxLength(20);

        builder.Property(e => e.AccountHolderName)
            .HasMaxLength(100);

        builder.Property(e => e.AccountType)
            .HasMaxLength(50);

        builder.Property(e => e.BankAddress)
            .HasMaxLength(100);

        builder.Property(e => e.IsPrimary)
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(e => e.IsActive)
            .IsRequired()
            .HasDefaultValue(true);

        builder.Property(e => e.IsVerified)
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(e => e.VerifiedBy)
            .HasMaxLength(100);

        builder.Property(e => e.Notes)
            .HasMaxLength(500);

        // Foreign key relationship
        builder.HasOne(e => e.Organisation)
            .WithMany()
            .HasForeignKey(e => e.OrganisationId)
            .OnDelete(DeleteBehavior.Restrict);

        // Indexes
        builder.HasIndex(e => new { e.EntityType, e.EntityId })
            .HasDatabaseName("IX_BusinessBankInfo_Entity");

        builder.HasIndex(e => e.BankAccountNumber)
            .HasDatabaseName("IX_BusinessBankInfo_BankAccountNumber");

        builder.HasIndex(e => e.IFSCCode)
            .HasDatabaseName("IX_BusinessBankInfo_IFSCCode");

        builder.HasIndex(e => e.IsPrimary)
            .HasDatabaseName("IX_BusinessBankInfo_IsPrimary");

        builder.HasIndex(e => e.IsActive)
            .HasDatabaseName("IX_BusinessBankInfo_IsActive");

        builder.HasIndex(e => e.IsVerified)
            .HasDatabaseName("IX_BusinessBankInfo_IsVerified");

        builder.HasIndex(e => new { e.EntityType, e.EntityId, e.IsPrimary })
            .HasDatabaseName("IX_BusinessBankInfo_Entity_IsPrimary");

        builder.HasIndex(e => new { e.OrganisationId, e.EntityType })
            .HasDatabaseName("IX_BusinessBankInfo_Organisation_EntityType");

        builder.HasIndex(e => new { e.BankName, e.BankAccountNumber })
            .HasDatabaseName("IX_BusinessBankInfo_Bank_Account");
    }
}