using Mapster;
using PCI.Application.Repositories;
using PCI.Application.Services.Interfaces;
using PCI.Domain.Models;
using PCI.Shared.Common;
using PCI.Shared.Dtos;

namespace PCI.Application.Services.Implementations;

public class AccountService(IUnitOfWork unitOfWork) : IAccountService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<ServiceResult<UserProfileDto>> CreateUserProfile(string userId, RegisterUserDto registerUserDto)
    {
        var existingUserProfile = await _unitOfWork.Repository<AppUserProfile>()
            .GetFirstOrDefaultAsync(x => x.UserId == userId);

        // Check if the user profile already exists
        if (existingUserProfile != null)
        {
            return ServiceResult<UserProfileDto>
                .Success(existingUserProfile.Adapt<UserProfileDto>());
        }
        try
        {
            var userProfile = new AppUserProfile
            {
                UserId = userId,
                FirstName = registerUserDto.FirstName,
                LastName = registerUserDto.LastName,
                ProfileImageUrl = registerUserDto.ProfileImageUrl,
                DateOfBirth = registerUserDto.DateOfBirth,
                Country = registerUserDto.Country,
                StreetAddress = registerUserDto.StreetAddress,
                City = registerUserDto.City,
                State = registerUserDto.State,
                PostalCode = registerUserDto.PostalCode,
                Bio = registerUserDto.Bio,
                CompanyName = registerUserDto.CompanyName,
                ContactPerson = registerUserDto.ContactPerson,
                WebsiteUrl = registerUserDto.WebsiteUrl,
                CreatedBy = userId,
                CreatedOn = DateTime.UtcNow,
            };

            _unitOfWork.Repository<AppUserProfile>().Add(userProfile);
            await _unitOfWork.SaveChangesAsync();

            return ServiceResult<UserProfileDto>
                .Success(userProfile.Adapt<UserProfileDto>());
        }
        catch (Exception ex)
        {
            return ServiceResult<UserProfileDto>
                .Error(new Problem("UserProfileCreationError", ex.ToString()));
        }
    }

    public async Task<ServiceResult<UserProfileDto>> GetUserProfileByUserId(string userId)
    {
        var userProfile = await _unitOfWork.Repository<AppUserProfile>()
            .GetFirstOrDefaultAsync(x => x.UserId == userId);

        if (userProfile == null)
        {
            return ServiceResult<UserProfileDto>
                .Error(new Problem("UserProfileNotFound", "User profile not found."));
        }

        return ServiceResult<UserProfileDto>.Success(userProfile.Adapt<UserProfileDto>());
    }

    public async Task<ServiceResult<bool>> UpdateProfile(UpdateProfileDto updateProfileDto)
    {
        var userProfile = await _unitOfWork.Repository<AppUserProfile>()
            .GetFirstOrDefaultAsync(x => x.Id == updateProfileDto.ProfileId);

        if (userProfile == null)
        {
            return ServiceResult<bool>
                .Error(new Problem("UserProfileNotFound", "User profile not found."));
        }

        userProfile.FirstName = updateProfileDto.FirstName;
        userProfile.LastName = updateProfileDto.LastName;
        userProfile.ProfileImageUrl = updateProfileDto.ProfileImageUrl;
        userProfile.Bio = updateProfileDto.Bio;
        userProfile.DateOfBirth = Convert.ToDateTime(updateProfileDto.DateOfBirth);

        _unitOfWork.Repository<AppUserProfile>().Update(userProfile);
        await _unitOfWork.SaveChangesAsync();

        return ServiceResult<bool>.Success(true);
    }

    public async Task<ServiceResult<bool>> UpdateProfileSettings(UpdateProfileSettingsDto updateProfileSettingsDto)
    {
        var userProfile = await _unitOfWork.Repository<AppUserProfile>()
            .GetFirstOrDefaultAsync(x => x.Id == updateProfileSettingsDto.ProfileId);

        if (userProfile == null)
        {
            return ServiceResult<bool>
                .Error(new Problem("UserProfileNotFound", "User profile not found."));
        }

        userProfile.CompanyName = updateProfileSettingsDto.CompanyName;
        userProfile.ContactPerson = updateProfileSettingsDto.ContactPerson;
        userProfile.WebsiteUrl = updateProfileSettingsDto.WebsiteUrl;
        userProfile.StreetAddress = updateProfileSettingsDto.StreetAddress;
        userProfile.PostalCode = updateProfileSettingsDto.PostalCode;
        userProfile.City = updateProfileSettingsDto.City;
        userProfile.State = updateProfileSettingsDto.State;
        userProfile.Country = updateProfileSettingsDto.Country;

        _unitOfWork.Repository<AppUserProfile>().Update(userProfile);
        await _unitOfWork.SaveChangesAsync();

        return ServiceResult<bool>.Success(true);
    }
}