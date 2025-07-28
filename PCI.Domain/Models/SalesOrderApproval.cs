using PCI.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PCI.Domain.Models;

public class SalesOrderApproval : BaseEntity
{
    [Required]
    public int SalesOrderId { get; set; }

    [StringLength(20)]
    public string ApprovalStatus { get; set; } = "Pending"; // Pending, Approved, Rejected

    // TODO: Add approval level logic based on order value, customer type, or organisation workflow
    // [StringLength(100)]
    // public string ApprovalLevel { get; set; } = "Level1"; // Level1, Level2, Final

    [StringLength(100)]
    public string ApprovedBy { get; set; }

    public DateTime? ApprovedDate { get; set; }

    [StringLength(1000)]
    public string ApprovalNotes { get; set; }

    [StringLength(500)]
    public string RejectionReason { get; set; }

    // Navigation properties
    [ForeignKey("SalesOrderId")]
    public virtual SalesOrder SalesOrder { get; set; }
}