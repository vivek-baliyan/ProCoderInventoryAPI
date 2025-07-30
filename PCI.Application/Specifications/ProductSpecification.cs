using PCI.Domain.Models;
using PCI.Shared.Common.Enums;
using PCI.Shared.Dtos.Product;

namespace PCI.Application.Specifications;

public class ProductSpecification : BaseSpecification<Product>
{
    public ProductSpecification(int organisationId, ProductFilterDto filter)
        : base(p => p.OrganisationId == organisationId)
    {
        ApplyFilters(filter);
        ApplySorting(filter);
        ApplyPaging(filter);
        ApplyIncludes();
    }

    private void ApplyFilters(ProductFilterDto filter)
    {
        if (!string.IsNullOrWhiteSpace(filter.SearchTerm))
        {
            var searchTerm = filter.SearchTerm.ToLower();
            AddCriteria(p =>
                p.Name.ToLower().Contains(searchTerm) ||
                p.SKU.ToLower().Contains(searchTerm));
        }

        if (filter.ProductType.HasValue)
        {
            AddCriteria(p => p.ProductType == (ProductType)filter.ProductType.Value);
        }

        if (filter.Status.HasValue)
        {
            AddCriteria(p => p.Status == (ProductStatus)filter.Status.Value);
        }

        if (filter.BrandId.HasValue)
        {
            AddCriteria(p => p.BrandId == filter.BrandId.Value);
        }

        if (filter.MinPrice.HasValue)
        {
            AddCriteria(p => p.SellingPrice >= filter.MinPrice.Value);
        }

        if (filter.MaxPrice.HasValue)
        {
            AddCriteria(p => p.SellingPrice <= filter.MaxPrice.Value);
        }

        if (filter.IsReturnable.HasValue)
        {
            AddCriteria(p => p.IsReturnable == filter.IsReturnable.Value);
        }

        if (filter.TrackInventory.HasValue)
        {
            AddCriteria(p => p.TrackInventory == filter.TrackInventory.Value);
        }

        if (filter.TagIds?.Any() == true)
        {
            AddCriteria(p => p.ProductTagAssignments.Any(pta => filter.TagIds.Contains(pta.ProductTagId)));
        }
    }

    private void ApplySorting(ProductFilterDto filter)
    {
        if (!string.IsNullOrWhiteSpace(filter.SortBy))
        {
            switch (filter.SortBy.ToLower())
            {
                case "name":
                    if (filter.SortDescending)
                        ApplyOrderByDescending(p => p.Name);
                    else
                        ApplyOrderBy(p => p.Name);
                    break;
                case "sku":
                    if (filter.SortDescending)
                        ApplyOrderByDescending(p => p.SKU);
                    else
                        ApplyOrderBy(p => p.SKU);
                    break;
                case "price":
                    if (filter.SortDescending)
                        ApplyOrderByDescending(p => p.SellingPrice);
                    else
                        ApplyOrderBy(p => p.SellingPrice);
                    break;
                case "status":
                    if (filter.SortDescending)
                        ApplyOrderByDescending(p => p.Status);
                    else
                        ApplyOrderBy(p => p.Status);
                    break;
                case "createdon":
                    if (filter.SortDescending)
                        ApplyOrderByDescending(p => p.CreatedOn);
                    else
                        ApplyOrderBy(p => p.CreatedOn);
                    break;
                default:
                    ApplyOrderBy(p => p.Name);
                    break;
            }
        }
        else
        {
            ApplyOrderBy(p => p.Name);
        }
    }

    private void ApplyPaging(ProductFilterDto filter)
    {
        var skip = (filter.PageIndex - 1) * filter.PageSize;
        ApplyPaging(skip, filter.PageSize);
    }

    private void ApplyIncludes()
    {
        AddInclude("Brand");
        AddInclude("ItemGroup");
        AddInclude("ProductImages");
        AddInclude("ProductTagAssignments");
    }
}