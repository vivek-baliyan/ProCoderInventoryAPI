﻿using Mapster;
using PCI.Application.Repositories;
using PCI.Application.Services.Interfaces;
using PCI.Domain.Models;
using PCI.Shared.Common;
using PCI.Shared.Common.Constants;
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
                SKU = createProductDto.SKU,
                Name = createProductDto.Name,
                Description = createProductDto.Description,
                ProductType = createProductDto.ProductType,
                Status = createProductDto.Status,
                IsActive = createProductDto.IsActive,
                IsTaxable = createProductDto.IsTaxable,
                TrackInventory = createProductDto.TrackInventory,
                SerialNumberTracking = createProductDto.SerialNumberTracking,
                BatchTracking = createProductDto.BatchTracking,
                ItemGroupId = createProductDto.ItemGroupId,
                BrandId = createProductDto.BrandId,
                ManufacturerPartNumber = createProductDto.ManufacturerPartNumber,
                UPC = createProductDto.UPC,
                EAN = createProductDto.EAN,
                ISBN = createProductDto.ISBN,
                SalesAccountId = createProductDto.SalesAccountId,
                PurchaseAccountId = createProductDto.PurchaseAccountId,
                InventoryAccountId = createProductDto.InventoryAccountId,
                SellingPrice = createProductDto.SellingPrice,
                CostPrice = createProductDto.CostPrice,
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

        existingProduct.SKU = updateProductDto.SKU;
        existingProduct.Name = updateProductDto.Name;
        existingProduct.Description = updateProductDto.Description;
        existingProduct.ProductType = updateProductDto.ProductType;
        existingProduct.Status = updateProductDto.Status;
        existingProduct.IsActive = updateProductDto.IsActive;
        existingProduct.IsTaxable = updateProductDto.IsTaxable;
        existingProduct.TrackInventory = updateProductDto.TrackInventory;
        existingProduct.SerialNumberTracking = updateProductDto.SerialNumberTracking;
        existingProduct.BatchTracking = updateProductDto.BatchTracking;
        existingProduct.ItemGroupId = updateProductDto.ItemGroupId;
        existingProduct.BrandId = updateProductDto.BrandId;
        existingProduct.ManufacturerPartNumber = updateProductDto.ManufacturerPartNumber;
        existingProduct.UPC = updateProductDto.UPC;
        existingProduct.EAN = updateProductDto.EAN;
        existingProduct.ISBN = updateProductDto.ISBN;
        existingProduct.SalesAccountId = updateProductDto.SalesAccountId;
        existingProduct.PurchaseAccountId = updateProductDto.PurchaseAccountId;
        existingProduct.InventoryAccountId = updateProductDto.InventoryAccountId;
        existingProduct.SellingPrice = updateProductDto.SellingPrice;
        existingProduct.CostPrice = updateProductDto.CostPrice;
        existingProduct.ModifiedBy = userId;
        existingProduct.ModifiedOn = DateTime.UtcNow;

        try
        {
            _unitOfWork.Repository<Product>().Update(existingProduct);
            await _unitOfWork.SaveChangesAsync();

            return ServiceResult<bool>.Success(true);
        }
        catch (Exception ex)
        {
            return ServiceResult<bool>
                .Error(new Problem(ErrorCodes.ProductUpdateError, ex.Message));
        }
    }

    public async Task<ServiceResult<List<ProductListItemDto>>> GetAllProducts(int pageIndex, int pageSize)
    {
        try
        {
            var products = await _unitOfWork.Repository<Product>()
                .GetPaginatedAsync(
                    pageIndex,
                    pageSize,
                    includeroperties: "Brand,ItemGroup,ProductImages");

            var productDtos = products.Select(p => new ProductListItemDto
            {
                Id = p.Id,
                SKU = p.SKU,
                Name = p.Name,
                Description = p.Description,
                ProductType = p.ProductType,
                Status = p.Status,
                IsActive = p.IsActive,
                SellingPrice = p.SellingPrice,
                CostPrice = p.CostPrice,
                BrandName = p.Brand?.Name,
                ItemGroupName = p.ItemGroup?.GroupName,
                Image = p.ProductImages?.FirstOrDefault()?.Adapt<ProductImageDto>()
            }).ToList();

            return ServiceResult<List<ProductListItemDto>>.Success(productDtos);
        }
        catch (Exception ex)
        {
            return ServiceResult<List<ProductListItemDto>>
                .Error(new Problem(ErrorCodes.ProductRetrievalError, ex.Message));
        }
    }

    public async Task<ServiceResult<ProductDto>> GetProductById(int id)
    {
        try
        {
            var product = await _unitOfWork.Repository<Product>()
                .GetFirstOrDefaultAsync(
                    x => x.Id == id,
                    includeroperties: "Brand,ItemGroup,ProductImages,ProductCategories,ProductTax");

            if (product == null)
            {
                return ServiceResult<ProductDto>
                    .Error(new Problem(ErrorCodes.ProductNotFound, "Product not found"));
            }

            var productDto = product.Adapt<ProductDto>();

            return ServiceResult<ProductDto>.Success(productDto);
        }
        catch (Exception ex)
        {
            return ServiceResult<ProductDto>
                .Error(new Problem(ErrorCodes.ProductRetrievalError, ex.Message));
        }
    }
}
