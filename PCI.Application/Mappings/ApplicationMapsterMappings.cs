using Mapster;
using PCI.Domain.Models;
using PCI.Shared.Dtos.Category;

namespace PCI.Application.Mappings;

public static class ApplicationMapsterMappings
{
    public static void Configure()
    {
        var config = TypeAdapterConfig.GlobalSettings;

        config.ForType<Organisation, OrganisationResponseDto>()
              .Map(destinationMember => destinationMember.OrganisationId, sourceMember => sourceMember.Id);

        config.ForType<Category, CategoryResponseDto>()
            .Map(destinationMember => destinationMember.ParentCategory, sourceMember => sourceMember.ParentCategory)
            .Map(destinationMember => destinationMember.ChildCategories, sourceMember => sourceMember.ChildCategories.Adapt<List<CategoryResponseDto>>())
            .Map(destinationMember => destinationMember.CategoryImages, sourceMember => sourceMember.CategoryImages.Adapt<List<CategoryImageDto>>())
            .MaxDepth(2);
    }
}
