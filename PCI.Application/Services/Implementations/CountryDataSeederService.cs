using Microsoft.Extensions.Logging;
using PCI.Application.Repositories;
using PCI.Application.Services.Interfaces;
using PCI.Domain.Models;
using PCI.Shared.Common;
using System.Text.Json;

namespace PCI.Application.Services.Implementations;

public class CountryDataSeederService(
    IUnitOfWork unitOfWork,
    IHttpClientFactory httpClientFactory,
    ILogger<CountryDataSeederService> logger) : ICountryDataSeederService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly HttpClient _httpClient = httpClientFactory.CreateClient();
    private readonly ILogger<CountryDataSeederService> _logger = logger;

    private const string CountriesApiUrl = "https://raw.githubusercontent.com/dr5hn/countries-states-cities-database/master/json/countries.json";
    private const string StatesApiUrl = "https://raw.githubusercontent.com/dr5hn/countries-states-cities-database/master/json/states.json";

    public async Task<ServiceResult<bool>> SeedCountriesAndStatesAsync()
    {
        try
        {
            _logger.LogInformation("Starting country and state data seeding...");

            // Check if data already exists
            var existingCountriesCount = await _unitOfWork.Repository<Country>().CountAsync();
            if (existingCountriesCount > 0)
            {
                _logger.LogInformation("Countries already exist in database. Skipping seeding.");
                return ServiceResult<bool>.Success(true);
            }

            // Download and seed countries
            var countriesResult = await SeedCountriesAsync();
            if (!countriesResult.Succeeded)
            {
                return countriesResult;
            }

            // Download and seed states
            var statesResult = await SeedStatesAsync();
            if (!statesResult.Succeeded)
            {
                return statesResult;
            }

            _logger.LogInformation("Country and state data seeding completed successfully.");
            return ServiceResult<bool>.Success(true);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during country and state data seeding");
            return ServiceResult<bool>.Error(new Problem("CountryDataSeederService.SeedCountriesAndStatesAsync", ex.Message));
        }
    }

    public async Task<ServiceResult<bool>> UpdateCountryDataAsync()
    {
        try
        {
            _logger.LogInformation("Starting country and state data update...");

            // Clear existing data
            var existingStates = await _unitOfWork.Repository<State>().GetAllAsync();
            var existingCountries = await _unitOfWork.Repository<Country>().GetAllAsync();

            foreach (var state in existingStates)
            {
                _unitOfWork.Repository<State>().Remove(state);
            }

            foreach (var country in existingCountries)
            {
                _unitOfWork.Repository<Country>().Remove(country);
            }

            await _unitOfWork.SaveChangesAsync();

            // Re-seed with fresh data
            var result = await SeedCountriesAndStatesAsync();

            _logger.LogInformation("Country and state data update completed successfully.");
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during country and state data update");
            return ServiceResult<bool>.Error(new Problem("CountryDataSeederService.UpdateCountryDataAsync", ex.Message));
        }
    }

    private async Task<ServiceResult<bool>> SeedCountriesAsync()
    {
        try
        {
            _logger.LogInformation("Downloading countries data...");

            var response = await _httpClient.GetStringAsync(CountriesApiUrl);
            var countriesData = JsonSerializer.Deserialize<List<CountryApiModel>>(response, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
            });

            if (countriesData == null || !countriesData.Any())
            {
                return ServiceResult<bool>.Error(new Problem("CountryDataSeederService.SeedCountriesAsync", "Failed to download countries data"));
            }

            _logger.LogInformation($"Downloaded {countriesData.Count} countries. Converting to domain models...");

            var countries = countriesData.Select(c => new Country
            {
                Name = c.Name,
                Iso2 = c.Iso2,
                Iso3 = c.Iso3,
                NumericCode = c.NumericCode,
                PhoneCode = c.Phonecode,
                Capital = c.Capital,
                Currency = c.Currency,
                CurrencyName = c.CurrencyName,
                CurrencySymbol = c.CurrencySymbol,
                Tld = c.Tld,
                Native = c.Native,
                Region = c.Region,
                Subregion = c.Subregion,
                Latitude = ParseDecimal(c.Latitude),
                Longitude = ParseDecimal(c.Longitude),
                Emoji = c.Emoji,
                EmojiU = c.EmojiU
            }).ToList();

            foreach (var country in countries)
            {
                _unitOfWork.Repository<Country>().Add(country);
            }

            await _unitOfWork.SaveChangesAsync();

            _logger.LogInformation($"Successfully seeded {countries.Count} countries.");
            return ServiceResult<bool>.Success(true);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error seeding countries data");
            return ServiceResult<bool>.Error(new Problem("CountryDataSeederService.SeedCountriesAsync", ex.Message));
        }
    }

    private async Task<ServiceResult<bool>> SeedStatesAsync()
    {
        try
        {
            _logger.LogInformation("Downloading states data...");

            var response = await _httpClient.GetStringAsync(StatesApiUrl);
            var statesData = JsonSerializer.Deserialize<List<StateApiModel>>(response, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
            });

            if (statesData == null || !statesData.Any())
            {
                return ServiceResult<bool>.Error(new Problem("CountryDataSeederService.SeedStatesAsync", "Failed to download states data"));
            }

            _logger.LogInformation($"Downloaded {statesData.Count} states. Converting to domain models...");

            // Get all countries to map states correctly
            var countries = await _unitOfWork.Repository<Country>().GetAllAsync();
            var countryLookup = countries.ToDictionary(c => c.Iso2, c => c.Id);

            var states = statesData.Where(s => countryLookup.ContainsKey(s.CountryCode))
                .Select(s => new State
                {
                    Name = s.Name,
                    StateCode = s.StateCode,
                    Type = s.Type,
                    CountryId = countryLookup[s.CountryCode],
                    Latitude = ParseDecimal(s.Latitude),
                    Longitude = ParseDecimal(s.Longitude)
                }).ToList();

            foreach (var state in states)
            {
                _unitOfWork.Repository<State>().Add(state);
            }

            await _unitOfWork.SaveChangesAsync();

            _logger.LogInformation($"Successfully seeded {states.Count} states.");
            return ServiceResult<bool>.Success(true);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error seeding states data");
            return ServiceResult<bool>.Error(new Problem("CountryDataSeederService.SeedStatesAsync", ex.Message));
        }
    }

    private static decimal? ParseDecimal(string value)
    {
        if (string.IsNullOrWhiteSpace(value) || !decimal.TryParse(value, out var result))
        {
            return null;
        }
        return result;
    }

    // API Models for JSON deserialization
    private class CountryApiModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Iso3 { get; set; } = string.Empty;
        public string Iso2 { get; set; } = string.Empty;
        public string NumericCode { get; set; } = string.Empty;
        public string Phonecode { get; set; } = string.Empty;
        public string Capital { get; set; } = string.Empty;
        public string Currency { get; set; } = string.Empty;
        public string CurrencyName { get; set; } = string.Empty;
        public string CurrencySymbol { get; set; } = string.Empty;
        public string Tld { get; set; } = string.Empty;
        public string Native { get; set; } = string.Empty;
        public string Region { get; set; } = string.Empty;
        public string Subregion { get; set; } = string.Empty;
        public string Latitude { get; set; } = string.Empty;
        public string Longitude { get; set; } = string.Empty;
        public string Emoji { get; set; } = string.Empty;
        public string EmojiU { get; set; } = string.Empty;
    }

    private class StateApiModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int CountryId { get; set; }
        public string CountryCode { get; set; } = string.Empty;
        public string CountryName { get; set; } = string.Empty;
        public string StateCode { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string Latitude { get; set; } = string.Empty;
        public string Longitude { get; set; } = string.Empty;
    }
}