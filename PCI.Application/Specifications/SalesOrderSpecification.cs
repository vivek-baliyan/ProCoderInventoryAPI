using PCI.Domain.Models;
using PCI.Shared.Dtos.SalesOrder;

namespace PCI.Application.Specifications;

public class SalesOrderSpecification : BaseSpecification<SalesOrder>
{
    public SalesOrderSpecification(int organisationId, SalesOrderFilterDto filter)
        : base(x => x.OrganisationId == organisationId)
    {
        ApplyFilters(filter);
        ApplySorting(filter);
        ApplyPaging(filter);
        ApplyIncludes();
    }

    private void ApplyFilters(SalesOrderFilterDto filter)
    {
        if (!string.IsNullOrEmpty(filter.OrderNumber))
        {
            AddCriteria(x => x.OrderNumber.Contains(filter.OrderNumber));
        }

        if (!string.IsNullOrEmpty(filter.Status))
        {
            AddCriteria(x => x.Status == filter.Status);
        }

        if (filter.CustomerId.HasValue)
        {
            AddCriteria(x => x.CustomerId == filter.CustomerId.Value);
        }

        if (!string.IsNullOrEmpty(filter.CustomerName))
        {
            AddCriteria(x => x.Customer.CompanyName.Contains(filter.CustomerName));
        }

        if (filter.OrderDateFrom.HasValue)
        {
            AddCriteria(x => x.OrderDate >= filter.OrderDateFrom.Value);
        }

        if (filter.OrderDateTo.HasValue)
        {
            AddCriteria(x => x.OrderDate <= filter.OrderDateTo.Value);
        }

        if (filter.ExpectedDeliveryDateFrom.HasValue)
        {
            AddCriteria(x => x.ExpectedDeliveryDate >= filter.ExpectedDeliveryDateFrom.Value);
        }

        if (filter.ExpectedDeliveryDateTo.HasValue)
        {
            AddCriteria(x => x.ExpectedDeliveryDate <= filter.ExpectedDeliveryDateTo.Value);
        }

        if (filter.TotalAmountFrom.HasValue)
        {
            AddCriteria(x => x.TotalAmount >= filter.TotalAmountFrom.Value);
        }

        if (filter.TotalAmountTo.HasValue)
        {
            AddCriteria(x => x.TotalAmount <= filter.TotalAmountTo.Value);
        }

        if (!string.IsNullOrEmpty(filter.ReferenceNumber))
        {
            AddCriteria(x => x.ReferenceNumber.Contains(filter.ReferenceNumber));
        }

        if (!string.IsNullOrEmpty(filter.QuoteNumber))
        {
            AddCriteria(x => x.QuoteNumber.Contains(filter.QuoteNumber));
        }
    }

    private void ApplySorting(SalesOrderFilterDto filter)
    {
        if (!string.IsNullOrEmpty(filter.SortBy))
        {
            switch (filter.SortBy.ToLower())
            {
                case "ordernumber":
                    if (filter.SortDirection?.ToLower() == "desc")
                        ApplyOrderByDescending(x => x.OrderNumber);
                    else
                        ApplyOrderBy(x => x.OrderNumber);
                    break;
                case "orderdate":
                    if (filter.SortDirection?.ToLower() == "desc")
                        ApplyOrderByDescending(x => x.OrderDate);
                    else
                        ApplyOrderBy(x => x.OrderDate);
                    break;
                case "status":
                    if (filter.SortDirection?.ToLower() == "desc")
                        ApplyOrderByDescending(x => x.Status);
                    else
                        ApplyOrderBy(x => x.Status);
                    break;
                case "totalamount":
                    if (filter.SortDirection?.ToLower() == "desc")
                        ApplyOrderByDescending(x => x.TotalAmount);
                    else
                        ApplyOrderBy(x => x.TotalAmount);
                    break;
                case "customername":
                    if (filter.SortDirection?.ToLower() == "desc")
                        ApplyOrderByDescending(x => x.Customer.CompanyName);
                    else
                        ApplyOrderBy(x => x.Customer.CompanyName);
                    break;
                default:
                    ApplyOrderByDescending(x => x.OrderDate);
                    break;
            }
        }
        else
        {
            ApplyOrderByDescending(x => x.OrderDate);
        }
    }

    private void ApplyPaging(SalesOrderFilterDto filter)
    {
        var skip = filter.PageIndex * filter.PageSize;
        ApplyPaging(skip, filter.PageSize);
    }

    private void ApplyIncludes()
    {
        AddInclude("Customer");
    }
}