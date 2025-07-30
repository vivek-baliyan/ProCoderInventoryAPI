using PCI.Domain.Common;
using PCI.Shared.Common.Enums;

namespace PCI.Domain.Models;

public class GLAccount : BaseEntity
{
    public string AccountCode { get; set; }
    public string AccountName { get; set; }
    public AccountType AccountType { get; set; }
    public string Description { get; set; }
    public bool IsActive { get; set; } = true;
    public bool IsSystemAccount { get; set; } = false;

    public int AccountSubTypeId { get; set; }
    public virtual AccountSubType AccountSubType { get; set; }

    // Hierarchy
    public int? ParentAccountId { get; set; }
    public virtual GLAccount ParentAccount { get; set; }

    // Multi-tenancy
    public int OrganisationId { get; set; }
    public virtual Organisation Organisation { get; set; }


    // Navigation properties
    public virtual ICollection<GLAccount> SubAccounts { get; set; } = new HashSet<GLAccount>();
    public virtual ICollection<Product> ProductsSalesAccount { get; set; } = new HashSet<Product>();
    public virtual ICollection<Product> ProductsPurchaseAccount { get; set; } = new HashSet<Product>();
    public virtual ICollection<Product> ProductsInventoryAccount { get; set; } = new HashSet<Product>();
    public virtual ICollection<AccountTransaction> AccountTransactions { get; set; } = new HashSet<AccountTransaction>();
}