using PCI.Shared.Common;
using PCI.Shared.Dtos.Common;

namespace PCI.Application.Services.Interfaces;

public interface IGLAccountService
{
    Task<ServiceResult<List<DropdownDto>>> GetGLAccountsForDropdown(int organisationId);
}