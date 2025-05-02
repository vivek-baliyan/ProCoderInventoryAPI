using PCI.Shared.Common.Enums;

namespace PCI.Shared.Dtos.Category;

public record CategoryListItemDto
{
    public int Id { get; init; }

    public string OrganisationId { get; init; }

    public string Name { get; init; }

    public VisibilityStatus Status { get; init; }

    public DateTime? PublishDate { get; init; }
}
