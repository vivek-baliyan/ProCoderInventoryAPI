using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

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
                name: "Categories",
                schema: "APP",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    PageTitle = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    UrlIdentifier = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: true),
                    Status = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    PublishDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ParentCategoryId = table.Column<int>(type: "INTEGER", nullable: true),
                    OrganisationId = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Categories_Categories_ParentCategoryId",
                        column: x => x.ParentCategoryId,
                        principalSchema: "APP",
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Categories_Organisations_OrganisationId",
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
                name: "Vendor",
                schema: "APP",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    VendorCode = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    VendorName = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    ContactPerson = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    PhoneNumber = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    Email = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    Address = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    City = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    State = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    PostalCode = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    Country = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    WebsiteUrl = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
                    OrganisationId = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vendor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vendor_Organisations_OrganisationId",
                        column: x => x.OrganisationId,
                        principalSchema: "APP",
                        principalTable: "Organisations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CategoryImages",
                schema: "APP",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ImagePath = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    AltText = table.Column<string>(type: "TEXT", nullable: true),
                    X = table.Column<int>(type: "INTEGER", nullable: true),
                    Y = table.Column<int>(type: "INTEGER", nullable: true),
                    Width = table.Column<int>(type: "INTEGER", nullable: true),
                    Height = table.Column<int>(type: "INTEGER", nullable: true),
                    Rotation = table.Column<int>(type: "INTEGER", nullable: true),
                    ScaleX = table.Column<double>(type: "REAL", nullable: true),
                    ScaleY = table.Column<double>(type: "REAL", nullable: true),
                    AspectRatio = table.Column<string>(type: "TEXT", nullable: true),
                    IsPrimary = table.Column<bool>(type: "INTEGER", nullable: false),
                    CategoryId = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CategoryImages_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalSchema: "APP",
                        principalTable: "Categories",
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
                    BalanceType = table.Column<int>(type: "INTEGER", nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: true),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: true),
                    IsSystemAccount = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                    AccountSubTypeId = table.Column<int>(type: "INTEGER", nullable: false),
                    ParentAccountId = table.Column<int>(type: "INTEGER", nullable: true),
                    CurrencyId = table.Column<int>(type: "INTEGER", nullable: true),
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
                        name: "FK_GLAccount_Currencies_CurrencyId",
                        column: x => x.CurrencyId,
                        principalSchema: "APP",
                        principalTable: "Currencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
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
                    ExpiryDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerPriceList", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerPriceList_PriceList_PriceListId",
                        column: x => x.PriceListId,
                        principalSchema: "APP",
                        principalTable: "PriceList",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SalesOrder",
                schema: "APP",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    OrderNumber = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    OrderDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Status = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    ExpectedDeliveryDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    CustomerId = table.Column<int>(type: "INTEGER", nullable: false),
                    OrganisationId = table.Column<int>(type: "INTEGER", nullable: false),
                    PriceListId = table.Column<int>(type: "INTEGER", nullable: true),
                    SubTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TaxAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Notes = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: true),
                    ShippingAddress = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    BillingAddress = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    IsDropShipment = table.Column<bool>(type: "INTEGER", nullable: false),
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
                    table.PrimaryKey("PK_SalesOrder", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SalesOrder_Organisations_OrganisationId",
                        column: x => x.OrganisationId,
                        principalSchema: "APP",
                        principalTable: "Organisations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SalesOrder_PriceList_PriceListId",
                        column: x => x.PriceListId,
                        principalSchema: "APP",
                        principalTable: "PriceList",
                        principalColumn: "Id");
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
                        name: "FK_VendorPriceList_Vendor_VendorId",
                        column: x => x.VendorId,
                        principalSchema: "APP",
                        principalTable: "Vendor",
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
                        name: "FK_AccountTransaction_Organisations_OrganisationId",
                        column: x => x.OrganisationId,
                        principalSchema: "APP",
                        principalTable: "Organisations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                    Description = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: true),
                    ProductType = table.Column<int>(type: "INTEGER", nullable: false, defaultValue: 1),
                    Status = table.Column<int>(type: "INTEGER", nullable: false, defaultValue: 1),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: true),
                    IsTaxable = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: true),
                    TrackInventory = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: true),
                    SerialNumberTracking = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                    BatchTracking = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                    ItemGroupId = table.Column<int>(type: "INTEGER", nullable: true),
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
                        name: "FK_Products_ItemGroup_ItemGroupId",
                        column: x => x.ItemGroupId,
                        principalSchema: "APP",
                        principalTable: "ItemGroup",
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
                        name: "FK_Products_Vendor_VendorId",
                        column: x => x.VendorId,
                        principalSchema: "APP",
                        principalTable: "Vendor",
                        principalColumn: "Id");
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
                        name: "FK_PurchaseOrder_SalesOrder_OriginatingSalesOrderId",
                        column: x => x.OriginatingSalesOrderId,
                        principalSchema: "APP",
                        principalTable: "SalesOrder",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PurchaseOrder_Vendor_VendorId",
                        column: x => x.VendorId,
                        principalSchema: "APP",
                        principalTable: "Vendor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BatchNumber",
                schema: "APP",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ProductId = table.Column<int>(type: "INTEGER", nullable: false),
                    BatchNumberValue = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    ExpiryDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ManufactureDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    QuantityAvailable = table.Column<int>(type: "INTEGER", nullable: false),
                    Status = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BatchNumber", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BatchNumber_Products_ProductId",
                        column: x => x.ProductId,
                        principalSchema: "APP",
                        principalTable: "Products",
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
                name: "ProductCategories",
                schema: "APP",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ProductId = table.Column<int>(type: "INTEGER", nullable: false),
                    CategoryId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductCategories_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalSchema: "APP",
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductCategories_Products_ProductId",
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
                        name: "FK_ProductInventory_Vendor_VendorId",
                        column: x => x.VendorId,
                        principalSchema: "APP",
                        principalTable: "Vendor",
                        principalColumn: "Id");
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
                        name: "FK_SalesOrderItem_SalesOrder_SalesOrderId",
                        column: x => x.SalesOrderId,
                        principalSchema: "APP",
                        principalTable: "SalesOrder",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SerialNumber",
                schema: "APP",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ProductId = table.Column<int>(type: "INTEGER", nullable: false),
                    SerialNumberValue = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Status = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    SoldDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ReturnedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SerialNumber", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SerialNumber_Products_ProductId",
                        column: x => x.ProductId,
                        principalSchema: "APP",
                        principalTable: "Products",
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
                        name: "FK_StockTransaction_SalesOrder_SalesOrderId",
                        column: x => x.SalesOrderId,
                        principalSchema: "APP",
                        principalTable: "SalesOrder",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                schema: "APP",
                table: "AccountSubType",
                columns: new[] { "Id", "AccountType", "Code", "CreatedBy", "CreatedOn", "Description", "DisplayOrder", "IsActive", "ModifiedBy", "ModifiedOn", "Name" },
                values: new object[,]
                {
                    { 1, 1, "CASH", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Cash and cash equivalents", 1, true, null, null, "Cash" },
                    { 2, 1, "BANK", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bank accounts", 2, true, null, null, "Bank" },
                    { 3, 1, "AR", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Money owed by customers", 3, true, null, null, "Accounts Receivable" },
                    { 4, 1, "INV", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Goods held for sale", 4, true, null, null, "Inventory" },
                    { 5, 1, "OCA", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Other current assets", 5, true, null, null, "Other Current Asset" },
                    { 6, 1, "FA", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Property, plant and equipment", 6, true, null, null, "Fixed Asset" },
                    { 7, 1, "AD", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Accumulated depreciation on fixed assets", 7, true, null, null, "Accumulated Depreciation" },
                    { 8, 1, "OA", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Other non-current assets", 8, true, null, null, "Other Asset" },
                    { 20, 2, "AP", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Money owed to suppliers", 1, true, null, null, "Accounts Payable" },
                    { 21, 2, "CC", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Credit card liabilities", 2, true, null, null, "Credit Card" },
                    { 22, 2, "TP", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Taxes owed", 3, true, null, null, "Tax Payable" },
                    { 23, 2, "OCL", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Other current liabilities", 4, true, null, null, "Other Current Liability" },
                    { 24, 2, "LTL", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Long-term debt and obligations", 5, true, null, null, "Long Term Liability" },
                    { 40, 3, "OE", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Owner's equity", 1, true, null, null, "Owner Equity" },
                    { 41, 3, "RE", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Accumulated profits", 2, true, null, null, "Retained Earnings" },
                    { 42, 3, "OBE", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Opening balance adjustments", 3, true, null, null, "Opening Balance Equity" },
                    { 60, 4, "SR", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Revenue from sales", 1, true, null, null, "Sales Revenue" },
                    { 61, 4, "SER", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Revenue from services", 2, true, null, null, "Service Revenue" },
                    { 62, 4, "OI", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Other income sources", 3, true, null, null, "Other Income" },
                    { 63, 4, "II", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Interest earned", 4, true, null, null, "Interest Income" },
                    { 80, 5, "COGS", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Direct costs of producing goods", 1, true, null, null, "Cost of Goods Sold" },
                    { 81, 5, "OPEX", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Operating expenses", 2, true, null, null, "Operating Expense" },
                    { 82, 5, "ADMIN", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Administrative expenses", 3, true, null, null, "Administrative Expense" },
                    { 83, 5, "SELL", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sales and marketing expenses", 4, true, null, null, "Selling Expense" },
                    { 84, 5, "IE", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Interest paid on debt", 5, true, null, null, "Interest Expense" },
                    { 85, 5, "TAX", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Tax expenses", 6, true, null, null, "Tax Expense" },
                    { 86, 5, "OEXP", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Other miscellaneous expenses", 7, true, null, null, "Other Expense" }
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
                name: "IX_AccountTransaction_OrganisationId",
                schema: "APP",
                table: "AccountTransaction",
                column: "OrganisationId");

            migrationBuilder.CreateIndex(
                name: "IX_BatchNumber_ProductId",
                schema: "APP",
                table: "BatchNumber",
                column: "ProductId");

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
                name: "IX_Categories_Name_UrlIdentifier",
                schema: "APP",
                table: "Categories",
                columns: new[] { "Name", "UrlIdentifier" });

            migrationBuilder.CreateIndex(
                name: "IX_Categories_OrganisationId",
                schema: "APP",
                table: "Categories",
                column: "OrganisationId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_ParentCategoryId",
                schema: "APP",
                table: "Categories",
                column: "ParentCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryImages_CategoryId",
                schema: "APP",
                table: "CategoryImages",
                column: "CategoryId");

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
                name: "IX_CustomerPriceList_PriceListId",
                schema: "APP",
                table: "CustomerPriceList",
                column: "PriceListId");

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
                name: "IX_GLAccount_CurrencyId",
                schema: "APP",
                table: "GLAccount",
                column: "CurrencyId");

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
                name: "IX_ProductCategories_CategoryId",
                schema: "APP",
                table: "ProductCategories",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductCategories_ProductId_CategoryId",
                schema: "APP",
                table: "ProductCategories",
                columns: new[] { "ProductId", "CategoryId" },
                unique: true);

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
                name: "IX_Product_IsActive",
                schema: "APP",
                table: "Products",
                column: "IsActive");

            migrationBuilder.CreateIndex(
                name: "IX_Product_ItemGroupId",
                schema: "APP",
                table: "Products",
                column: "ItemGroupId");

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
                name: "IX_Product_OrganisationId_IsActive",
                schema: "APP",
                table: "Products",
                columns: new[] { "OrganisationId", "IsActive" });

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
                name: "IX_ProductTax_ProductId",
                schema: "APP",
                table: "ProductTax",
                column: "ProductId",
                unique: true);

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
                name: "IX_SalesOrder_OrganisationId",
                schema: "APP",
                table: "SalesOrder",
                column: "OrganisationId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrder_PriceListId",
                schema: "APP",
                table: "SalesOrder",
                column: "PriceListId");

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
                name: "IX_SerialNumber_ProductId",
                schema: "APP",
                table: "SerialNumber",
                column: "ProductId");

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
                name: "IX_Vendor_OrganisationId",
                schema: "APP",
                table: "Vendor",
                column: "OrganisationId");

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountTransaction",
                schema: "APP");

            migrationBuilder.DropTable(
                name: "BatchNumber",
                schema: "APP");

            migrationBuilder.DropTable(
                name: "CategoryImages",
                schema: "APP");

            migrationBuilder.DropTable(
                name: "CustomerPriceList",
                schema: "APP");

            migrationBuilder.DropTable(
                name: "PriceListItem",
                schema: "APP");

            migrationBuilder.DropTable(
                name: "ProductAttributeValue",
                schema: "APP");

            migrationBuilder.DropTable(
                name: "ProductCategories",
                schema: "APP");

            migrationBuilder.DropTable(
                name: "ProductImages",
                schema: "APP");

            migrationBuilder.DropTable(
                name: "ProductInventory",
                schema: "APP");

            migrationBuilder.DropTable(
                name: "ProductPhysical",
                schema: "APP");

            migrationBuilder.DropTable(
                name: "ProductTax",
                schema: "APP");

            migrationBuilder.DropTable(
                name: "PurchaseOrderItem",
                schema: "APP");

            migrationBuilder.DropTable(
                name: "SalesOrderItem",
                schema: "APP");

            migrationBuilder.DropTable(
                name: "SerialNumber",
                schema: "APP");

            migrationBuilder.DropTable(
                name: "StockTransaction",
                schema: "APP");

            migrationBuilder.DropTable(
                name: "VendorPriceList",
                schema: "APP");

            migrationBuilder.DropTable(
                name: "ItemAttributeOption",
                schema: "APP");

            migrationBuilder.DropTable(
                name: "Categories",
                schema: "APP");

            migrationBuilder.DropTable(
                name: "TaxClassifications",
                schema: "APP");

            migrationBuilder.DropTable(
                name: "Products",
                schema: "APP");

            migrationBuilder.DropTable(
                name: "PurchaseOrder",
                schema: "APP");

            migrationBuilder.DropTable(
                name: "ItemAttribute",
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
                name: "SalesOrder",
                schema: "APP");

            migrationBuilder.DropTable(
                name: "Vendor",
                schema: "APP");

            migrationBuilder.DropTable(
                name: "ItemGroup",
                schema: "APP");

            migrationBuilder.DropTable(
                name: "AccountSubType",
                schema: "APP");

            migrationBuilder.DropTable(
                name: "Currencies",
                schema: "APP");

            migrationBuilder.DropTable(
                name: "PriceList",
                schema: "APP");

            migrationBuilder.DropTable(
                name: "Organisations",
                schema: "APP");
        }
    }
}
