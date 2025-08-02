using PCI.Domain.Models;
using PCI.Shared.Common.Enums;
using PCI.Shared.Dtos.Customer;

namespace PCI.Application.Specifications;

public class CustomerSpecification : BaseSpecification<Customer>
{
    public CustomerSpecification(int organisationId, CustomerFilterDto filter)
        : base(c => c.OrganisationId == organisationId)
    {
        ApplyFilters(filter);
        ApplySorting(filter);
        ApplyPaging(filter);
        ApplyIncludes();
    }

    private void ApplyFilters(CustomerFilterDto filter)
    {
        if (!string.IsNullOrWhiteSpace(filter.SearchTerm))
        {
            var searchTerm = filter.SearchTerm.ToLower();
            AddCriteria(c =>
                c.DisplayName.ToLower().Contains(searchTerm) ||
                c.CustomerCode.ToLower().Contains(searchTerm) ||
                (c.CompanyName != null && c.CompanyName.ToLower().Contains(searchTerm)) ||
                c.CustomerContacts.Any(cc => cc.Email != null && cc.Email.ToLower().Contains(searchTerm)));
        }

        if (!string.IsNullOrWhiteSpace(filter.CustomerCode))
        {
            AddCriteria(c => c.CustomerCode.ToLower().Contains(filter.CustomerCode));
        }

        if (!string.IsNullOrWhiteSpace(filter.CustomerName))
        {
            AddCriteria(c => c.DisplayName.ToLower().Contains(filter.CustomerName));
        }

        if (!string.IsNullOrWhiteSpace(filter.Email))
        {
            AddCriteria(c => c.CustomerContacts.Any(cc => cc.Email != null && cc.Email.ToLower().Contains(filter.Email)));
        }

        if (!string.IsNullOrWhiteSpace(filter.WorkPhone))
        {
            AddCriteria(c => c.CustomerContacts.Any(cc => cc.PhoneNumber != null && cc.PhoneNumber.ToLower().Contains(filter.WorkPhone)));
        }

        if (filter.CustomerType.HasValue && filter.CustomerType > 0)
        {
            AddCriteria(c => c.CustomerType == (CustomerType)filter.CustomerType.Value);
        }

        if (!string.IsNullOrWhiteSpace(filter.City))
        {
            AddCriteria(c => c.CustomerAddresses.Any(ca => ca.City != null && ca.City.ToLower().Contains(filter.City)));
        }

        if (filter.StateId > 0)
        {
            AddCriteria(c => c.CustomerAddresses.Any(ca => ca.StateId == filter.StateId));
        }

        if (filter.CountryId > 0)
        {
            AddCriteria(c => c.CustomerAddresses.Any(ca => ca.CountryId == filter.CountryId));
        }

        if (filter.IsActive.HasValue)
        {
            AddCriteria(c => c.IsActive == filter.IsActive.Value);
        }

        if (filter.CurrencyId.HasValue)
        {
            AddCriteria(c => c.CurrencyId == filter.CurrencyId.Value);
        }

        if (filter.MinCreditLimit.HasValue)
        {
            AddCriteria(c => c.CustomerFinancial != null && c.CustomerFinancial.CreditLimit >= filter.MinCreditLimit.Value);
        }

        if (filter.MaxCreditLimit.HasValue)
        {
            AddCriteria(c => c.CustomerFinancial != null && c.CustomerFinancial.CreditLimit <= filter.MaxCreditLimit.Value);
        }

        if (filter.CreatedFrom.HasValue)
        {
            AddCriteria(c => c.CreatedOn >= filter.CreatedFrom.Value);
        }

        if (filter.CreatedTo.HasValue)
        {
            AddCriteria(c => c.CreatedOn <= filter.CreatedTo.Value);
        }
    }

    private void ApplySorting(CustomerFilterDto filter)
    {
        if (!string.IsNullOrWhiteSpace(filter.SortBy))
        {
            switch (filter.SortBy.ToLower())
            {
                case "customername":
                    if (filter.SortDescending)
                        ApplyOrderByDescending(c => c.DisplayName);
                    else
                        ApplyOrderBy(c => c.DisplayName);
                    break;
                case "customercode":
                    if (filter.SortDescending)
                        ApplyOrderByDescending(c => c.CustomerCode);
                    else
                        ApplyOrderBy(c => c.CustomerCode);
                    break;
                case "companyname":
                    if (filter.SortDescending)
                        ApplyOrderByDescending(c => c.CompanyName);
                    else
                        ApplyOrderBy(c => c.CompanyName);
                    break;
                case "email":
                    if (filter.SortDescending)
                        ApplyOrderByDescending(c => c.CustomerContacts.FirstOrDefault().Email);
                    else
                        ApplyOrderBy(c => c.CustomerContacts.FirstOrDefault().Email);
                    break;
                case "city":
                    if (filter.SortDescending)
                        ApplyOrderByDescending(c => c.CustomerAddresses.FirstOrDefault().City);
                    else
                        ApplyOrderBy(c => c.CustomerAddresses.FirstOrDefault().City);
                    break;
                case "country":
                    if (filter.SortDescending)
                        ApplyOrderByDescending(c => c.CustomerAddresses.FirstOrDefault().CountryId);
                    else
                        ApplyOrderBy(c => c.CustomerAddresses.FirstOrDefault().CountryId);
                    break;
                case "creditlimit":
                    if (filter.SortDescending)
                        ApplyOrderByDescending(c => c.CustomerFinancial.CreditLimit);
                    else
                        ApplyOrderBy(c => c.CustomerFinancial.CreditLimit);
                    break;
                case "createdon":
                    if (filter.SortDescending)
                        ApplyOrderByDescending(c => c.CreatedOn);
                    else
                        ApplyOrderBy(c => c.CreatedOn);
                    break;
                default:
                    ApplyOrderBy(c => c.DisplayName);
                    break;
            }
        }
        else
        {
            ApplyOrderBy(c => c.DisplayName);
        }
    }

    private void ApplyPaging(CustomerFilterDto filter)
    {
        var skip = (filter.PageIndex - 1) * filter.PageSize;
        ApplyPaging(skip, filter.PageSize);
    }

    private void ApplyIncludes()
    {
        AddInclude("Currency");
        AddInclude("CustomerFinancial");
        AddInclude("CustomerContacts");
        AddInclude("CustomerAddresses");
    }
}