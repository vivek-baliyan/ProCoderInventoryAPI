using Mapster;
using PCI.Domain.Models;
using PCI.Shared.Dtos;

namespace PCI.Application.Mappings;

public static class ApplicationMapsterMappings
{
    public static void Configure()
    {
        var config = TypeAdapterConfig.GlobalSettings;

        config.ForType<Organisation, OrganisationDto>()
              .Map(dest => dest.OrganisationId, src => src.Id);

        config.ForType<Category, CategoryDto>()
            .Map(dest => dest.ParentCategory, src => src.ParentCategory)
            .Map(dest => dest.ChildCategories, src => src.ChildCategories.Adapt<List<CategoryDto>>())
            .MaxDepth(2);
    }
}
