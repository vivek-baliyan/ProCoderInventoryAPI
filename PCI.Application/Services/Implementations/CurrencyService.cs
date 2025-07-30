using PCI.Application.Repositories;
using PCI.Application.Services.Interfaces;
using PCI.Domain.Models;
using PCI.Shared.Common;
using PCI.Shared.Dtos.Common;

namespace PCI.Application.Services.Implementations;

public class CurrencyService(IUnitOfWork unitOfWork) : ICurrencyService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<ServiceResult<List<DropdownDto>>> GetCurrenciesForDropdown(int organisationId)
    {
        try
        {
            var currencies = await _unitOfWork.Repository<Currency>()
                .GetFilteredAsync(c => c.IsActive && c.OrganisationId == organisationId);

            var result = currencies
                .Select(c => new DropdownDto
                {
                    Value = c.Id,
                    Label = c.Name,
                    Code = c.Code,
                    AdditionalData = new { Symbol = c.Symbol }
                })
                .OrderBy(c => c.Label)
                .ToList();

            return ServiceResult<List<DropdownDto>>.Success(result);
        }
        catch (Exception ex)
        {
            return ServiceResult<List<DropdownDto>>.Error(new Problem("CurrencyService.GetCurrenciesForDropdown", ex.Message));
        }
    }
}