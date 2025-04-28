using PCI.Shared.Common.Enums;

namespace PCI.Shared.Dtos;

public record CategoryDto
{
    public int Id { get; init; }

    public string UserId { get; init; }

    public string Name { get; init; }

    public string PageTitle { get; init; }

    public string UrlIdentifier { get; init; }

    public string Description { get; init; }

    public int? ParentCategoryId { get; init; }

    public string ImagePath { get; init; }

    public VisibilityStatus Status { get; init; }

    public DateTime? PublishDate { get; init; }

    public virtual CategoryDto ParentCategory { get; init; }

    public virtual List<CategoryDto> ChildCategories { get; init; }
    public virtual List<CategoryImageDto> CategoryImages { get; init; }
}
