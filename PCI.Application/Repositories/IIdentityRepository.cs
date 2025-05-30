﻿using Microsoft.AspNetCore.Identity;
using PCI.Domain.Models;

namespace PCI.Application.Repositories;

public interface IIdentityRepository
{
    #region Role Manager
    Task<IdentityResult> CreateRoleAsync(AppRole role);
    Task<List<AppRole>> GetAllRolesAsync();
    Task<AppRole> FindRoleByNameAsync(string roleName);
    #endregion

    #region User Manager
    Task<IdentityResult> CreateUserAsync(AppUser user, string password);
    Task<IdentityResult> AddUserToRoleAsync(AppUser user, string roleName);
    Task<AppUser> FindUserByIdAsync(string userId);
    Task<AppUser> FindUserByEmailAsync(string email);
    Task<bool> ValidateUserPasswordAsync(AppUser user, string password);
    Task<List<string>> GetUserRolesAsync(AppUser user);
    Task<IdentityResult> UpdateUserAsync(AppUser user);
    Task<IdentityResult> ChangeUserPasswordAsync(AppUser user, string currentPassword, string newPassword);

    #endregion
}