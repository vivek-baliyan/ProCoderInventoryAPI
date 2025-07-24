using PCI.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PCI.Domain.Models;

public class ChartOfAccounts : BaseEntity
{
    [Required]
    [StringLength(20)]
    public string AccountCode { get; set; }

    [Required]
    [StringLength(200)]
    public string AccountName { get; set; }

    [Required]
    [StringLength(50)]
    public string AccountType { get; set; } // Asset, Liability, Income, Expense, Equity

    [StringLength(50)]
    public string SubType { get; set; } // Current Asset, Fixed Asset, Sales Revenue, COGS, etc.

    [StringLength(1000)]
    public string Description { get; set; }

    public bool IsActive { get; set; } = true;

    // Sub-account hierarchy (up to 2 levels)
    public int? ParentAccountId { get; set; }

    [ForeignKey("ParentAccountId")]
    public virtual ChartOfAccounts ParentAccount { get; set; }

    // Current balance (for balance sheet accounts)
    [Column(TypeName = "decimal(18,2)")]
    public decimal CurrentBalance { get; set; } = 0;

    public int OrganisationId { get; set; }

    [ForeignKey("OrganisationId")]
    public virtual Organisation Organisation { get; set; }

    // Navigation properties
    public virtual ICollection<ChartOfAccounts> SubAccounts { get; set; } = new HashSet<ChartOfAccounts>();
    public virtual ICollection<Product> ProductsSalesAccount { get; set; } = new HashSet<Product>();
    public virtual ICollection<Product> ProductsPurchaseAccount { get; set; } = new HashSet<Product>();
    public virtual ICollection<Product> ProductsInventoryAccount { get; set; } = new HashSet<Product>();
    public virtual ICollection<AccountTransaction> AccountTransactions { get; set; } = new HashSet<AccountTransaction>();
}