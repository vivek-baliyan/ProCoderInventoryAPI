namespace PCI.Shared.Dtos;

public record UpdateProfileDto
{
    public int ProfileId { get; init; }

    public string UserId { get; init; }

    public string PhoneNumber { get; init; }

    public string FirstName { get; init; }

    public string LastName { get; init; }

    public string ProfileImageUrl { get; init; }

    public string Bio { get; init; }

    public string DateOfBirth { get; init; }
}
