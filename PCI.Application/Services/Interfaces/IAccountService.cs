using PCI.Domain.Models;
using PCI.Shared.Common;
using PCI.Shared.Dtos;

namespace PCI.Application.Services.Interfaces;

public interface IAccountService
{
    Task<ServiceResult<AppUserProfile>> CreateUserProfile(string userId, RegisterUserDto registerUserDto);
    Task<ServiceResult<AppUserProfile>> GetUserProfileByUserId(string userId);
}