namespace PCI.Shared.Dtos;

public record UserProfileDetailDto : UserProfileDto
{
    public string Email { get; init; }

    public string PhoneNumber { get; init; }

    public string UserName { get; init; }
}
