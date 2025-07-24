using Mapster;
using PCI.Application.Repositories;
using PCI.Application.Services.Interfaces;
using PCI.Domain.Models;
using PCI.Shared.Common;
using PCI.Shared.Common.Constants;
using PCI.Shared.Common.Enums;
using PCI.Shared.Dtos.Category;

namespace PCI.Application.Services.Implementations;

public class CategoryService(IUnitOfWork unitOfWork, IImageService imageService) : ICategoryService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IImageService _imageService = imageService;

    public async Task<ServiceResult<bool>> CreateCategory(
        string userId,
        int organisationId,
        CreateCategoryDto createCategoryDto)
    {
        var existingCategory = await _unitOfWork.Repository<Category>()
            .GetFirstOrDefaultAsync(x => x.Name == createCategoryDto.Name
            && x.OrganisationId == organisationId);

        // Check if the category already exists
        if (existingCategory != null)
        {
            return ServiceResult<bool>
                .Error(new Problem(ErrorCodes.CategoryAlreadyExists, Messages.CategoryAlreadyExists));
        }
        try
        {
            var category = new Category
            {
                OrganisationId = organisationId,
                Name = createCategoryDto.Name,
                PageTitle = createCategoryDto.PageTitle,
                UrlIdentifier = createCategoryDto.UrlIdentifier,
                Description = createCategoryDto.Description,
                ParentCategoryId = createCategoryDto.ParentCategoryId == 0 ? null : createCategoryDto.ParentCategoryId,
                Status = (VisibilityStatus)createCategoryDto.Status,
                PublishDate = createCategoryDto.PublishDate,
                CreatedBy = userId,
                CreatedOn = DateTime.UtcNow,
            };

            _unitOfWork.Repository<Category>().Add(category);
            await _unitOfWork.SaveChangesAsync();

            try
            {
                if (!string.IsNullOrEmpty(createCategoryDto.Image))
                {
                    category.CategoryImages = await UploadCategoryImage(userId, createCategoryDto.Image);

                    _unitOfWork.Repository<Category>().Update(category);
                    await _unitOfWork.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                return ServiceResult<bool>
                    .Error(new Problem(ErrorCodes.CategoryImageUploadError, ex.Message));
            }

            return ServiceResult<bool>.Success(true);
        }
        catch (Exception ex)
        {
            return ServiceResult<bool>
                .Error(new Problem(ErrorCodes.CategoryCreationError, ex.Message));
        }
    }

    public async Task<ServiceResult<bool>> UpdateCategory(
        string userId,
        int organisationId,
        UpdateCategoryDto updateCategoryDto)
    {
        var category = await _unitOfWork.Repository<Category>()
            .GetFirstOrDefaultAsync(x => x.Id == updateCategoryDto.CategoryId && x.OrganisationId == organisationId);

        if (category == null)
        {
            return ServiceResult<bool>
                .Error(new Problem(ErrorCodes.CategoryNotFound, Messages.CategoryNotFound));
        }

        category.Name = updateCategoryDto.Name;
        category.PageTitle = updateCategoryDto.PageTitle;
        category.UrlIdentifier = updateCategoryDto.UrlIdentifier;
        category.Description = updateCategoryDto.Description;
        category.ParentCategoryId = updateCategoryDto.ParentCategoryId == 0 ? null : updateCategoryDto.ParentCategoryId;
        category.Status = (VisibilityStatus)updateCategoryDto.Status;
        category.PublishDate = updateCategoryDto.PublishDate;
        category.ModifiedBy = userId;
        category.ModifiedOn = DateTime.UtcNow;

        _unitOfWork.Repository<Category>().Update(category);
        await _unitOfWork.SaveChangesAsync();

        try
        {
            if (!string.IsNullOrEmpty(updateCategoryDto.Image))
            {
                category.CategoryImages = await UploadCategoryImage(userId, updateCategoryDto.Image);

                _unitOfWork.Repository<Category>().Update(category);
                await _unitOfWork.SaveChangesAsync();
            }
        }
        catch (Exception ex)
        {
            return ServiceResult<bool>
                .Error(new Problem(ErrorCodes.CategoryImageUploadError, ex.Message));
        }

        return ServiceResult<bool>.Success(true);
    }

    public async Task<ServiceResult<List<CategoryDropdownResponseDto>>> GetCategoriesForDropdown()
    {
        var categories = await _unitOfWork.Repository<Category>().GetAllAsync();

        return ServiceResult<List<CategoryDropdownResponseDto>>
            .Success(categories.Adapt<List<CategoryDropdownResponseDto>>());
    }

    public async Task<ServiceResult<List<CategoryListItemDto>>> GetAllCategories(int pageIndex, int pageSize)
    {
        var categories = await _unitOfWork.Repository<Category>()
            .GetPaginatedAsync(pageIndex, pageSize);

        return ServiceResult<List<CategoryListItemDto>>
            .Success(categories.Adapt<List<CategoryListItemDto>>());
    }

    public async Task<ServiceResult<CategoryDto>> GetCategoryById(int id)
    {
        var category = await _unitOfWork.Repository<Category>()
            .GetFirstOrDefaultAsync(x => x.Id == id, "CategoryImages");

        if (category == null)
        {
            return ServiceResult<CategoryDto>
                .Error(new Problem(ErrorCodes.CategoryNotFound, Messages.CategoryNotFound));
        }

        var categoryDto = category.Adapt<CategoryDto>();

        var imagePath = categoryDto.CategoryImages?.FirstOrDefault()?.ImagePath;
        var image = await _imageService.ConvertImageToBase64(imagePath);

        if (!string.IsNullOrEmpty(image))
        {
            categoryDto.CategoryImages.FirstOrDefault().Image = image;
        }

        return ServiceResult<CategoryDto>.Success(categoryDto);
    }

    private async Task<List<CategoryImage>> UploadCategoryImage(string userId, string image)
    {
        var imagePath = await _imageService.UploadImage(image);

        return [new CategoryImage()
        {
            ImagePath = imagePath,
            CreatedBy = userId,
            CreatedOn = DateTime.UtcNow,
        }];
    }
}
