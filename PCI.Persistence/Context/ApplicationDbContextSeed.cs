using Microsoft.Extensions.Logging;
using PCI.Domain.Models;
using System.Text.Json;

namespace PCI.Persistence.Context;

public class ApplicationDbContextSeed
{
    public static async Task SeedAccountSubTypesAsync(ApplicationDbContext context, ILoggerFactory loggerFactory)
    {
        try
        {
            var path = Directory.GetCurrentDirectory();

            if (!context.Set<AccountSubType>().Any())
            {
                var accountSubTypesData = File.ReadAllText(path + @"/Data/accountSubTypes.json");

                var accountSubTypes = JsonSerializer.Deserialize<List<AccountSubType>>(accountSubTypesData);

                if (accountSubTypes != null && accountSubTypes.Any())
                {
                    await context.Set<AccountSubType>().AddRangeAsync(accountSubTypes);
                    await context.SaveChangesAsync();
                }
            }
        }
        catch (Exception ex)
        {
            var logger = loggerFactory.CreateLogger<ApplicationDbContextSeed>();
            logger.LogError(ex, "An error occurred while seeding AccountSubTypes");
        }
    }

    public static async Task SeedCurrenciesAsync(ApplicationDbContext context, ILoggerFactory loggerFactory)
    {
        try
        {
            var path = Directory.GetCurrentDirectory();

            if (!context.Set<Currency>().Any())
            {
                var currenciesData = File.ReadAllText(path + @"/Data/currencies.json");

                var currencies = JsonSerializer.Deserialize<List<Currency>>(currenciesData);

                if (currencies != null && currencies.Any())
                {
                    await context.Set<Currency>().AddRangeAsync(currencies);
                    await context.SaveChangesAsync();
                }
            }
        }
        catch (Exception ex)
        {
            var logger = loggerFactory.CreateLogger<ApplicationDbContextSeed>();
            logger.LogError(ex, "An error occurred while seeding Currencies");
        }
    }

    public static async Task SeedUnitOfMeasuresAsync(ApplicationDbContext context, ILoggerFactory loggerFactory)
    {
        try
        {
            var path = Directory.GetCurrentDirectory();

            if (!context.Set<UnitOfMeasure>().Any())
            {
                var unitOfMeasuresData = File.ReadAllText(path + @"/Data/unitOfMeasures.json");

                var unitOfMeasures = JsonSerializer.Deserialize<List<UnitOfMeasure>>(unitOfMeasuresData);

                if (unitOfMeasures != null && unitOfMeasures.Any())
                {
                    await context.Set<UnitOfMeasure>().AddRangeAsync(unitOfMeasures);
                    await context.SaveChangesAsync();
                }
            }
        }
        catch (Exception ex)
        {
            var logger = loggerFactory.CreateLogger<ApplicationDbContextSeed>();
            logger.LogError(ex, "An error occurred while seeding UnitOfMeasures");
        }
    }

    public static async Task SeedBrandsAsync(ApplicationDbContext context, ILoggerFactory loggerFactory)
    {
        try
        {
            var path = Directory.GetCurrentDirectory();

            if (!context.Set<Brand>().Any())
            {
                var brandsData = File.ReadAllText(path + @"/Data/brands.json");

                var brands = JsonSerializer.Deserialize<List<Brand>>(brandsData);

                if (brands != null && brands.Any())
                {
                    await context.Set<Brand>().AddRangeAsync(brands);
                    await context.SaveChangesAsync();
                }
            }
        }
        catch (Exception ex)
        {
            var logger = loggerFactory.CreateLogger<ApplicationDbContextSeed>();
            logger.LogError(ex, "An error occurred while seeding Brands");
        }
    }

    public static async Task SeedTaxClassificationsAsync(ApplicationDbContext context, ILoggerFactory loggerFactory)
    {
        try
        {
            var path = Directory.GetCurrentDirectory();

            if (!context.Set<TaxClassification>().Any())
            {
                var taxClassificationsData = File.ReadAllText(path + @"/Data/taxClassifications.json");

                var taxClassifications = JsonSerializer.Deserialize<List<TaxClassification>>(taxClassificationsData);

                if (taxClassifications != null && taxClassifications.Any())
                {
                    await context.Set<TaxClassification>().AddRangeAsync(taxClassifications);
                    await context.SaveChangesAsync();
                }
            }
        }
        catch (Exception ex)
        {
            var logger = loggerFactory.CreateLogger<ApplicationDbContextSeed>();
            logger.LogError(ex, "An error occurred while seeding TaxClassifications");
        }
    }
}
