using Mapster;
using PCI.Domain.Models;
using PCI.Shared.Dtos;

namespace PCI.Application.Mappings;

public static class ApplicationMapsterMappings
{
    public static void Configure()
    {
        var config = TypeAdapterConfig.GlobalSettings;

        // Configure domain-specific mappings
        CustomerMappings.Configure(config);

        // Configure other general mappings
        ConfigureGeneralMappings(config);
    }

    private static void ConfigureGeneralMappings(TypeAdapterConfig config)
    {
        // Organisation mappings
        config.ForType<Organisation, OrganisationDto>()
              .Map(dst => dst.OrganisationId, src => src.Id);
    }
}
