using PCI.Domain.Common;
using PCI.Shared.Common.Enums;

namespace PCI.Domain.Models;

public class ChartOfAccounts : BaseEntity
{
    public string AccountCode { get; set; }
    public string AccountName { get; set; }
    public AccountType AccountType { get; set; }
    public AccountSubType SubType { get; set; }
    public BalanceType NormalBalanceType { get; set; }
    public string Description { get; set; }
    public bool IsActive { get; set; } = true;
    public bool IsSystemAccount { get; set; } = false;
    public decimal CurrentBalance { get; set; } = 0;

    // Hierarchy
    public int? ParentAccountId { get; set; }
    public virtual ChartOfAccounts ParentAccount { get; set; }

    // Currency and integration
    public int? CurrencyId { get; set; }
    public virtual Currency Currency { get; set; }
    public string ExternalAccountId { get; set; }
    public string ExternalSystemName { get; set; }
    public DateTime? LastSyncDate { get; set; }

    // Multi-tenancy
    public int OrganisationId { get; set; }
    public virtual Organisation Organisation { get; set; }

    // Navigation properties
    public virtual ICollection<ChartOfAccounts> SubAccounts { get; set; } = new HashSet<ChartOfAccounts>();
    public virtual ICollection<Product> ProductsSalesAccount { get; set; } = new HashSet<Product>();
    public virtual ICollection<Product> ProductsPurchaseAccount { get; set; } = new HashSet<Product>();
    public virtual ICollection<Product> ProductsInventoryAccount { get; set; } = new HashSet<Product>();
    public virtual ICollection<AccountTransaction> AccountTransactions { get; set; } = new HashSet<AccountTransaction>();
}