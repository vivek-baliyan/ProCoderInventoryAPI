using Mapster;
using PCI.Application.Repositories;
using PCI.Application.Services.Interfaces;
using PCI.Domain.Models;
using PCI.Shared.Common;
using PCI.Shared.Common.Constants;
using PCI.Shared.Dtos;

namespace PCI.Application.Services.Implementations;

public class OrganisationService(IUnitOfWork unitOfWork) : IOrganisationService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<ServiceResult<OrganisationDto>> CreateOrganisation(string userId, OrganisationDto organisationDto)
    {
        var existingExisting = await _unitOfWork.Repository<Organisation>()
            .GetFirstOrDefaultAsync(x => x.UserId == userId);

        // Check if the user profile already exists
        if (existingExisting != null)
        {
            return ServiceResult<OrganisationDto>
                .Success(existingExisting.Adapt<OrganisationDto>());
        }
        try
        {
            var organisation = new Organisation
            {
                UserId = userId,
                CompanyName = organisationDto.CompanyName,
                ContactPerson = organisationDto.ContactPerson,
                PhoneNumber = organisationDto.PhoneNumber,
                Email = organisationDto.Email,
                WebsiteUrl = organisationDto.WebsiteUrl,
                Address = organisationDto.Address,
                Country = organisationDto.Country,
                State = organisationDto.State,
                City = organisationDto.City,
                PostalCode = organisationDto.PostalCode,
                CreatedBy = userId,
                CreatedOn = DateTime.UtcNow,
            };

            _unitOfWork.Repository<Organisation>().Add(organisation);
            await _unitOfWork.SaveChangesAsync();

            return ServiceResult<OrganisationDto>
                .Success(organisation.Adapt<OrganisationDto>());
        }
        catch (Exception ex)
        {
            return ServiceResult<OrganisationDto>
                .Error(new Problem(ErrorCodes.OrganisationCreationError, ex.Message));
        }
    }

    public async Task<ServiceResult<OrganisationDto>> GetOrganisationByUserId(string userId)
    {
        var organisation = await _unitOfWork.Repository<Organisation>()
            .GetFirstOrDefaultAsync(x => x.UserId == userId);

        if (organisation == null)
        {
            return ServiceResult<OrganisationDto>
                .Error(new Problem(ErrorCodes.OrganisationNotFound, Messages.OrganisationNotFound));
        }

        return ServiceResult<OrganisationDto>.Success(organisation.Adapt<OrganisationDto>());
    }

    public async Task<ServiceResult<OrganisationDto>> UpdateOrganisation(string userId, OrganisationDto updateOrganisationDto)
    {
        var organisation = await _unitOfWork.Repository<Organisation>()
            .GetFirstOrDefaultAsync(x => x.Id == updateOrganisationDto.OrganisationId && x.UserId == userId);

        if (organisation == null)
        {
            return ServiceResult<OrganisationDto>
                .Error(new Problem(ErrorCodes.OrganisationNotFound, Messages.OrganisationNotFound));
        }

        organisation.CompanyName = updateOrganisationDto.CompanyName;
        organisation.ContactPerson = updateOrganisationDto.ContactPerson;
        organisation.PhoneNumber = updateOrganisationDto.PhoneNumber;
        organisation.Email = updateOrganisationDto.Email;
        organisation.WebsiteUrl = updateOrganisationDto.WebsiteUrl;
        organisation.Address = updateOrganisationDto.Address;
        organisation.PostalCode = updateOrganisationDto.PostalCode;
        organisation.City = updateOrganisationDto.City;
        organisation.State = updateOrganisationDto.State;
        organisation.Country = updateOrganisationDto.Country;
        organisation.ModifiedBy = userId;
        organisation.ModifiedOn = DateTime.UtcNow;

        _unitOfWork.Repository<Organisation>().Update(organisation);
        await _unitOfWork.SaveChangesAsync();

        return ServiceResult<OrganisationDto>.Success(organisation.Adapt<OrganisationDto>());
    }
}