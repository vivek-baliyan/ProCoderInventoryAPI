using PCI.Shared.Common;
using PCI.Shared.Dtos;

namespace PCI.Application.Services.Interfaces;

public interface IAccountService
{
    Task<ServiceResult<UserProfileDto>> CreateUserProfile(string userId, RegisterUserDto registerUserDto);
    Task<ServiceResult<UserProfileDto>> GetUserProfileByUserId(string userId);
    Task<ServiceResult<UserProfileDto>> UpdateUserProfile(UpdateProfileDto updateProfileDto);
    Task<ServiceResult<UserProfileDto>> UpdateUserProfileSettings(UpdateProfileSettingsDto updateProfileSettingsDto);
}