namespace PCI.Shared.Dtos;

public record OrganisationDto
{
    public int OrganisationId { get; init; }

    public string CompanyName { get; init; }

    public string ContactPerson { get; init; }

    public string PhoneNumber { get; init; }

    public string Email { get; init; }

    public string WebsiteUrl { get; init; }

    public string Address { get; init; }

    public string PostalCode { get; init; }

    public string City { get; init; }

    public string State { get; init; }

    public string Country { get; init; }
}
