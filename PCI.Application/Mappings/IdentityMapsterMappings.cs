using Mapster;
using PCI.Domain.Models;
using PCI.Shared.Dtos;

namespace PCI.Application.Mappings;

public static class IdentityMapsterMappings
{
    public static void Configure()
    {
        var config = TypeAdapterConfig.GlobalSettings;

        config.NewConfig<AppUser, UserDto>()
            .Map(dest => dest.UserRoles, src => src.UserRoles.Adapt<List<UserRoleDto>>());

        config.NewConfig<AppUser, LoginResponseDto>()
            .Map(dest => dest.UserId, src => src.Id)
            .Map(dest => dest.UserRoles, src => src.UserRoles.Adapt<List<UserRoleDto>>());
    }
}
