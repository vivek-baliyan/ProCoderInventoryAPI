using PCI.Domain.Models;
using PCI.Shared.Dtos.Vendor;

namespace PCI.Application.Specifications;

public class VendorSpecification : BaseSpecification<Vendor>
{
    public VendorSpecification(int organisationId, VendorFilterDto filter)
        : base(v => v.OrganisationId == organisationId)
    {
        ApplyFilters(filter);
        ApplySorting(filter);
        ApplyPaging(filter);
        ApplyIncludes();
    }

    private void ApplyFilters(VendorFilterDto filter)
    {
        if (!string.IsNullOrWhiteSpace(filter.SearchTerm))
        {
            var searchTerm = filter.SearchTerm.ToLower();
            AddCriteria(v =>
                v.VendorName.Contains(searchTerm, StringComparison.CurrentCultureIgnoreCase) ||
                v.VendorCode.Contains(searchTerm, StringComparison.CurrentCultureIgnoreCase) ||
                (v.CompanyName != null && v.CompanyName.Contains(searchTerm, StringComparison.CurrentCultureIgnoreCase)) ||
                (v.Email != null && v.Email.Contains(searchTerm, StringComparison.CurrentCultureIgnoreCase)) ||
                (v.ContactPerson != null && v.ContactPerson.Contains(searchTerm, StringComparison.CurrentCultureIgnoreCase)));
        }

        if (!string.IsNullOrWhiteSpace(filter.VendorCode))
        {
            AddCriteria(v => v.VendorCode.Contains(filter.VendorCode, StringComparison.CurrentCultureIgnoreCase));
        }

        if (!string.IsNullOrWhiteSpace(filter.VendorName))
        {
            AddCriteria(v => v.VendorName.Contains(filter.VendorName, StringComparison.CurrentCultureIgnoreCase));
        }

        if (!string.IsNullOrWhiteSpace(filter.CompanyName))
        {
            AddCriteria(v => v.CompanyName != null && v.CompanyName.Contains(filter.CompanyName, StringComparison.CurrentCultureIgnoreCase));
        }

        if (!string.IsNullOrWhiteSpace(filter.Email))
        {
            AddCriteria(v => v.Email != null && v.Email.Contains(filter.Email, StringComparison.CurrentCultureIgnoreCase));
        }

        if (!string.IsNullOrWhiteSpace(filter.PhoneNumber))
        {
            AddCriteria(v => v.PhoneNumber != null && v.PhoneNumber.Contains(filter.PhoneNumber));
        }

        if (filter.VendorType.HasValue)
        {
            AddCriteria(v => v.VendorType == filter.VendorType.Value);
        }

        if (filter.Category.HasValue)
        {
            AddCriteria(v => v.Category == filter.Category.Value);
        }

        if (filter.Status.HasValue)
        {
            AddCriteria(v => v.Status == filter.Status.Value);
        }

        if (!string.IsNullOrWhiteSpace(filter.Industry))
        {
            AddCriteria(v => v.Industry != null && v.Industry.Contains(filter.Industry, StringComparison.CurrentCultureIgnoreCase));
        }

        if (!string.IsNullOrWhiteSpace(filter.City))
        {
            AddCriteria(v => v.City != null && v.City.Contains(filter.City, StringComparison.CurrentCultureIgnoreCase));
        }

        if (!string.IsNullOrWhiteSpace(filter.State))
        {
            AddCriteria(v => v.State != null && v.State.Contains(filter.State, StringComparison.CurrentCultureIgnoreCase));
        }

        if (!string.IsNullOrWhiteSpace(filter.Country))
        {
            AddCriteria(v => v.Country != null && v.Country.Contains(filter.Country, StringComparison.CurrentCultureIgnoreCase));
        }

        if (filter.CurrencyId.HasValue)
        {
            AddCriteria(v => v.CurrencyId == filter.CurrencyId.Value);
        }

        if (filter.IsPreferredVendor.HasValue)
        {
            AddCriteria(v => v.IsPreferredVendor == filter.IsPreferredVendor.Value);
        }

        if (filter.IsBlacklisted.HasValue)
        {
            AddCriteria(v => v.IsBlacklisted == filter.IsBlacklisted.Value);
        }

        if (filter.IsManufacturer.HasValue)
        {
            AddCriteria(v => v.IsManufacturer == filter.IsManufacturer.Value);
        }

        if (filter.IsDropshipVendor.HasValue)
        {
            AddCriteria(v => v.IsDropshipVendor == filter.IsDropshipVendor.Value);
        }

        if (filter.HasPortalAccess.HasValue)
        {
            AddCriteria(v => v.HasPortalAccess == filter.HasPortalAccess.Value);
        }

        if (filter.RequiresPOApproval.HasValue)
        {
            AddCriteria(v => v.RequiresPOApproval == filter.RequiresPOApproval.Value);
        }

        // Financial Filters
        if (filter.MinCreditLimit.HasValue)
        {
            AddCriteria(v => v.CreditLimit >= filter.MinCreditLimit.Value);
        }

        if (filter.MaxCreditLimit.HasValue)
        {
            AddCriteria(v => v.CreditLimit <= filter.MaxCreditLimit.Value);
        }

        if (filter.MinCurrentBalance.HasValue)
        {
            AddCriteria(v => v.CurrentBalance >= filter.MinCurrentBalance.Value);
        }

        if (filter.MaxCurrentBalance.HasValue)
        {
            AddCriteria(v => v.CurrentBalance <= filter.MaxCurrentBalance.Value);
        }

        if (filter.MinOutstandingAmount.HasValue)
        {
            AddCriteria(v => v.OutstandingAmount >= filter.MinOutstandingAmount.Value);
        }

        if (filter.MaxOutstandingAmount.HasValue)
        {
            AddCriteria(v => v.OutstandingAmount <= filter.MaxOutstandingAmount.Value);
        }

        // Performance Filters
        if (filter.MinPerformanceRating.HasValue)
        {
            AddCriteria(v => v.PerformanceRating >= filter.MinPerformanceRating.Value);
        }

        if (filter.MaxPerformanceRating.HasValue)
        {
            AddCriteria(v => v.PerformanceRating <= filter.MaxPerformanceRating.Value);
        }

        if (filter.MinOnTimeDeliveryPercentage.HasValue)
        {
            AddCriteria(v => v.OnTimeDeliveryPercentage >= filter.MinOnTimeDeliveryPercentage.Value);
        }

        if (filter.MaxOnTimeDeliveryPercentage.HasValue)
        {
            AddCriteria(v => v.OnTimeDeliveryPercentage <= filter.MaxOnTimeDeliveryPercentage.Value);
        }

        if (filter.MinQualityRating.HasValue)
        {
            AddCriteria(v => v.QualityRating >= filter.MinQualityRating.Value);
        }

        if (filter.MaxQualityRating.HasValue)
        {
            AddCriteria(v => v.QualityRating <= filter.MaxQualityRating.Value);
        }

        // Date Filters
        if (filter.CreatedFrom.HasValue)
        {
            AddCriteria(v => v.CreatedOn >= filter.CreatedFrom.Value);
        }

        if (filter.CreatedTo.HasValue)
        {
            AddCriteria(v => v.CreatedOn <= filter.CreatedTo.Value);
        }

        if (filter.LastOrderFrom.HasValue)
        {
            AddCriteria(v => v.LastOrderDate >= filter.LastOrderFrom.Value);
        }

        if (filter.LastOrderTo.HasValue)
        {
            AddCriteria(v => v.LastOrderDate <= filter.LastOrderTo.Value);
        }

        if (filter.LastPaymentFrom.HasValue)
        {
            AddCriteria(v => v.LastPaymentDate >= filter.LastPaymentFrom.Value);
        }

        if (filter.LastPaymentTo.HasValue)
        {
            AddCriteria(v => v.LastPaymentDate <= filter.LastPaymentTo.Value);
        }
    }

    private void ApplySorting(VendorFilterDto filter)
    {
        if (!string.IsNullOrWhiteSpace(filter.SortBy))
        {
            switch (filter.SortBy.ToLower())
            {
                case "vendorname":
                    if (filter.SortDescending)
                        ApplyOrderByDescending(v => v.VendorName);
                    else
                        ApplyOrderBy(v => v.VendorName);
                    break;
                case "vendorcode":
                    if (filter.SortDescending)
                        ApplyOrderByDescending(v => v.VendorCode);
                    else
                        ApplyOrderBy(v => v.VendorCode);
                    break;
                case "companyname":
                    if (filter.SortDescending)
                        ApplyOrderByDescending(v => v.CompanyName);
                    else
                        ApplyOrderBy(v => v.CompanyName);
                    break;
                case "email":
                    if (filter.SortDescending)
                        ApplyOrderByDescending(v => v.Email);
                    else
                        ApplyOrderBy(v => v.Email);
                    break;
                case "city":
                    if (filter.SortDescending)
                        ApplyOrderByDescending(v => v.City);
                    else
                        ApplyOrderBy(v => v.City);
                    break;
                case "country":
                    if (filter.SortDescending)
                        ApplyOrderByDescending(v => v.Country);
                    else
                        ApplyOrderBy(v => v.Country);
                    break;
                case "vendortype":
                    if (filter.SortDescending)
                        ApplyOrderByDescending(v => v.VendorType);
                    else
                        ApplyOrderBy(v => v.VendorType);
                    break;
                case "category":
                    if (filter.SortDescending)
                        ApplyOrderByDescending(v => v.Category);
                    else
                        ApplyOrderBy(v => v.Category);
                    break;
                case "status":
                    if (filter.SortDescending)
                        ApplyOrderByDescending(v => v.Status);
                    else
                        ApplyOrderBy(v => v.Status);
                    break;
                case "creditlimit":
                    if (filter.SortDescending)
                        ApplyOrderByDescending(v => v.CreditLimit);
                    else
                        ApplyOrderBy(v => v.CreditLimit);
                    break;
                case "currentbalance":
                    if (filter.SortDescending)
                        ApplyOrderByDescending(v => v.CurrentBalance);
                    else
                        ApplyOrderBy(v => v.CurrentBalance);
                    break;
                case "outstandingamount":
                    if (filter.SortDescending)
                        ApplyOrderByDescending(v => v.OutstandingAmount);
                    else
                        ApplyOrderBy(v => v.OutstandingAmount);
                    break;
                case "performancerating":
                    if (filter.SortDescending)
                        ApplyOrderByDescending(v => v.PerformanceRating);
                    else
                        ApplyOrderBy(v => v.PerformanceRating);
                    break;
                case "ontimedeliverypercentage":
                    if (filter.SortDescending)
                        ApplyOrderByDescending(v => v.OnTimeDeliveryPercentage);
                    else
                        ApplyOrderBy(v => v.OnTimeDeliveryPercentage);
                    break;
                case "lastorderdate":
                    if (filter.SortDescending)
                        ApplyOrderByDescending(v => v.LastOrderDate);
                    else
                        ApplyOrderBy(v => v.LastOrderDate);
                    break;
                case "lastpaymentdate":
                    if (filter.SortDescending)
                        ApplyOrderByDescending(v => v.LastPaymentDate);
                    else
                        ApplyOrderBy(v => v.LastPaymentDate);
                    break;
                case "createdon":
                    if (filter.SortDescending)
                        ApplyOrderByDescending(v => v.CreatedOn);
                    else
                        ApplyOrderBy(v => v.CreatedOn);
                    break;
                default:
                    ApplyOrderBy(v => v.VendorName);
                    break;
            }
        }
        else
        {
            ApplyOrderBy(v => v.VendorName);
        }
    }

    private void ApplyPaging(VendorFilterDto filter)
    {
        var skip = (filter.PageIndex - 1) * filter.PageSize;
        ApplyPaging(skip, filter.PageSize);
    }

    private void ApplyIncludes()
    {
        AddInclude("Currency");
        AddInclude("ParentVendor");
    }
}