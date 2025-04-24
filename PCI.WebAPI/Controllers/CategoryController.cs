using Microsoft.AspNetCore.Mvc;
using PCI.Application.Services.Interfaces;
using PCI.Shared.Dtos;

namespace PCI.WebAPI.Controllers;

public class CategoryController(ICategoryService categoryService) : BaseController
{
    private readonly ICategoryService _categoryService = categoryService;

    [HttpPost]
    public async Task<ActionResult<CategoryDto>> CreateCategory([FromBody] CreateCategoryDto createCategoryDto)
    {
        try
        {
            var result = await _categoryService.CreateCategory(UserId, createCategoryDto);

            if (!result.Succeeded)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ErrorResponse(result));
            }

            return StatusCode(StatusCodes.Status200OK, SuccessResponse(result.ResultData, "Category created successfully."));
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                ErrorResponse(ex.ToString(), $"An error occurred while creating category: {ex.Message}"));
        }
    }
}
