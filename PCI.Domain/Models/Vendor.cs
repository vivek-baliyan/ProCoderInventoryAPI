using PCI.Domain.Common;
using PCI.Shared.Common.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PCI.Domain.Models;

public class Vendor : BaseEntity
{
    [Required]
    [StringLength(50)]
    public string VendorCode { get; set; }

    [Required]
    [StringLength(200)]
    public string VendorName { get; set; }

    [StringLength(200)]
    public string CompanyName { get; set; }

    [StringLength(100)]
    public string ContactPerson { get; set; }

    [StringLength(20)]
    public string PhoneNumber { get; set; }

    [StringLength(20)]
    public string MobileNumber { get; set; }

    [StringLength(100)]
    public string Email { get; set; }

    [StringLength(500)]
    public string Address { get; set; }

    [StringLength(100)]
    public string City { get; set; }

    [StringLength(100)]
    public string State { get; set; }

    [StringLength(20)]
    public string PostalCode { get; set; }

    [StringLength(100)]
    public string Country { get; set; }

    [StringLength(200)]
    public string WebsiteUrl { get; set; }

    // Vendor Classification
    public VendorType VendorType { get; set; } = VendorType.Supplier;
    public VendorCategory Category { get; set; } = VendorCategory.RawMaterials;
    public VendorStatus Status { get; set; } = VendorStatus.Active;

    [StringLength(100)]
    public string Industry { get; set; }

    // Vendor Hierarchy
    public int? ParentVendorId { get; set; }
    public bool IsManufacturer { get; set; } = false;
    public bool IsDropshipVendor { get; set; } = false;

    // Financial Management
    [Column(TypeName = "decimal(18,2)")]
    public decimal CurrentBalance { get; set; } = 0;

    [Column(TypeName = "decimal(18,2)")]
    public decimal CreditLimit { get; set; } = 0;

    public int PaymentTermDays { get; set; } = 30;

    [Column(TypeName = "decimal(18,2)")]
    public decimal OutstandingAmount { get; set; } = 0;

    public DateTime? LastOrderDate { get; set; }
    public DateTime? LastPaymentDate { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal TotalPurchasesYTD { get; set; } = 0;

    [Column(TypeName = "decimal(18,2)")]
    public decimal TotalPurchasesLifetime { get; set; } = 0;

    public VendorPaymentMethod PreferredPaymentMethod { get; set; } = VendorPaymentMethod.BankTransfer;

    // Performance & Quality Tracking
    [Column(TypeName = "decimal(3,2)")]
    public decimal PerformanceRating { get; set; } = 0; // 0-5 scale

    public int OnTimeDeliveryPercentage { get; set; } = 0;
    public int QualityRating { get; set; } = 0; // 0-100 scale
    public DateTime? LastPerformanceReview { get; set; }
    public bool IsPreferredVendor { get; set; } = false;
    public bool IsBlacklisted { get; set; } = false;

    [StringLength(500)]
    public string BlacklistReason { get; set; }

    // Tax & Banking Information
    [StringLength(50)]
    public string TaxIdentificationNumber { get; set; }

    [StringLength(50)]
    public string GSTNumber { get; set; }

    [StringLength(50)]
    public string PANNumber { get; set; }

    [StringLength(100)]
    public string BankAccountNumber { get; set; }

    [StringLength(200)]
    public string BankName { get; set; }

    [StringLength(200)]
    public string BankBranch { get; set; }

    [StringLength(20)]
    public string IFSCCode { get; set; }

    // Currency
    public int? CurrencyId { get; set; }

    // Portal & Communication
    [StringLength(100)]
    public string PortalAccessEmail { get; set; }

    public bool HasPortalAccess { get; set; } = false;

    [StringLength(50)]
    public string PreferredCommunicationMethod { get; set; } = "Email";

    // Business Rules
    public bool RequiresPOApproval { get; set; } = false;

    [Column(TypeName = "decimal(18,2)")]
    public decimal MinimumOrderValue { get; set; } = 0;

    // Status Management
    public DateTime? StatusChangedDate { get; set; }

    [StringLength(500)]
    public string StatusChangeReason { get; set; }

    [StringLength(1000)]
    public string Notes { get; set; }

    // Multi-tenancy
    public int OrganisationId { get; set; }

    // Navigation Properties
    [ForeignKey("OrganisationId")]
    public virtual Organisation Organisation { get; set; }

    [ForeignKey("CurrencyId")]
    public virtual Currency Currency { get; set; }

    [ForeignKey("ParentVendorId")]
    public virtual Vendor ParentVendor { get; set; }

    // Related Entities
    public virtual ICollection<Vendor> ChildVendors { get; set; } = new HashSet<Vendor>();
    public virtual ICollection<Product> Products { get; set; } = new HashSet<Product>();
    public virtual ICollection<VendorContact> VendorContacts { get; set; } = new HashSet<VendorContact>();
    public virtual ICollection<VendorAddress> VendorAddresses { get; set; } = new HashSet<VendorAddress>();
    public virtual ICollection<VendorDocument> VendorDocuments { get; set; } = new HashSet<VendorDocument>();
    public virtual ICollection<PurchaseOrder> PurchaseOrders { get; set; } = new HashSet<PurchaseOrder>();
}