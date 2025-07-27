using Microsoft.AspNetCore.Mvc;
using PCI.Application.Services.Interfaces;
using PCI.Shared.Dtos.Vendor;

namespace PCI.WebAPI.Controllers;

public class VendorController(
    IVendorService vendorService,
    IHttpContextAccessor httpContextAccessor) : BaseController(httpContextAccessor)
{
    private readonly IVendorService _vendorService = vendorService;

    [HttpPost("create")]
    public async Task<ActionResult<bool>> CreateVendor([FromBody] CreateVendorDto createVendorDto)
    {
        var result = await _vendorService.CreateVendor(
            UserId,
            OrganisationId,
            createVendorDto);

        if (!result.Succeeded)
        {
            return StatusCode(StatusCodes.Status400BadRequest, ErrorResponse(result));
        }

        return StatusCode(StatusCodes.Status200OK, SuccessResponse(result.ResultData, "Vendor created successfully."));
    }

    [HttpPut("update")]
    public async Task<ActionResult<bool>> UpdateVendor([FromBody] UpdateVendorDto updateVendorDto)
    {
        var result = await _vendorService.UpdateVendor(
            UserId,
            OrganisationId,
            updateVendorDto);

        if (!result.Succeeded)
        {
            return StatusCode(StatusCodes.Status400BadRequest, ErrorResponse(result));
        }

        return StatusCode(StatusCodes.Status200OK, SuccessResponse(result.ResultData, "Vendor updated successfully."));
    }

    [HttpGet("getById/{id}")]
    public async Task<ActionResult<VendorDto>> GetVendorById(int id)
    {
        var result = await _vendorService.GetVendorById(id);

        if (!result.Succeeded)
        {
            return StatusCode(StatusCodes.Status400BadRequest, ErrorResponse(result));
        }

        return StatusCode(StatusCodes.Status200OK, SuccessResponse(result.ResultData, "Vendor retrieved successfully."));
    }

    [HttpGet("all/{pageIndex}/{pageSize}")]
    public async Task<ActionResult<VendorListItemDto>> GetAllVendors(int pageIndex, int pageSize)
    {
        var result = await _vendorService.GetAllVendors(OrganisationId, pageIndex, pageSize);

        if (!result.Succeeded)
        {
            return StatusCode(StatusCodes.Status400BadRequest, ErrorResponse(result));
        }

        return StatusCode(StatusCodes.Status200OK, SuccessResponse(result.ResultData, "Vendors retrieved successfully."));
    }

    [HttpPost("filter")]
    public async Task<ActionResult<VendorListItemDto>> GetFilteredVendors([FromBody] VendorFilterDto filter)
    {
        var result = await _vendorService.GetFilteredVendors(OrganisationId, filter);

        if (!result.Succeeded)
        {
            return StatusCode(StatusCodes.Status400BadRequest, ErrorResponse(result));
        }

        return StatusCode(StatusCodes.Status200OK, SuccessResponse(result.ResultData, "Filtered vendors retrieved successfully."));
    }

    [HttpDelete("delete/{id}")]
    public async Task<ActionResult<bool>> DeleteVendor(int id)
    {
        var result = await _vendorService.DeleteVendor(id, UserId);

        if (!result.Succeeded)
        {
            return StatusCode(StatusCodes.Status400BadRequest, ErrorResponse(result));
        }

        return StatusCode(StatusCodes.Status200OK, SuccessResponse(result.ResultData, "Vendor deleted successfully."));
    }

    [HttpPatch("toggle-status/{id}")]
    public async Task<ActionResult<bool>> ToggleVendorStatus(int id)
    {
        var result = await _vendorService.ToggleVendorStatus(id, UserId);

        if (!result.Succeeded)
        {
            return StatusCode(StatusCodes.Status400BadRequest, ErrorResponse(result));
        }

        return StatusCode(StatusCodes.Status200OK, SuccessResponse(result.ResultData, "Vendor status updated successfully."));
    }

    [HttpPatch("update-performance/{id}")]
    public async Task<ActionResult<bool>> UpdateVendorPerformance(
        int id,
        [FromBody] UpdateVendorPerformanceDto performanceDto)
    {
        var result = await _vendorService.UpdateVendorPerformance(
            id,
            performanceDto.PerformanceRating,
            performanceDto.OnTimeDeliveryPercentage,
            performanceDto.QualityRating,
            UserId);

        if (!result.Succeeded)
        {
            return StatusCode(StatusCodes.Status400BadRequest, ErrorResponse(result));
        }

        return StatusCode(StatusCodes.Status200OK, SuccessResponse(result.ResultData, "Vendor performance updated successfully."));
    }

    [HttpPatch("blacklist/{id}")]
    public async Task<ActionResult<bool>> BlacklistVendor(int id, [FromBody] BlacklistVendorDto blacklistDto)
    {
        var result = await _vendorService.BlacklistVendor(id, blacklistDto.Reason, UserId);

        if (!result.Succeeded)
        {
            return StatusCode(StatusCodes.Status400BadRequest, ErrorResponse(result));
        }

        return StatusCode(StatusCodes.Status200OK, SuccessResponse(result.ResultData, "Vendor blacklisted successfully."));
    }

    [HttpPatch("remove-blacklist/{id}")]
    public async Task<ActionResult<bool>> RemoveVendorFromBlacklist(int id)
    {
        var result = await _vendorService.RemoveVendorFromBlacklist(id, UserId);

        if (!result.Succeeded)
        {
            return StatusCode(StatusCodes.Status400BadRequest, ErrorResponse(result));
        }

        return StatusCode(StatusCodes.Status200OK, SuccessResponse(result.ResultData, "Vendor removed from blacklist successfully."));
    }

    [HttpPatch("mark-preferred/{id}")]
    public async Task<ActionResult<bool>> MarkAsPreferredVendor(int id)
    {
        var result = await _vendorService.MarkAsPreferredVendor(id, UserId);

        if (!result.Succeeded)
        {
            return StatusCode(StatusCodes.Status400BadRequest, ErrorResponse(result));
        }

        return StatusCode(StatusCodes.Status200OK, SuccessResponse(result.ResultData, "Vendor marked as preferred successfully."));
    }

    [HttpPatch("remove-preferred/{id}")]
    public async Task<ActionResult<bool>> RemovePreferredVendorStatus(int id)
    {
        var result = await _vendorService.RemovePreferredVendorStatus(id, UserId);

        if (!result.Succeeded)
        {
            return StatusCode(StatusCodes.Status400BadRequest, ErrorResponse(result));
        }

        return StatusCode(StatusCodes.Status200OK, SuccessResponse(result.ResultData, "Preferred vendor status removed successfully."));
    }
}