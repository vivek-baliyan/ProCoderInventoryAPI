using PCI.Application.Repositories;
using PCI.Application.Services.Interfaces;
using PCI.Domain.Models;
using PCI.Shared.Common;
using PCI.Shared.Dtos;

namespace PCI.Application.Services.Implementations;

public class AccountService(IUnitOfWork unitOfWork) : IAccountService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<ServiceResult<AppUserProfile>> CreateUserProfile(string userId, RegisterUserDto registerUserDto)
    {
        var existingUserProfile = await _unitOfWork.Repository<AppUserProfile>()
            .GetFirstOrDefaultAsync(x => x.UserId == userId);

        // Check if the user profile already exists
        if (existingUserProfile != null)
        {
            return ServiceResult<AppUserProfile>.Success(existingUserProfile);
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

            return ServiceResult<AppUserProfile>.Success(userProfile);
        }
        catch (Exception ex)
        {
            return ServiceResult<AppUserProfile>.Error(
                new Problem("UserProfileCreationError", ex.ToString()));
        }
    }

    public async Task<ServiceResult<AppUserProfile>> GetUserProfileByUserId(string userId)
    {
        var userProfile = await _unitOfWork.Repository<AppUserProfile>()
            .GetFirstOrDefaultAsync(x => x.UserId == userId);

        if (userProfile == null)
        {
            return ServiceResult<AppUserProfile>.Error(
                new Problem("UserProfileNotFound", "User profile not found."));
        }

        return ServiceResult<AppUserProfile>.Success(userProfile);
    }
}