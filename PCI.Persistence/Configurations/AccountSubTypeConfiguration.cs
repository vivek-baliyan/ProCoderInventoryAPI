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

        // Seed data
        builder.HasData(
            // Asset Sub-types
            new AccountSubType { Id = 1, Code = "CASH", Name = "Cash", Description = "Cash and cash equivalents", AccountType = AccountType.Asset, DisplayOrder = 1, IsActive = true },
            new AccountSubType { Id = 2, Code = "BANK", Name = "Bank", Description = "Bank accounts", AccountType = AccountType.Asset, DisplayOrder = 2, IsActive = true },
            new AccountSubType { Id = 3, Code = "AR", Name = "Accounts Receivable", Description = "Money owed by customers", AccountType = AccountType.Asset, DisplayOrder = 3, IsActive = true },
            new AccountSubType { Id = 4, Code = "INV", Name = "Inventory", Description = "Goods held for sale", AccountType = AccountType.Asset, DisplayOrder = 4, IsActive = true },
            new AccountSubType { Id = 5, Code = "OCA", Name = "Other Current Asset", Description = "Other current assets", AccountType = AccountType.Asset, DisplayOrder = 5, IsActive = true },
            new AccountSubType { Id = 6, Code = "FA", Name = "Fixed Asset", Description = "Property, plant and equipment", AccountType = AccountType.Asset, DisplayOrder = 6, IsActive = true },
            new AccountSubType { Id = 7, Code = "AD", Name = "Accumulated Depreciation", Description = "Accumulated depreciation on fixed assets", AccountType = AccountType.Asset, DisplayOrder = 7, IsActive = true },
            new AccountSubType { Id = 8, Code = "OA", Name = "Other Asset", Description = "Other non-current assets", AccountType = AccountType.Asset, DisplayOrder = 8, IsActive = true },

            // Liability Sub-types
            new AccountSubType { Id = 20, Code = "AP", Name = "Accounts Payable", Description = "Money owed to suppliers", AccountType = AccountType.Liability, DisplayOrder = 1, IsActive = true },
            new AccountSubType { Id = 21, Code = "CC", Name = "Credit Card", Description = "Credit card liabilities", AccountType = AccountType.Liability, DisplayOrder = 2, IsActive = true },
            new AccountSubType { Id = 22, Code = "TP", Name = "Tax Payable", Description = "Taxes owed", AccountType = AccountType.Liability, DisplayOrder = 3, IsActive = true },
            new AccountSubType { Id = 23, Code = "OCL", Name = "Other Current Liability", Description = "Other current liabilities", AccountType = AccountType.Liability, DisplayOrder = 4, IsActive = true },
            new AccountSubType { Id = 24, Code = "LTL", Name = "Long Term Liability", Description = "Long-term debt and obligations", AccountType = AccountType.Liability, DisplayOrder = 5, IsActive = true },

            // Equity Sub-types
            new AccountSubType { Id = 40, Code = "OE", Name = "Owner Equity", Description = "Owner's equity", AccountType = AccountType.Equity, DisplayOrder = 1, IsActive = true },
            new AccountSubType { Id = 41, Code = "RE", Name = "Retained Earnings", Description = "Accumulated profits", AccountType = AccountType.Equity, DisplayOrder = 2, IsActive = true },
            new AccountSubType { Id = 42, Code = "OBE", Name = "Opening Balance Equity", Description = "Opening balance adjustments", AccountType = AccountType.Equity, DisplayOrder = 3, IsActive = true },

            // Income Sub-types
            new AccountSubType { Id = 60, Code = "SR", Name = "Sales Revenue", Description = "Revenue from sales", AccountType = AccountType.Income, DisplayOrder = 1, IsActive = true },
            new AccountSubType { Id = 61, Code = "SER", Name = "Service Revenue", Description = "Revenue from services", AccountType = AccountType.Income, DisplayOrder = 2, IsActive = true },
            new AccountSubType { Id = 62, Code = "OI", Name = "Other Income", Description = "Other income sources", AccountType = AccountType.Income, DisplayOrder = 3, IsActive = true },
            new AccountSubType { Id = 63, Code = "II", Name = "Interest Income", Description = "Interest earned", AccountType = AccountType.Income, DisplayOrder = 4, IsActive = true },

            // Expense Sub-types
            new AccountSubType { Id = 80, Code = "COGS", Name = "Cost of Goods Sold", Description = "Direct costs of producing goods", AccountType = AccountType.Expense, DisplayOrder = 1, IsActive = true },
            new AccountSubType { Id = 81, Code = "OPEX", Name = "Operating Expense", Description = "Operating expenses", AccountType = AccountType.Expense, DisplayOrder = 2, IsActive = true },
            new AccountSubType { Id = 82, Code = "ADMIN", Name = "Administrative Expense", Description = "Administrative expenses", AccountType = AccountType.Expense, DisplayOrder = 3, IsActive = true },
            new AccountSubType { Id = 83, Code = "SELL", Name = "Selling Expense", Description = "Sales and marketing expenses", AccountType = AccountType.Expense, DisplayOrder = 4, IsActive = true },
            new AccountSubType { Id = 84, Code = "IE", Name = "Interest Expense", Description = "Interest paid on debt", AccountType = AccountType.Expense, DisplayOrder = 5, IsActive = true },
            new AccountSubType { Id = 85, Code = "TAX", Name = "Tax Expense", Description = "Tax expenses", AccountType = AccountType.Expense, DisplayOrder = 6, IsActive = true },
            new AccountSubType { Id = 86, Code = "OEXP", Name = "Other Expense", Description = "Other miscellaneous expenses", AccountType = AccountType.Expense, DisplayOrder = 7, IsActive = true }
        );
    }
}