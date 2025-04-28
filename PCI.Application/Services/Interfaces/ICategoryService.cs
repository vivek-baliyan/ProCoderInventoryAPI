using PCI.Shared.Common;
using PCI.Shared.Dtos;

namespace PCI.Application.Services.Interfaces;

public interface ICategoryService
{
    Task<ServiceResult<CategoryDto>> CreateCategory(string userId, int organisationId, CreateCategoryDto createCategoryDto);
    Task<ServiceResult<List<CategoryDropdownDto>>> GetCategoriesForDropdown();
    Task<ServiceResult<List<CategoryListItemDto>>> GetAllCategories(int pageIndex, int pageSize);
}
