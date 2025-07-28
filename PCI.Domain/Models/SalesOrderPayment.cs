using PCI.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PCI.Domain.Models;

public class SalesOrderPayment : BaseEntity
{
    [Required]
    public int SalesOrderId { get; set; }

    [StringLength(20)]
    public string PaymentStatus { get; set; } = "Pending"; // Pending, Paid, PartiallyPaid, Overdue

    [StringLength(50)]
    public string PaymentTerms { get; set; } = "Net30";

    public DateTime? DueDate { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal PaidAmount { get; set; } = 0;

    [Column(TypeName = "decimal(18,2)")]
    public decimal BalanceAmount { get; set; } = 0;

    [StringLength(100)]
    public string PaymentMethod { get; set; }

    [StringLength(100)]
    public string PaymentReference { get; set; }

    public DateTime? PaymentDate { get; set; }

    [StringLength(500)]
    public string PaymentNotes { get; set; }

    // Navigation properties
    [ForeignKey("SalesOrderId")]
    public virtual SalesOrder SalesOrder { get; set; }
}