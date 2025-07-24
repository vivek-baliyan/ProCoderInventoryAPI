using PCI.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PCI.Domain.Models;

public class ItemAttributeOption : BaseEntity
{
    [Required]
    [StringLength(100)]
    public string OptionValue { get; set; }

    [StringLength(500)]
    public string Description { get; set; }

    public int DisplayOrder { get; set; }

    public bool IsActive { get; set; } = true;

    public int ItemAttributeId { get; set; }

    [ForeignKey("ItemAttributeId")]
    public virtual ItemAttribute ItemAttribute { get; set; }

    public virtual ICollection<ProductAttributeValue> ProductAttributeValues { get; set; } = new HashSet<ProductAttributeValue>();
}