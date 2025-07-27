using PCI.Domain.Models;
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
                c.CustomerName.Contains(searchTerm, StringComparison.CurrentCultureIgnoreCase) ||
                c.CustomerCode.Contains(searchTerm, StringComparison.CurrentCultureIgnoreCase) ||
                (c.CompanyName != null && c.CompanyName.Contains(searchTerm, StringComparison.CurrentCultureIgnoreCase)) ||
                (c.Email != null && c.Email.Contains(searchTerm, StringComparison.CurrentCultureIgnoreCase)) ||
                (c.ContactPerson != null && c.ContactPerson.Contains(searchTerm, StringComparison.CurrentCultureIgnoreCase)));
        }

        if (!string.IsNullOrWhiteSpace(filter.CustomerCode))
        {
            AddCriteria(c => c.CustomerCode.Contains(filter.CustomerCode, StringComparison.CurrentCultureIgnoreCase));
        }

        if (!string.IsNullOrWhiteSpace(filter.CustomerName))
        {
            AddCriteria(c => c.CustomerName.Contains(filter.CustomerName, StringComparison.CurrentCultureIgnoreCase));
        }

        if (!string.IsNullOrWhiteSpace(filter.Email))
        {
            AddCriteria(c => c.Email != null && c.Email.Contains(filter.Email, StringComparison.CurrentCultureIgnoreCase));
        }

        if (!string.IsNullOrWhiteSpace(filter.PhoneNumber))
        {
            AddCriteria(c => c.PhoneNumber != null && c.PhoneNumber.Contains(filter.PhoneNumber));
        }

        if (filter.CustomerType.HasValue)
        {
            AddCriteria(c => c.CustomerType == filter.CustomerType.Value);
        }

        if (!string.IsNullOrWhiteSpace(filter.City))
        {
            AddCriteria(c => c.City != null && c.City.Contains(filter.City, StringComparison.CurrentCultureIgnoreCase));
        }

        if (!string.IsNullOrWhiteSpace(filter.State))
        {
            AddCriteria(c => c.State != null && c.State.Contains(filter.State, StringComparison.CurrentCultureIgnoreCase));
        }

        if (!string.IsNullOrWhiteSpace(filter.Country))
        {
            AddCriteria(c => c.Country != null && c.Country.Contains(filter.Country, StringComparison.CurrentCultureIgnoreCase));
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
            AddCriteria(c => c.CreditLimit >= filter.MinCreditLimit.Value);
        }

        if (filter.MaxCreditLimit.HasValue)
        {
            AddCriteria(c => c.CreditLimit <= filter.MaxCreditLimit.Value);
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
                        ApplyOrderByDescending(c => c.Email);
                    else
                        ApplyOrderBy(c => c.Email);
                    break;
                case "city":
                    if (filter.SortDescending)
                        ApplyOrderByDescending(c => c.City);
                    else
                        ApplyOrderBy(c => c.City);
                    break;
                case "country":
                    if (filter.SortDescending)
                        ApplyOrderByDescending(c => c.Country);
                    else
                        ApplyOrderBy(c => c.Country);
                    break;
                case "creditlimit":
                    if (filter.SortDescending)
                        ApplyOrderByDescending(c => c.CreditLimit);
                    else
                        ApplyOrderBy(c => c.CreditLimit);
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
    }
}