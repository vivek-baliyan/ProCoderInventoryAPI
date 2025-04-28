namespace PCI.Shared.Dtos;

public record RegisterUserDto
{
    public string Email { get; set; }
    public string Password { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public bool SignInAfterRegistration { get; set; } = true;
}