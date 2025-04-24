using Mapster;
using PCI.Application.Repositories;
using PCI.Application.Services.Interfaces;
using PCI.Domain.Models;
using PCI.Shared.Common;
using PCI.Shared.Dtos;

namespace PCI.Application.Services.Implementations;

public class CategoryService(IUnitOfWork unitOfWork) : ICategoryService
{
    private readonly IUnitOfWork unitOfWork = unitOfWork;

    public async Task<ServiceResult<CategoryDto>> CreateCategory(string userId, CreateCategoryDto createCategoryDto)
    {
        var existingCategory = await unitOfWork.Repository<Category>()
            .GetFirstOrDefaultAsync(x => x.Name == createCategoryDto.Name && x.UserId == userId);

        // Check if the category already exists
        if (existingCategory != null)
        {
            return ServiceResult<CategoryDto>
                .Error(new Problem(ErrorCodes.CategoryAlreadyExists, Messages.CategoryAlreadyExists));
        }
        try
        {
            var category = new Category
            {
                UserId = userId,
                Name = createCategoryDto.Name,
                PageTitle = createCategoryDto.PageTitle,
                UrlIdentifier = createCategoryDto.UrlIdentifier,
                Description = createCategoryDto.Description,
                ParentCategoryId = createCategoryDto.ParentCategoryId,
                ImagePath = createCategoryDto.ImagePath,
                Status = (VisibilityStatus)createCategoryDto.Status,
                PublishDate = DateTime.UtcNow,
                CreatedBy = userId,
                CreatedOn = DateTime.UtcNow,
            };

            unitOfWork.Repository<Category>().Add(category);
            await unitOfWork.SaveChangesAsync();

            return ServiceResult<CategoryDto>
                .Success(category.Adapt<CategoryDto>());
        }
        catch (Exception ex)
        {
            return ServiceResult<CategoryDto>
                .Error(new Problem(ErrorCodes.CategoryCreationError, ex.ToString()));
        }
    }
}
