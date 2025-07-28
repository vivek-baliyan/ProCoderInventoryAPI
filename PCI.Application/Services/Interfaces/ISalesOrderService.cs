using PCI.Shared.Common;
using PCI.Shared.Dtos.SalesOrder;

namespace PCI.Application.Services.Interfaces;

public interface ISalesOrderService
{
    Task<ServiceResult<bool>> CreateSalesOrder(string userId, int organisationId, CreateSalesOrderDto createSalesOrderDto);
    Task<ServiceResult<bool>> UpdateSalesOrder(string userId, int organisationId, UpdateSalesOrderDto updateSalesOrderDto);
    Task<ServiceResult<SalesOrderDto>> GetSalesOrderById(int id);
    Task<ServiceResult<List<SalesOrderListItemDto>>> GetAllSalesOrders(int pageIndex, int pageSize);
    Task<ServiceResult<List<SalesOrderListItemDto>>> GetFilteredSalesOrders(int organisationId, SalesOrderFilterDto filter);
    Task<ServiceResult<bool>> UpdateSalesOrderStatus(int id, string status, string userId);
    Task<ServiceResult<bool>> DeleteSalesOrder(int id, string userId);
}