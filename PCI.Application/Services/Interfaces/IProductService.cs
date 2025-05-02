using PCI.Shared.Common;
using PCI.Shared.Dtos.Product;

namespace PCI.Application.Services.Interfaces;

public interface IProductService
{
    Task<ServiceResult<bool>> CreateProduct(string userId, int OrganisationId, CreateProductDto createProductDto);
    Task<ServiceResult<bool>> UpdateProduct(string userId, int OrganisationId, UpdateProductDto updateProductDto);
    Task<ServiceResult<ProductDto>> GetProductById(int id);
}