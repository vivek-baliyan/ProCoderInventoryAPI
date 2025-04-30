using Microsoft.AspNetCore.Mvc;
using PCI.Application.Services.Interfaces;
using PCI.Shared.Common.Constants;
using PCI.Shared.Dtos;

namespace PCI.WebAPI.Controllers;
public class OrganisationController(
    IOrganisationService organisationService,
    IHttpContextAccessor httpContextAccessor) : BaseController(httpContextAccessor)
{
    private readonly IOrganisationService _organisationService = organisationService;

    [HttpPost("create")]
    public async Task<ActionResult<OrganisationDto>> CreateOrganisation(OrganisationDto updateProfileSettingsDto)
    {
        var result = await _organisationService.CreateOrganisation(UserId, updateProfileSettingsDto);

        if (!result.Succeeded)
        {
            return StatusCode(StatusCodes.Status400BadRequest, ErrorResponse(result));
        }

        return StatusCode(StatusCodes.Status200OK,
            SuccessResponse(result.ResultData, Messages.OrganisationUpdated));
    }

    [HttpPut("update")]
    public async Task<ActionResult<OrganisationDto>> UpdateOrganisation(OrganisationDto updateProfileSettingsDto)
    {
        var result = await _organisationService.UpdateOrganisation(UserId, updateProfileSettingsDto);

        if (!result.Succeeded)
        {
            return StatusCode(StatusCodes.Status400BadRequest, ErrorResponse(result));
        }

        return StatusCode(StatusCodes.Status200OK,
            SuccessResponse(result.ResultData, Messages.OrganisationUpdated));
    }

    [HttpGet("getByUserId/{userId}")]
    public async Task<ActionResult<OrganisationDto>> GetUserOrganisation(string userId)
    {
        //if (userId != UserId)
        //{
        //    return StatusCode(StatusCodes.Status403Forbidden,
        //        ErrorResponse("You are not authorized to access this resource", "Forbidden"));
        //}

        var result = await _organisationService.GetOrganisationByUserId(userId);

        if (!result.Succeeded)
        {
            return StatusCode(StatusCodes.Status400BadRequest, ErrorResponse(result));
        }

        return StatusCode(StatusCodes.Status200OK,
            SuccessResponse(result.ResultData, "User organisation retrieved successfully"));
    }
}