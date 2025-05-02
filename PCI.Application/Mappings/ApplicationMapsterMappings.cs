using Mapster;
using PCI.Domain.Models;
using PCI.Shared.Dtos;
using PCI.Shared.Dtos.Category;

namespace PCI.Application.Mappings;

public static class ApplicationMapsterMappings
{
    public static void Configure()
    {
        var config = TypeAdapterConfig.GlobalSettings;

        config.ForType<Organisation, OrganisationDto>()
              .Map(destinationMember => destinationMember.OrganisationId, sourceMember => sourceMember.Id);

        config.ForType<Category, CategoryDto>()
            .Map(destinationMember => destinationMember.ParentCategory, sourceMember => sourceMember.ParentCategory)
            .Map(destinationMember => destinationMember.ChildCategories, sourceMember => sourceMember.ChildCategories.Adapt<List<CategoryDto>>())
            .Map(destinationMember => destinationMember.CategoryImages, sourceMember => sourceMember.CategoryImages.Adapt<List<CategoryImageDto>>())
            .MaxDepth(2);
    }
}
