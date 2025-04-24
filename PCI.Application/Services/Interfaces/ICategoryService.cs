using PCI.Shared.Common;
using PCI.Shared.Dtos;

namespace PCI.Application.Services.Interfaces;

public interface ICategoryService
{
    Task<ServiceResult<CategoryDto>> CreateCategory(string userId, CreateCategoryDto createCategoryDto);
}
