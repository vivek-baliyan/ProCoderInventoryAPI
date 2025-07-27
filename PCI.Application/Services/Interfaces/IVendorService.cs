using PCI.Shared.Common;
using PCI.Shared.Dtos.Vendor;

namespace PCI.Application.Services.Interfaces;

public interface IVendorService
{
    Task<ServiceResult<bool>> CreateVendor(string userId, int organisationId, CreateVendorDto createVendorDto);
    Task<ServiceResult<bool>> UpdateVendor(string userId, int organisationId, UpdateVendorDto updateVendorDto);
    Task<ServiceResult<VendorDto>> GetVendorById(int id);
    Task<ServiceResult<List<VendorListItemDto>>> GetAllVendors(int organisationId, int pageIndex, int pageSize);
    Task<ServiceResult<List<VendorListItemDto>>> GetFilteredVendors(int organisationId, VendorFilterDto filter);
    Task<ServiceResult<bool>> DeleteVendor(int id, string userId);
    Task<ServiceResult<bool>> ToggleVendorStatus(int id, string userId);
    Task<ServiceResult<bool>> UpdateVendorPerformance(int id, decimal performanceRating, int onTimeDeliveryPercentage, int qualityRating, string userId);
    Task<ServiceResult<bool>> BlacklistVendor(int id, string reason, string userId);
    Task<ServiceResult<bool>> RemoveVendorFromBlacklist(int id, string userId);
    Task<ServiceResult<bool>> MarkAsPreferredVendor(int id, string userId);
    Task<ServiceResult<bool>> RemovePreferredVendorStatus(int id, string userId);
}