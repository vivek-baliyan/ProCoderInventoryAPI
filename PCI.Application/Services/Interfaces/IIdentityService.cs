using Microsoft.AspNetCore.Identity;
using PCI.Shared.Common;
using PCI.Shared.Dtos;

namespace PCI.Application.Services.Interfaces;

public interface IIdentityService
{
    Task<ServiceResult<IdentityResult>> CreateRole(CreateRoleDto addAppRoleDto);
    Task<ServiceResult<IReadOnlyList<RoleDto>>> GetAllRoles();
    Task<ServiceResult<RoleDto>> GetRoleByName(string roleName);

    Task<ServiceResult<IdentityResult>> CreateUser(RegisterUserDto registerUserDto);
    Task<ServiceResult<IdentityResult>> AssignRoleToUser(string userEmail, string roleName);
    Task<ServiceResult<UserDto>> GetUserById(string userId);
    Task<ServiceResult<UserDto>> GetUserByEmail(string userEmail);
    Task<ServiceResult<LoginResponseDto>> UserLogin(UserLoginDto loginUserDto);
}
