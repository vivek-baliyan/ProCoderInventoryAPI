using PCI.Shared.Common;
using PCI.Shared.Dtos.Common;

namespace PCI.Application.Services.Interfaces;

public interface IBrandService
{
    Task<ServiceResult<List<DropdownDto>>> GetBrandsForDropdown(int organisationId);
}