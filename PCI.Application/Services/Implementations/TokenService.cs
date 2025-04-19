using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PCI.Application.Services.Interfaces;
using PCI.Shared.Dtos;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace PCI.Application.Services.Implementations;

public class TokenService(IConfiguration configuration) : ITokenService
{
    private readonly SymmetricSecurityKey _key = new(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
    private readonly IConfiguration _configuration = configuration;

    public string GenerateAccessToken(LoginResponseDto loginResponseDto)
    {
        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.NameId, loginResponseDto.UserId),
            new(JwtRegisteredClaimNames.Name, loginResponseDto.UserName),
            new(JwtRegisteredClaimNames.Email, loginResponseDto.Email)
        };

        claims.AddRange(loginResponseDto.UserRoles.Select(role => new Claim(ClaimTypes.Role, role)));

        var signingCredentials = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);
        var expirationMinutes = int.Parse(_configuration["Jwt:TokenExpirationMinutes"]);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.Now.AddMinutes(expirationMinutes),
            SigningCredentials = signingCredentials
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        return tokenHandler.CreateEncodedJwt(tokenDescriptor);
    }

    public bool GetPrincipalFromExpiredToken(string token)
    {
        bool IsValidAuthToken = true;
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false,
            ValidateIssuer = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = _key,
            ValidateLifetime = false
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var securityToken);
        if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals("HS512", StringComparison.InvariantCultureIgnoreCase))
            IsValidAuthToken = false;

        return IsValidAuthToken;
    }

    public string GenerateRefreshToken()
    {
        var randomNumber = new byte[64];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }
}
