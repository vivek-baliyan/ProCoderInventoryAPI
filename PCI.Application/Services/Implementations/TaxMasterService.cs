using PCI.Application.Repositories;
using PCI.Application.Services.Interfaces;
using PCI.Domain.Models;
using PCI.Shared.Common;
using PCI.Shared.Dtos.Common;

namespace PCI.Application.Services.Implementations;

public class TaxMasterService(IUnitOfWork unitOfWork) : ITaxMasterService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<ServiceResult<List<DropdownDto>>> GetTaxMastersForDropdown(int organisationId)
    {
        try
        {
            var taxes = await _unitOfWork.Repository<TaxMaster>()
                .GetFilteredAsync(tm => tm.IsActive && tm.OrganisationId == organisationId);

            var result = taxes
                .Select(tm => new DropdownDto
                {
                    Value = tm.Id,
                    Label = tm.TaxName,
                    Code = tm.TaxCode,
                    AdditionalData = new { TaxRate = tm.TaxRate }
                })
                .OrderBy(tm => tm.Label)
                .ToList();

            return ServiceResult<List<DropdownDto>>.Success(result);
        }
        catch (Exception ex)
        {
            return ServiceResult<List<DropdownDto>>.Error(new Problem("TaxMasterService.GetTaxMastersForDropdown", ex.Message));
        }
    }
}