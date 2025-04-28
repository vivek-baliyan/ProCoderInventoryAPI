using Microsoft.AspNetCore.Identity;
using PCI.Shared.Common;
using PCI.Shared.Dtos;

namespace PCI.Application.Services.Interfaces;

public interface IIdentityService
{
    #region Role Management

    Task<ServiceResult<IdentityResult>> CreateRole(CreateRoleDto addAppRoleDto);
    Task<ServiceResult<IReadOnlyList<RoleDto>>> GetAllRoles();
    Task<ServiceResult<RoleDto>> GetRoleByName(string roleName);

    #endregion

    #region User Management

    Task<ServiceResult<IdentityResult>> CreateUser(RegisterUserDto registerUserDto);
    Task<ServiceResult<IdentityResult>> AssignRoleToUser(string userEmail, string roleName);
    Task<ServiceResult<UserDto>> GetUserById(string userId);
    Task<ServiceResult<UserDto>> GetUserByEmail(string userEmail);
    Task<ServiceResult<IdentityResult>> UpdateUserDetails(UpdateUserDetailsDto updateUserDetailsDto);
    Task<ServiceResult<IdentityResult>> UpdateLoginDetails(UpdateLoginDetailsDto updateLoginDetailsDto);
    Task<ServiceResult<UserDto>> UserLogin(UserLoginDto loginUserDto);

    #endregion
}
