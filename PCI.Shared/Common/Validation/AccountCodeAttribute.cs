using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace PCI.Shared.Common.Validation;

public class AccountCodeAttribute : ValidationAttribute
{
    private static readonly Regex AccountCodePattern = new(@"^[0-9A-Z-]+$", RegexOptions.Compiled);

    public AccountCodeAttribute() : base("Account code must contain only numbers, uppercase letters, and hyphens.")
    {
    }

    public override bool IsValid(object value)
    {
        if (value == null)
            return true; // Let [Required] handle null validation

        if (value is not string accountCode)
            return false;

        if (string.IsNullOrWhiteSpace(accountCode))
            return false;

        if (accountCode.Length > 20)
            return false;

        return AccountCodePattern.IsMatch(accountCode);
    }
}