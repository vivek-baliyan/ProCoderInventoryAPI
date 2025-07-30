using PCI.Shared.Common;
using PCI.Shared.Dtos.Common;

namespace PCI.Application.Services.Interfaces;

public interface IUnitOfMeasureService
{
    Task<ServiceResult<List<DropdownDto>>> GetUnitsForDropdown(int organisationId, int unitType);
}