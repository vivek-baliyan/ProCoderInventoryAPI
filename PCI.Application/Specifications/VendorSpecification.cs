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
                v.VendorName.ToLower().Contains(searchTerm) ||
                v.VendorCode.ToLower().Contains(searchTerm) ||
                (v.CompanyName != null && v.CompanyName.ToLower().Contains(searchTerm)) ||
                v.BusinessContacts.Any(bc => bc.Email != null && bc.Email.ToLower().Contains(searchTerm)) ||
                v.BusinessContacts.Any(bc => (bc.FirstName != null && bc.FirstName.ToLower().Contains(searchTerm)) || 
                                            (bc.LastName != null && bc.LastName.ToLower().Contains(searchTerm))));
        }

        if (!string.IsNullOrWhiteSpace(filter.VendorCode))
        {
            AddCriteria(v => v.VendorCode.ToLower().Contains(filter.VendorCode));
        }

        if (!string.IsNullOrWhiteSpace(filter.VendorName))
        {
            AddCriteria(v => v.VendorName.ToLower().Contains(filter.VendorName));
        }

        if (!string.IsNullOrWhiteSpace(filter.CompanyName))
        {
            AddCriteria(v => v.CompanyName != null && v.CompanyName.ToLower().Contains(filter.CompanyName));
        }

        if (!string.IsNullOrWhiteSpace(filter.Email))
        {
            AddCriteria(v => v.BusinessContacts.Any(bc => bc.Email != null && bc.Email.ToLower().Contains(filter.Email)));
        }

        if (!string.IsNullOrWhiteSpace(filter.PhoneNumber))
        {
            AddCriteria(v => v.BusinessContacts.Any(bc => bc.PhoneNumber != null && bc.PhoneNumber.Contains(filter.PhoneNumber)));
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
            AddCriteria(v => v.Industry != null && v.Industry.ToLower().Contains(filter.Industry));
        }

        if (!string.IsNullOrWhiteSpace(filter.City))
        {
            AddCriteria(v => v.BusinessAddresses.Any(ba => ba.City != null && ba.City.ToLower().Contains(filter.City)));
        }

        if (!string.IsNullOrWhiteSpace(filter.State))
        {
            AddCriteria(v => v.BusinessAddresses.Any(ba => ba.State != null && ba.State.ToLower().Contains(filter.State)));
        }

        if (!string.IsNullOrWhiteSpace(filter.Country))
        {
            AddCriteria(v => v.BusinessAddresses.Any(ba => ba.Country != null && ba.Country.ToLower().Contains(filter.Country)));
        }

        if (filter.CurrencyId.HasValue)
        {
            AddCriteria(v => v.CurrencyId == filter.CurrencyId.Value);
        }

        if (filter.IsBlacklisted.HasValue)
        {
            AddCriteria(v => v.VendorFinancial != null && v.VendorFinancial.IsBlacklisted == filter.IsBlacklisted.Value);
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
            AddCriteria(v => v.VendorFinancial != null && v.VendorFinancial.CurrentBalance >= filter.MinCreditLimit.Value);
        }

        if (filter.MaxCreditLimit.HasValue)
        {
            AddCriteria(v => v.VendorFinancial != null && v.VendorFinancial.CurrentBalance <= filter.MaxCreditLimit.Value);
        }

        if (filter.MinCurrentBalance.HasValue)
        {
            AddCriteria(v => v.VendorFinancial != null && v.VendorFinancial.CurrentBalance >= filter.MinCurrentBalance.Value);
        }

        if (filter.MaxCurrentBalance.HasValue)
        {
            AddCriteria(v => v.VendorFinancial != null && v.VendorFinancial.CurrentBalance <= filter.MaxCurrentBalance.Value);
        }

        if (filter.MinOutstandingAmount.HasValue)
        {
            AddCriteria(v => v.VendorFinancial != null && v.VendorFinancial.OutstandingAmount >= filter.MinOutstandingAmount.Value);
        }

        if (filter.MaxOutstandingAmount.HasValue)
        {
            AddCriteria(v => v.VendorFinancial != null && v.VendorFinancial.OutstandingAmount <= filter.MaxOutstandingAmount.Value);
        }

        // Performance Filters
        if (filter.MinPerformanceRating.HasValue)
        {
            AddCriteria(v => v.VendorFinancial != null && v.VendorFinancial.QualityRating >= filter.MinPerformanceRating.Value);
        }

        if (filter.MaxPerformanceRating.HasValue)
        {
            AddCriteria(v => v.VendorFinancial != null && v.VendorFinancial.QualityRating <= filter.MaxPerformanceRating.Value);
        }

        if (filter.MinOnTimeDeliveryPercentage.HasValue)
        {
            AddCriteria(v => v.VendorFinancial != null && v.VendorFinancial.OnTimeDeliveryRate >= filter.MinOnTimeDeliveryPercentage.Value);
        }

        if (filter.MaxOnTimeDeliveryPercentage.HasValue)
        {
            AddCriteria(v => v.VendorFinancial != null && v.VendorFinancial.OnTimeDeliveryRate <= filter.MaxOnTimeDeliveryPercentage.Value);
        }

        if (filter.MinQualityRating.HasValue)
        {
            AddCriteria(v => v.VendorFinancial != null && v.VendorFinancial.QualityRating >= filter.MinQualityRating.Value);
        }

        if (filter.MaxQualityRating.HasValue)
        {
            AddCriteria(v => v.VendorFinancial != null && v.VendorFinancial.QualityRating <= filter.MaxQualityRating.Value);
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
            AddCriteria(v => v.VendorFinancial != null && v.VendorFinancial.LastPurchaseDate >= filter.LastOrderFrom.Value);
        }

        if (filter.LastOrderTo.HasValue)
        {
            AddCriteria(v => v.VendorFinancial != null && v.VendorFinancial.LastPurchaseDate <= filter.LastOrderTo.Value);
        }

        if (filter.LastPaymentFrom.HasValue)
        {
            AddCriteria(v => v.VendorFinancial != null && v.VendorFinancial.LastPaymentDate >= filter.LastPaymentFrom.Value);
        }

        if (filter.LastPaymentTo.HasValue)
        {
            AddCriteria(v => v.VendorFinancial != null && v.VendorFinancial.LastPaymentDate <= filter.LastPaymentTo.Value);
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
                        ApplyOrderByDescending(v => v.BusinessContacts.FirstOrDefault().Email);
                    else
                        ApplyOrderBy(v => v.BusinessContacts.FirstOrDefault().Email);
                    break;
                case "city":
                    if (filter.SortDescending)
                        ApplyOrderByDescending(v => v.BusinessAddresses.FirstOrDefault().City);
                    else
                        ApplyOrderBy(v => v.BusinessAddresses.FirstOrDefault().City);
                    break;
                case "country":
                    if (filter.SortDescending)
                        ApplyOrderByDescending(v => v.BusinessAddresses.FirstOrDefault().Country);
                    else
                        ApplyOrderBy(v => v.BusinessAddresses.FirstOrDefault().Country);
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
                        ApplyOrderByDescending(v => v.VendorFinancial.CurrentBalance);
                    else
                        ApplyOrderBy(v => v.VendorFinancial.CurrentBalance);
                    break;
                case "currentbalance":
                    if (filter.SortDescending)
                        ApplyOrderByDescending(v => v.VendorFinancial.CurrentBalance);
                    else
                        ApplyOrderBy(v => v.VendorFinancial.CurrentBalance);
                    break;
                case "outstandingamount":
                    if (filter.SortDescending)
                        ApplyOrderByDescending(v => v.VendorFinancial.OutstandingAmount);
                    else
                        ApplyOrderBy(v => v.VendorFinancial.OutstandingAmount);
                    break;
                case "performancerating":
                    if (filter.SortDescending)
                        ApplyOrderByDescending(v => v.VendorFinancial.QualityRating);
                    else
                        ApplyOrderBy(v => v.VendorFinancial.QualityRating);
                    break;
                case "ontimedeliverypercentage":
                    if (filter.SortDescending)
                        ApplyOrderByDescending(v => v.VendorFinancial.OnTimeDeliveryRate);
                    else
                        ApplyOrderBy(v => v.VendorFinancial.OnTimeDeliveryRate);
                    break;
                case "lastorderdate":
                    if (filter.SortDescending)
                        ApplyOrderByDescending(v => v.VendorFinancial.LastPurchaseDate);
                    else
                        ApplyOrderBy(v => v.VendorFinancial.LastPurchaseDate);
                    break;
                case "lastpaymentdate":
                    if (filter.SortDescending)
                        ApplyOrderByDescending(v => v.VendorFinancial.LastPaymentDate);
                    else
                        ApplyOrderBy(v => v.VendorFinancial.LastPaymentDate);
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
        AddInclude("VendorFinancial");
        AddInclude("BusinessContacts");
        AddInclude("BusinessAddresses");
    }
}