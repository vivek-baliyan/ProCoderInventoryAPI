using Microsoft.AspNetCore.Mvc;
using PCI.Application.Services.Interfaces;
using PCI.Shared.Dtos.Category;

namespace PCI.WebAPI.Controllers;
public class CategoryController(
    ICategoryService categoryService,
    IHttpContextAccessor httpContextAccessor) : BaseController(httpContextAccessor)
{
    private readonly ICategoryService _categoryService = categoryService;

    [HttpPost("create")]
    public async Task<ActionResult<bool>> CreateCategory([FromBody] CreateCategoryDto createCategoryDto)
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

    [HttpPut("update")]
    public async Task<ActionResult<bool>> UpdateCategory([FromBody] UpdateCategoryDto updateCategoryDto)
    {
        var result = await _categoryService.UpdateCategory(
            UserId,
            OrganisationId,
            updateCategoryDto);

        if (!result.Succeeded)
        {
            return StatusCode(StatusCodes.Status400BadRequest, ErrorResponse(result));
        }

        return StatusCode(StatusCodes.Status200OK, SuccessResponse(result.ResultData, "Category updated successfully."));
    }

    [HttpGet("dropdown")]
    public async Task<ActionResult<CategoryDropdownResponseDto>> GetCategoriesForDropdown()
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

    [HttpGet("getById/{id}")]
    public async Task<ActionResult<CategoryDto>> GetCategoryById(int id)
    {
        var result = await _categoryService.GetCategoryById(id);

        if (!result.Succeeded)
        {
            return StatusCode(StatusCodes.Status400BadRequest, ErrorResponse(result));
        }

        return StatusCode(StatusCodes.Status200OK, SuccessResponse(result.ResultData, "Category retreived successfully."));
    }
}
