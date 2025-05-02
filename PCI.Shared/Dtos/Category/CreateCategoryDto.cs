namespace PCI.Shared.Dtos.Category;

public record CreateCategoryDto
{
    public string Name { get; init; }

    public string PageTitle { get; init; }

    public string UrlIdentifier { get; init; }

    public string Description { get; init; }

    public int? ParentCategoryId { get; init; }

    public string Image { get; init; }

    public int Status { get; init; }

    public DateTime? PublishDate { get; init; }
}
