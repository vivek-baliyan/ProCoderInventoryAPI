﻿namespace PCI.Shared.Dtos.Identity;

public record UserLoginDto
{
    public string Email { get; set; }
    public string Password { get; set; }
}