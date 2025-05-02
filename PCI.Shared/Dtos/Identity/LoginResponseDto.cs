namespace PCI.Shared.Dtos.Identity;

public record LoginResponseDto
{
    public string UserId { get; init; }
    public string UserName { get; init; }
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string Email { get; init; }
    public string ProfileImageUrl { get; init; }
    public string AccessToken { get; init; }
    public string RefreshToken { get; init; }
    public List<string> UserRoles { get; init; } = [];
    public string LastLoginDevice { get; init; }
    public DateTime? LastLogin { get; set; }
    public DateTime? LastPasswordChange { get; set; }
}