using Microsoft.AspNetCore.Mvc;
using PCI.Application.Services.Interfaces;
using PCI.Shared.Dtos;

namespace PCI.WebAPI.Controllers;
public class CategoryController(
    ICategoryService categoryService,
    IHttpContextAccessor httpContextAccessor) : BaseController(httpContextAccessor)
{
    private readonly ICategoryService _categoryService = categoryService;

    [HttpPost("create")]
    public async Task<ActionResult<CategoryDto>> CreateCategory([FromBody] CreateCategoryDto createCategoryDto)
    {
        var result = await _categoryService.CreateCategory(
            UserId,
            OrganisationId,
            createCategoryDto);

        if (!result.Succeeded)
        {
            return StatusCode(StatusCodes.Status400BadRequest, ErrorResponse(result));
        }

        return StatusCode(StatusCodes.Status200OK, SuccessResponse(result.ResultData, "Category created successfully."));
    }

    [HttpGet("dropdown")]
    public async Task<ActionResult<CategoryDropdownDto>> GetCategoriesForDropdown()
    {

        var result = await _categoryService.GetCategoriesForDropdown();

        if (!result.Succeeded)
        {
            return StatusCode(StatusCodes.Status400BadRequest, ErrorResponse(result));
        }

        return StatusCode(StatusCodes.Status200OK, SuccessResponse(result.ResultData, "Categories retreived successfully."));
    }

    [HttpGet("all/{pageIndex}/{pageSize}")]
    public async Task<ActionResult<CategoryListItemDto>> GetAllCategories(int pageIndex, int pageSize)
    {
        var result = await _categoryService.GetAllCategories(pageIndex, pageSize);

        if (!result.Succeeded)
        {
            return StatusCode(StatusCodes.Status400BadRequest, ErrorResponse(result));
        }

        return StatusCode(StatusCodes.Status200OK, SuccessResponse(result.ResultData, "Categories retreived successfully."));
    }
}
