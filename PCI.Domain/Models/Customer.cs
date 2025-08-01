using PCI.Domain.Common;
using PCI.Shared.Common.Enums;

namespace PCI.Domain.Models;

public class Customer : BaseEntity
{
    public string CustomerCode { get; set; }

    public string CustomerName { get; set; }

    public string CompanyName { get; set; }

    public string WebsiteUrl { get; set; }

    // Customer Type (Individual, Business)
    public CustomerType CustomerType { get; set; } = CustomerType.Individual;


    // Currency
    public int? CurrencyId { get; set; }

    // Status
    public bool IsActive { get; set; } = true;

    // Notes
    public string Notes { get; set; }

    // Concurrency control
    public byte[] RowVersion { get; set; }

    // Multi-tenancy
    public int OrganisationId { get; set; }

    // Navigation properties
    public virtual Organisation Organisation { get; set; }

    public virtual Currency Currency { get; set; }

    // Related entities
    public virtual ICollection<SalesOrder> SalesOrders { get; set; } = new HashSet<SalesOrder>();
    public virtual ICollection<Invoice> Invoices { get; set; } = new HashSet<Invoice>();
    public virtual ICollection<CustomerPriceList> CustomerPriceLists { get; set; } = new HashSet<CustomerPriceList>();
    public virtual ICollection<CustomerDocument> CustomerDocuments { get; set; } = new HashSet<CustomerDocument>();

    // Normalized entities
    public virtual ICollection<BusinessAddress> BusinessAddresses { get; set; } = new HashSet<BusinessAddress>();
    public virtual ICollection<BusinessContact> BusinessContacts { get; set; } = new HashSet<BusinessContact>();
    public virtual ICollection<BusinessTaxInfo> BusinessTaxInfos { get; set; } = new HashSet<BusinessTaxInfo>();
    public virtual ICollection<BusinessBankInfo> BusinessBankInfos { get; set; } = new HashSet<BusinessBankInfo>();
    public virtual CustomerFinancial CustomerFinancial { get; set; }
}