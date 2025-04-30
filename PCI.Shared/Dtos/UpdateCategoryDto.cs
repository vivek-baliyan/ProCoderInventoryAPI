namespace PCI.Shared.Dtos;

public record UpdateCategoryDto : CreateCategoryDto
{
    public int CategoryId { get; set; }
}
