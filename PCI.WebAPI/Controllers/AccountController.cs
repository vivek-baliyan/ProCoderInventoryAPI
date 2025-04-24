using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PCI.Application.Services.Interfaces;
using PCI.Shared.Common;
using PCI.Shared.Dtos;

namespace PCI.WebAPI.Controllers;

public class AccountController(
    IIdentityService identityService,
    IAccountService accountService,
    ITokenService tokenService) : BaseController
{
    private readonly IIdentityService _identityService = identityService;
    private readonly IAccountService _accountService = accountService;
    private readonly ITokenService _tokenService = tokenService;

    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterUserDto registerUserDto)
    {
        try
        {
            // 1. Create the user in the identity database
            var result = await _identityService.CreateUser(registerUserDto);

            if (!result.Succeeded)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ErrorResponse(result));
            }

            try
            {
                var userResponse = await _identityService.GetUserByEmail(registerUserDto.Email);
                if (!userResponse.Succeeded)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, ErrorResponse(userResponse));
                }

                // 2. Create the user profile in the application database
                var userProfile = await _accountService.CreateUserProfile(userResponse.ResultData.Id, registerUserDto);

                if (!userProfile.Succeeded)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, ErrorResponse(userProfile));
                }

                return StatusCode(StatusCodes.Status200OK,
                    SuccessResponse(userProfile.ResultData.UserId, Messages.UserProfileCreated));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    ErrorResponse(ex.ToString(), $"An error occurred while creating user profile: {ex.Message}"));
            }
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                ErrorResponse(ex.ToString(), $"An error occurred during registration: {ex.Message}"));
        }
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<ActionResult<LoginResponseDto>> Login(UserLoginDto loginUserDto)
    {
        try
        {
            var logintResult = await _identityService.UserLogin(loginUserDto);

            if (!logintResult.Succeeded)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ErrorResponse(logintResult));
            }

            var userProfileResult = await _accountService.GetUserProfileByUserId(logintResult.ResultData.Id);

            if (!userProfileResult.Succeeded)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ErrorResponse(userProfileResult));
            }

            var accessToken = _tokenService.GenerateAccessToken(logintResult.ResultData);
            var refreshToken = _tokenService.GenerateRefreshToken();

            var loginResponse = BindLoginResponse(
                logintResult.ResultData,
                userProfileResult.ResultData,
                accessToken,
                refreshToken);

            return StatusCode(StatusCodes.Status200OK,
                SuccessResponse(loginResponse, Messages.UserLoggedIn));
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                ErrorResponse(ex.ToString(), $"An error occurred during login: {ex.Message}"));
        }
    }

    [HttpPost("assignRoleToUser")]
    public async Task<IActionResult> AssignRoleToUser(AssignUserRoleDto assignUserRoleDto)
    {
        try
        {
            var result = await _identityService.AssignRoleToUser(assignUserRoleDto.UserEmail, assignUserRoleDto.RoleName);

            if (!result.Succeeded)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ErrorResponse(result));
            }

            return StatusCode(StatusCodes.Status200OK,
                SuccessResponse(result.ResultData, Messages.RoleAssigned));
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                ErrorResponse(ex.ToString(), $"An error occurred while assigning role to user: {ex.Message}"));
        }
    }

    [HttpPut("updateLoginDetails")]
    public async Task<IActionResult> UpdateLoginDetails(UpdateLoginDetailsDto updateLoginDetailsDto)
    {
        try
        {
            var result = await _identityService.UpdateLoginDetails(updateLoginDetailsDto);

            if (!result.Succeeded)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ErrorResponse(result));
            }

            return StatusCode(StatusCodes.Status200OK,
                SuccessResponse(result.ResultData, Messages.UserLoginDetailsUpdated));
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                ErrorResponse(ex.ToString(), $"An error occurred while updating user login details: {ex.Message}"));
        }
    }

    [HttpGet("getAccountByUserId/{userId}")]
    public async Task<ActionResult<UserProfileDetailDto>> GetAccountByUserId(string userId)
    {
        try
        {
            var result = await _identityService.GetUserById(userId);

            if (!result.Succeeded)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ErrorResponse(result));
            }

            var userProfileResult = await _accountService.GetUserProfileByUserId(userId);

            if (!userProfileResult.Succeeded)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ErrorResponse(userProfileResult));
            }

            var userProfileDetail = userProfileResult.ResultData.Adapt<UserProfileDetailDto>() with
            {
                Email = result.ResultData.Email,
                UserName = result.ResultData.UserName,
                PhoneNumber = result.ResultData.PhoneNumber,
            };

            return StatusCode(StatusCodes.Status200OK,
                SuccessResponse(userProfileDetail, Messages.UserProfileRetrieved));
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                ErrorResponse(ex.ToString(), $"An error occurred while retrieving user: {ex.Message}"));
        }
    }

    [HttpPut("updateProfile")]
    public async Task<IActionResult> UpdateProfile(UpdateProfileDto updateProfileDto)
    {
        try
        {
            var identityUpdateResult = await _identityService.UpdateUser(
                new UpdateUserPhoneNumberDto(updateProfileDto.UserId, updateProfileDto.PhoneNumber));

            if (!identityUpdateResult.Succeeded)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ErrorResponse(identityUpdateResult));
            }

            var result = await _accountService.UpdateProfile(updateProfileDto);

            if (!result.Succeeded)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ErrorResponse(result));
            }

            return StatusCode(StatusCodes.Status200OK,
                SuccessResponse(result.ResultData, Messages.UserProfileUpdated));
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                ErrorResponse(ex.ToString(), $"An error occurred while updating user profile: {ex.Message}"));
        }
    }

    [HttpPut("updateProfileSettings")]
    public async Task<IActionResult> UpdateProfileSettings(UpdateProfileSettingsDto updateProfileSettingsDto)
    {
        try
        {
            var result = await _accountService.UpdateProfileSettings(updateProfileSettingsDto);

            if (!result.Succeeded)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ErrorResponse(result));
            }

            return StatusCode(StatusCodes.Status200OK,
                SuccessResponse(result.ResultData, Messages.UserProfileUpdated));
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                ErrorResponse(ex.ToString(), $"An error occurred while updating user profile: {ex.Message}"));
        }
    }

    private static LoginResponseDto BindLoginResponse(
        UserDto userDto,
        UserProfileDto userProfileDto,
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

            FirstName = userProfileDto.FirstName,
            LastName = userProfileDto.LastName,
            ProfileImageUrl = userProfileDto.ProfileImageUrl,
            AccessToken = accessToken,
            RefreshToken = refreshToken
        };
    }
}