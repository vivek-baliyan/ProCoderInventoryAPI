using Microsoft.Extensions.DependencyInjection;
using PCI.Application.Mappings;
using PCI.Application.Services.Implementations;
using PCI.Application.Services.Interfaces;

namespace PCI.Application;

public static class ServiceExtensions
{
    public static void ConfigureApplication(this IServiceCollection services)
    {
        ApplicationMapsterMappings.Configure();

        services.AddScoped<IIdentityService, IdentityService>();
        services.AddScoped<ISessionManagementService, SessionManagementService>();

        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IOrganisationService, OrganisationService>();
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<ICustomerService, CustomerService>();
        services.AddScoped<IVendorService, VendorService>();
        services.AddScoped<ISalesOrderService, SalesOrderService>();
        services.AddScoped<IImageService, ImageService>();
        
        // Master Data Services
        services.AddScoped<IBrandService, BrandService>();
        services.AddScoped<ICountryService, CountryService>();
        services.AddScoped<IStateService, StateService>();
        services.AddScoped<ICurrencyService, CurrencyService>();
        services.AddScoped<IUnitOfMeasureService, UnitOfMeasureService>();
        services.AddScoped<ITaxClassificationService, TaxClassificationService>();
        services.AddScoped<ITaxMasterService, TaxMasterService>();
        services.AddScoped<IHSNMasterService, HSNMasterService>();
        services.AddScoped<ICountryDataSeederService, CountryDataSeederService>();
    }
}
