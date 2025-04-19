using Mapster;
using Microsoft.AspNetCore.Identity;
using PCI.Application.Repositories;
using PCI.Application.Services.Interfaces;
using PCI.Domain.Models;
using PCI.Shared.Common;
using PCI.Shared.Dtos;

namespace PCI.Application.Services.Implementations;

public class IdentityService(IUnitOfWork unitOfWork) : IIdentityService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<ServiceResult<IdentityResult>> CreateRole(CreateRoleDto addAppRoleDto)
    {
        var result = await _unitOfWork.IdentityRepository.CreateRoleAsync(addAppRoleDto.Adapt<AppRole>());
        if (!result.Succeeded)
        {
            var errors = result.Errors.Select(e => new Problem(e.Code, e.Description)).ToList();
            return ServiceResult<IdentityResult>.Errors(errors);
        }

        return ServiceResult<IdentityResult>.Success(result);
    }

    public async Task<ServiceResult<IReadOnlyList<RoleDto>>> GetAllRoles()
    {
        var roles = await _unitOfWork.IdentityRepository.GetAllRolesAsync();

        return ServiceResult<IReadOnlyList<RoleDto>>.Success(roles.Adapt<IReadOnlyList<RoleDto>>());
    }

    public async Task<ServiceResult<RoleDto>> GetRoleByName(string roleName)
    {
        var role = await _unitOfWork.IdentityRepository.FindRoleByNameAsync(roleName);

        return ServiceResult<RoleDto>.Success(role.Adapt<RoleDto>());
    }

    public async Task<ServiceResult<IdentityResult>> CreateUser(RegisterUserDto registerUserDto)
    {
        var user = new AppUser
        {
            UserName = registerUserDto.Email,
            Email = registerUserDto.Email,
            CreatedOn = DateTime.UtcNow,
            IsDeleted = false
        };

        var createUserResult = await _unitOfWork.IdentityRepository.CreateUserAsync(user, registerUserDto.Password);

        if (!createUserResult.Succeeded)
        {
            var errors = createUserResult.Errors.Select(e => new Problem(e.Code, e.Description)).ToList();
            return ServiceResult<IdentityResult>.Errors(errors);
        }
        // Assign default role
        var addUserRoleResult = await _unitOfWork.IdentityRepository.AddUserToRoleAsync(user, "Shopper");

        if (!addUserRoleResult.Succeeded)
        {
            var addUserRoleError = addUserRoleResult.Errors.Select(e => new Problem(e.Code, e.Description)).ToList();
            return ServiceResult<IdentityResult>.Errors(addUserRoleError);
        }

        return ServiceResult<IdentityResult>.Success(createUserResult);
    }

    public async Task<ServiceResult<IdentityResult>> AssignRoleToUser(string userEmail, string roleName)
    {
        var user = await _unitOfWork.IdentityRepository.FindUserByEmailAsync(userEmail);

        // Check if the user exists
        if (user == null)
        {
            return ServiceResult<IdentityResult>.Error(new Problem("UserNotFound", $"User with email {userEmail} not found."));
        }

        var result = await _unitOfWork.IdentityRepository.AddUserToRoleAsync(user, roleName);

        if (!result.Succeeded)
        {
            var errors = result.Errors.Select(e => new Problem(e.Code, e.Description)).ToList();
            return ServiceResult<IdentityResult>.Errors(errors);
        }

        return ServiceResult<IdentityResult>.Success(result);
    }

    public async Task<ServiceResult<UserDto>> GetUserById(string userId)
    {
        var user = await _unitOfWork.IdentityRepository.FindUserByIdAsync(userId);

        if (user == null)
        {
            return ServiceResult<UserDto>.Error(new Problem("UserNotFound", $"User with ID {userId} not found."));
        }

        return ServiceResult<UserDto>.Success(user.Adapt<UserDto>());
    }

    public async Task<ServiceResult<UserDto>> GetUserByEmail(string userEmail)
    {
        var user = await _unitOfWork.IdentityRepository.FindUserByEmailAsync(userEmail);

        if (user == null)
        {
            return ServiceResult<UserDto>.Error(new Problem("UserNotFound", $"User with Email {userEmail} not found."));
        }

        return ServiceResult<UserDto>.Success(user.Adapt<UserDto>());
    }

    public async Task<ServiceResult<LoginResponseDto>> UserLogin(UserLoginDto loginUserDto)
    {
        var user = await _unitOfWork.IdentityRepository.FindUserByEmailAsync(loginUserDto.Email);

        if (user == null)
        {
            return ServiceResult<LoginResponseDto>.Error(new Problem("InvalidCredentials", "Invalid email or password."));
        }

        var result = await _unitOfWork.IdentityRepository.ValidateUserPasswordAsync(user, loginUserDto.Password);

        if (!result)
        {
            return ServiceResult<LoginResponseDto>.Error(new Problem("InvalidCredentials", "Invalid email or password."));
        }

        var loginResponse = user.Adapt<LoginResponseDto>();
        loginResponse = loginResponse with
        {
            UserRoles = await _unitOfWork.IdentityRepository.GetUserRolesAsync(user)
        };

        return ServiceResult<LoginResponseDto>.Success(loginResponse);
    }
}
