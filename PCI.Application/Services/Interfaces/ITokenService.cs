using PCI.Shared.Dtos.Identity;

namespace PCI.Application.Services.Interfaces;

public interface ITokenService
{
    string GenerateAccessToken(UserDto userDto);
    string GenerateRefreshToken();
    bool GetPrincipalFromExpiredToken(string token);
}
