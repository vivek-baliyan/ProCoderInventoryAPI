using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PCI.Application.Services.Interfaces;
using PCI.Shared.Common.Constants;
using PCI.Shared.Dtos.Identity;

namespace PCI.WebAPI.Controllers;

public class AccountController(
    IIdentityService identityService,
    ITokenService tokenService,
    IOrganisationService organisationService,
    IHttpContextAccessor httpContextAccessor) : BaseController(httpContextAccessor)
{
    private readonly IIdentityService _identityService = identityService;
    private readonly ITokenService _tokenService = tokenService;
    private readonly IOrganisationService _organisationService = organisationService;

    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterUserDto registerUserDto)
    {
        var result = await _identityService.CreateUser(registerUserDto);

        if (!result.Succeeded)
        {
            return StatusCode(StatusCodes.Status400BadRequest, ErrorResponse(result));
        }

        return StatusCode(StatusCodes.Status200OK,
                SuccessResponse(result.ResultData, Messages.OrganisationCreated));
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<ActionResult<LoginResponseDto>> Login(UserLoginDto loginUserDto)
    {
        var logintResult = await _identityService.UserLogin(loginUserDto);

        if (!logintResult.Succeeded)
        {
            return StatusCode(StatusCodes.Status400BadRequest, ErrorResponse(logintResult));
        }

        var organisationResult = await _organisationService.GetOrganisationByUserId(logintResult.ResultData.Id);
        var userDto = logintResult.ResultData with
        {
            OrganisationId = organisationResult.Succeeded ? organisationResult.ResultData.OrganisationId : 0
        };

        var accessToken = _tokenService.GenerateAccessToken(userDto);

        var refreshToken = _tokenService.GenerateRefreshToken();

        var loginResponse = BindLoginResponse(
            userDto,
            accessToken,
            refreshToken);

        return StatusCode(StatusCodes.Status200OK,
            SuccessResponse(loginResponse, Messages.UserLoggedIn));
    }

    [HttpPost("assignRoleToUser")]
    public async Task<IActionResult> AssignRoleToUser(AssignUserRoleDto assignUserRoleDto)
    {
        var result = await _identityService.AssignRoleToUser(assignUserRoleDto.UserEmail, assignUserRoleDto.RoleName);

        if (!result.Succeeded)
        {
            return StatusCode(StatusCodes.Status400BadRequest, ErrorResponse(result));
        }

        return StatusCode(StatusCodes.Status200OK,
            SuccessResponse(result.ResultData, Messages.RoleAssigned));
    }

    [HttpGet("getAccountByUserId/{userId}")]
    public async Task<ActionResult<UserDto>> GetAccountByUserId(string userId)
    {
        var result = await _identityService.GetUserById(userId);

        if (!result.Succeeded)
        {
            return StatusCode(StatusCodes.Status400BadRequest, ErrorResponse(result));
        }

        return StatusCode(StatusCodes.Status200OK,
            SuccessResponse(result.ResultData, Messages.OrganisationRetrieved));
    }

    [HttpPut("updateProfile")]
    public async Task<IActionResult> UpdateProfile(UpdateUserDetailsDto updateUserDetailsDto)
    {
        var identityUpdateResult = await _identityService.UpdateUserDetails(updateUserDetailsDto);

        if (!identityUpdateResult.Succeeded)
        {
            return StatusCode(StatusCodes.Status400BadRequest, ErrorResponse(identityUpdateResult));
        }

        return StatusCode(StatusCodes.Status200OK,
            SuccessResponse(identityUpdateResult.ResultData, Messages.OrganisationUpdated));
    }

    [HttpPut("updateLoginDetails")]
    public async Task<IActionResult> UpdateLoginDetails(UpdateLoginDetailsDto updateLoginDetailsDto)
    {
        var result = await _identityService.UpdateLoginDetails(updateLoginDetailsDto);

        if (!result.Succeeded)
        {
            return StatusCode(StatusCodes.Status400BadRequest, ErrorResponse(result));
        }

        return StatusCode(StatusCodes.Status200OK,
            SuccessResponse(result.ResultData, Messages.UserLoginDetailsUpdated));
    }

    private static LoginResponseDto BindLoginResponse(
        UserDto userDto,
        string accessToken,
        string refreshToken)
    {
        return new LoginResponseDto()
        {
            UserId = userDto.Id,
            UserName = userDto.UserName,
            Email = userDto.Email,
            LastLogin = userDto.LastLogin,
            LastLoginDevice = userDto.LastLoginDevice,
            LastPasswordChange = userDto.LastPasswordChange,
            UserRoles = userDto.Roles,

            FirstName = userDto.FirstName,
            LastName = userDto.LastName,
            ProfileImageUrl = userDto.ProfileImageUrl,
            AccessToken = accessToken,
            RefreshToken = refreshToken
        };
    }
}