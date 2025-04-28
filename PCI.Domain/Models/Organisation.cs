using PCI.Domain.Common;

namespace PCI.Domain.Models;

public class Organisation : BaseEntity
{
    public string UserId { get; set; } // Foreign key to Identity's AspNetUsers table

    public string CompanyName { get; set; }
    public string ContactPerson { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public string WebsiteUrl { get; set; }
    public string Address { get; set; }
    public string Country { get; set; }
    public string State { get; set; }
    public string City { get; set; }
    public string PostalCode { get; set; }
}