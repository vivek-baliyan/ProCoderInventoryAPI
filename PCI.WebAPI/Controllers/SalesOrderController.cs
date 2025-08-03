using Microsoft.AspNetCore.Mvc;
using PCI.Application.Services.Interfaces;
using PCI.Shared.Dtos.SalesOrder;

namespace PCI.WebAPI.Controllers;

public class SalesOrderController(
    ISalesOrderService salesOrderService,
    ICodeGenerationService codeGenerationService,
    IHttpContextAccessor httpContextAccessor) : BaseController(httpContextAccessor)
{
    private readonly ISalesOrderService _salesOrderService = salesOrderService;
    private readonly ICodeGenerationService _codeGenerationService = codeGenerationService;

    [HttpPost("create")]
    public async Task<ActionResult<bool>> CreateSalesOrder([FromBody] CreateSalesOrderDto createSalesOrderDto)
    {
        var result = await _salesOrderService.CreateSalesOrder(
            UserId,
            OrganisationId,
            createSalesOrderDto);

        if (!result.Succeeded)
        {
            return StatusCode(StatusCodes.Status400BadRequest, ErrorResponse(result));
        }

        return StatusCode(StatusCodes.Status200OK, SuccessResponse(result.ResultData, "Sales order created successfully."));
    }

    [HttpPut("update")]
    public async Task<ActionResult<bool>> UpdateSalesOrder([FromBody] UpdateSalesOrderDto updateSalesOrderDto)
    {
        var result = await _salesOrderService.UpdateSalesOrder(
            UserId,
            OrganisationId,
            updateSalesOrderDto);

        if (!result.Succeeded)
        {
            return StatusCode(StatusCodes.Status400BadRequest, ErrorResponse(result));
        }

        return StatusCode(StatusCodes.Status200OK, SuccessResponse(result.ResultData, "Sales order updated successfully."));
    }

    [HttpGet("getById/{id}")]
    public async Task<ActionResult<SalesOrderDto>> GetSalesOrderById(int id)
    {
        var result = await _salesOrderService.GetSalesOrderById(id);

        if (!result.Succeeded)
        {
            return StatusCode(StatusCodes.Status400BadRequest, ErrorResponse(result));
        }

        return StatusCode(StatusCodes.Status200OK, SuccessResponse(result.ResultData, "Sales order retrieved successfully."));
    }

    [HttpGet("all/{pageIndex}/{pageSize}")]
    public async Task<ActionResult<List<SalesOrderListItemDto>>> GetAllSalesOrders(int pageIndex, int pageSize)
    {
        var result = await _salesOrderService.GetAllSalesOrders(pageIndex, pageSize);

        if (!result.Succeeded)
        {
            return StatusCode(StatusCodes.Status400BadRequest, ErrorResponse(result));
        }

        return StatusCode(StatusCodes.Status200OK, SuccessResponse(result.ResultData, "Sales orders retrieved successfully."));
    }

    [HttpPost("filter")]
    public async Task<ActionResult<List<SalesOrderListItemDto>>> GetFilteredSalesOrders([FromBody] SalesOrderFilterDto filter)
    {
        var result = await _salesOrderService.GetFilteredSalesOrders(OrganisationId, filter);

        if (!result.Succeeded)
        {
            return StatusCode(StatusCodes.Status400BadRequest, ErrorResponse(result));
        }

        return StatusCode(StatusCodes.Status200OK, SuccessResponse(result.ResultData, "Filtered sales orders retrieved successfully."));
    }

    [HttpPut("updateStatus/{id}")]
    public async Task<ActionResult<bool>> UpdateSalesOrderStatus(int id, [FromBody] UpdateStatusDto updateStatusDto)
    {
        var result = await _salesOrderService.UpdateSalesOrderStatus(id, updateStatusDto.Status, UserId);

        if (!result.Succeeded)
        {
            return StatusCode(StatusCodes.Status400BadRequest, ErrorResponse(result));
        }

        return StatusCode(StatusCodes.Status200OK, SuccessResponse(result.ResultData, "Sales order status updated successfully."));
    }

    [HttpDelete("delete/{id}")]
    public async Task<ActionResult<bool>> DeleteSalesOrder(int id)
    {
        var result = await _salesOrderService.DeleteSalesOrder(id, UserId);

        if (!result.Succeeded)
        {
            return StatusCode(StatusCodes.Status400BadRequest, ErrorResponse(result));
        }

        return StatusCode(StatusCodes.Status200OK, SuccessResponse(result.ResultData, "Sales order deleted successfully."));
    }

    [HttpGet("generateOrderNumber")]
    public async Task<ActionResult<string>> GenerateSalesOrderNumber()
    {
        try
        {
            var orderNumber = await _codeGenerationService.GenerateSalesOrderNumberAsync(OrganisationId);
            return StatusCode(StatusCodes.Status200OK, SuccessResponse(orderNumber, "Sales order number generated successfully."));
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ErrorResponse("Failed to generate sales order number.", ex.Message));
        }
    }
}

public class UpdateStatusDto
{
    public string Status { get; set; }
}