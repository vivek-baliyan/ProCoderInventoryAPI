using Microsoft.AspNetCore.Mvc;
using PCI.Application.Services.Interfaces;
using PCI.Shared.Dtos.Customer;

namespace PCI.WebAPI.Controllers;

public class CustomerController(
    ICustomerService customerService,
    IHttpContextAccessor httpContextAccessor) : BaseController(httpContextAccessor)
{
    private readonly ICustomerService _customerService = customerService;

    [HttpPost("create")]
    public async Task<ActionResult<bool>> CreateCustomer([FromBody] CreateCustomerDto createCustomerDto)
    {
        var result = await _customerService.CreateCustomer(
            UserId,
            OrganisationId,
            createCustomerDto);

        if (!result.Succeeded)
        {
            return StatusCode(StatusCodes.Status400BadRequest, ErrorResponse(result));
        }

        return StatusCode(StatusCodes.Status200OK, SuccessResponse(result.ResultData, "Customer created successfully."));
    }

    [HttpPut("update")]
    public async Task<ActionResult<bool>> UpdateCustomer([FromBody] UpdateCustomerDto updateCustomerDto)
    {
        var result = await _customerService.UpdateCustomer(
            UserId,
            OrganisationId,
            updateCustomerDto);

        if (!result.Succeeded)
        {
            return StatusCode(StatusCodes.Status400BadRequest, ErrorResponse(result));
        }

        return StatusCode(StatusCodes.Status200OK, SuccessResponse(result.ResultData, "Customer updated successfully."));
    }

    [HttpGet("getById/{id}")]
    public async Task<ActionResult<CustomerDto>> GetCustomerById(int id)
    {
        var result = await _customerService.GetCustomerById(id);

        if (!result.Succeeded)
        {
            return StatusCode(StatusCodes.Status400BadRequest, ErrorResponse(result));
        }

        return StatusCode(StatusCodes.Status200OK, SuccessResponse(result.ResultData, "Customer retrieved successfully."));
    }

    [HttpGet("all/{pageIndex}/{pageSize}")]
    public async Task<ActionResult<CustomerListItemDto>> GetAllCustomers(int pageIndex, int pageSize)
    {
        var result = await _customerService.GetAllCustomers(OrganisationId, pageIndex, pageSize);

        if (!result.Succeeded)
        {
            return StatusCode(StatusCodes.Status400BadRequest, ErrorResponse(result));
        }

        return StatusCode(StatusCodes.Status200OK, SuccessResponse(result.ResultData, "Customers retrieved successfully."));
    }

    [HttpPost("filter")]
    public async Task<ActionResult<CustomerListItemDto>> GetFilteredCustomers([FromBody] CustomerFilterDto filter)
    {
        var result = await _customerService.GetFilteredCustomers(OrganisationId, filter);

        if (!result.Succeeded)
        {
            return StatusCode(StatusCodes.Status400BadRequest, ErrorResponse(result));
        }

        return StatusCode(StatusCodes.Status200OK, SuccessResponse(result.ResultData, "Filtered customers retrieved successfully."));
    }

    [HttpDelete("delete/{id}")]
    public async Task<ActionResult<bool>> DeleteCustomer(int id)
    {
        var result = await _customerService.DeleteCustomer(id, UserId);

        if (!result.Succeeded)
        {
            return StatusCode(StatusCodes.Status400BadRequest, ErrorResponse(result));
        }

        return StatusCode(StatusCodes.Status200OK, SuccessResponse(result.ResultData, "Customer deleted successfully."));
    }

    [HttpPatch("toggle-status/{id}")]
    public async Task<ActionResult<bool>> ToggleCustomerStatus(int id)
    {
        var result = await _customerService.ToggleCustomerStatus(id, UserId);

        if (!result.Succeeded)
        {
            return StatusCode(StatusCodes.Status400BadRequest, ErrorResponse(result));
        }

        return StatusCode(StatusCodes.Status200OK, SuccessResponse(result.ResultData, "Customer status updated successfully."));
    }
}