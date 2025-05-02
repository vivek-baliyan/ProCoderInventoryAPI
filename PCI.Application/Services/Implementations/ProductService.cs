using PCI.Application.Repositories;
using PCI.Application.Services.Interfaces;
using PCI.Shared.Common;

namespace PCI.Application.Services.Implementations;

public class ProductService(IUnitOfWork unitOfWork, IImageService imageService) : IProductService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IImageService _imageService = imageService;

    public async Task<ServiceResult<bool>> CreateProduct(CreateProductDto createProductDto)
    {

    }

}
