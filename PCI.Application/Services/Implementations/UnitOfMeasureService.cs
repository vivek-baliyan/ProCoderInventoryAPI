using PCI.Application.Repositories;
using PCI.Application.Services.Interfaces;
using PCI.Application.Specifications;
using PCI.Domain.Models;
using PCI.Shared.Common;
using PCI.Shared.Dtos.Common;

namespace PCI.Application.Services.Implementations;

public class UnitOfMeasureService(IUnitOfWork unitOfWork) : IUnitOfMeasureService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<ServiceResult<List<DropdownDto>>> GetUnitsForDropdown(int organisationId, int unitType)
    {
        try
        {
            var specification = new UnitOfMeasureDropdownSpecification(organisationId, unitType);
            var units = await _unitOfWork.Repository<UnitOfMeasure>().GetAsync(specification);

            var result = units
                .Select(u => new DropdownDto
                {
                    Value = u.Id,
                    Label = u.Name,
                    Code = u.Code,
                    AdditionalData = new { u.UnitType }
                })
                .ToList();

            return ServiceResult<List<DropdownDto>>.Success(result);
        }
        catch (Exception ex)
        {
            return ServiceResult<List<DropdownDto>>.Error(new Problem("UnitOfMeasureService.GetUnitsForDropdown", ex.Message));
        }
    }
}