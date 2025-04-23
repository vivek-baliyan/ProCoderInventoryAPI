using PCI.Shared.Common;
using PCI.Shared.Dtos;

namespace PCI.Application.Services.Interfaces;

public interface IAccountService
{
    Task<ServiceResult<UserProfileDto>> CreateUserProfile(string userId, RegisterUserDto registerUserDto);
    Task<ServiceResult<UserProfileDto>> GetUserProfileByUserId(string userId);
    Task<ServiceResult<bool>> UpdateProfile(UpdateProfileDto updateProfileDto);
    Task<ServiceResult<bool>> UpdateProfileSettings(UpdateProfileSettingsDto updateProfileSettingsDto);
}