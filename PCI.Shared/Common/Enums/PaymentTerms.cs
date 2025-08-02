namespace PCI.Shared.Common.Enums;

public enum PaymentTerms
{
    DueOnReceipt = 1,
    Net15 = 2,
    Net30 = 3,
    Net45 = 4,
    Net60 = 5,
    Net90 = 6,
    COD = 7,          // Cash on Delivery
    CIA = 8,          // Cash in Advance
    EOM = 9,          // End of Month
    Custom = 10       // For non-standard terms
}