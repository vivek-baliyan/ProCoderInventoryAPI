using PCI.Shared.Common;
using PCI.Shared.Dtos.Common;

namespace PCI.Application.Services.Interfaces;

public interface ITaxMasterService
{
    Task<ServiceResult<List<DropdownDto>>> GetTaxMastersForDropdown(int organisationId);
}