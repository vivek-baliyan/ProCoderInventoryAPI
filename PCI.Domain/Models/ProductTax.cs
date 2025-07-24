using PCI.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PCI.Domain.Models;

public class ProductTax : BaseEntity
{
    public int ProductId { get; set; }

    public int? HSNMasterId { get; set; }
    public int? TaxMasterId { get; set; }

    public bool IsTaxExempt { get; set; } = false;

    [StringLength(200)]
    public string TaxExemptReason { get; set; }

    [ForeignKey("ProductId")]
    public virtual Product Product { get; set; }

    [ForeignKey("HSNMasterId")]
    public virtual HSNMaster HSNMaster { get; set; }

    [ForeignKey("TaxMasterId")]
    public virtual TaxMaster TaxMaster { get; set; }
}