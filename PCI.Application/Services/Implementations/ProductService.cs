using Mapster;
using PCI.Application.Repositories;
using PCI.Application.Services.Interfaces;
using PCI.Domain.Models;
using PCI.Shared.Common;
using PCI.Shared.Common.Constants;
using PCI.Shared.Common.Enums;
using PCI.Shared.Dtos.Product;

namespace PCI.Application.Services.Implementations;

public class ProductService(IUnitOfWork unitOfWork, IImageService imageService) : IProductService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IImageService _imageService = imageService;

    public async Task<ServiceResult<bool>> CreateProduct(
        string userId,
        int OrganisationId,
        CreateProductDto createProductDto)
    {
        var existingProduct = await _unitOfWork.Repository<Product>()
            .GetFirstOrDefaultAsync(x => x.Name == createProductDto.Name);

        // Check if the product already exists
        if (existingProduct != null)
        {
            return ServiceResult<bool>
                .Error(new Problem(ErrorCodes.ProductAlreadyExists, "Product already exists"));
        }

        try
        {
            var product = new Product
            {
                Name = createProductDto.Name,
                PageTitle = createProductDto.PageTitle,
                UrlIdentifier = createProductDto.UrlIdentifier,
                Description = createProductDto.Description,
                OldPrice = createProductDto.OldPrice,
                Price = createProductDto.Price,
                Coupon = createProductDto.Coupon,
                Status = (VisibilityStatus)createProductDto.Status,
                PublishDate = createProductDto.PublishDate,
                SKU = createProductDto.SKU,
                StockQuantity = createProductDto.StockQuantity,
                OrganisationId = OrganisationId,
                CreatedBy = userId,
                CreatedOn = DateTime.UtcNow,
            };

            _unitOfWork.Repository<Product>().Add(product);
            await _unitOfWork.SaveChangesAsync();

            return ServiceResult<bool>.Success(true);
        }
        catch (Exception ex)
        {
            return ServiceResult<bool>
                .Error(new Problem(ErrorCodes.ProductCreationError, ex.Message));
        }
    }

    public async Task<ServiceResult<bool>> UpdateProduct(
        string userId,
        int OrganisationId,
        UpdateProductDto updateProductDto)
    {
        var existingProduct = await _unitOfWork.Repository<Product>()
            .GetFirstOrDefaultAsync(x => x.Id == updateProductDto.Id);

        // Check if the product already exists
        if (existingProduct == null)
        {
            return ServiceResult<bool>
                .Error(new Problem(ErrorCodes.ProductNotFound, "Product not found"));
        }

        existingProduct.Name = updateProductDto.Name;
        existingProduct.PageTitle = updateProductDto.PageTitle;
        existingProduct.UrlIdentifier = updateProductDto.UrlIdentifier;
        existingProduct.Description = updateProductDto.Description;
        existingProduct.OldPrice = updateProductDto.OldPrice;
        existingProduct.Price = updateProductDto.Price;
        existingProduct.Coupon = updateProductDto.Coupon;
        existingProduct.Status = (VisibilityStatus)updateProductDto.Status;
        existingProduct.PublishDate = updateProductDto.PublishDate;
        existingProduct.SKU = updateProductDto.SKU;
        existingProduct.StockQuantity = updateProductDto.StockQuantity;

        _unitOfWork.Repository<Product>().Update(existingProduct);
        await _unitOfWork.SaveChangesAsync();

        return ServiceResult<bool>.Success(true);
    }

    public async Task<ServiceResult<List<ProductListItemDto>>> GetAllProducts(int pageIndex, int pageSize)
    {
        var products = await _unitOfWork.Repository<Product>()
            .GetPaginatedAsync(pageIndex, pageSize);

        return ServiceResult<List<ProductListItemDto>>
            .Success(products.Adapt<List<ProductListItemDto>>());
    }

    public async Task<ServiceResult<ProductDto>> GetProductById(int id)
    {
        var product = await _unitOfWork.Repository<Product>()
            .GetFirstOrDefaultAsync(x => x.Id == id);

        if (product == null)
        {
            return ServiceResult<ProductDto>
                .Error(new Problem(ErrorCodes.ProductNotFound, "Product not found"));
        }

        var productDto = product.Adapt<ProductDto>();


        return ServiceResult<ProductDto>.Success(productDto);

    }
}
