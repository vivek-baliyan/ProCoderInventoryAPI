namespace PCI.Shared.Dtos.Category;

public record UpdateCategoryDto : CreateCategoryDto
{
    public int CategoryId { get; set; }
}
