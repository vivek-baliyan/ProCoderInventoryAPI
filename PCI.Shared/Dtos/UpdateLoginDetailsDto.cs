namespace PCI.Shared.Dtos;

public record UpdateLoginDetailsDto
{
    public string UserId { get; set; }
    public string Email { get; set; }
    public string CurrentPassword { get; set; }
    public string NewPassword { get; set; }
}