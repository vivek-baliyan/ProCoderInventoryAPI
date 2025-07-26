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
              .Map(destinationMember => destinationMember.OrganisationId, sourceMember => sourceMember.Id);
    }
}
