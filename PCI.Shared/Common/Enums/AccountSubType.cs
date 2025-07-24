namespace PCI.Shared.Common.Enums;

public enum AccountSubType
{
    // Asset Sub-types
    Cash = 1,
    Bank = 2,
    AccountsReceivable = 3,
    Inventory = 4,
    OtherCurrentAsset = 5,
    FixedAsset = 6,
    AccumulatedDepreciation = 7,
    OtherAsset = 8,

    // Liability Sub-types
    AccountsPayable = 20,
    CreditCard = 21,
    TaxPayable = 22,
    OtherCurrentLiability = 23,
    LongTermLiability = 24,

    // Equity Sub-types
    OwnerEquity = 40,
    RetainedEarnings = 41,
    OpeningBalanceEquity = 42,

    // Income Sub-types
    SalesRevenue = 60,
    ServiceRevenue = 61,
    OtherIncome = 62,
    InterestIncome = 63,

    // Expense Sub-types
    CostOfGoodsSold = 80,
    OperatingExpense = 81,
    AdministrativeExpense = 82,
    SellingExpense = 83,
    InterestExpense = 84,
    TaxExpense = 85,
    OtherExpense = 86
}