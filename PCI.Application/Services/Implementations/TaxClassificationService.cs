using PCI.Application.Repositories;
using PCI.Application.Services.Interfaces;
using PCI.Domain.Models;
using PCI.Shared.Common;
using PCI.Shared.Dtos.Common;

namespace PCI.Application.Services.Implementations;

public class TaxClassificationService(IUnitOfWork unitOfWork) : ITaxClassificationService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<ServiceResult<List<DropdownDto>>> GetTaxClassificationsForDropdown(int organisationId)
    {
        try
        {
            var taxClassifications = await _unitOfWork.Repository<TaxClassification>()
                .GetFilteredAsync(tc => tc.IsActive && tc.OrganisationId == organisationId);

            var result = taxClassifications
                .Select(tc => new DropdownDto
                {
                    Value = tc.Id,
                    Label = tc.Description,
                    Code = tc.Code,
                    AdditionalData = new { ClassificationType = tc.ClassificationType }
                })
                .OrderBy(tc => tc.Label)
                .ToList();

            return ServiceResult<List<DropdownDto>>.Success(result);
        }
        catch (Exception ex)
        {
            return ServiceResult<List<DropdownDto>>.Error(new Problem("TaxClassificationService.GetTaxClassificationsForDropdown", ex.Message));
        }
    }
}