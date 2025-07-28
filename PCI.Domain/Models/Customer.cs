using PCI.Domain.Common;
using PCI.Shared.Common.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PCI.Domain.Models;

public class Customer : BaseEntity
{
    [Required]
    [StringLength(50)]
    public string CustomerCode { get; set; }

    [Required]
    [StringLength(200)]
    public string CustomerName { get; set; }

    [StringLength(200)]
    public string CompanyName { get; set; }


    [StringLength(200)]
    public string WebsiteUrl { get; set; }

    // Customer Type (Individual, Business)
    public CustomerType CustomerType { get; set; } = CustomerType.Individual;


    // Currency
    public int? CurrencyId { get; set; }

    // Status
    public bool IsActive { get; set; } = true;

    // Notes
    [StringLength(1000)]
    public string Notes { get; set; }

    // Multi-tenancy
    public int OrganisationId { get; set; }

    // Navigation properties
    [ForeignKey("OrganisationId")]
    public virtual Organisation Organisation { get; set; }

    [ForeignKey("CurrencyId")]
    public virtual Currency Currency { get; set; }

    // Related entities
    public virtual ICollection<SalesOrder> SalesOrders { get; set; } = new HashSet<SalesOrder>();
    public virtual ICollection<Invoice> Invoices { get; set; } = new HashSet<Invoice>();
    public virtual ICollection<CustomerPriceList> CustomerPriceLists { get; set; } = new HashSet<CustomerPriceList>();

    // Normalized entities
    public virtual ICollection<BusinessAddress> BusinessAddresses { get; set; } = new HashSet<BusinessAddress>();
    public virtual ICollection<BusinessContact> BusinessContacts { get; set; } = new HashSet<BusinessContact>();
    public virtual ICollection<BusinessTaxInfo> BusinessTaxInfos { get; set; } = new HashSet<BusinessTaxInfo>();
    public virtual ICollection<BusinessBankInfo> BusinessBankInfos { get; set; } = new HashSet<BusinessBankInfo>();
    public virtual CustomerFinancial CustomerFinancial { get; set; }
}