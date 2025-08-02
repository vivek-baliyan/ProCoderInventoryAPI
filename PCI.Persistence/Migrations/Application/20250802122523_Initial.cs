using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PCI.Persistence.Migrations.Application
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "APP");

            migrationBuilder.CreateTable(
                name: "AccountSubType",
                schema: "APP",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Code = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    AccountType = table.Column<int>(type: "INTEGER", nullable: false),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: true),
                    DisplayOrder = table.Column<int>(type: "INTEGER", nullable: false, defaultValue: 0),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountSubType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Countries",
                schema: "APP",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Iso2 = table.Column<string>(type: "TEXT", maxLength: 2, nullable: true),
                    Iso3 = table.Column<string>(type: "TEXT", maxLength: 3, nullable: true),
                    NumericCode = table.Column<string>(type: "TEXT", maxLength: 3, nullable: true),
                    PhoneCode = table.Column<string>(type: "TEXT", maxLength: 10, nullable: true),
                    Capital = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    Currency = table.Column<string>(type: "TEXT", maxLength: 3, nullable: true),
                    CurrencyName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    CurrencySymbol = table.Column<string>(type: "TEXT", maxLength: 5, nullable: true),
                    Tld = table.Column<string>(type: "TEXT", maxLength: 10, nullable: true),
                    Native = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    Region = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    Subregion = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    Latitude = table.Column<decimal>(type: "decimal(10,8)", nullable: true),
                    Longitude = table.Column<decimal>(type: "decimal(11,8)", nullable: true),
                    Emoji = table.Column<string>(type: "TEXT", maxLength: 10, nullable: true),
                    EmojiU = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Organisations",
                schema: "APP",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<string>(type: "TEXT", maxLength: 36, nullable: true),
                    CompanyName = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true),
                    ContactPerson = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true),
                    PhoneNumber = table.Column<string>(type: "TEXT", nullable: true),
                    Email = table.Column<string>(type: "TEXT", nullable: true),
                    WebsiteUrl = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
                    Address = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true),
                    Country = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    State = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    City = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    PostalCode = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organisations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "States",
                schema: "APP",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    StateCode = table.Column<string>(type: "TEXT", maxLength: 10, nullable: true),
                    Type = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    Latitude = table.Column<decimal>(type: "decimal(10,8)", nullable: true),
                    Longitude = table.Column<decimal>(type: "decimal(11,8)", nullable: true),
                    CountryId = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_States", x => x.Id);
                    table.ForeignKey(
                        name: "FK_States_Countries_CountryId",
                        column: x => x.CountryId,
                        principalSchema: "APP",
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Brand",
                schema: "APP",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    LogoUrl = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
                    OrganisationId = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brand", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Brand_Organisations_OrganisationId",
                        column: x => x.OrganisationId,
                        principalSchema: "APP",
                        principalTable: "Organisations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Currencies",
                schema: "APP",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Code = table.Column<string>(type: "TEXT", maxLength: 3, nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Symbol = table.Column<string>(type: "TEXT", maxLength: 10, nullable: true),
                    DecimalPlaces = table.Column<int>(type: "INTEGER", nullable: false),
                    ExchangeRate = table.Column<decimal>(type: "decimal(18,6)", nullable: false),
                    IsBaseCurrency = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
                    OrganisationId = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currencies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Currencies_Organisations_OrganisationId",
                        column: x => x.OrganisationId,
                        principalSchema: "APP",
                        principalTable: "Organisations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GLAccount",
                schema: "APP",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AccountCode = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    AccountName = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    AccountType = table.Column<int>(type: "INTEGER", nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: true),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: true),
                    IsSystemAccount = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                    AccountSubTypeId = table.Column<int>(type: "INTEGER", nullable: false),
                    ParentAccountId = table.Column<int>(type: "INTEGER", nullable: true),
                    OrganisationId = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GLAccount", x => x.Id);
                    table.CheckConstraint("CK_GLAccount_AccountCode_Format", "AccountCode - '^[0-9A-Z-]+$'");
                    table.CheckConstraint("CK_GLAccount_AccountCode_NotEmpty", "LENGTH(TRIM(AccountCode)) > 0");
                    table.CheckConstraint("CK_GLAccount_AccountName_NotEmpty", "LENGTH(TRIM(AccountName)) > 0");
                    table.CheckConstraint("CK_GLAccount_MaxDepth", "CASE \r\n                WHEN ParentAccountId IS NULL THEN 0\r\n                ELSE 1\r\n              END <= 1");
                    table.CheckConstraint("CK_GLAccount_NoSelfReference", "ParentAccountId IS NULL OR ParentAccountId != Id");
                    table.ForeignKey(
                        name: "FK_GLAccount_AccountSubType_AccountSubTypeId",
                        column: x => x.AccountSubTypeId,
                        principalSchema: "APP",
                        principalTable: "AccountSubType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GLAccount_GLAccount_ParentAccountId",
                        column: x => x.ParentAccountId,
                        principalSchema: "APP",
                        principalTable: "GLAccount",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GLAccount_Organisations_OrganisationId",
                        column: x => x.OrganisationId,
                        principalSchema: "APP",
                        principalTable: "Organisations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ItemGroup",
                schema: "APP",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    GroupName = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: true),
                    SKUPattern = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
                    OrganisationId = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemGroup", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemGroup_Organisations_OrganisationId",
                        column: x => x.OrganisationId,
                        principalSchema: "APP",
                        principalTable: "Organisations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PriceList",
                schema: "APP",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: true),
                    Type = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    PricingMethod = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    MarkupPercentage = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    Currency = table.Column<string>(type: "TEXT", maxLength: 10, nullable: true),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsDefault = table.Column<bool>(type: "INTEGER", nullable: false),
                    EffectiveDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ExpiryDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    OrganisationId = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PriceList", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PriceList_Organisations_OrganisationId",
                        column: x => x.OrganisationId,
                        principalSchema: "APP",
                        principalTable: "Organisations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductTags",
                schema: "APP",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    Color = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
                    OrganisationId = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductTags", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductTags_Organisations_OrganisationId",
                        column: x => x.OrganisationId,
                        principalSchema: "APP",
                        principalTable: "Organisations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TaxClassifications",
                schema: "APP",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Code = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false),
                    ClassificationType = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    DefaultTaxRate = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    Category = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true),
                    CountryCode = table.Column<string>(type: "TEXT", maxLength: 10, nullable: true),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
                    OrganisationId = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaxClassifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaxClassifications_Organisations_OrganisationId",
                        column: x => x.OrganisationId,
                        principalSchema: "APP",
                        principalTable: "Organisations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TaxMaster",
                schema: "APP",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TaxCode = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    TaxName = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    TaxRate = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: true),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
                    OrganisationId = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaxMaster", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaxMaster_Organisations_OrganisationId",
                        column: x => x.OrganisationId,
                        principalSchema: "APP",
                        principalTable: "Organisations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UnitOfMeasure",
                schema: "APP",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Code = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    UnitType = table.Column<int>(type: "INTEGER", nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
                    OrganisationId = table.Column<int>(type: "INTEGER", nullable: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnitOfMeasure", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UnitOfMeasure_Organisations_OrganisationId",
                        column: x => x.OrganisationId,
                        principalSchema: "APP",
                        principalTable: "Organisations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                schema: "APP",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CustomerCode = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    DisplayName = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    CompanyName = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true),
                    WebsiteUrl = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true),
                    CustomerType = table.Column<int>(type: "INTEGER", nullable: false, defaultValue: 1),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: true),
                    OrganisationId = table.Column<int>(type: "INTEGER", nullable: false),
                    CurrencyId = table.Column<int>(type: "INTEGER", nullable: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Customers_Currencies_CurrencyId",
                        column: x => x.CurrencyId,
                        principalSchema: "APP",
                        principalTable: "Currencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Customers_Organisations_OrganisationId",
                        column: x => x.OrganisationId,
                        principalSchema: "APP",
                        principalTable: "Organisations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Vendors",
                schema: "APP",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    VendorCode = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    VendorName = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    CompanyName = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true),
                    WebsiteUrl = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true),
                    VendorType = table.Column<int>(type: "INTEGER", nullable: false, defaultValue: 1),
                    Category = table.Column<int>(type: "INTEGER", nullable: false, defaultValue: 1),
                    Status = table.Column<int>(type: "INTEGER", nullable: false, defaultValue: 1),
                    Industry = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    ParentVendorId = table.Column<int>(type: "INTEGER", nullable: true),
                    IsManufacturer = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                    IsDropshipVendor = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                    CurrencyId = table.Column<int>(type: "INTEGER", nullable: true),
                    PortalAccessEmail = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    HasPortalAccess = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                    PreferredCommunicationMethod = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true, defaultValue: "Email"),
                    RequiresPOApproval = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                    StatusChangedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    StatusChangeReason = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    Notes = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: true),
                    OrganisationId = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vendors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vendors_Currencies_CurrencyId",
                        column: x => x.CurrencyId,
                        principalSchema: "APP",
                        principalTable: "Currencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Vendors_Organisations_OrganisationId",
                        column: x => x.OrganisationId,
                        principalSchema: "APP",
                        principalTable: "Organisations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Vendors_Vendors_ParentVendorId",
                        column: x => x.ParentVendorId,
                        principalSchema: "APP",
                        principalTable: "Vendors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "ItemAttribute",
                schema: "APP",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AttributeName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    DisplayOrder = table.Column<int>(type: "INTEGER", nullable: false),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
                    ItemGroupId = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemAttribute", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemAttribute_ItemGroup_ItemGroupId",
                        column: x => x.ItemGroupId,
                        principalSchema: "APP",
                        principalTable: "ItemGroup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CustomerAddresses",
                schema: "APP",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CustomerId = table.Column<int>(type: "INTEGER", nullable: false),
                    AddressType = table.Column<int>(type: "INTEGER", nullable: false, defaultValue: 1),
                    AddressLine1 = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true),
                    AddressLine2 = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true),
                    City = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    State = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    PostalCode = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    Country = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    IsPrimary = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerAddresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerAddresses_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalSchema: "APP",
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CustomerBankInfos",
                schema: "APP",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CustomerId = table.Column<int>(type: "INTEGER", nullable: false),
                    BankName = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true),
                    AccountNumber = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    AccountHolderName = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true),
                    IFSCCode = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    SWIFTCode = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    BranchAddress = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    IsPrimary = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerBankInfos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerBankInfos_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalSchema: "APP",
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CustomerContacts",
                schema: "APP",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CustomerId = table.Column<int>(type: "INTEGER", nullable: false),
                    ContactType = table.Column<int>(type: "INTEGER", nullable: false, defaultValue: 1),
                    Salutation = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    FirstName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    LastName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    Email = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    PhoneNumber = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    MobileNumber = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    IsPrimary = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerContacts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerContacts_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalSchema: "APP",
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CustomerDocuments",
                schema: "APP",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CustomerId = table.Column<int>(type: "INTEGER", nullable: false),
                    FileName = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    OriginalFileName = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    FileExtension = table.Column<string>(type: "TEXT", maxLength: 10, nullable: true),
                    ContentType = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    FileSizeBytes = table.Column<long>(type: "INTEGER", nullable: false),
                    FilePath = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false),
                    DocumentType = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true, defaultValue: "Other"),
                    Description = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    UploadedDate = table.Column<DateTime>(type: "TEXT", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    UploadedBy = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: true),
                    Notes = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerDocuments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerDocuments_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalSchema: "APP",
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CustomerFinancials",
                schema: "APP",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CustomerId = table.Column<int>(type: "INTEGER", nullable: false),
                    CurrentBalance = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m),
                    CreditLimit = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m),
                    OutstandingAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m),
                    PaymentTermDays = table.Column<int>(type: "INTEGER", nullable: false, defaultValue: 30),
                    PaymentTerms = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true, defaultValue: "Due on Receipt"),
                    TotalSalesYTD = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m),
                    TotalSalesLifetime = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m),
                    MinimumOrderValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m),
                    LastSaleDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    LastPaymentDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    PreferredPaymentMethod = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true, defaultValue: "BankTransfer"),
                    IsOnCreditHold = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                    CreditHoldReason = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    CreditReviewDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    DefaultDiscountPercentage = table.Column<decimal>(type: "decimal(5,2)", nullable: false, defaultValue: 0m),
                    Notes = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerFinancials", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerFinancials_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalSchema: "APP",
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CustomerPriceList",
                schema: "APP",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CustomerId = table.Column<int>(type: "INTEGER", nullable: false),
                    PriceListId = table.Column<int>(type: "INTEGER", nullable: false),
                    IsDefault = table.Column<bool>(type: "INTEGER", nullable: false),
                    EffectiveDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ExpiryDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerPriceList", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerPriceList_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalSchema: "APP",
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CustomerPriceList_PriceList_PriceListId",
                        column: x => x.PriceListId,
                        principalSchema: "APP",
                        principalTable: "PriceList",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CustomerTaxInfos",
                schema: "APP",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CustomerId = table.Column<int>(type: "INTEGER", nullable: false),
                    TaxType = table.Column<int>(type: "INTEGER", nullable: false, defaultValue: 3),
                    TaxNumber = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    IsPrimary = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerTaxInfos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerTaxInfos_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalSchema: "APP",
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SalesOrders",
                schema: "APP",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    OrderNumber = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    OrderDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Status = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true, defaultValue: "Draft"),
                    ExpectedDeliveryDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ReferenceNumber = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    QuoteNumber = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    CustomerId = table.Column<int>(type: "INTEGER", nullable: false),
                    OrganisationId = table.Column<int>(type: "INTEGER", nullable: false),
                    PriceListId = table.Column<int>(type: "INTEGER", nullable: true),
                    SubTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m),
                    TaxAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m),
                    Notes = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: true),
                    BillingAddress = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    ConfirmedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    PackedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ShippedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    DeliveredDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesOrders", x => x.Id);
                    table.CheckConstraint("CK_SalesOrder_SubTotal_NonNegative", "SubTotal >= 0");
                    table.CheckConstraint("CK_SalesOrder_TaxAmount_NonNegative", "TaxAmount >= 0");
                    table.CheckConstraint("CK_SalesOrder_TotalAmount_NonNegative", "TotalAmount >= 0");
                    table.ForeignKey(
                        name: "FK_SalesOrders_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalSchema: "APP",
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SalesOrders_Organisations_OrganisationId",
                        column: x => x.OrganisationId,
                        principalSchema: "APP",
                        principalTable: "Organisations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SalesOrders_PriceList_PriceListId",
                        column: x => x.PriceListId,
                        principalSchema: "APP",
                        principalTable: "PriceList",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "BusinessAddresses",
                schema: "APP",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    EntityType = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    EntityId = table.Column<int>(type: "INTEGER", nullable: false),
                    AddressType = table.Column<int>(type: "INTEGER", nullable: false, defaultValue: 1),
                    AddressLabel = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    AddressLine1 = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false),
                    AddressLine2 = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    City = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    State = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    PostalCode = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    Country = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    Region = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    IsDefault = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: true),
                    Notes = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    VendorId = table.Column<int>(type: "INTEGER", nullable: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessAddresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BusinessAddresses_Vendors_VendorId",
                        column: x => x.VendorId,
                        principalSchema: "APP",
                        principalTable: "Vendors",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BusinessBankInfos",
                schema: "APP",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    EntityType = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    EntityId = table.Column<int>(type: "INTEGER", nullable: false),
                    BankAccountNumber = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    BankName = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    BankBranch = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true),
                    IFSCCode = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    SWIFTCode = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    AccountHolderName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    AccountType = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    BankAddress = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    IsPrimary = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: true),
                    IsVerified = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                    VerificationDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    VerifiedBy = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    Notes = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    VendorId = table.Column<int>(type: "INTEGER", nullable: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessBankInfos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BusinessBankInfos_Vendors_VendorId",
                        column: x => x.VendorId,
                        principalSchema: "APP",
                        principalTable: "Vendors",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BusinessContacts",
                schema: "APP",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    EntityType = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    EntityId = table.Column<int>(type: "INTEGER", nullable: false),
                    ContactType = table.Column<int>(type: "INTEGER", nullable: false, defaultValue: 1),
                    Salutation = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    FirstName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    LastName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    Email = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    PhoneNumber = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    MobileNumber = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    IsPrimary = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: true),
                    VendorId = table.Column<int>(type: "INTEGER", nullable: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessContacts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BusinessContacts_Vendors_VendorId",
                        column: x => x.VendorId,
                        principalSchema: "APP",
                        principalTable: "Vendors",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BusinessTaxInfos",
                schema: "APP",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    EntityType = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    EntityId = table.Column<int>(type: "INTEGER", nullable: false),
                    TaxType = table.Column<int>(type: "INTEGER", nullable: false),
                    TaxNumber = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: true),
                    IsPrimary = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                    Notes = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    VendorId = table.Column<int>(type: "INTEGER", nullable: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessTaxInfos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BusinessTaxInfos_Vendors_VendorId",
                        column: x => x.VendorId,
                        principalSchema: "APP",
                        principalTable: "Vendors",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Products",
                schema: "APP",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SKU = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    ProductType = table.Column<int>(type: "INTEGER", nullable: false, defaultValue: 1),
                    Status = table.Column<int>(type: "INTEGER", nullable: false, defaultValue: 1),
                    IsReturnable = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: true),
                    TrackInventory = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: true),
                    BrandId = table.Column<int>(type: "INTEGER", nullable: true),
                    ManufacturerPartNumber = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    UPC = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    EAN = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    ISBN = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    SalesAccountId = table.Column<int>(type: "INTEGER", nullable: true),
                    PurchaseAccountId = table.Column<int>(type: "INTEGER", nullable: true),
                    InventoryAccountId = table.Column<int>(type: "INTEGER", nullable: true),
                    SellingPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CostPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    OrganisationId = table.Column<int>(type: "INTEGER", nullable: false),
                    CurrencyId = table.Column<int>(type: "INTEGER", nullable: true),
                    CurrencyId1 = table.Column<int>(type: "INTEGER", nullable: true),
                    TaxMasterId = table.Column<int>(type: "INTEGER", nullable: true),
                    UnitOfMeasureId = table.Column<int>(type: "INTEGER", nullable: true),
                    UnitOfMeasureId1 = table.Column<int>(type: "INTEGER", nullable: true),
                    UnitOfMeasureId2 = table.Column<int>(type: "INTEGER", nullable: true),
                    VendorId = table.Column<int>(type: "INTEGER", nullable: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.CheckConstraint("CK_Product_CostPrice_NonNegative", "CostPrice IS NULL OR CostPrice >= 0");
                    table.CheckConstraint("CK_Product_SellingPrice_NonNegative", "SellingPrice IS NULL OR SellingPrice >= 0");
                    table.ForeignKey(
                        name: "FK_Products_Brand_BrandId",
                        column: x => x.BrandId,
                        principalSchema: "APP",
                        principalTable: "Brand",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Products_Currencies_CurrencyId",
                        column: x => x.CurrencyId,
                        principalSchema: "APP",
                        principalTable: "Currencies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Products_Currencies_CurrencyId1",
                        column: x => x.CurrencyId1,
                        principalSchema: "APP",
                        principalTable: "Currencies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Products_GLAccount_InventoryAccountId",
                        column: x => x.InventoryAccountId,
                        principalSchema: "APP",
                        principalTable: "GLAccount",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Products_GLAccount_PurchaseAccountId",
                        column: x => x.PurchaseAccountId,
                        principalSchema: "APP",
                        principalTable: "GLAccount",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Products_GLAccount_SalesAccountId",
                        column: x => x.SalesAccountId,
                        principalSchema: "APP",
                        principalTable: "GLAccount",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Products_Organisations_OrganisationId",
                        column: x => x.OrganisationId,
                        principalSchema: "APP",
                        principalTable: "Organisations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Products_TaxMaster_TaxMasterId",
                        column: x => x.TaxMasterId,
                        principalSchema: "APP",
                        principalTable: "TaxMaster",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Products_UnitOfMeasure_UnitOfMeasureId",
                        column: x => x.UnitOfMeasureId,
                        principalSchema: "APP",
                        principalTable: "UnitOfMeasure",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Products_UnitOfMeasure_UnitOfMeasureId1",
                        column: x => x.UnitOfMeasureId1,
                        principalSchema: "APP",
                        principalTable: "UnitOfMeasure",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Products_UnitOfMeasure_UnitOfMeasureId2",
                        column: x => x.UnitOfMeasureId2,
                        principalSchema: "APP",
                        principalTable: "UnitOfMeasure",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Products_Vendors_VendorId",
                        column: x => x.VendorId,
                        principalSchema: "APP",
                        principalTable: "Vendors",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "VendorDocuments",
                schema: "APP",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    VendorId = table.Column<int>(type: "INTEGER", nullable: false),
                    DocumentName = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    DocumentType = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    FilePath = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false),
                    FileExtension = table.Column<string>(type: "TEXT", maxLength: 10, nullable: true),
                    FileSizeBytes = table.Column<long>(type: "INTEGER", nullable: false),
                    ExpiryDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: true),
                    IsConfidential = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                    UploadedBy = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    UploadedOn = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Notes = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VendorDocuments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VendorDocuments_Vendors_VendorId",
                        column: x => x.VendorId,
                        principalSchema: "APP",
                        principalTable: "Vendors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VendorFinancials",
                schema: "APP",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    VendorId = table.Column<int>(type: "INTEGER", nullable: false),
                    CurrentBalance = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m),
                    OutstandingAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m),
                    PaymentTermDays = table.Column<int>(type: "INTEGER", nullable: false, defaultValue: 30),
                    TotalPurchasesYTD = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m),
                    TotalPurchasesLifetime = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m),
                    MinimumOrderValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m),
                    LastPurchaseDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    LastPaymentDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    PreferredPaymentMethod = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true, defaultValue: "BankTransfer"),
                    OnTimeDeliveryRate = table.Column<decimal>(type: "decimal(3,2)", nullable: false, defaultValue: 0m),
                    QualityRating = table.Column<decimal>(type: "decimal(3,2)", nullable: false, defaultValue: 0m),
                    EarlyPaymentDiscountPercentage = table.Column<decimal>(type: "decimal(5,2)", nullable: false, defaultValue: 0m),
                    EarlyPaymentDiscountDays = table.Column<int>(type: "INTEGER", nullable: false, defaultValue: 10),
                    IsBlacklisted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                    BlacklistReason = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    LastReviewDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Notes = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VendorFinancials", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VendorFinancials_Vendors_VendorId",
                        column: x => x.VendorId,
                        principalSchema: "APP",
                        principalTable: "Vendors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VendorPerformances",
                schema: "APP",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    VendorId = table.Column<int>(type: "INTEGER", nullable: false),
                    PerformanceRating = table.Column<decimal>(type: "decimal(3,2)", nullable: false, defaultValue: 0m),
                    OnTimeDeliveryPercentage = table.Column<int>(type: "INTEGER", nullable: false, defaultValue: 0),
                    QualityRating = table.Column<int>(type: "INTEGER", nullable: false, defaultValue: 0),
                    CommunicationRating = table.Column<int>(type: "INTEGER", nullable: false, defaultValue: 0),
                    PriceCompetitivenessRating = table.Column<int>(type: "INTEGER", nullable: false, defaultValue: 0),
                    LastPerformanceReview = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ReviewPeriodStart = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ReviewPeriodEnd = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ReviewedBy = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    TotalOrdersInPeriod = table.Column<int>(type: "INTEGER", nullable: false, defaultValue: 0),
                    OnTimeDeliveries = table.Column<int>(type: "INTEGER", nullable: false, defaultValue: 0),
                    LateDeliveries = table.Column<int>(type: "INTEGER", nullable: false, defaultValue: 0),
                    QualityIssues = table.Column<int>(type: "INTEGER", nullable: false, defaultValue: 0),
                    ResolvedComplaints = table.Column<int>(type: "INTEGER", nullable: false, defaultValue: 0),
                    UnresolvedComplaints = table.Column<int>(type: "INTEGER", nullable: false, defaultValue: 0),
                    IsPreferredVendor = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                    IsBlacklisted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                    BlacklistReason = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    BlacklistDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    BlacklistedBy = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    StrengthsNoted = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: true),
                    AreasForImprovement = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: true),
                    ActionPlan = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: true),
                    Notes = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VendorPerformances", x => x.Id);
                    table.CheckConstraint("CK_VendorPerformance_CommunicationRating_Range", "CommunicationRating >= 0 AND CommunicationRating <= 100");
                    table.CheckConstraint("CK_VendorPerformance_OnTimeDeliveryPercentage_Range", "OnTimeDeliveryPercentage >= 0 AND OnTimeDeliveryPercentage <= 100");
                    table.CheckConstraint("CK_VendorPerformance_PerformanceRating_Range", "PerformanceRating >= 0 AND PerformanceRating <= 5");
                    table.CheckConstraint("CK_VendorPerformance_PriceCompetitivenessRating_Range", "PriceCompetitivenessRating >= 0 AND PriceCompetitivenessRating <= 100");
                    table.CheckConstraint("CK_VendorPerformance_QualityRating_Range", "QualityRating >= 0 AND QualityRating <= 100");
                    table.CheckConstraint("CK_VendorPerformance_ReviewPeriod_Valid", "ReviewPeriodEnd >= ReviewPeriodStart");
                    table.ForeignKey(
                        name: "FK_VendorPerformances_Vendors_VendorId",
                        column: x => x.VendorId,
                        principalSchema: "APP",
                        principalTable: "Vendors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VendorPriceList",
                schema: "APP",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    VendorId = table.Column<int>(type: "INTEGER", nullable: false),
                    PriceListId = table.Column<int>(type: "INTEGER", nullable: false),
                    IsDefault = table.Column<bool>(type: "INTEGER", nullable: false),
                    EffectiveDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ExpiryDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VendorPriceList", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VendorPriceList_PriceList_PriceListId",
                        column: x => x.PriceListId,
                        principalSchema: "APP",
                        principalTable: "PriceList",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VendorPriceList_Vendors_VendorId",
                        column: x => x.VendorId,
                        principalSchema: "APP",
                        principalTable: "Vendors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ItemAttributeOption",
                schema: "APP",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    OptionValue = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    DisplayOrder = table.Column<int>(type: "INTEGER", nullable: false),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
                    ItemAttributeId = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemAttributeOption", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemAttributeOption_ItemAttribute_ItemAttributeId",
                        column: x => x.ItemAttributeId,
                        principalSchema: "APP",
                        principalTable: "ItemAttribute",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Invoices",
                schema: "APP",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    InvoiceNumber = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    InvoiceDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DueDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Status = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true, defaultValue: "Draft"),
                    CustomerId = table.Column<int>(type: "INTEGER", nullable: false),
                    OrganisationId = table.Column<int>(type: "INTEGER", nullable: false),
                    SalesOrderId = table.Column<int>(type: "INTEGER", nullable: true),
                    SubTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TaxAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AmountPaid = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m),
                    AmountDue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Notes = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: true),
                    PaymentTerms = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    PriceListId = table.Column<int>(type: "INTEGER", nullable: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Invoices_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalSchema: "APP",
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Invoices_Organisations_OrganisationId",
                        column: x => x.OrganisationId,
                        principalSchema: "APP",
                        principalTable: "Organisations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Invoices_PriceList_PriceListId",
                        column: x => x.PriceListId,
                        principalSchema: "APP",
                        principalTable: "PriceList",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Invoices_SalesOrders_SalesOrderId",
                        column: x => x.SalesOrderId,
                        principalSchema: "APP",
                        principalTable: "SalesOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "PurchaseOrder",
                schema: "APP",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PONumber = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    OrderDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Status = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    ExpectedDeliveryDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    VendorId = table.Column<int>(type: "INTEGER", nullable: false),
                    OrganisationId = table.Column<int>(type: "INTEGER", nullable: false),
                    PriceListId = table.Column<int>(type: "INTEGER", nullable: true),
                    SubTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TaxAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Notes = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: true),
                    DeliveryAddress = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    PaymentTerms = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    ConfirmedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ReceivedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    OriginatingSalesOrderId = table.Column<int>(type: "INTEGER", nullable: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseOrder", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PurchaseOrder_Organisations_OrganisationId",
                        column: x => x.OrganisationId,
                        principalSchema: "APP",
                        principalTable: "Organisations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PurchaseOrder_PriceList_PriceListId",
                        column: x => x.PriceListId,
                        principalSchema: "APP",
                        principalTable: "PriceList",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PurchaseOrder_SalesOrders_OriginatingSalesOrderId",
                        column: x => x.OriginatingSalesOrderId,
                        principalSchema: "APP",
                        principalTable: "SalesOrders",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PurchaseOrder_Vendors_VendorId",
                        column: x => x.VendorId,
                        principalSchema: "APP",
                        principalTable: "Vendors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SalesOrderApprovals",
                schema: "APP",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SalesOrderId = table.Column<int>(type: "INTEGER", nullable: false),
                    ApprovalStatus = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true, defaultValue: "Pending"),
                    ApprovedBy = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    ApprovedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ApprovalNotes = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: true),
                    RejectionReason = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesOrderApprovals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SalesOrderApprovals_SalesOrders_SalesOrderId",
                        column: x => x.SalesOrderId,
                        principalSchema: "APP",
                        principalTable: "SalesOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SalesOrderDocuments",
                schema: "APP",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SalesOrderId = table.Column<int>(type: "INTEGER", nullable: false),
                    DocumentName = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    DocumentType = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    FilePath = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false),
                    FileExtension = table.Column<string>(type: "TEXT", maxLength: 10, nullable: true),
                    FileSizeBytes = table.Column<long>(type: "INTEGER", nullable: false),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: true),
                    IsConfidential = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                    UploadedBy = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    UploadedOn = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Notes = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesOrderDocuments", x => x.Id);
                    table.CheckConstraint("CK_SalesOrderDocument_FileSizeBytes_Positive", "FileSizeBytes > 0");
                    table.ForeignKey(
                        name: "FK_SalesOrderDocuments_SalesOrders_SalesOrderId",
                        column: x => x.SalesOrderId,
                        principalSchema: "APP",
                        principalTable: "SalesOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SalesOrderPayments",
                schema: "APP",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SalesOrderId = table.Column<int>(type: "INTEGER", nullable: false),
                    PaymentStatus = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true, defaultValue: "Pending"),
                    PaymentTerms = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true, defaultValue: "Net30"),
                    DueDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    PaidAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m),
                    BalanceAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m),
                    PaymentMethod = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    PaymentReference = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    PaymentDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    PaymentNotes = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesOrderPayments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SalesOrderPayments_SalesOrders_SalesOrderId",
                        column: x => x.SalesOrderId,
                        principalSchema: "APP",
                        principalTable: "SalesOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SalesOrderShippings",
                schema: "APP",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SalesOrderId = table.Column<int>(type: "INTEGER", nullable: false),
                    ShippingMethod = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    TrackingNumber = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    ShippingCost = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m),
                    CarrierName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    EstimatedDeliveryDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ActualDeliveryDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ShippingAddress = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    ShippingStatus = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true, defaultValue: "Pending"),
                    ShippingNotes = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    IsDropShipment = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesOrderShippings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SalesOrderShippings_SalesOrders_SalesOrderId",
                        column: x => x.SalesOrderId,
                        principalSchema: "APP",
                        principalTable: "SalesOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PriceListItem",
                schema: "APP",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PriceListId = table.Column<int>(type: "INTEGER", nullable: false),
                    ProductId = table.Column<int>(type: "INTEGER", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MinQuantity = table.Column<int>(type: "INTEGER", nullable: true),
                    MaxQuantity = table.Column<int>(type: "INTEGER", nullable: true),
                    DiscountPercentage = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    EffectiveDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ExpiryDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PriceListItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PriceListItem_PriceList_PriceListId",
                        column: x => x.PriceListId,
                        principalSchema: "APP",
                        principalTable: "PriceList",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PriceListItem_Products_ProductId",
                        column: x => x.ProductId,
                        principalSchema: "APP",
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductImages",
                schema: "APP",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ProductId = table.Column<int>(type: "INTEGER", nullable: false),
                    ImagePath = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    IsMain = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductImages_Products_ProductId",
                        column: x => x.ProductId,
                        principalSchema: "APP",
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductInventory",
                schema: "APP",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ProductId = table.Column<int>(type: "INTEGER", nullable: false),
                    UnitOfMeasureId = table.Column<int>(type: "INTEGER", nullable: true),
                    ReorderLevel = table.Column<int>(type: "INTEGER", nullable: true),
                    ReorderQuantity = table.Column<int>(type: "INTEGER", nullable: true),
                    MinimumStock = table.Column<int>(type: "INTEGER", nullable: true),
                    MaximumStock = table.Column<int>(type: "INTEGER", nullable: true),
                    QuantityOnHand = table.Column<int>(type: "INTEGER", nullable: false),
                    OpeningStock = table.Column<int>(type: "INTEGER", nullable: true),
                    OpeningStockValue = table.Column<decimal>(type: "TEXT", nullable: true),
                    AverageCost = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    IsSaleable = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsPurchasable = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsReturnable = table.Column<bool>(type: "INTEGER", nullable: false),
                    VendorId = table.Column<int>(type: "INTEGER", nullable: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductInventory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductInventory_Products_ProductId",
                        column: x => x.ProductId,
                        principalSchema: "APP",
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductInventory_UnitOfMeasure_UnitOfMeasureId",
                        column: x => x.UnitOfMeasureId,
                        principalSchema: "APP",
                        principalTable: "UnitOfMeasure",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProductInventory_Vendors_VendorId",
                        column: x => x.VendorId,
                        principalSchema: "APP",
                        principalTable: "Vendors",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProductItemGroups",
                schema: "APP",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ProductId = table.Column<int>(type: "INTEGER", nullable: false),
                    ItemGroupId = table.Column<int>(type: "INTEGER", nullable: false),
                    IsPrimary = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductItemGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductItemGroups_ItemGroup_ItemGroupId",
                        column: x => x.ItemGroupId,
                        principalSchema: "APP",
                        principalTable: "ItemGroup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductItemGroups_Products_ProductId",
                        column: x => x.ProductId,
                        principalSchema: "APP",
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductPhysical",
                schema: "APP",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ProductId = table.Column<int>(type: "INTEGER", nullable: false),
                    Weight = table.Column<decimal>(type: "decimal(10,3)", nullable: true),
                    WeightUnitId = table.Column<int>(type: "INTEGER", nullable: true),
                    Length = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    Width = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    Height = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    DimensionUnitId = table.Column<int>(type: "INTEGER", nullable: true),
                    Volume = table.Column<decimal>(type: "decimal(10,3)", nullable: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductPhysical", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductPhysical_Products_ProductId",
                        column: x => x.ProductId,
                        principalSchema: "APP",
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductPhysical_UnitOfMeasure_DimensionUnitId",
                        column: x => x.DimensionUnitId,
                        principalSchema: "APP",
                        principalTable: "UnitOfMeasure",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProductPhysical_UnitOfMeasure_WeightUnitId",
                        column: x => x.WeightUnitId,
                        principalSchema: "APP",
                        principalTable: "UnitOfMeasure",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProductTagAssignments",
                schema: "APP",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ProductId = table.Column<int>(type: "INTEGER", nullable: false),
                    ProductTagId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductTagAssignments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductTagAssignments_ProductTags_ProductTagId",
                        column: x => x.ProductTagId,
                        principalSchema: "APP",
                        principalTable: "ProductTags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductTagAssignments_Products_ProductId",
                        column: x => x.ProductId,
                        principalSchema: "APP",
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductTax",
                schema: "APP",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ProductId = table.Column<int>(type: "INTEGER", nullable: false),
                    TaxClassificationId = table.Column<int>(type: "INTEGER", nullable: true),
                    TaxMasterId = table.Column<int>(type: "INTEGER", nullable: true),
                    IsTaxExempt = table.Column<bool>(type: "INTEGER", nullable: false),
                    TaxExemptReason = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductTax", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductTax_Products_ProductId",
                        column: x => x.ProductId,
                        principalSchema: "APP",
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductTax_TaxClassifications_TaxClassificationId",
                        column: x => x.TaxClassificationId,
                        principalSchema: "APP",
                        principalTable: "TaxClassifications",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProductTax_TaxMaster_TaxMasterId",
                        column: x => x.TaxMasterId,
                        principalSchema: "APP",
                        principalTable: "TaxMaster",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SalesOrderItem",
                schema: "APP",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SalesOrderId = table.Column<int>(type: "INTEGER", nullable: false),
                    ProductId = table.Column<int>(type: "INTEGER", nullable: false),
                    Quantity = table.Column<int>(type: "INTEGER", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DiscountPercentage = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    DiscountAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    LineTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    QuantityAllocated = table.Column<int>(type: "INTEGER", nullable: false),
                    QuantityPacked = table.Column<int>(type: "INTEGER", nullable: false),
                    QuantityShipped = table.Column<int>(type: "INTEGER", nullable: false),
                    QuantityDelivered = table.Column<int>(type: "INTEGER", nullable: false),
                    ItemNotes = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesOrderItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SalesOrderItem_Products_ProductId",
                        column: x => x.ProductId,
                        principalSchema: "APP",
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SalesOrderItem_SalesOrders_SalesOrderId",
                        column: x => x.SalesOrderId,
                        principalSchema: "APP",
                        principalTable: "SalesOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductAttributeValue",
                schema: "APP",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ProductId = table.Column<int>(type: "INTEGER", nullable: false),
                    ItemAttributeOptionId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductAttributeValue", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductAttributeValue_ItemAttributeOption_ItemAttributeOptionId",
                        column: x => x.ItemAttributeOptionId,
                        principalSchema: "APP",
                        principalTable: "ItemAttributeOption",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductAttributeValue_Products_ProductId",
                        column: x => x.ProductId,
                        principalSchema: "APP",
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AccountTransaction",
                schema: "APP",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AccountId = table.Column<int>(type: "INTEGER", nullable: false),
                    TransactionType = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    EntryType = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TransactionDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ReferenceNumber = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    ReferenceType = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    ReferenceId = table.Column<int>(type: "INTEGER", nullable: true),
                    Description = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: true),
                    Notes = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    OrganisationId = table.Column<int>(type: "INTEGER", nullable: false),
                    InvoiceId = table.Column<int>(type: "INTEGER", nullable: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountTransaction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccountTransaction_GLAccount_AccountId",
                        column: x => x.AccountId,
                        principalSchema: "APP",
                        principalTable: "GLAccount",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AccountTransaction_Invoices_InvoiceId",
                        column: x => x.InvoiceId,
                        principalSchema: "APP",
                        principalTable: "Invoices",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AccountTransaction_Organisations_OrganisationId",
                        column: x => x.OrganisationId,
                        principalSchema: "APP",
                        principalTable: "Organisations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PurchaseOrderItem",
                schema: "APP",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PurchaseOrderId = table.Column<int>(type: "INTEGER", nullable: false),
                    ProductId = table.Column<int>(type: "INTEGER", nullable: false),
                    QuantityOrdered = table.Column<int>(type: "INTEGER", nullable: false),
                    UnitCost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DiscountPercentage = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    DiscountAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    LineTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    QuantityReceived = table.Column<int>(type: "INTEGER", nullable: false),
                    QuantityPending = table.Column<int>(type: "INTEGER", nullable: false),
                    ExpectedReceiptDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ItemNotes = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseOrderItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PurchaseOrderItem_Products_ProductId",
                        column: x => x.ProductId,
                        principalSchema: "APP",
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PurchaseOrderItem_PurchaseOrder_PurchaseOrderId",
                        column: x => x.PurchaseOrderId,
                        principalSchema: "APP",
                        principalTable: "PurchaseOrder",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StockTransaction",
                schema: "APP",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ProductId = table.Column<int>(type: "INTEGER", nullable: false),
                    TransactionType = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    MovementType = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    Quantity = table.Column<int>(type: "INTEGER", nullable: false),
                    UnitCost = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TotalValue = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ReferenceNumber = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    ReferenceType = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    ReferenceId = table.Column<int>(type: "INTEGER", nullable: true),
                    Notes = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    TransactionDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    WarehouseId = table.Column<int>(type: "INTEGER", nullable: true),
                    SalesOrderId = table.Column<int>(type: "INTEGER", nullable: true),
                    PurchaseOrderId = table.Column<int>(type: "INTEGER", nullable: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockTransaction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StockTransaction_Products_ProductId",
                        column: x => x.ProductId,
                        principalSchema: "APP",
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StockTransaction_PurchaseOrder_PurchaseOrderId",
                        column: x => x.PurchaseOrderId,
                        principalSchema: "APP",
                        principalTable: "PurchaseOrder",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StockTransaction_SalesOrders_SalesOrderId",
                        column: x => x.SalesOrderId,
                        principalSchema: "APP",
                        principalTable: "SalesOrders",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "InvoiceItem",
                schema: "APP",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    InvoiceId = table.Column<int>(type: "INTEGER", nullable: false),
                    ProductId = table.Column<int>(type: "INTEGER", nullable: false),
                    Quantity = table.Column<int>(type: "INTEGER", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DiscountPercentage = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    DiscountAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    LineTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TaxAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ItemNotes = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    SalesOrderItemId = table.Column<int>(type: "INTEGER", nullable: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InvoiceItem_Invoices_InvoiceId",
                        column: x => x.InvoiceId,
                        principalSchema: "APP",
                        principalTable: "Invoices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InvoiceItem_Products_ProductId",
                        column: x => x.ProductId,
                        principalSchema: "APP",
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InvoiceItem_SalesOrderItem_SalesOrderItemId",
                        column: x => x.SalesOrderItemId,
                        principalSchema: "APP",
                        principalTable: "SalesOrderItem",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccountSubType_AccountType",
                schema: "APP",
                table: "AccountSubType",
                column: "AccountType");

            migrationBuilder.CreateIndex(
                name: "IX_AccountSubType_AccountType_DisplayOrder",
                schema: "APP",
                table: "AccountSubType",
                columns: new[] { "AccountType", "DisplayOrder" });

            migrationBuilder.CreateIndex(
                name: "IX_AccountSubType_Code",
                schema: "APP",
                table: "AccountSubType",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AccountSubType_IsActive",
                schema: "APP",
                table: "AccountSubType",
                column: "IsActive");

            migrationBuilder.CreateIndex(
                name: "IX_AccountTransaction_AccountId",
                schema: "APP",
                table: "AccountTransaction",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountTransaction_InvoiceId",
                schema: "APP",
                table: "AccountTransaction",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountTransaction_OrganisationId",
                schema: "APP",
                table: "AccountTransaction",
                column: "OrganisationId");

            migrationBuilder.CreateIndex(
                name: "IX_Brand_IsActive",
                schema: "APP",
                table: "Brand",
                column: "IsActive");

            migrationBuilder.CreateIndex(
                name: "IX_Brand_Name",
                schema: "APP",
                table: "Brand",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Brand_OrganisationId",
                schema: "APP",
                table: "Brand",
                column: "OrganisationId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessAddress_AddressType",
                schema: "APP",
                table: "BusinessAddresses",
                column: "AddressType");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessAddress_Entity",
                schema: "APP",
                table: "BusinessAddresses",
                columns: new[] { "EntityType", "EntityId" });

            migrationBuilder.CreateIndex(
                name: "IX_BusinessAddress_Entity_AddressType",
                schema: "APP",
                table: "BusinessAddresses",
                columns: new[] { "EntityType", "EntityId", "AddressType" });

            migrationBuilder.CreateIndex(
                name: "IX_BusinessAddress_Entity_IsDefault",
                schema: "APP",
                table: "BusinessAddresses",
                columns: new[] { "EntityType", "EntityId", "IsDefault" });

            migrationBuilder.CreateIndex(
                name: "IX_BusinessAddress_IsActive",
                schema: "APP",
                table: "BusinessAddresses",
                column: "IsActive");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessAddress_IsDefault",
                schema: "APP",
                table: "BusinessAddresses",
                column: "IsDefault");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessAddresses_VendorId",
                schema: "APP",
                table: "BusinessAddresses",
                column: "VendorId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessBankInfo_Bank_Account",
                schema: "APP",
                table: "BusinessBankInfos",
                columns: new[] { "BankName", "BankAccountNumber" });

            migrationBuilder.CreateIndex(
                name: "IX_BusinessBankInfo_BankAccountNumber",
                schema: "APP",
                table: "BusinessBankInfos",
                column: "BankAccountNumber");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessBankInfo_Entity",
                schema: "APP",
                table: "BusinessBankInfos",
                columns: new[] { "EntityType", "EntityId" });

            migrationBuilder.CreateIndex(
                name: "IX_BusinessBankInfo_Entity_IsPrimary",
                schema: "APP",
                table: "BusinessBankInfos",
                columns: new[] { "EntityType", "EntityId", "IsPrimary" });

            migrationBuilder.CreateIndex(
                name: "IX_BusinessBankInfo_IFSCCode",
                schema: "APP",
                table: "BusinessBankInfos",
                column: "IFSCCode");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessBankInfo_IsActive",
                schema: "APP",
                table: "BusinessBankInfos",
                column: "IsActive");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessBankInfo_IsPrimary",
                schema: "APP",
                table: "BusinessBankInfos",
                column: "IsPrimary");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessBankInfo_IsVerified",
                schema: "APP",
                table: "BusinessBankInfos",
                column: "IsVerified");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessBankInfos_VendorId",
                schema: "APP",
                table: "BusinessBankInfos",
                column: "VendorId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessContact_ContactType",
                schema: "APP",
                table: "BusinessContacts",
                column: "ContactType");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessContact_Email",
                schema: "APP",
                table: "BusinessContacts",
                column: "Email");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessContact_Entity",
                schema: "APP",
                table: "BusinessContacts",
                columns: new[] { "EntityType", "EntityId" });

            migrationBuilder.CreateIndex(
                name: "IX_BusinessContact_Entity_ContactType",
                schema: "APP",
                table: "BusinessContacts",
                columns: new[] { "EntityType", "EntityId", "ContactType" });

            migrationBuilder.CreateIndex(
                name: "IX_BusinessContact_Entity_IsPrimary",
                schema: "APP",
                table: "BusinessContacts",
                columns: new[] { "EntityType", "EntityId", "IsPrimary" });

            migrationBuilder.CreateIndex(
                name: "IX_BusinessContact_IsActive",
                schema: "APP",
                table: "BusinessContacts",
                column: "IsActive");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessContact_IsPrimary",
                schema: "APP",
                table: "BusinessContacts",
                column: "IsPrimary");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessContacts_VendorId",
                schema: "APP",
                table: "BusinessContacts",
                column: "VendorId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessTaxInfo_Entity",
                schema: "APP",
                table: "BusinessTaxInfos",
                columns: new[] { "EntityType", "EntityId" });

            migrationBuilder.CreateIndex(
                name: "IX_BusinessTaxInfo_Entity_TaxType_Unique",
                schema: "APP",
                table: "BusinessTaxInfos",
                columns: new[] { "EntityType", "EntityId", "TaxType" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BusinessTaxInfo_IsActive",
                schema: "APP",
                table: "BusinessTaxInfos",
                column: "IsActive");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessTaxInfo_IsPrimary",
                schema: "APP",
                table: "BusinessTaxInfos",
                column: "IsPrimary");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessTaxInfo_TaxNumber",
                schema: "APP",
                table: "BusinessTaxInfos",
                column: "TaxNumber");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessTaxInfo_TaxType",
                schema: "APP",
                table: "BusinessTaxInfos",
                column: "TaxType");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessTaxInfo_TaxType_TaxNumber",
                schema: "APP",
                table: "BusinessTaxInfos",
                columns: new[] { "TaxType", "TaxNumber" });

            migrationBuilder.CreateIndex(
                name: "IX_BusinessTaxInfos_VendorId",
                schema: "APP",
                table: "BusinessTaxInfos",
                column: "VendorId");

            migrationBuilder.CreateIndex(
                name: "IX_Countries_Iso2",
                schema: "APP",
                table: "Countries",
                column: "Iso2",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Countries_Iso3",
                schema: "APP",
                table: "Countries",
                column: "Iso3",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Countries_Name",
                schema: "APP",
                table: "Countries",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Currencies_Code",
                schema: "APP",
                table: "Currencies",
                column: "Code");

            migrationBuilder.CreateIndex(
                name: "IX_Currencies_IsActive",
                schema: "APP",
                table: "Currencies",
                column: "IsActive");

            migrationBuilder.CreateIndex(
                name: "IX_Currencies_IsBaseCurrency",
                schema: "APP",
                table: "Currencies",
                column: "IsBaseCurrency");

            migrationBuilder.CreateIndex(
                name: "IX_Currencies_OrganisationId",
                schema: "APP",
                table: "Currencies",
                column: "OrganisationId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerAddress_AddressType",
                schema: "APP",
                table: "CustomerAddresses",
                column: "AddressType");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerAddress_Customer_AddressType",
                schema: "APP",
                table: "CustomerAddresses",
                columns: new[] { "CustomerId", "AddressType" });

            migrationBuilder.CreateIndex(
                name: "IX_CustomerAddress_Customer_IsPrimary",
                schema: "APP",
                table: "CustomerAddresses",
                columns: new[] { "CustomerId", "IsPrimary" });

            migrationBuilder.CreateIndex(
                name: "IX_CustomerAddress_CustomerId",
                schema: "APP",
                table: "CustomerAddresses",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerAddress_IsActive",
                schema: "APP",
                table: "CustomerAddresses",
                column: "IsActive");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerAddress_IsPrimary",
                schema: "APP",
                table: "CustomerAddresses",
                column: "IsPrimary");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerBankInfo_AccountNumber",
                schema: "APP",
                table: "CustomerBankInfos",
                column: "AccountNumber");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerBankInfo_Customer_IsPrimary",
                schema: "APP",
                table: "CustomerBankInfos",
                columns: new[] { "CustomerId", "IsPrimary" });

            migrationBuilder.CreateIndex(
                name: "IX_CustomerBankInfo_CustomerId",
                schema: "APP",
                table: "CustomerBankInfos",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerBankInfo_IsActive",
                schema: "APP",
                table: "CustomerBankInfos",
                column: "IsActive");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerBankInfo_IsPrimary",
                schema: "APP",
                table: "CustomerBankInfos",
                column: "IsPrimary");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerContact_ContactType",
                schema: "APP",
                table: "CustomerContacts",
                column: "ContactType");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerContact_Customer_ContactType",
                schema: "APP",
                table: "CustomerContacts",
                columns: new[] { "CustomerId", "ContactType" });

            migrationBuilder.CreateIndex(
                name: "IX_CustomerContact_Customer_IsPrimary",
                schema: "APP",
                table: "CustomerContacts",
                columns: new[] { "CustomerId", "IsPrimary" });

            migrationBuilder.CreateIndex(
                name: "IX_CustomerContact_CustomerId",
                schema: "APP",
                table: "CustomerContacts",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerContact_Email",
                schema: "APP",
                table: "CustomerContacts",
                column: "Email");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerContact_IsActive",
                schema: "APP",
                table: "CustomerContacts",
                column: "IsActive");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerContact_IsPrimary",
                schema: "APP",
                table: "CustomerContacts",
                column: "IsPrimary");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerDocument_Customer_DocumentType",
                schema: "APP",
                table: "CustomerDocuments",
                columns: new[] { "CustomerId", "DocumentType" });

            migrationBuilder.CreateIndex(
                name: "IX_CustomerDocument_CustomerId",
                schema: "APP",
                table: "CustomerDocuments",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerDocument_DocumentType",
                schema: "APP",
                table: "CustomerDocuments",
                column: "DocumentType");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerDocument_IsActive",
                schema: "APP",
                table: "CustomerDocuments",
                column: "IsActive");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerFinancials_CustomerId",
                schema: "APP",
                table: "CustomerFinancials",
                column: "CustomerId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CustomerPriceList_CustomerId",
                schema: "APP",
                table: "CustomerPriceList",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerPriceList_PriceListId",
                schema: "APP",
                table: "CustomerPriceList",
                column: "PriceListId");

            migrationBuilder.CreateIndex(
                name: "IX_Customer_CurrencyId",
                schema: "APP",
                table: "Customers",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_Customer_CustomerCode",
                schema: "APP",
                table: "Customers",
                column: "CustomerCode");

            migrationBuilder.CreateIndex(
                name: "IX_Customer_CustomerType",
                schema: "APP",
                table: "Customers",
                column: "CustomerType");

            migrationBuilder.CreateIndex(
                name: "IX_Customer_DisplayName",
                schema: "APP",
                table: "Customers",
                column: "DisplayName");

            migrationBuilder.CreateIndex(
                name: "IX_Customer_IsActive",
                schema: "APP",
                table: "Customers",
                column: "IsActive");

            migrationBuilder.CreateIndex(
                name: "IX_Customer_OrganisationId",
                schema: "APP",
                table: "Customers",
                column: "OrganisationId");

            migrationBuilder.CreateIndex(
                name: "IX_Customer_OrganisationId_CustomerCode",
                schema: "APP",
                table: "Customers",
                columns: new[] { "OrganisationId", "CustomerCode" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Customer_OrganisationId_CustomerType",
                schema: "APP",
                table: "Customers",
                columns: new[] { "OrganisationId", "CustomerType" });

            migrationBuilder.CreateIndex(
                name: "IX_Customer_OrganisationId_IsActive",
                schema: "APP",
                table: "Customers",
                columns: new[] { "OrganisationId", "IsActive" });

            migrationBuilder.CreateIndex(
                name: "IX_CustomerTaxInfo_Customer_IsPrimary",
                schema: "APP",
                table: "CustomerTaxInfos",
                columns: new[] { "CustomerId", "IsPrimary" });

            migrationBuilder.CreateIndex(
                name: "IX_CustomerTaxInfo_Customer_TaxType",
                schema: "APP",
                table: "CustomerTaxInfos",
                columns: new[] { "CustomerId", "TaxType" });

            migrationBuilder.CreateIndex(
                name: "IX_CustomerTaxInfo_CustomerId",
                schema: "APP",
                table: "CustomerTaxInfos",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerTaxInfo_IsActive",
                schema: "APP",
                table: "CustomerTaxInfos",
                column: "IsActive");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerTaxInfo_IsPrimary",
                schema: "APP",
                table: "CustomerTaxInfos",
                column: "IsPrimary");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerTaxInfo_TaxNumber",
                schema: "APP",
                table: "CustomerTaxInfos",
                column: "TaxNumber");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerTaxInfo_TaxType",
                schema: "APP",
                table: "CustomerTaxInfos",
                column: "TaxType");

            migrationBuilder.CreateIndex(
                name: "IX_GLAccount_AccountCode",
                schema: "APP",
                table: "GLAccount",
                column: "AccountCode");

            migrationBuilder.CreateIndex(
                name: "IX_GLAccount_AccountName",
                schema: "APP",
                table: "GLAccount",
                column: "AccountName");

            migrationBuilder.CreateIndex(
                name: "IX_GLAccount_AccountSubTypeId",
                schema: "APP",
                table: "GLAccount",
                column: "AccountSubTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_GLAccount_AccountType",
                schema: "APP",
                table: "GLAccount",
                column: "AccountType");

            migrationBuilder.CreateIndex(
                name: "IX_GLAccount_IsActive",
                schema: "APP",
                table: "GLAccount",
                column: "IsActive");

            migrationBuilder.CreateIndex(
                name: "IX_GLAccount_IsSystemAccount",
                schema: "APP",
                table: "GLAccount",
                column: "IsSystemAccount");

            migrationBuilder.CreateIndex(
                name: "IX_GLAccount_OrganisationId",
                schema: "APP",
                table: "GLAccount",
                column: "OrganisationId");

            migrationBuilder.CreateIndex(
                name: "IX_GLAccount_OrganisationId_AccountCode",
                schema: "APP",
                table: "GLAccount",
                columns: new[] { "OrganisationId", "AccountCode" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GLAccount_OrganisationId_AccountType_IsActive",
                schema: "APP",
                table: "GLAccount",
                columns: new[] { "OrganisationId", "AccountType", "IsActive" });

            migrationBuilder.CreateIndex(
                name: "IX_GLAccount_OrganisationId_IsActive",
                schema: "APP",
                table: "GLAccount",
                columns: new[] { "OrganisationId", "IsActive" });

            migrationBuilder.CreateIndex(
                name: "IX_GLAccount_ParentAccountId",
                schema: "APP",
                table: "GLAccount",
                column: "ParentAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceItem_InvoiceId",
                schema: "APP",
                table: "InvoiceItem",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceItem_ProductId",
                schema: "APP",
                table: "InvoiceItem",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceItem_SalesOrderItemId",
                schema: "APP",
                table: "InvoiceItem",
                column: "SalesOrderItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_CustomerId",
                schema: "APP",
                table: "Invoices",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_InvoiceDate",
                schema: "APP",
                table: "Invoices",
                column: "InvoiceDate");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_InvoiceNumber",
                schema: "APP",
                table: "Invoices",
                column: "InvoiceNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_OrganisationId",
                schema: "APP",
                table: "Invoices",
                column: "OrganisationId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_PriceListId",
                schema: "APP",
                table: "Invoices",
                column: "PriceListId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_SalesOrderId",
                schema: "APP",
                table: "Invoices",
                column: "SalesOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_Status",
                schema: "APP",
                table: "Invoices",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_ItemAttribute_ItemGroupId",
                schema: "APP",
                table: "ItemAttribute",
                column: "ItemGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemAttributeOption_ItemAttributeId",
                schema: "APP",
                table: "ItemAttributeOption",
                column: "ItemAttributeId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemGroup_OrganisationId",
                schema: "APP",
                table: "ItemGroup",
                column: "OrganisationId");

            migrationBuilder.CreateIndex(
                name: "IX_PriceList_OrganisationId",
                schema: "APP",
                table: "PriceList",
                column: "OrganisationId");

            migrationBuilder.CreateIndex(
                name: "IX_PriceListItem_PriceListId",
                schema: "APP",
                table: "PriceListItem",
                column: "PriceListId");

            migrationBuilder.CreateIndex(
                name: "IX_PriceListItem_ProductId",
                schema: "APP",
                table: "PriceListItem",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductAttributeValue_ItemAttributeOptionId",
                schema: "APP",
                table: "ProductAttributeValue",
                column: "ItemAttributeOptionId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductAttributeValue_ProductId",
                schema: "APP",
                table: "ProductAttributeValue",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductImages_ProductId",
                schema: "APP",
                table: "ProductImages",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductInventory_ProductId",
                schema: "APP",
                table: "ProductInventory",
                column: "ProductId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductInventory_UnitOfMeasureId",
                schema: "APP",
                table: "ProductInventory",
                column: "UnitOfMeasureId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductInventory_VendorId",
                schema: "APP",
                table: "ProductInventory",
                column: "VendorId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductItemGroup_ItemGroupId",
                schema: "APP",
                table: "ProductItemGroups",
                column: "ItemGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductItemGroup_ProductId",
                schema: "APP",
                table: "ProductItemGroups",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductItemGroup_ProductId_IsPrimary",
                schema: "APP",
                table: "ProductItemGroups",
                columns: new[] { "ProductId", "IsPrimary" });

            migrationBuilder.CreateIndex(
                name: "IX_ProductItemGroup_ProductId_ItemGroupId",
                schema: "APP",
                table: "ProductItemGroups",
                columns: new[] { "ProductId", "ItemGroupId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductPhysical_DimensionUnitId",
                schema: "APP",
                table: "ProductPhysical",
                column: "DimensionUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductPhysical_ProductId",
                schema: "APP",
                table: "ProductPhysical",
                column: "ProductId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductPhysical_WeightUnitId",
                schema: "APP",
                table: "ProductPhysical",
                column: "WeightUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_BrandId",
                schema: "APP",
                table: "Products",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_Name",
                schema: "APP",
                table: "Products",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Product_OrganisationId",
                schema: "APP",
                table: "Products",
                column: "OrganisationId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_OrganisationId_SKU",
                schema: "APP",
                table: "Products",
                columns: new[] { "OrganisationId", "SKU" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Product_ProductType",
                schema: "APP",
                table: "Products",
                column: "ProductType");

            migrationBuilder.CreateIndex(
                name: "IX_Product_SKU",
                schema: "APP",
                table: "Products",
                column: "SKU");

            migrationBuilder.CreateIndex(
                name: "IX_Product_Status",
                schema: "APP",
                table: "Products",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CurrencyId",
                schema: "APP",
                table: "Products",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CurrencyId1",
                schema: "APP",
                table: "Products",
                column: "CurrencyId1");

            migrationBuilder.CreateIndex(
                name: "IX_Products_InventoryAccountId",
                schema: "APP",
                table: "Products",
                column: "InventoryAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_PurchaseAccountId",
                schema: "APP",
                table: "Products",
                column: "PurchaseAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_SalesAccountId",
                schema: "APP",
                table: "Products",
                column: "SalesAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_TaxMasterId",
                schema: "APP",
                table: "Products",
                column: "TaxMasterId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_UnitOfMeasureId",
                schema: "APP",
                table: "Products",
                column: "UnitOfMeasureId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_UnitOfMeasureId1",
                schema: "APP",
                table: "Products",
                column: "UnitOfMeasureId1");

            migrationBuilder.CreateIndex(
                name: "IX_Products_UnitOfMeasureId2",
                schema: "APP",
                table: "Products",
                column: "UnitOfMeasureId2");

            migrationBuilder.CreateIndex(
                name: "IX_Products_VendorId",
                schema: "APP",
                table: "Products",
                column: "VendorId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductTagAssignments_ProductId_ProductTagId",
                schema: "APP",
                table: "ProductTagAssignments",
                columns: new[] { "ProductId", "ProductTagId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductTagAssignments_ProductTagId",
                schema: "APP",
                table: "ProductTagAssignments",
                column: "ProductTagId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductTags_Name_OrganisationId",
                schema: "APP",
                table: "ProductTags",
                columns: new[] { "Name", "OrganisationId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductTags_OrganisationId",
                schema: "APP",
                table: "ProductTags",
                column: "OrganisationId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductTax_ProductId",
                schema: "APP",
                table: "ProductTax",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductTax_TaxClassificationId",
                schema: "APP",
                table: "ProductTax",
                column: "TaxClassificationId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductTax_TaxMasterId",
                schema: "APP",
                table: "ProductTax",
                column: "TaxMasterId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrder_OrganisationId",
                schema: "APP",
                table: "PurchaseOrder",
                column: "OrganisationId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrder_OriginatingSalesOrderId",
                schema: "APP",
                table: "PurchaseOrder",
                column: "OriginatingSalesOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrder_PriceListId",
                schema: "APP",
                table: "PurchaseOrder",
                column: "PriceListId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrder_VendorId",
                schema: "APP",
                table: "PurchaseOrder",
                column: "VendorId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrderItem_ProductId",
                schema: "APP",
                table: "PurchaseOrderItem",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrderItem_PurchaseOrderId",
                schema: "APP",
                table: "PurchaseOrderItem",
                column: "PurchaseOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrderApprovals_ApprovalStatus",
                schema: "APP",
                table: "SalesOrderApprovals",
                column: "ApprovalStatus");

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrderApprovals_ApprovedDate",
                schema: "APP",
                table: "SalesOrderApprovals",
                column: "ApprovedDate");

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrderApprovals_SalesOrderId",
                schema: "APP",
                table: "SalesOrderApprovals",
                column: "SalesOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrderDocument_DocumentType",
                schema: "APP",
                table: "SalesOrderDocuments",
                column: "DocumentType");

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrderDocument_IsActive",
                schema: "APP",
                table: "SalesOrderDocuments",
                column: "IsActive");

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrderDocument_SalesOrderId",
                schema: "APP",
                table: "SalesOrderDocuments",
                column: "SalesOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrderDocument_UploadedOn",
                schema: "APP",
                table: "SalesOrderDocuments",
                column: "UploadedOn");

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrderItem_ProductId",
                schema: "APP",
                table: "SalesOrderItem",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrderItem_SalesOrderId",
                schema: "APP",
                table: "SalesOrderItem",
                column: "SalesOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrderPayments_DueDate",
                schema: "APP",
                table: "SalesOrderPayments",
                column: "DueDate");

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrderPayments_PaymentStatus",
                schema: "APP",
                table: "SalesOrderPayments",
                column: "PaymentStatus");

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrderPayments_SalesOrderId",
                schema: "APP",
                table: "SalesOrderPayments",
                column: "SalesOrderId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrder_CustomerId",
                schema: "APP",
                table: "SalesOrders",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrder_OrderDate",
                schema: "APP",
                table: "SalesOrders",
                column: "OrderDate");

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrder_OrderNumber",
                schema: "APP",
                table: "SalesOrders",
                column: "OrderNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrder_OrganisationId",
                schema: "APP",
                table: "SalesOrders",
                column: "OrganisationId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrder_OrganisationId_CustomerId",
                schema: "APP",
                table: "SalesOrders",
                columns: new[] { "OrganisationId", "CustomerId" });

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrder_OrganisationId_OrderDate",
                schema: "APP",
                table: "SalesOrders",
                columns: new[] { "OrganisationId", "OrderDate" });

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrder_OrganisationId_Status",
                schema: "APP",
                table: "SalesOrders",
                columns: new[] { "OrganisationId", "Status" });

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrder_ReferenceNumber",
                schema: "APP",
                table: "SalesOrders",
                column: "ReferenceNumber");

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrder_Status",
                schema: "APP",
                table: "SalesOrders",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrders_PriceListId",
                schema: "APP",
                table: "SalesOrders",
                column: "PriceListId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrderShippings_EstimatedDeliveryDate",
                schema: "APP",
                table: "SalesOrderShippings",
                column: "EstimatedDeliveryDate");

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrderShippings_SalesOrderId",
                schema: "APP",
                table: "SalesOrderShippings",
                column: "SalesOrderId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrderShippings_ShippingStatus",
                schema: "APP",
                table: "SalesOrderShippings",
                column: "ShippingStatus");

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrderShippings_TrackingNumber",
                schema: "APP",
                table: "SalesOrderShippings",
                column: "TrackingNumber");

            migrationBuilder.CreateIndex(
                name: "IX_States_CountryId",
                schema: "APP",
                table: "States",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_States_Name",
                schema: "APP",
                table: "States",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_States_StateCode",
                schema: "APP",
                table: "States",
                column: "StateCode");

            migrationBuilder.CreateIndex(
                name: "IX_StockTransaction_ProductId",
                schema: "APP",
                table: "StockTransaction",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_StockTransaction_PurchaseOrderId",
                schema: "APP",
                table: "StockTransaction",
                column: "PurchaseOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_StockTransaction_SalesOrderId",
                schema: "APP",
                table: "StockTransaction",
                column: "SalesOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_TaxClassifications_ClassificationType",
                schema: "APP",
                table: "TaxClassifications",
                column: "ClassificationType");

            migrationBuilder.CreateIndex(
                name: "IX_TaxClassifications_Code",
                schema: "APP",
                table: "TaxClassifications",
                column: "Code");

            migrationBuilder.CreateIndex(
                name: "IX_TaxClassifications_CountryCode",
                schema: "APP",
                table: "TaxClassifications",
                column: "CountryCode");

            migrationBuilder.CreateIndex(
                name: "IX_TaxClassifications_IsActive",
                schema: "APP",
                table: "TaxClassifications",
                column: "IsActive");

            migrationBuilder.CreateIndex(
                name: "IX_TaxClassifications_OrganisationId",
                schema: "APP",
                table: "TaxClassifications",
                column: "OrganisationId");

            migrationBuilder.CreateIndex(
                name: "IX_TaxMaster_OrganisationId",
                schema: "APP",
                table: "TaxMaster",
                column: "OrganisationId");

            migrationBuilder.CreateIndex(
                name: "IX_UnitOfMeasure_Code",
                schema: "APP",
                table: "UnitOfMeasure",
                column: "Code");

            migrationBuilder.CreateIndex(
                name: "IX_UnitOfMeasure_IsActive",
                schema: "APP",
                table: "UnitOfMeasure",
                column: "IsActive");

            migrationBuilder.CreateIndex(
                name: "IX_UnitOfMeasure_OrganisationId",
                schema: "APP",
                table: "UnitOfMeasure",
                column: "OrganisationId");

            migrationBuilder.CreateIndex(
                name: "IX_UnitOfMeasure_UnitType",
                schema: "APP",
                table: "UnitOfMeasure",
                column: "UnitType");

            migrationBuilder.CreateIndex(
                name: "IX_VendorDocument_DocumentType",
                schema: "APP",
                table: "VendorDocuments",
                column: "DocumentType");

            migrationBuilder.CreateIndex(
                name: "IX_VendorDocument_ExpiryDate",
                schema: "APP",
                table: "VendorDocuments",
                column: "ExpiryDate");

            migrationBuilder.CreateIndex(
                name: "IX_VendorDocument_IsActive",
                schema: "APP",
                table: "VendorDocuments",
                column: "IsActive");

            migrationBuilder.CreateIndex(
                name: "IX_VendorDocument_VendorId",
                schema: "APP",
                table: "VendorDocuments",
                column: "VendorId");

            migrationBuilder.CreateIndex(
                name: "IX_VendorDocument_VendorId_DocumentType",
                schema: "APP",
                table: "VendorDocuments",
                columns: new[] { "VendorId", "DocumentType" });

            migrationBuilder.CreateIndex(
                name: "IX_VendorDocument_VendorId_IsActive",
                schema: "APP",
                table: "VendorDocuments",
                columns: new[] { "VendorId", "IsActive" });

            migrationBuilder.CreateIndex(
                name: "IX_VendorFinancials_VendorId",
                schema: "APP",
                table: "VendorFinancials",
                column: "VendorId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_VendorPerformance_IsBlacklisted",
                schema: "APP",
                table: "VendorPerformances",
                column: "IsBlacklisted");

            migrationBuilder.CreateIndex(
                name: "IX_VendorPerformance_IsPreferredVendor",
                schema: "APP",
                table: "VendorPerformances",
                column: "IsPreferredVendor");

            migrationBuilder.CreateIndex(
                name: "IX_VendorPerformance_PerformanceRating",
                schema: "APP",
                table: "VendorPerformances",
                column: "PerformanceRating");

            migrationBuilder.CreateIndex(
                name: "IX_VendorPerformance_ReviewPeriodEnd",
                schema: "APP",
                table: "VendorPerformances",
                column: "ReviewPeriodEnd");

            migrationBuilder.CreateIndex(
                name: "IX_VendorPerformance_ReviewPeriodStart",
                schema: "APP",
                table: "VendorPerformances",
                column: "ReviewPeriodStart");

            migrationBuilder.CreateIndex(
                name: "IX_VendorPerformance_Vendor_Period",
                schema: "APP",
                table: "VendorPerformances",
                columns: new[] { "VendorId", "ReviewPeriodStart", "ReviewPeriodEnd" });

            migrationBuilder.CreateIndex(
                name: "IX_VendorPerformance_VendorId",
                schema: "APP",
                table: "VendorPerformances",
                column: "VendorId");

            migrationBuilder.CreateIndex(
                name: "IX_VendorPriceList_PriceListId",
                schema: "APP",
                table: "VendorPriceList",
                column: "PriceListId");

            migrationBuilder.CreateIndex(
                name: "IX_VendorPriceList_VendorId",
                schema: "APP",
                table: "VendorPriceList",
                column: "VendorId");

            migrationBuilder.CreateIndex(
                name: "IX_Vendor_Category",
                schema: "APP",
                table: "Vendors",
                column: "Category");

            migrationBuilder.CreateIndex(
                name: "IX_Vendor_CurrencyId",
                schema: "APP",
                table: "Vendors",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_Vendor_Industry",
                schema: "APP",
                table: "Vendors",
                column: "Industry");

            migrationBuilder.CreateIndex(
                name: "IX_Vendor_IsManufacturer",
                schema: "APP",
                table: "Vendors",
                column: "IsManufacturer");

            migrationBuilder.CreateIndex(
                name: "IX_Vendor_OrganisationId",
                schema: "APP",
                table: "Vendors",
                column: "OrganisationId");

            migrationBuilder.CreateIndex(
                name: "IX_Vendor_OrganisationId_Category",
                schema: "APP",
                table: "Vendors",
                columns: new[] { "OrganisationId", "Category" });

            migrationBuilder.CreateIndex(
                name: "IX_Vendor_OrganisationId_Status",
                schema: "APP",
                table: "Vendors",
                columns: new[] { "OrganisationId", "Status" });

            migrationBuilder.CreateIndex(
                name: "IX_Vendor_OrganisationId_VendorCode",
                schema: "APP",
                table: "Vendors",
                columns: new[] { "OrganisationId", "VendorCode" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Vendor_OrganisationId_VendorType",
                schema: "APP",
                table: "Vendors",
                columns: new[] { "OrganisationId", "VendorType" });

            migrationBuilder.CreateIndex(
                name: "IX_Vendor_ParentVendorId",
                schema: "APP",
                table: "Vendors",
                column: "ParentVendorId");

            migrationBuilder.CreateIndex(
                name: "IX_Vendor_Status",
                schema: "APP",
                table: "Vendors",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_Vendor_VendorCode",
                schema: "APP",
                table: "Vendors",
                column: "VendorCode");

            migrationBuilder.CreateIndex(
                name: "IX_Vendor_VendorName",
                schema: "APP",
                table: "Vendors",
                column: "VendorName");

            migrationBuilder.CreateIndex(
                name: "IX_Vendor_VendorType",
                schema: "APP",
                table: "Vendors",
                column: "VendorType");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountTransaction",
                schema: "APP");

            migrationBuilder.DropTable(
                name: "BusinessAddresses",
                schema: "APP");

            migrationBuilder.DropTable(
                name: "BusinessBankInfos",
                schema: "APP");

            migrationBuilder.DropTable(
                name: "BusinessContacts",
                schema: "APP");

            migrationBuilder.DropTable(
                name: "BusinessTaxInfos",
                schema: "APP");

            migrationBuilder.DropTable(
                name: "CustomerAddresses",
                schema: "APP");

            migrationBuilder.DropTable(
                name: "CustomerBankInfos",
                schema: "APP");

            migrationBuilder.DropTable(
                name: "CustomerContacts",
                schema: "APP");

            migrationBuilder.DropTable(
                name: "CustomerDocuments",
                schema: "APP");

            migrationBuilder.DropTable(
                name: "CustomerFinancials",
                schema: "APP");

            migrationBuilder.DropTable(
                name: "CustomerPriceList",
                schema: "APP");

            migrationBuilder.DropTable(
                name: "CustomerTaxInfos",
                schema: "APP");

            migrationBuilder.DropTable(
                name: "InvoiceItem",
                schema: "APP");

            migrationBuilder.DropTable(
                name: "PriceListItem",
                schema: "APP");

            migrationBuilder.DropTable(
                name: "ProductAttributeValue",
                schema: "APP");

            migrationBuilder.DropTable(
                name: "ProductImages",
                schema: "APP");

            migrationBuilder.DropTable(
                name: "ProductInventory",
                schema: "APP");

            migrationBuilder.DropTable(
                name: "ProductItemGroups",
                schema: "APP");

            migrationBuilder.DropTable(
                name: "ProductPhysical",
                schema: "APP");

            migrationBuilder.DropTable(
                name: "ProductTagAssignments",
                schema: "APP");

            migrationBuilder.DropTable(
                name: "ProductTax",
                schema: "APP");

            migrationBuilder.DropTable(
                name: "PurchaseOrderItem",
                schema: "APP");

            migrationBuilder.DropTable(
                name: "SalesOrderApprovals",
                schema: "APP");

            migrationBuilder.DropTable(
                name: "SalesOrderDocuments",
                schema: "APP");

            migrationBuilder.DropTable(
                name: "SalesOrderPayments",
                schema: "APP");

            migrationBuilder.DropTable(
                name: "SalesOrderShippings",
                schema: "APP");

            migrationBuilder.DropTable(
                name: "States",
                schema: "APP");

            migrationBuilder.DropTable(
                name: "StockTransaction",
                schema: "APP");

            migrationBuilder.DropTable(
                name: "VendorDocuments",
                schema: "APP");

            migrationBuilder.DropTable(
                name: "VendorFinancials",
                schema: "APP");

            migrationBuilder.DropTable(
                name: "VendorPerformances",
                schema: "APP");

            migrationBuilder.DropTable(
                name: "VendorPriceList",
                schema: "APP");

            migrationBuilder.DropTable(
                name: "Invoices",
                schema: "APP");

            migrationBuilder.DropTable(
                name: "SalesOrderItem",
                schema: "APP");

            migrationBuilder.DropTable(
                name: "ItemAttributeOption",
                schema: "APP");

            migrationBuilder.DropTable(
                name: "ProductTags",
                schema: "APP");

            migrationBuilder.DropTable(
                name: "TaxClassifications",
                schema: "APP");

            migrationBuilder.DropTable(
                name: "Countries",
                schema: "APP");

            migrationBuilder.DropTable(
                name: "PurchaseOrder",
                schema: "APP");

            migrationBuilder.DropTable(
                name: "Products",
                schema: "APP");

            migrationBuilder.DropTable(
                name: "ItemAttribute",
                schema: "APP");

            migrationBuilder.DropTable(
                name: "SalesOrders",
                schema: "APP");

            migrationBuilder.DropTable(
                name: "Brand",
                schema: "APP");

            migrationBuilder.DropTable(
                name: "GLAccount",
                schema: "APP");

            migrationBuilder.DropTable(
                name: "TaxMaster",
                schema: "APP");

            migrationBuilder.DropTable(
                name: "UnitOfMeasure",
                schema: "APP");

            migrationBuilder.DropTable(
                name: "Vendors",
                schema: "APP");

            migrationBuilder.DropTable(
                name: "ItemGroup",
                schema: "APP");

            migrationBuilder.DropTable(
                name: "Customers",
                schema: "APP");

            migrationBuilder.DropTable(
                name: "PriceList",
                schema: "APP");

            migrationBuilder.DropTable(
                name: "AccountSubType",
                schema: "APP");

            migrationBuilder.DropTable(
                name: "Currencies",
                schema: "APP");

            migrationBuilder.DropTable(
                name: "Organisations",
                schema: "APP");
        }
    }
}
