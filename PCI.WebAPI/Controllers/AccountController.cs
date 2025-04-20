using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PCI.Application.Services.Interfaces;
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
                    SuccessResponse(userProfile.ResultData.UserId, "User profile created successfully."));
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

            var loginResponse = new LoginResponseDto()
            {
                UserId = logintResult.ResultData.Id,
                UserName = logintResult.ResultData.UserName,
                Email = logintResult.ResultData.Email,
                FirstName = userProfileResult.ResultData.FirstName,
                LastName = userProfileResult.ResultData.LastName,
                ProfileImageUrl = userProfileResult.ResultData.ProfileImageUrl,
                UserRoles = logintResult.ResultData.Roles,
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };

            return StatusCode(StatusCodes.Status200OK,
                SuccessResponse(loginResponse, "User logged in successfully."));
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
                SuccessResponse(result.ResultData, "Role assigned to user successfully."));
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                ErrorResponse(ex.ToString(), $"An error occurred while assigning role to user: {ex.Message}"));
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
                SuccessResponse(userProfileDetail, "User retrieved successfully."));
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                ErrorResponse(ex.ToString(), $"An error occurred while retrieving user: {ex.Message}"));
        }
    }

    [HttpPut("updateUserProfile")]
    public async Task<IActionResult> UpdateUserProfile(UpdateProfileDto updateProfileDto)
    {
        try
        {
            var identityUpdateResult = await _identityService.UpdateUser(
                new UpdateUserDto(updateProfileDto.UserId, updateProfileDto.Email, updateProfileDto.PhoneNumber));

            if (!identityUpdateResult.Succeeded)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ErrorResponse(identityUpdateResult));
            }

            var result = await _accountService.UpdateUserProfile(updateProfileDto);

            if (!result.Succeeded)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ErrorResponse(result));
            }

            return StatusCode(StatusCodes.Status200OK,
                SuccessResponse(result.ResultData, "User profile updated successfully."));
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                ErrorResponse(ex.ToString(), $"An error occurred while updating user profile: {ex.Message}"));
        }
    }

    [HttpPut("updateUserProfileSettings")]
    public async Task<IActionResult> UpdateUserProfileSettings(UpdateProfileSettingsDto updateProfileSettingsDto)
    {
        try
        {
            var result = await _accountService.UpdateUserProfileSettings(updateProfileSettingsDto);

            if (!result.Succeeded)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ErrorResponse(result));
            }

            return StatusCode(StatusCodes.Status200OK,
                SuccessResponse(result.ResultData, "User profile settings updated successfully."));
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                ErrorResponse(ex.ToString(), $"An error occurred while updating user profile: {ex.Message}"));
        }
    }
}