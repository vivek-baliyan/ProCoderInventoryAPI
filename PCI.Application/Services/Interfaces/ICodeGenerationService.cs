namespace PCI.Application.Services.Interfaces;

public interface ICodeGenerationService
{
    Task<string> GenerateCustomerCodeAsync(int organisationId);
    Task<string> GenerateVendorCodeAsync(int organisationId);
    Task<string> GenerateSalesOrderNumberAsync(int organisationId);
    Task<string> GenerateInvoiceNumberAsync(int organisationId);
}