using PCI.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PCI.Domain.Models;

public class ItemAttribute : BaseEntity
{
    [Required]
    [StringLength(100)]
    public string AttributeName { get; set; }

    [StringLength(500)]
    public string Description { get; set; }

    public int DisplayOrder { get; set; }

    public bool IsActive { get; set; } = true;

    public int ItemGroupId { get; set; }

    [ForeignKey("ItemGroupId")]
    public virtual ItemGroup ItemGroup { get; set; }

    public virtual ICollection<ItemAttributeOption> AttributeOptions { get; set; } = new HashSet<ItemAttributeOption>();
}