using Mapster;
using PCI.Domain.Models;
using PCI.Shared.Dtos;

namespace PCI.Application.Mappings;

public static class IdentityMapsterMappings
{
    public static void Configure()
    {
        var config = TypeAdapterConfig.GlobalSettings;

        config.NewConfig<AppUser, LoginResponseDto>()
            .Map(dest => dest.UserId, src => src.Id);
    }
}
