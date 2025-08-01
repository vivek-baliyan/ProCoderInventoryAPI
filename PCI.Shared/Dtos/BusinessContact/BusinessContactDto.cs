using PCI.Shared.Common.Enums;

namespace PCI.Shared.Dtos.BusinessContact;

public record BusinessContactDto
{
    public int Id { get; set; }
    public string EntityType { get; set; }
    public int EntityId { get; set; }
    public ContactType ContactType { get; set; }
    public string Salutation { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string JobTitle { get; set; }
    public string Department { get; set; }
    public string Role { get; set; }
    public string Email { get; set; }
    public string WorkPhone { get; set; }
    public string Mobile { get; set; }
    public string Extension { get; set; }
    public string LinkedInProfile { get; set; }
    public bool IsPrimary { get; set; }
    public bool IsActive { get; set; }
    public string Notes { get; set; }
}