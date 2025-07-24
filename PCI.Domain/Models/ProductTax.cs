using PCI.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PCI.Domain.Models;

public class ProductTax : BaseEntity
{
    public int ProductId { get; set; }

    public int? TaxClassificationId { get; set; }
    public int? TaxMasterId { get; set; }

    public bool IsTaxExempt { get; set; } = false;

    [StringLength(200)]
    public string TaxExemptReason { get; set; }

    [ForeignKey("ProductId")]
    public virtual Product Product { get; set; }

    [ForeignKey("TaxClassificationId")]
    public virtual TaxClassification TaxClassification { get; set; }

    [ForeignKey("TaxMasterId")]
    public virtual TaxMaster TaxMaster { get; set; }
}