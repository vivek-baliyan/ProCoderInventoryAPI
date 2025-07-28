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
        services.AddScoped<IImageService, ImageService>();
    }
}
