using Microsoft.AspNetCore.Mvc;
using PCI.Application.Services.Interfaces;
using PCI.Shared.Dtos.Product;

namespace PCI.WebAPI.Controllers;
public class ProductController(
    IProductService productService,
    IHttpContextAccessor httpContextAccessor) : BaseController(httpContextAccessor)
{
    private readonly IProductService _productService = productService;

    [HttpPost("create")]
    public async Task<ActionResult<bool>> CreateProduct([FromBody] CreateProductDto createProductDto)
    {
        var result = await _productService.CreateProduct(
            UserId,
            OrganisationId,
            createProductDto);

        if (!result.Succeeded)
        {
            return StatusCode(StatusCodes.Status400BadRequest, ErrorResponse(result));
        }

        return StatusCode(StatusCodes.Status200OK, SuccessResponse(result.ResultData, "Product created successfully."));
    }

    [HttpPut("update")]
    public async Task<ActionResult<ProductDto>> UpdateProduct([FromBody] UpdateProductDto updateProductDto)
    {
        var result = await _productService.UpdateProduct(
            UserId,
            OrganisationId,
            updateProductDto);

        if (!result.Succeeded)
        {
            return StatusCode(StatusCodes.Status400BadRequest, ErrorResponse(result));
        }

        return StatusCode(StatusCodes.Status200OK, SuccessResponse(result.ResultData, "Product updated successfully."));
    }

    [HttpGet("getById/{id}")]
    public async Task<ActionResult<ProductDto>> GetProductById(int id)
    {
        var result = await _productService.GetProductById(id);

        if (!result.Succeeded)
        {
            return StatusCode(StatusCodes.Status400BadRequest, ErrorResponse(result));
        }

        return StatusCode(StatusCodes.Status200OK, SuccessResponse(result.ResultData, "Product retreived successfully."));
    }
}
