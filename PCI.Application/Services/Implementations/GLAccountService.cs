using PCI.Application.Repositories;
using PCI.Application.Services.Interfaces;
using PCI.Domain.Models;
using PCI.Shared.Common;
using PCI.Shared.Dtos.Common;

namespace PCI.Application.Services.Implementations;

public class GLAccountService(IUnitOfWork unitOfWork) : IGLAccountService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<ServiceResult<List<DropdownDto>>> GetGLAccountsForDropdown(int organisationId)
    {
        try
        {
            var brands = await _unitOfWork.Repository<GLAccount>()
                .GetFilteredAsync(b => b.IsActive && b.OrganisationId == organisationId);

            var result = brands
                .Select(b => new DropdownDto
                {
                    Value = b.Id,
                    Label = b.AccountName,
                    Code = b.AccountCode,
                    AdditionalData = new { b.AccountType }
                })
                .OrderBy(b => b.Label)
                .ToList();

            return ServiceResult<List<DropdownDto>>.Success(result);
        }
        catch (Exception ex)
        {
            return ServiceResult<List<DropdownDto>>.Error(new Problem("BrandService.GetGLAccountsForDropdown", ex.Message));
        }
    }
}