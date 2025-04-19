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
                return StatusCode(StatusCodes.Status400BadRequest, result);
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
                return StatusCode(StatusCodes.Status400BadRequest, logintResult);
            }

            var userProfileResult = await _accountService.GetUserProfileByUserId(logintResult.ResultData.Id);

            if (!userProfileResult.Succeeded)
            {
                return StatusCode(StatusCodes.Status400BadRequest, userProfileResult);
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

            return StatusCode(StatusCodes.Status200OK, SuccessResponse(loginResponse, "User logged in successfully."));
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
                return StatusCode(StatusCodes.Status400BadRequest, result);
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
    public async Task<IActionResult> GetAccountByUserId(string userId)
    {
        try
        {
            var result = await _identityService.GetUserById(userId);

            if (!result.Succeeded)
            {
                return StatusCode(StatusCodes.Status400BadRequest, result);
            }

            var userProfileResult = await _accountService.GetUserProfileByUserId(userId);

            return StatusCode(StatusCodes.Status200OK,
                SuccessResponse(userProfileResult.ResultData, "User retrieved successfully."));
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                ErrorResponse(ex.ToString(), $"An error occurred while retrieving user: {ex.Message}"));
        }
    }

}