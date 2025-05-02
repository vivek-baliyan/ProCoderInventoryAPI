using Mapster;
using Microsoft.AspNetCore.Identity;
using PCI.Application.Repositories;
using PCI.Application.Services.Interfaces;
using PCI.Domain.Models;
using PCI.Shared.Common;
using PCI.Shared.Common.Constants;
using PCI.Shared.Dtos.Identity;

namespace PCI.Application.Services.Implementations;

public class IdentityService(IUnitOfWork unitOfWork) : IIdentityService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    #region Role Management

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

    #endregion

    #region User Management

    public async Task<ServiceResult<IdentityResult>> CreateUser(RegisterUserDto registerUserDto)
    {
        var user = new AppUser
        {
            UserName = registerUserDto.Email.Split('@')[0],
            Email = registerUserDto.Email,
            FirstName = registerUserDto.FirstName,
            LastName = registerUserDto.LastName,
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
            return ServiceResult<IdentityResult>.Error(new Problem(ErrorCodes.UserNotFound, $"User with email {userEmail} not found."));
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
            return ServiceResult<UserDto>.Error(new Problem(ErrorCodes.UserNotFound, $"User with ID {userId} not found."));
        }

        return ServiceResult<UserDto>.Success(user.Adapt<UserDto>());
    }

    public async Task<ServiceResult<UserDto>> GetUserByEmail(string userEmail)
    {
        var user = await _unitOfWork.IdentityRepository.FindUserByEmailAsync(userEmail);

        if (user == null)
        {
            return ServiceResult<UserDto>.Error(new Problem(ErrorCodes.UserNotFound, $"User with Email {userEmail} not found."));
        }

        return ServiceResult<UserDto>.Success(user.Adapt<UserDto>());
    }

    public async Task<ServiceResult<IdentityResult>> UpdateUserDetails(UpdateUserDetailsDto updateUserDetailsDto)
    {
        var user = await _unitOfWork.IdentityRepository.FindUserByIdAsync(updateUserDetailsDto.UserId);

        if (user == null)
        {
            return ServiceResult<IdentityResult>
                .Error(new Problem(ErrorCodes.UserNotFound, $"User with ID {updateUserDetailsDto.UserId} not found."));
        }

        user.FirstName = updateUserDetailsDto.FirstName;
        user.LastName = updateUserDetailsDto.LastName;
        user.ProfileImageUrl = updateUserDetailsDto.ProfileImageUrl;
        user.Bio = updateUserDetailsDto.Bio;
        user.DateOfBirth = updateUserDetailsDto.DateOfBirth;
        user.PhoneNumber = updateUserDetailsDto.PhoneNumber;
        user.Country = updateUserDetailsDto.Country;
        user.Address = updateUserDetailsDto.Address;
        user.City = updateUserDetailsDto.City;
        user.State = updateUserDetailsDto.State;
        user.PostalCode = updateUserDetailsDto.PostalCode;

        var result = await _unitOfWork.IdentityRepository.UpdateUserAsync(user);

        if (!result.Succeeded)
        {
            var errors = result.Errors.Select(e => new Problem(e.Code, e.Description)).ToList();
            return ServiceResult<IdentityResult>.Errors(errors);
        }

        return ServiceResult<IdentityResult>.Success(result);
    }

    public async Task<ServiceResult<IdentityResult>> UpdateLoginDetails(UpdateLoginDetailsDto updateLoginDetailsDto)
    {
        var user = await _unitOfWork.IdentityRepository.FindUserByIdAsync(updateLoginDetailsDto.UserId);

        if (user == null)
        {
            return ServiceResult<IdentityResult>
                .Error(new Problem(ErrorCodes.UserNotFound, $"User with ID {updateLoginDetailsDto.UserId} not found."));
        }

        user.Email = updateLoginDetailsDto.Email;
        user.UserName = updateLoginDetailsDto.Email.Split('@')[0];
        user.LastPasswordChange = DateTime.UtcNow;

        var result = await _unitOfWork.IdentityRepository.ChangeUserPasswordAsync(
            user,
            updateLoginDetailsDto.CurrentPassword,
            updateLoginDetailsDto.NewPassword);

        if (!result.Succeeded)
        {
            var errors = result.Errors.Select(e => new Problem(e.Code, e.Description)).ToList();
            return ServiceResult<IdentityResult>.Errors(errors);
        }

        return ServiceResult<IdentityResult>.Success(result);
    }

    public async Task<ServiceResult<UserDto>> UserLogin(UserLoginDto loginUserDto)
    {
        var user = await _unitOfWork.IdentityRepository.FindUserByEmailAsync(loginUserDto.Email);

        if (user == null)
        {
            return ServiceResult<UserDto>.Error(new Problem(ErrorCodes.InvalidCredentials, Messages.InvalidCredentials));
        }

        var result = await _unitOfWork.IdentityRepository.ValidateUserPasswordAsync(user, loginUserDto.Password);

        if (!result)
        {
            return ServiceResult<UserDto>.Error(new Problem(ErrorCodes.InvalidCredentials, Messages.InvalidCredentials));
        }

        var userDto = user.Adapt<UserDto>() with
        {
            Roles = await _unitOfWork.IdentityRepository.GetUserRolesAsync(user)
        };

        return ServiceResult<UserDto>.Success(userDto);
    }

    #endregion
}
