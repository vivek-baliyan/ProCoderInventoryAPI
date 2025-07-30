using Mapster;
using PCI.Application.Repositories;
using PCI.Application.Services.Interfaces;
using PCI.Application.Specifications;
using PCI.Domain.Models;
using PCI.Shared.Common;
using PCI.Shared.Common.Constants;
using PCI.Shared.Dtos.SalesOrder;

namespace PCI.Application.Services.Implementations;

public class SalesOrderService(IUnitOfWork unitOfWork) : ISalesOrderService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<ServiceResult<bool>> CreateSalesOrder(
        string userId,
        int organisationId,
        CreateSalesOrderDto createSalesOrderDto)
    {
        var existingSalesOrder = await _unitOfWork.Repository<SalesOrder>()
            .GetFirstOrDefaultAsync(x => x.OrderNumber == createSalesOrderDto.OrderNumber && x.OrganisationId == organisationId);

        if (existingSalesOrder != null)
        {
            return ServiceResult<bool>
                .Error(new Problem(ErrorCodes.SalesOrderAlreadyExists, "Sales order with this order number already exists"));
        }

        using var transaction = await _unitOfWork.BeginTransactionAsync();
        try
        {
            // Create main SalesOrder entity
            var salesOrder = new SalesOrder
            {
                OrderNumber = createSalesOrderDto.OrderNumber,
                OrderDate = createSalesOrderDto.OrderDate,
                Status = "Draft",
                ExpectedDeliveryDate = createSalesOrderDto.ExpectedDeliveryDate,
                ReferenceNumber = createSalesOrderDto.ReferenceNumber,
                QuoteNumber = createSalesOrderDto.QuoteNumber,
                CustomerId = createSalesOrderDto.CustomerId,
                OrganisationId = organisationId,
                PriceListId = createSalesOrderDto.PriceListId,
                Notes = createSalesOrderDto.Notes,
                BillingAddress = createSalesOrderDto.BillingAddress,
                CreatedBy = userId,
                CreatedOn = DateTime.UtcNow
            };

            _unitOfWork.Repository<SalesOrder>().Add(salesOrder);
            await _unitOfWork.SaveChangesAsync();

            // Calculate totals
            decimal subTotal = 0;
            decimal taxAmount = 0;

            // Create SalesOrderItems
            foreach (var itemDto in createSalesOrderDto.SalesOrderItems)
            {
                var lineTotal = itemDto.Quantity * itemDto.UnitPrice - itemDto.DiscountAmount;
                subTotal += lineTotal;

                var salesOrderItem = new SalesOrderItem
                {
                    SalesOrderId = salesOrder.Id,
                    ProductId = itemDto.ProductId,
                    Quantity = (int)itemDto.Quantity,
                    UnitPrice = itemDto.UnitPrice,
                    DiscountPercentage = itemDto.DiscountPercentage,
                    DiscountAmount = itemDto.DiscountAmount,
                    LineTotal = lineTotal,
                    ItemNotes = itemDto.Description,
                    CreatedBy = userId,
                    CreatedOn = DateTime.UtcNow
                };

                _unitOfWork.Repository<SalesOrderItem>().Add(salesOrderItem);
            }

            // Update SalesOrder totals
            salesOrder.SubTotal = subTotal;
            salesOrder.TaxAmount = taxAmount;
            salesOrder.TotalAmount = subTotal + taxAmount;

            // Create SalesOrderPayment if provided
            if (createSalesOrderDto.Payment != null)
            {
                var salesOrderPayment = new SalesOrderPayment
                {
                    SalesOrderId = salesOrder.Id,
                    PaymentMethod = createSalesOrderDto.Payment.PaymentMethod,
                    PaymentTerms = createSalesOrderDto.Payment.PaymentTerms,
                    DueDate = createSalesOrderDto.Payment.DueDate,
                    PaymentNotes = createSalesOrderDto.Payment.PaymentNotes,
                    CreatedBy = userId,
                    CreatedOn = DateTime.UtcNow
                };

                _unitOfWork.Repository<SalesOrderPayment>().Add(salesOrderPayment);
            }

            // Create SalesOrderShipping if provided
            if (createSalesOrderDto.Shipping != null)
            {
                var salesOrderShipping = new SalesOrderShipping
                {
                    SalesOrderId = salesOrder.Id,
                    ShippingAddress = createSalesOrderDto.Shipping.ShippingAddress,
                    ShippingMethod = createSalesOrderDto.Shipping.ShippingMethod,
                    ShippingCost = createSalesOrderDto.Shipping.ShippingCost,
                    TrackingNumber = createSalesOrderDto.Shipping.TrackingNumber,
                    EstimatedDeliveryDate = createSalesOrderDto.Shipping.EstimatedDeliveryDate,
                    ShippingNotes = createSalesOrderDto.Shipping.ShippingNotes,
                    CreatedBy = userId,
                    CreatedOn = DateTime.UtcNow
                };

                _unitOfWork.Repository<SalesOrderShipping>().Add(salesOrderShipping);
            }

            _unitOfWork.Repository<SalesOrder>().Update(salesOrder);
            await _unitOfWork.SaveChangesAsync();
            await transaction.CommitAsync();

            return ServiceResult<bool>.Success(true);
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            return ServiceResult<bool>
                .Error(new Problem(ErrorCodes.SalesOrderCreationError, ex.Message, ex.ToString()));
        }
    }

    public async Task<ServiceResult<bool>> UpdateSalesOrder(
        string userId,
        int organisationId,
        UpdateSalesOrderDto updateSalesOrderDto)
    {
        var existingSalesOrder = await _unitOfWork.Repository<SalesOrder>()
            .GetFirstOrDefaultAsync(x => x.Id == updateSalesOrderDto.Id && x.OrganisationId == organisationId,
                includeroperties: "SalesOrderItems,SalesOrderPayment,SalesOrderShipping");

        if (existingSalesOrder == null)
        {
            return ServiceResult<bool>
                .Error(new Problem(ErrorCodes.SalesOrderNotFound, "Sales order not found"));
        }

        using var transaction = await _unitOfWork.BeginTransactionAsync();
        try
        {
            // Update main SalesOrder entity
            existingSalesOrder.OrderNumber = updateSalesOrderDto.OrderNumber;
            existingSalesOrder.OrderDate = updateSalesOrderDto.OrderDate;
            existingSalesOrder.Status = updateSalesOrderDto.Status;
            existingSalesOrder.ExpectedDeliveryDate = updateSalesOrderDto.ExpectedDeliveryDate;
            existingSalesOrder.ReferenceNumber = updateSalesOrderDto.ReferenceNumber;
            existingSalesOrder.QuoteNumber = updateSalesOrderDto.QuoteNumber;
            existingSalesOrder.CustomerId = updateSalesOrderDto.CustomerId;
            existingSalesOrder.PriceListId = updateSalesOrderDto.PriceListId;
            existingSalesOrder.Notes = updateSalesOrderDto.Notes;
            existingSalesOrder.BillingAddress = updateSalesOrderDto.BillingAddress;
            existingSalesOrder.ModifiedBy = userId;
            existingSalesOrder.ModifiedOn = DateTime.UtcNow;

            // Remove existing items
            var existingItems = await _unitOfWork.Repository<SalesOrderItem>()
                .GetFilteredAsync(x => x.SalesOrderId == existingSalesOrder.Id);
            foreach (var item in existingItems)
            {
                _unitOfWork.Repository<SalesOrderItem>().Remove(item);
            }

            // Calculate totals
            decimal subTotal = 0;
            decimal taxAmount = 0;

            // Add updated items
            foreach (var itemDto in updateSalesOrderDto.SalesOrderItems)
            {
                var lineTotal = itemDto.Quantity * itemDto.UnitPrice - itemDto.DiscountAmount;
                subTotal += lineTotal;

                var salesOrderItem = new SalesOrderItem
                {
                    SalesOrderId = existingSalesOrder.Id,
                    ProductId = itemDto.ProductId,
                    Quantity = (int)itemDto.Quantity,
                    UnitPrice = itemDto.UnitPrice,
                    DiscountPercentage = itemDto.DiscountPercentage,
                    DiscountAmount = itemDto.DiscountAmount,
                    LineTotal = lineTotal,
                    ItemNotes = itemDto.Description,
                    CreatedBy = userId,
                    CreatedOn = DateTime.UtcNow
                };

                _unitOfWork.Repository<SalesOrderItem>().Add(salesOrderItem);
            }

            // Update totals
            existingSalesOrder.SubTotal = subTotal;
            existingSalesOrder.TaxAmount = taxAmount;
            existingSalesOrder.TotalAmount = subTotal + taxAmount;

            // Update payment if provided
            if (updateSalesOrderDto.Payment != null)
            {
                if (existingSalesOrder.SalesOrderPayment != null)
                {
                    existingSalesOrder.SalesOrderPayment.PaymentMethod = updateSalesOrderDto.Payment.PaymentMethod;
                    existingSalesOrder.SalesOrderPayment.PaymentTerms = updateSalesOrderDto.Payment.PaymentTerms;
                    existingSalesOrder.SalesOrderPayment.DueDate = updateSalesOrderDto.Payment.DueDate;
                    existingSalesOrder.SalesOrderPayment.PaymentNotes = updateSalesOrderDto.Payment.PaymentNotes;
                    existingSalesOrder.SalesOrderPayment.ModifiedBy = userId;
                    existingSalesOrder.SalesOrderPayment.ModifiedOn = DateTime.UtcNow;
                }
                else
                {
                    var salesOrderPayment = new SalesOrderPayment
                    {
                        SalesOrderId = existingSalesOrder.Id,
                        PaymentMethod = updateSalesOrderDto.Payment.PaymentMethod,
                        PaymentTerms = updateSalesOrderDto.Payment.PaymentTerms,
                        DueDate = updateSalesOrderDto.Payment.DueDate,
                        PaymentNotes = updateSalesOrderDto.Payment.PaymentNotes,
                        CreatedBy = userId,
                        CreatedOn = DateTime.UtcNow
                    };
                    _unitOfWork.Repository<SalesOrderPayment>().Add(salesOrderPayment);
                }
            }

            // Update shipping if provided
            if (updateSalesOrderDto.Shipping != null)
            {
                if (existingSalesOrder.SalesOrderShipping != null)
                {
                    existingSalesOrder.SalesOrderShipping.ShippingAddress = updateSalesOrderDto.Shipping.ShippingAddress;
                    existingSalesOrder.SalesOrderShipping.ShippingMethod = updateSalesOrderDto.Shipping.ShippingMethod;
                    existingSalesOrder.SalesOrderShipping.ShippingCost = updateSalesOrderDto.Shipping.ShippingCost;
                    existingSalesOrder.SalesOrderShipping.TrackingNumber = updateSalesOrderDto.Shipping.TrackingNumber;
                    existingSalesOrder.SalesOrderShipping.EstimatedDeliveryDate = updateSalesOrderDto.Shipping.EstimatedDeliveryDate;
                    existingSalesOrder.SalesOrderShipping.ShippingNotes = updateSalesOrderDto.Shipping.ShippingNotes;
                    existingSalesOrder.SalesOrderShipping.ModifiedBy = userId;
                    existingSalesOrder.SalesOrderShipping.ModifiedOn = DateTime.UtcNow;
                }
                else
                {
                    var salesOrderShipping = new SalesOrderShipping
                    {
                        SalesOrderId = existingSalesOrder.Id,
                        ShippingAddress = updateSalesOrderDto.Shipping.ShippingAddress,
                        ShippingMethod = updateSalesOrderDto.Shipping.ShippingMethod,
                        ShippingCost = updateSalesOrderDto.Shipping.ShippingCost,
                        TrackingNumber = updateSalesOrderDto.Shipping.TrackingNumber,
                        EstimatedDeliveryDate = updateSalesOrderDto.Shipping.EstimatedDeliveryDate,
                        ShippingNotes = updateSalesOrderDto.Shipping.ShippingNotes,
                        CreatedBy = userId,
                        CreatedOn = DateTime.UtcNow
                    };
                    _unitOfWork.Repository<SalesOrderShipping>().Add(salesOrderShipping);
                }
            }

            _unitOfWork.Repository<SalesOrder>().Update(existingSalesOrder);
            await _unitOfWork.SaveChangesAsync();
            await transaction.CommitAsync();

            return ServiceResult<bool>.Success(true);
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            return ServiceResult<bool>
                .Error(new Problem(ErrorCodes.SalesOrderUpdateError, ex.Message));
        }
    }

    public async Task<ServiceResult<SalesOrderDto>> GetSalesOrderById(int id)
    {
        try
        {
            var salesOrder = await _unitOfWork.Repository<SalesOrder>()
                .GetFirstOrDefaultAsync(
                    x => x.Id == id,
                    includeroperties: "Customer,PriceList,SalesOrderItems.Product,SalesOrderPayment,SalesOrderShipping,SalesOrderApprovals,SalesOrderDocuments");

            if (salesOrder == null)
            {
                return ServiceResult<SalesOrderDto>
                    .Error(new Problem(ErrorCodes.SalesOrderNotFound, "Sales order not found"));
            }

            var salesOrderDto = salesOrder.Adapt<SalesOrderDto>();

            return ServiceResult<SalesOrderDto>.Success(salesOrderDto);
        }
        catch (Exception ex)
        {
            return ServiceResult<SalesOrderDto>
                .Error(new Problem(ErrorCodes.SalesOrderRetrievalError, ex.Message));
        }
    }

    public async Task<ServiceResult<List<SalesOrderListItemDto>>> GetAllSalesOrders(int pageIndex, int pageSize)
    {
        try
        {
            var salesOrders = await _unitOfWork.Repository<SalesOrder>()
                .GetPaginatedAsync(
                    pageIndex,
                    pageSize,
                    includeroperties: "Customer");

            var salesOrderDtos = salesOrders.Select(so => new SalesOrderListItemDto
            {
                Id = so.Id,
                OrderNumber = so.OrderNumber,
                OrderDate = so.OrderDate,
                Status = so.Status,
                ExpectedDeliveryDate = so.ExpectedDeliveryDate,
                ReferenceNumber = so.ReferenceNumber,
                CustomerId = so.CustomerId,
                CustomerName = so.Customer?.CompanyName,
                SubTotal = so.SubTotal,
                TaxAmount = so.TaxAmount,
                TotalAmount = so.TotalAmount,
                CreatedOn = so.CreatedOn,
                CreatedBy = so.CreatedBy
            }).ToList();

            return ServiceResult<List<SalesOrderListItemDto>>.Success(salesOrderDtos);
        }
        catch (Exception ex)
        {
            return ServiceResult<List<SalesOrderListItemDto>>
                .Error(new Problem(ErrorCodes.SalesOrderRetrievalError, ex.Message));
        }
    }

    public async Task<ServiceResult<List<SalesOrderListItemDto>>> GetFilteredSalesOrders(int organisationId, SalesOrderFilterDto filter)
    {
        try
        {
            var specification = new SalesOrderSpecification(organisationId, filter);
            var salesOrders = await _unitOfWork.Repository<SalesOrder>().GetAsync(specification);

            var salesOrderDtos = salesOrders.Select(so => new SalesOrderListItemDto
            {
                Id = so.Id,
                OrderNumber = so.OrderNumber,
                OrderDate = so.OrderDate,
                Status = so.Status,
                ExpectedDeliveryDate = so.ExpectedDeliveryDate,
                ReferenceNumber = so.ReferenceNumber,
                CustomerId = so.CustomerId,
                CustomerName = so.Customer?.CompanyName,
                SubTotal = so.SubTotal,
                TaxAmount = so.TaxAmount,
                TotalAmount = so.TotalAmount,
                CreatedOn = so.CreatedOn,
                CreatedBy = so.CreatedBy
            }).ToList();

            return ServiceResult<List<SalesOrderListItemDto>>.Success(salesOrderDtos);
        }
        catch (Exception ex)
        {
            return ServiceResult<List<SalesOrderListItemDto>>
                .Error(new Problem(ErrorCodes.SalesOrderRetrievalError, ex.Message));
        }
    }

    public async Task<ServiceResult<bool>> UpdateSalesOrderStatus(int id, string status, string userId)
    {
        try
        {
            var salesOrder = await _unitOfWork.Repository<SalesOrder>()
                .GetFirstOrDefaultAsync(x => x.Id == id);

            if (salesOrder == null)
            {
                return ServiceResult<bool>
                    .Error(new Problem(ErrorCodes.SalesOrderNotFound, "Sales order not found"));
            }

            salesOrder.Status = status;
            salesOrder.ModifiedBy = userId;
            salesOrder.ModifiedOn = DateTime.UtcNow;

            // Update workflow tracking dates based on status
            switch (status.ToLower())
            {
                case "confirmed":
                    salesOrder.ConfirmedDate = DateTime.UtcNow;
                    break;
                case "packed":
                    salesOrder.PackedDate = DateTime.UtcNow;
                    break;
                case "shipped":
                    salesOrder.ShippedDate = DateTime.UtcNow;
                    break;
                case "delivered":
                    salesOrder.DeliveredDate = DateTime.UtcNow;
                    break;
            }

            _unitOfWork.Repository<SalesOrder>().Update(salesOrder);
            await _unitOfWork.SaveChangesAsync();

            return ServiceResult<bool>.Success(true);
        }
        catch (Exception ex)
        {
            return ServiceResult<bool>
                .Error(new Problem(ErrorCodes.SalesOrderUpdateError, ex.Message));
        }
    }

    public async Task<ServiceResult<bool>> DeleteSalesOrder(int id, string userId)
    {
        try
        {
            var salesOrder = await _unitOfWork.Repository<SalesOrder>()
                .GetFirstOrDefaultAsync(x => x.Id == id);

            if (salesOrder == null)
            {
                return ServiceResult<bool>
                    .Error(new Problem(ErrorCodes.SalesOrderNotFound, "Sales order not found"));
            }

            // Only allow deletion if status is Draft
            if (salesOrder.Status != "Draft")
            {
                return ServiceResult<bool>
                    .Error(new Problem(ErrorCodes.SalesOrderDeletionError, "Only draft sales orders can be deleted"));
            }

            _unitOfWork.Repository<SalesOrder>().Remove(salesOrder);
            await _unitOfWork.SaveChangesAsync();

            return ServiceResult<bool>.Success(true);
        }
        catch (Exception ex)
        {
            return ServiceResult<bool>
                .Error(new Problem(ErrorCodes.SalesOrderDeletionError, ex.Message));
        }
    }
}