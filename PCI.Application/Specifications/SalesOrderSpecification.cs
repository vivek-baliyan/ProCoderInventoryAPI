using PCI.Domain.Models;
using PCI.Shared.Dtos.SalesOrder;
using System.Linq.Expressions;

namespace PCI.Application.Specifications;

public class SalesOrderSpecification : BaseSpecification<SalesOrder>
{
    public SalesOrderSpecification(int organisationId, SalesOrderFilterDto filter) : base()
    {
        // Base filter for organisation
        ApplyFilter(x => x.OrganisationId == organisationId);

        // Apply filters
        if (!string.IsNullOrEmpty(filter.OrderNumber))
        {
            ApplyFilter(x => x.OrderNumber.Contains(filter.OrderNumber));
        }

        if (!string.IsNullOrEmpty(filter.Status))
        {
            ApplyFilter(x => x.Status == filter.Status);
        }

        if (filter.CustomerId.HasValue)
        {
            ApplyFilter(x => x.CustomerId == filter.CustomerId.Value);
        }

        if (!string.IsNullOrEmpty(filter.CustomerName))
        {
            ApplyFilter(x => x.Customer.CompanyName.Contains(filter.CustomerName));
        }

        if (filter.OrderDateFrom.HasValue)
        {
            ApplyFilter(x => x.OrderDate >= filter.OrderDateFrom.Value);
        }

        if (filter.OrderDateTo.HasValue)
        {
            ApplyFilter(x => x.OrderDate <= filter.OrderDateTo.Value);
        }

        if (filter.ExpectedDeliveryDateFrom.HasValue)
        {
            ApplyFilter(x => x.ExpectedDeliveryDate >= filter.ExpectedDeliveryDateFrom.Value);
        }

        if (filter.ExpectedDeliveryDateTo.HasValue)
        {
            ApplyFilter(x => x.ExpectedDeliveryDate <= filter.ExpectedDeliveryDateTo.Value);
        }

        if (filter.TotalAmountFrom.HasValue)
        {
            ApplyFilter(x => x.TotalAmount >= filter.TotalAmountFrom.Value);
        }

        if (filter.TotalAmountTo.HasValue)
        {
            ApplyFilter(x => x.TotalAmount <= filter.TotalAmountTo.Value);
        }

        if (!string.IsNullOrEmpty(filter.ReferenceNumber))
        {
            ApplyFilter(x => x.ReferenceNumber.Contains(filter.ReferenceNumber));
        }

        if (!string.IsNullOrEmpty(filter.QuoteNumber))
        {
            ApplyFilter(x => x.QuoteNumber.Contains(filter.QuoteNumber));
        }

        // Include related entities
        ApplyInclude("Customer");

        // Apply sorting
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

        // Apply pagination
        ApplyPaging(filter.PageIndex * filter.PageSize, filter.PageSize);
    }
}