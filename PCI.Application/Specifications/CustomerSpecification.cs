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
                c.CustomerName.ToLower().Contains(searchTerm) ||
                c.CustomerCode.ToLower().Contains(searchTerm) ||
                (c.CompanyName != null && c.CompanyName.ToLower().Contains(searchTerm)) ||
                c.BusinessContacts.Any(bc => bc.Email != null && bc.Email.ToLower().Contains(searchTerm)));
        }

        if (!string.IsNullOrWhiteSpace(filter.CustomerCode))
        {
            AddCriteria(c => c.CustomerCode.ToLower().Contains(filter.CustomerCode));
        }

        if (!string.IsNullOrWhiteSpace(filter.CustomerName))
        {
            AddCriteria(c => c.CustomerName.ToLower().Contains(filter.CustomerName));
        }

        if (!string.IsNullOrWhiteSpace(filter.Email))
        {
            AddCriteria(c => c.BusinessContacts.Any(bc => bc.Email != null && bc.Email.ToLower().Contains(filter.Email)));
        }

        if (!string.IsNullOrWhiteSpace(filter.WorkPhone))
        {
            AddCriteria(c => c.BusinessContacts.Any(bc => bc.PhoneNumber != null && bc.PhoneNumber.ToLower().Contains(filter.WorkPhone)));
        }

        if (filter.CustomerType.HasValue && filter.CustomerType > 0)
        {
            AddCriteria(c => c.CustomerType == (CustomerType)filter.CustomerType.Value);
        }

        if (!string.IsNullOrWhiteSpace(filter.City))
        {
            AddCriteria(c => c.BusinessAddresses.Any(ba => ba.City != null && ba.City.ToLower().Contains(filter.City)));
        }

        if (!string.IsNullOrWhiteSpace(filter.State))
        {
            AddCriteria(c => c.BusinessAddresses.Any(ba => ba.State != null && ba.State.ToLower().Contains(filter.State)));
        }

        if (!string.IsNullOrWhiteSpace(filter.Country))
        {
            AddCriteria(c => c.BusinessAddresses.Any(ba => ba.Country != null && ba.Country.ToLower().Contains(filter.Country)));
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
                        ApplyOrderByDescending(c => c.CustomerName);
                    else
                        ApplyOrderBy(c => c.CustomerName);
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
                        ApplyOrderByDescending(c => c.BusinessContacts.FirstOrDefault().Email);
                    else
                        ApplyOrderBy(c => c.BusinessContacts.FirstOrDefault().Email);
                    break;
                case "city":
                    if (filter.SortDescending)
                        ApplyOrderByDescending(c => c.BusinessAddresses.FirstOrDefault().City);
                    else
                        ApplyOrderBy(c => c.BusinessAddresses.FirstOrDefault().City);
                    break;
                case "country":
                    if (filter.SortDescending)
                        ApplyOrderByDescending(c => c.BusinessAddresses.FirstOrDefault().Country);
                    else
                        ApplyOrderBy(c => c.BusinessAddresses.FirstOrDefault().Country);
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
                    ApplyOrderBy(c => c.CustomerName);
                    break;
            }
        }
        else
        {
            ApplyOrderBy(c => c.CustomerName);
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
        AddInclude("BusinessContacts");
        AddInclude("BusinessAddresses");
    }
}