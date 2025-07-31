using PCI.Domain.Common;

namespace PCI.Domain.Models;

public class BusinessBankInfo : BaseEntity
{
    public string EntityType { get; set; } // "Customer", "Vendor"

    public int EntityId { get; set; }

    public string BankAccountNumber { get; set; }

    public string BankName { get; set; }

    public string BankBranch { get; set; }

    public string IFSCCode { get; set; }

    public string SWIFTCode { get; set; }

    public string AccountHolderName { get; set; }

    public string AccountType { get; set; } // Savings, Current, etc.

    public string BankAddress { get; set; }

    public bool IsPrimary { get; set; } = false;

    public bool IsActive { get; set; } = true;

    public bool IsVerified { get; set; } = false;

    public DateTime? VerificationDate { get; set; }

    public string VerifiedBy { get; set; }

    public string Notes { get; set; }

    // Multi-tenancy
    public int OrganisationId { get; set; }

    // Navigation properties
    public virtual Organisation Organisation { get; set; }
}