using PCI.Shared.Common;
using PCI.Shared.Dtos;

namespace PCI.Application.Services.Interfaces;

public interface IOrganisationService
{
    Task<ServiceResult<OrganisationDto>> CreateOrganisation(string userId, OrganisationDto organisationDto);
    Task<ServiceResult<OrganisationDto>> GetOrganisationByUserId(string userId);
    Task<ServiceResult<OrganisationDto>> UpdateOrganisation(string userId, OrganisationDto organisationDto);
}