using PCI.Shared.Common;
using PCI.Shared.Dtos.Common;

namespace PCI.Application.Services.Interfaces;

public interface IHSNMasterService
{
    Task<ServiceResult<List<DropdownDto>>> GetHSNMastersForDropdown();
}