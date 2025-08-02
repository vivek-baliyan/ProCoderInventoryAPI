using PCI.Domain.Common;
using PCI.Shared.Common.Enums;

namespace PCI.Domain.Models;

public class Customer : BaseEntity
{
    // Basic customer information
    public string CustomerCode { get; set; }
    public string DisplayName { get; set; }
    public string CompanyName { get; set; }
    public string WebsiteUrl { get; set; }
    public CustomerType CustomerType { get; set; } = CustomerType.Individual;
    public bool IsActive { get; set; } = true;

    public int OrganisationId { get; set; }
    public int? CurrencyId { get; set; }

    public virtual Organisation Organisation { get; set; }
    public virtual Currency Currency { get; set; }

    public virtual ICollection<CustomerContact> CustomerContacts { get; set; } = new HashSet<CustomerContact>();
    public virtual ICollection<CustomerAddress> CustomerAddresses { get; set; } = new HashSet<CustomerAddress>();
    public virtual ICollection<CustomerTaxInfo> CustomerTaxInfos { get; set; } = new HashSet<CustomerTaxInfo>();
    public virtual ICollection<CustomerBankInfo> CustomerBankInfos { get; set; } = new HashSet<CustomerBankInfo>();
    public virtual CustomerFinancial CustomerFinancial { get; set; }
    public virtual ICollection<CustomerDocument> CustomerDocuments { get; set; } = new HashSet<CustomerDocument>();
    public virtual ICollection<CustomerPriceList> CustomerPriceLists { get; set; } = new HashSet<CustomerPriceList>();

    public virtual ICollection<SalesOrder> SalesOrders { get; set; } = new HashSet<SalesOrder>();
    public virtual ICollection<Invoice> Invoices { get; set; } = new HashSet<Invoice>();
}