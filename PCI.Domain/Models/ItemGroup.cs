using PCI.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PCI.Domain.Models;

public class ItemGroup : BaseEntity
{
    [Required]
    [StringLength(200)]
    public string GroupName { get; set; }

    [StringLength(1000)]
    public string Description { get; set; }

    [StringLength(100)]
    public string SKUPattern { get; set; }

    public bool IsActive { get; set; } = true;

    public int OrganisationId { get; set; }

    [ForeignKey("OrganisationId")]
    public virtual Organisation Organisation { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new HashSet<Product>();
    public virtual ICollection<ItemAttribute> ItemAttributes { get; set; } = new HashSet<ItemAttribute>();
}