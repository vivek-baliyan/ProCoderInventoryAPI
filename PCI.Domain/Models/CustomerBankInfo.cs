using PCI.Domain.Common;

namespace PCI.Domain.Models;

public class CustomerBankInfo : BaseEntity
{
    public int CustomerId { get; set; }
    
    public string BankName { get; set; }
    public string AccountNumber { get; set; }
    public string AccountHolderName { get; set; }
    public string IFSCCode { get; set; }
    public string SWIFTCode { get; set; }
    public string BranchAddress { get; set; }

    public bool IsPrimary { get; set; } = false;
    public bool IsActive { get; set; } = true;

    // Navigation property
    public virtual Customer Customer { get; set; }
}