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

    [StringLength(100)]
    public string ContactPerson { get; set; }

    [StringLength(20)]
    public string PhoneNumber { get; set; }

    [StringLength(20)]
    public string MobileNumber { get; set; }

    [StringLength(100)]
    public string Email { get; set; }

    [StringLength(500)]
    public string BillingAddress { get; set; }

    [StringLength(500)]
    public string ShippingAddress { get; set; }

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

    // Customer Type (Individual, Business)
    public CustomerType CustomerType { get; set; } = CustomerType.Individual;

    // Payment Terms
    public int PaymentTermDays { get; set; } = 30;
    
    [Column(TypeName = "decimal(18,2)")]
    public decimal? CreditLimit { get; set; }

    // Tax Information
    [StringLength(50)]
    public string TaxNumber { get; set; }

    [StringLength(50)]
    public string GSTNumber { get; set; }

    [StringLength(50)]
    public string PANNumber { get; set; }

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
}