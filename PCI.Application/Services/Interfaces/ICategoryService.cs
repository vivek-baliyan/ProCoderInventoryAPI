﻿using PCI.Shared.Common;
using PCI.Shared.Dtos.Category;

namespace PCI.Application.Services.Interfaces;

public interface ICategoryService
{
    Task<ServiceResult<bool>> CreateCategory(string userId, int organisationId, CreateCategoryDto createCategoryDto);
    Task<ServiceResult<bool>> UpdateCategory(string userId, int organisationId, UpdateCategoryDto updateCategoryDto);
    Task<ServiceResult<List<CategoryDropdownResponseDto>>> GetCategoriesForDropdown();
    Task<ServiceResult<List<CategoryListItemDto>>> GetAllCategories(int pageIndex, int pageSize);
    Task<ServiceResult<CategoryDto>> GetCategoryById(int id);
}
