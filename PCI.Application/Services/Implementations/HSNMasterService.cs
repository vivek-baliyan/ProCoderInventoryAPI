using PCI.Application.Repositories;
using PCI.Application.Services.Interfaces;
using PCI.Domain.Models;
using PCI.Shared.Common;
using PCI.Shared.Dtos.Common;

namespace PCI.Application.Services.Implementations;

public class HSNMasterService(IUnitOfWork unitOfWork) : IHSNMasterService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<ServiceResult<List<DropdownDto>>> GetHSNMastersForDropdown()
    {
        try
        {
            var hsnMasters = await _unitOfWork.Repository<HSNMaster>()
                .GetFilteredAsync(hsn => hsn.IsActive);

            var result = hsnMasters
                .Select(hsn => new DropdownDto
                {
                    Value = hsn.Id,
                    Label = $"{hsn.HSNCode} - {hsn.Description}",
                    Code = hsn.HSNCode,
                    Description = hsn.Description
                })
                .OrderBy(hsn => hsn.Code)
                .ToList();

            return ServiceResult<List<DropdownDto>>.Success(result);
        }
        catch (Exception ex)
        {
            return ServiceResult<List<DropdownDto>>.Error(new Problem("HSNMasterService.GetHSNMastersForDropdown", ex.Message));
        }
    }
}