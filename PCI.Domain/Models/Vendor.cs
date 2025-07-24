using PCI.Domain.Common;
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

    [StringLength(100)]
    public string ContactPerson { get; set; }

    [StringLength(20)]
    public string PhoneNumber { get; set; }

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

    public bool IsActive { get; set; } = true;

    public int OrganisationId { get; set; }

    [ForeignKey("OrganisationId")]
    public virtual Organisation Organisation { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new HashSet<Product>();
}