using PCI.Application.Repositories;
using PCI.Application.Services.Interfaces;
using PCI.Domain.Models;

namespace PCI.Application.Services.Implementations;

public class CodeGenerationService(IUnitOfWork unitOfWork) : ICodeGenerationService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<string> GenerateCustomerCodeAsync(int organisationId)
    {
        const string prefix = "CUST";
        var customers = await _unitOfWork.Repository<Customer>()
            .GetFilteredAsync(c => c.OrganisationId == organisationId && c.CustomerCode.StartsWith(prefix));

        var lastCustomer = customers
            .OrderByDescending(c => c.CustomerCode)
            .FirstOrDefault();

        return GenerateNextCode(prefix, lastCustomer?.CustomerCode);
    }

    public async Task<string> GenerateVendorCodeAsync(int organisationId)
    {
        const string prefix = "VEND";
        var vendors = await _unitOfWork.Repository<Vendor>()
            .GetFilteredAsync(v => v.OrganisationId == organisationId && v.VendorCode.StartsWith(prefix));

        var lastVendor = vendors
            .OrderByDescending(v => v.VendorCode)
            .FirstOrDefault();

        return GenerateNextCode(prefix, lastVendor?.VendorCode);
    }

    public async Task<string> GenerateSalesOrderNumberAsync(int organisationId)
    {
        const string prefix = "SO";
        var salesOrders = await _unitOfWork.Repository<SalesOrder>()
            .GetFilteredAsync(so => so.OrganisationId == organisationId && so.OrderNumber.StartsWith(prefix));

        var lastSalesOrder = salesOrders
            .OrderByDescending(so => so.OrderNumber)
            .FirstOrDefault();

        return GenerateNextCode(prefix, lastSalesOrder?.OrderNumber);
    }

    public async Task<string> GenerateInvoiceNumberAsync(int organisationId)
    {
        const string prefix = "INV";
        var invoices = await _unitOfWork.Repository<Invoice>()
            .GetFilteredAsync(i => i.OrganisationId == organisationId && i.InvoiceNumber.StartsWith(prefix));

        var lastInvoice = invoices
            .OrderByDescending(i => i.InvoiceNumber)
            .FirstOrDefault();

        return GenerateNextCode(prefix, lastInvoice?.InvoiceNumber);
    }

    private static string GenerateNextCode(string prefix, string lastCode)
    {
        if (string.IsNullOrEmpty(lastCode))
        {
            return $"{prefix}-001";
        }

        // Extract number from last code (e.g., "CUST-001" -> "001")
        var parts = lastCode.Split('-');
        if (parts.Length != 2 || !int.TryParse(parts[1], out int lastNumber))
        {
            return $"{prefix}-001";
        }

        var nextNumber = lastNumber + 1;
        return $"{prefix}-{nextNumber:D3}";
    }
}