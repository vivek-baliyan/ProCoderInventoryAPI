namespace PCI.Shared.Dtos;

public record UserDto
{
    public string Id { get; init; }

    public string Email { get; init; }

    public string PhoneNumber { get; init; }

    public string UserName { get; init; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string ProfileImageUrl { get; set; }

    public DateTime? DateOfBirth { get; set; }

    public string Country { get; set; }

    public string Address { get; set; }

    public string City { get; set; }

    public string State { get; set; }

    public string PostalCode { get; set; }

    public string Bio { get; set; }

    public bool IsDeleted { get; init; }

    public DateTime? LastLogin { get; init; }

    public string LastLoginDevice { get; init; }

    public DateTime? LastPasswordChange { get; init; }

    public List<string> Roles { get; init; } = [];

    public int OrganisationId { get; init; }

    //Sessions: Count = 0
}
