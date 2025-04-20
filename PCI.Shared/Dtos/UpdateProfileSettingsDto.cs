namespace PCI.Shared.Dtos;

public record UpdateProfileSettingsDto
{
    public int ProfileId { get; init; }

    public string UserId { get; init; }

    public string CompanyName { get; init; }

    public string ContactPerson { get; init; }

    public string WebsiteUrl { get; init; }

    public string StreetAddress { get; init; }

    public string PostalCode { get; init; }

    public string City { get; init; }

    public string State { get; init; }

    public string Country { get; init; }
}
