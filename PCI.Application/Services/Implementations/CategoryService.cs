using Mapster;
using PCI.Application.Repositories;
using PCI.Application.Services.Interfaces;
using PCI.Domain.Models;
using PCI.Shared.Common;
using PCI.Shared.Dtos;

namespace PCI.Application.Services.Implementations;

public class CategoryService(IUnitOfWork unitOfWork) : ICategoryService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<ServiceResult<CategoryDto>> CreateCategory(string userId, CreateCategoryDto createCategoryDto)
    {
        var existingCategory = await _unitOfWork.Repository<Category>()
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
                ParentCategoryId = createCategoryDto.ParentCategoryId == 0 ? null : createCategoryDto.ParentCategoryId,
                Status = (VisibilityStatus)createCategoryDto.Status,
                PublishDate = DateTime.UtcNow,
                CreatedBy = userId,
                CreatedOn = DateTime.UtcNow,
            };

            _unitOfWork.Repository<Category>().Add(category);
            await _unitOfWork.SaveChangesAsync();

            var imagePath = await UploadCategoryImage(createCategoryDto.Image);

            var categoryImage = new CategoryImage()
            {
                ImagePath = imagePath
            };

            category.CategoryImages.Add(categoryImage);
            await _unitOfWork.SaveChangesAsync();

            return ServiceResult<CategoryDto>
                .Success(category.Adapt<CategoryDto>());
        }
        catch (Exception ex)
        {
            return ServiceResult<CategoryDto>
                .Error(new Problem(ErrorCodes.CategoryCreationError, ex.ToString()));
        }
    }

    public async Task<ServiceResult<List<CategoryDropdownDto>>> GetCategoriesForDropdown()
    {
        var categories = await _unitOfWork.Repository<Category>().GetAllAsync();

        return ServiceResult<List<CategoryDropdownDto>>
            .Success(categories.Adapt<List<CategoryDropdownDto>>());
    }

    private static async Task<string> UploadCategoryImage(string base64Data)
    {
        if (base64Data.Contains(','))
        {
            base64Data = base64Data.Split(',')[1];
        }

        byte[] imageBytes = Convert.FromBase64String(base64Data);

        // Generate a unique file name
        string fileName = $"{Guid.NewGuid()}.png";
        string folderPath = Path.Combine(Environment.CurrentDirectory, "Uploads");
        string filePath = Path.Combine(folderPath, fileName);

        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

        await File.WriteAllBytesAsync(filePath, imageBytes);

        return $"/Uploads/{fileName}";
    }
}
