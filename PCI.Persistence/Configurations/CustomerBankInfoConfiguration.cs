using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PCI.Domain.Models;

namespace PCI.Persistence.Configurations;

public class CustomerBankInfoConfiguration : IEntityTypeConfiguration<CustomerBankInfo>
{
    public void Configure(EntityTypeBuilder<CustomerBankInfo> builder)
    {
        builder.ToTable("CustomerBankInfos");

        builder.Property(e => e.CustomerId)
            .IsRequired();

        builder.Property(e => e.BankName)
            .HasMaxLength(200);

        builder.Property(e => e.AccountNumber)
            .HasMaxLength(50);

        builder.Property(e => e.AccountHolderName)
            .HasMaxLength(200);

        builder.Property(e => e.IFSCCode)
            .HasMaxLength(20);

        builder.Property(e => e.SWIFTCode)
            .HasMaxLength(20);

        builder.Property(e => e.BranchAddress)
            .HasMaxLength(500);

        builder.Property(e => e.IsPrimary)
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(e => e.IsActive)
            .IsRequired()
            .HasDefaultValue(true);

        // Relationship
        builder.HasOne(e => e.Customer)
            .WithMany(c => c.CustomerBankInfos)
            .HasForeignKey(e => e.CustomerId)
            .OnDelete(DeleteBehavior.Cascade);

        // Indexes
        builder.HasIndex(e => e.CustomerId)
            .HasDatabaseName("IX_CustomerBankInfo_CustomerId");

        builder.HasIndex(e => e.AccountNumber)
            .HasDatabaseName("IX_CustomerBankInfo_AccountNumber");

        builder.HasIndex(e => e.IsPrimary)
            .HasDatabaseName("IX_CustomerBankInfo_IsPrimary");

        builder.HasIndex(e => e.IsActive)
            .HasDatabaseName("IX_CustomerBankInfo_IsActive");

        builder.HasIndex(e => new { e.CustomerId, e.IsPrimary })
            .HasDatabaseName("IX_CustomerBankInfo_Customer_IsPrimary");
    }
}