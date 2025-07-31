using PCI.Application.Repositories;
using PCI.Application.Services.Interfaces;
using PCI.Application.Specifications;
using PCI.Domain.Models;
using PCI.Shared.Common;
using PCI.Shared.Dtos.Common;

namespace PCI.Application.Services.Implementations;

public class StateService(IUnitOfWork unitOfWork) : IStateService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<ServiceResult<List<DropdownDto>>> GetStatesForDropdown(int? countryId = null)
    {
        try
        {
            var specification = new StateSpecification(countryId);
            var states = await _unitOfWork.Repository<State>().GetAsync(specification);

            var result = states
                .Select(s => new DropdownDto
                {
                    Value = s.Id,
                    Label = s.Name,
                    Code = s.StateCode,
                    AdditionalData = new { s.CountryId, s.Type }
                })
                .ToList();

            return ServiceResult<List<DropdownDto>>.Success(result);
        }
        catch (Exception ex)
        {
            return ServiceResult<List<DropdownDto>>.Error(new Problem("StateService.GetStatesForDropdown", ex.Message));
        }
    }
}