using PCI.Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace PCI.Domain.Models;

public class BusinessBankInfo : BaseEntity
{
    [Required]
    [StringLength(50)]
    public string EntityType { get; set; } // "Customer", "Vendor"

    [Required]
    public int EntityId { get; set; }

    [Required]
    [StringLength(100)]
    public string BankAccountNumber { get; set; }

    [Required]
    [StringLength(200)]
    public string BankName { get; set; }

    [StringLength(200)]
    public string BankBranch { get; set; }

    [StringLength(20)]
    public string IFSCCode { get; set; }

    [StringLength(20)]
    public string SWIFTCode { get; set; }

    [StringLength(100)]
    public string AccountHolderName { get; set; }

    [StringLength(50)]
    public string AccountType { get; set; } // Savings, Current, etc.

    [StringLength(100)]
    public string BankAddress { get; set; }

    public bool IsPrimary { get; set; } = false;

    public bool IsActive { get; set; } = true;

    public bool IsVerified { get; set; } = false;

    public DateTime? VerificationDate { get; set; }

    [StringLength(100)]
    public string VerifiedBy { get; set; }

    [StringLength(500)]
    public string Notes { get; set; }

    // Multi-tenancy
    public int OrganisationId { get; set; }

    // Navigation properties
    public virtual Organisation Organisation { get; set; }
}