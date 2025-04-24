namespace PCI.Shared.Dtos;

public record CreateCategoryDto
{
    public string UserId { get; init; }

    public string Name { get; init; }

    public string PageTitle { get; init; }

    public string UrlIdentifier { get; init; }

    public string Description { get; init; }

    public int? ParentCategoryId { get; init; }

    public string ImagePath { get; init; }

    public int Status { get; init; }

    public DateTime? PublishDate { get; init; }
}
