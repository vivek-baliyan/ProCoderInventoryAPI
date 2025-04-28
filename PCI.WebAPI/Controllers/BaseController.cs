using Microsoft.AspNetCore.Mvc;
using PCI.Shared.Common;
using PCI.Shared.Common.Constants;
using System.IdentityModel.Tokens.Jwt;

namespace PCI.WebAPI.Controllers;

[ApiController]
//[Authorize]
[Route("api/[controller]")]
public class BaseController : ControllerBase
{
    protected string UserId { get; set; }
    protected int OrganisationId { get; set; }
    protected string SessionId { get; set; }
    protected string IpAddress { get; set; }
    protected string Token { get; set; }

    public BaseController(IHttpContextAccessor contextAccessor)
    {
        //Get claims from the token
        string authHeader = contextAccessor.HttpContext.Request.Headers.Authorization;

        IpAddress = contextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();

        if (authHeader == null || !authHeader.StartsWith("Bearer ")) return;

        Token = authHeader.Split(' ')[1];

        var jsonToken = new JwtSecurityTokenHandler().ReadToken(Token) as JwtSecurityToken;
        var claims = jsonToken.Claims;

        foreach (var claim in claims)
        {
            switch (claim.Type)
            {
                case JwtRegisteredClaimNames.NameId:
                    UserId = claim.Value;
                    break;
                case JwtRegisteredClaimNames.Sid:
                    SessionId = claim.Value;
                    break;
                case JwtClaimNames.OrganisationId:
                    OrganisationId = Convert.ToInt32(claim.Value);
                    break;
            }
        }
    }

    protected ApiResponse<T> SuccessResponse<T>(ServiceResult<T> result)
    {
        return new ApiResponse<T>
        {
            Success = true,
            Data = result.ResultData,
        };
    }

    protected ApiResponse<T> SuccessResponse<T>(T data, string message)
    {
        return new ApiResponse<T>
        {
            Success = true,
            Data = data,
            Message = message,
        };
    }

    protected ApiResponse<T> ErrorResponse<T>(ServiceResult<T> result)
    {
        return new ApiResponse<T>
        {
            Success = false,
            Errors = result.Problems.Select(p =>
                new ApiError(p.Code, p.Description)).ToList()
        };
    }

    protected ApiResponse<object> ErrorResponse(string errorDescription, string message)
    {
        return new ApiResponse<object>
        {
            Success = false,
            Errors = [new ApiError("ErrorOccurred", errorDescription)],
            Message = message
        };
    }
}
