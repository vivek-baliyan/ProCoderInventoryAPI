namespace PCI.Shared.Dtos.Identity;

public record UpdateUserDetailsDto()
{
    public string UserId { get; init; }

    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string ProfileImageUrl { get; init; }
    public string Bio { get; init; }
    public DateTime? DateOfBirth { get; init; }
    public string PhoneNumber { get; init; }
    public string Address { get; init; }
    public string Country { get; init; }
    public string State { get; init; }
    public string City { get; init; }
    public string PostalCode { get; init; }
};
