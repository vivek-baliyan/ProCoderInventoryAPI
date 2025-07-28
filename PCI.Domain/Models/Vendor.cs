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
    public virtual ICollection<VendorDocument> VendorDocuments { get; set; } = new HashSet<VendorDocument>();
    public virtual ICollection<PurchaseOrder> PurchaseOrders { get; set; } = new HashSet<PurchaseOrder>();
    
    // Normalized entities
    public virtual ICollection<BusinessAddress> BusinessAddresses { get; set; } = new HashSet<BusinessAddress>();
    public virtual ICollection<BusinessContact> BusinessContacts { get; set; } = new HashSet<BusinessContact>();
    public virtual ICollection<BusinessTaxInfo> BusinessTaxInfos { get; set; } = new HashSet<BusinessTaxInfo>();
    public virtual ICollection<BusinessBankInfo> BusinessBankInfos { get; set; } = new HashSet<BusinessBankInfo>();
    public virtual VendorFinancial VendorFinancial { get; set; }
    public virtual ICollection<VendorPerformance> VendorPerformances { get; set; } = new HashSet<VendorPerformance>();
}