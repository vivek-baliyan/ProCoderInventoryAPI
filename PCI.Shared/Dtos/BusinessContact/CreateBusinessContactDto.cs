using System.ComponentModel.DataAnnotations;
using PCI.Shared.Common.Enums;

namespace PCI.Shared.Dtos.BusinessContact;

public record CreateBusinessContactDto
{
    [Required(ErrorMessage = "Entity type is required")]
    [StringLength(50, ErrorMessage = "Entity type cannot exceed 50 characters")]
    public string EntityType { get; set; }

    [Required(ErrorMessage = "Entity ID is required")]
    public int EntityId { get; set; }

    public ContactType ContactType { get; set; } = ContactType.Primary;

    // Contact Person Details
    [StringLength(20, ErrorMessage = "Salutation cannot exceed 20 characters")]
    public string Salutation { get; set; }

    [StringLength(100, ErrorMessage = "First name cannot exceed 100 characters")]
    public string FirstName { get; set; }

    [StringLength(100, ErrorMessage = "Last name cannot exceed 100 characters")]
    public string LastName { get; set; }

    [StringLength(100, ErrorMessage = "Job title cannot exceed 100 characters")]
    public string JobTitle { get; set; }

    [StringLength(100, ErrorMessage = "Department cannot exceed 100 characters")]
    public string Department { get; set; }

    [StringLength(100, ErrorMessage = "Role cannot exceed 100 characters")]
    public string Role { get; set; }

    [StringLength(100, ErrorMessage = "Email cannot exceed 100 characters")]
    [EmailAddress(ErrorMessage = "Invalid email format")]
    public string Email { get; set; }

    [StringLength(20, ErrorMessage = "Work phone cannot exceed 20 characters")]
    [Phone(ErrorMessage = "Invalid work phone format")]
    public string WorkPhone { get; set; }

    [StringLength(20, ErrorMessage = "Mobile cannot exceed 20 characters")]
    [Phone(ErrorMessage = "Invalid mobile format")]
    public string Mobile { get; set; }

    [StringLength(20, ErrorMessage = "Extension cannot exceed 20 characters")]
    public string Extension { get; set; }

    [StringLength(100, ErrorMessage = "LinkedIn profile cannot exceed 100 characters")]
    public string LinkedInProfile { get; set; }

    public bool IsPrimary { get; set; } = false;

    public bool IsActive { get; set; } = true;

    [StringLength(500, ErrorMessage = "Notes cannot exceed 500 characters")]
    public string Notes { get; set; }
}