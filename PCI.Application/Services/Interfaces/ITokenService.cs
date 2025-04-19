using PCI.Shared.Dtos;

namespace PCI.Application.Services.Interfaces;

public interface ITokenService
{
    string GenerateAccessToken(LoginResponseDto loginResponseDto);
    string GenerateRefreshToken();
    bool GetPrincipalFromExpiredToken(string token);
}
