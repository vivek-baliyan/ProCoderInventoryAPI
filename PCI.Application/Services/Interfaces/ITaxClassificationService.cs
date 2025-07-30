using PCI.Shared.Common;
using PCI.Shared.Dtos.Common;

namespace PCI.Application.Services.Interfaces;

public interface ITaxClassificationService
{
    Task<ServiceResult<List<DropdownDto>>> GetTaxClassificationsForDropdown(int organisationId);
}