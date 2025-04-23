using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PCI.Application.Repositories;
using PCI.Domain.Models;

namespace PCI.Persistence.Repositories;

public class IdentityRepository(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager) : IIdentityRepository
{
    private readonly UserManager<AppUser> _userManager = userManager;
    private readonly RoleManager<AppRole> _roleManager = roleManager;

    #region Role Manager

    public async Task<IdentityResult> CreateRoleAsync(AppRole role)
    {
        return await _roleManager.CreateAsync(role);
    }

    public async Task<List<AppRole>> GetAllRolesAsync()
    {
        return await _roleManager.Roles.ToListAsync();
    }

    public async Task<AppRole> FindRoleByNameAsync(string roleName)
    {
        return await _roleManager.FindByNameAsync(roleName);
    }

    #endregion

    #region User Manager

    public async Task<IdentityResult> CreateUserAsync(AppUser user, string password)
    {
        return await _userManager.CreateAsync(user, password);
    }

    public async Task<IdentityResult> AddUserToRoleAsync(AppUser user, string roleName)
    {
        return await _userManager.AddToRoleAsync(user, roleName);
    }

    public async Task<AppUser> FindUserByIdAsync(string userId)
    {
        return await _userManager.FindByIdAsync(userId);
    }

    public async Task<AppUser> FindUserByEmailAsync(string email)
    {
        return await _userManager.FindByEmailAsync(email);
    }

    public async Task<bool> ValidateUserPasswordAsync(AppUser user, string password)
    {
        return await _userManager.CheckPasswordAsync(user, password);
    }

    public async Task<List<string>> GetUserRolesAsync(AppUser user)
    {
        return [.. await _userManager.GetRolesAsync(user)];
    }

    public async Task<IdentityResult> UpdateUserAsync(AppUser user)
    {
        return await _userManager.UpdateAsync(user);
    }

    public async Task<IdentityResult> ChangeUserPasswordAsync(AppUser user, string currentPassword, string newPassword)
    {
        return await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);
    }

    #endregion
}