using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PCI.Persistence.Migrations.Application
{
    /// <inheritdoc />
    public partial class CustomersAndVendors : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductInventory_Vendor_VendorId",
                schema: "APP",
                table: "ProductInventory");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Vendor_VendorId",
                schema: "APP",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseOrder_SalesOrder_OriginatingSalesOrderId",
                schema: "APP",
                table: "PurchaseOrder");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseOrder_Vendor_VendorId",
                schema: "APP",
                table: "PurchaseOrder");

            migrationBuilder.DropForeignKey(
                name: "FK_SalesOrder_Organisations_OrganisationId",
                schema: "APP",
                table: "SalesOrder");

            migrationBuilder.DropForeignKey(
                name: "FK_SalesOrder_PriceList_PriceListId",
                schema: "APP",
                table: "SalesOrder");

            migrationBuilder.DropForeignKey(
                name: "FK_SalesOrderItem_SalesOrder_SalesOrderId",
                schema: "APP",
                table: "SalesOrderItem");

            migrationBuilder.DropForeignKey(
                name: "FK_StockTransaction_SalesOrder_SalesOrderId",
                schema: "APP",
                table: "StockTransaction");

            migrationBuilder.DropForeignKey(
                name: "FK_Vendor_Organisations_OrganisationId",
                schema: "APP",
                table: "Vendor");

            migrationBuilder.DropForeignKey(
                name: "FK_VendorPriceList_Vendor_VendorId",
                schema: "APP",
                table: "VendorPriceList");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Vendor",
                schema: "APP",
                table: "Vendor");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SalesOrder",
                schema: "APP",
                table: "SalesOrder");

            migrationBuilder.DeleteData(
                schema: "APP",
                table: "AccountSubType",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                schema: "APP",
                table: "AccountSubType",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                schema: "APP",
                table: "AccountSubType",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                schema: "APP",
                table: "AccountSubType",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                schema: "APP",
                table: "AccountSubType",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                schema: "APP",
                table: "AccountSubType",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                schema: "APP",
                table: "AccountSubType",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                schema: "APP",
                table: "AccountSubType",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                schema: "APP",
                table: "AccountSubType",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                schema: "APP",
                table: "AccountSubType",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                schema: "APP",
                table: "AccountSubType",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                schema: "APP",
                table: "AccountSubType",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                schema: "APP",
                table: "AccountSubType",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                schema: "APP",
                table: "AccountSubType",
                keyColumn: "Id",
                keyValue: 40);

            migrationBuilder.DeleteData(
                schema: "APP",
                table: "AccountSubType",
                keyColumn: "Id",
                keyValue: 41);

            migrationBuilder.DeleteData(
                schema: "APP",
                table: "AccountSubType",
                keyColumn: "Id",
                keyValue: 42);

            migrationBuilder.DeleteData(
                schema: "APP",
                table: "AccountSubType",
                keyColumn: "Id",
                keyValue: 60);

            migrationBuilder.DeleteData(
                schema: "APP",
                table: "AccountSubType",
                keyColumn: "Id",
                keyValue: 61);

            migrationBuilder.DeleteData(
                schema: "APP",
                table: "AccountSubType",
                keyColumn: "Id",
                keyValue: 62);

            migrationBuilder.DeleteData(
                schema: "APP",
                table: "AccountSubType",
                keyColumn: "Id",
                keyValue: 63);

            migrationBuilder.DeleteData(
                schema: "APP",
                table: "AccountSubType",
                keyColumn: "Id",
                keyValue: 80);

            migrationBuilder.DeleteData(
                schema: "APP",
                table: "AccountSubType",
                keyColumn: "Id",
                keyValue: 81);

            migrationBuilder.DeleteData(
                schema: "APP",
                table: "AccountSubType",
                keyColumn: "Id",
                keyValue: 82);

            migrationBuilder.DeleteData(
                schema: "APP",
                table: "AccountSubType",
                keyColumn: "Id",
                keyValue: 83);

            migrationBuilder.DeleteData(
                schema: "APP",
                table: "AccountSubType",
                keyColumn: "Id",
                keyValue: 84);

            migrationBuilder.DeleteData(
                schema: "APP",
                table: "AccountSubType",
                keyColumn: "Id",
                keyValue: 85);

            migrationBuilder.DeleteData(
                schema: "APP",
                table: "AccountSubType",
                keyColumn: "Id",
                keyValue: 86);

            migrationBuilder.DropColumn(
                name: "City",
                schema: "APP",
                table: "Vendor");

            migrationBuilder.DropColumn(
                name: "ContactPerson",
                schema: "APP",
                table: "Vendor");

            migrationBuilder.DropColumn(
                name: "Country",
                schema: "APP",
                table: "Vendor");

            migrationBuilder.DropColumn(
                name: "IsActive",
                schema: "APP",
                table: "Vendor");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                schema: "APP",
                table: "Vendor");

            migrationBuilder.DropColumn(
                name: "PostalCode",
                schema: "APP",
                table: "Vendor");

            migrationBuilder.RenameTable(
                name: "Vendor",
                schema: "APP",
                newName: "Vendors",
                newSchema: "APP");

            migrationBuilder.RenameTable(
                name: "SalesOrder",
                schema: "APP",
                newName: "SalesOrders",
                newSchema: "APP");

            migrationBuilder.RenameColumn(
                name: "State",
                schema: "APP",
                table: "Vendors",
                newName: "PortalAccessEmail");

            migrationBuilder.RenameColumn(
                name: "Email",
                schema: "APP",
                table: "Vendors",
                newName: "Industry");

            migrationBuilder.RenameColumn(
                name: "Address",
                schema: "APP",
                table: "Vendors",
                newName: "StatusChangeReason");

            migrationBuilder.RenameIndex(
                name: "IX_SalesOrder_PriceListId",
                schema: "APP",
                table: "SalesOrders",
                newName: "IX_SalesOrders_PriceListId");

            migrationBuilder.RenameIndex(
                name: "IX_SalesOrder_OrganisationId",
                schema: "APP",
                table: "SalesOrders",
                newName: "IX_SalesOrders_OrganisationId");

            migrationBuilder.AddColumn<int>(
                name: "InvoiceId",
                schema: "APP",
                table: "AccountTransaction",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Category",
                schema: "APP",
                table: "Vendors",
                type: "INTEGER",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.AddColumn<string>(
                name: "CompanyName",
                schema: "APP",
                table: "Vendors",
                type: "TEXT",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CurrencyId",
                schema: "APP",
                table: "Vendors",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "HasPortalAccess",
                schema: "APP",
                table: "Vendors",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDropshipVendor",
                schema: "APP",
                table: "Vendors",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsManufacturer",
                schema: "APP",
                table: "Vendors",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Notes",
                schema: "APP",
                table: "Vendors",
                type: "TEXT",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ParentVendorId",
                schema: "APP",
                table: "Vendors",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PreferredCommunicationMethod",
                schema: "APP",
                table: "Vendors",
                type: "TEXT",
                maxLength: 50,
                nullable: true,
                defaultValue: "Email");

            migrationBuilder.AddColumn<bool>(
                name: "RequiresPOApproval",
                schema: "APP",
                table: "Vendors",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                schema: "APP",
                table: "Vendors",
                type: "INTEGER",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.AddColumn<DateTime>(
                name: "StatusChangedDate",
                schema: "APP",
                table: "Vendors",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "VendorType",
                schema: "APP",
                table: "Vendors",
                type: "INTEGER",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Vendors",
                schema: "APP",
                table: "Vendors",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SalesOrders",
                schema: "APP",
                table: "SalesOrders",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Customers",
                schema: "APP",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CustomerCode = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    CustomerName = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    CompanyName = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true),
                    WebsiteUrl = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true),
                    CustomerType = table.Column<int>(type: "INTEGER", nullable: false, defaultValue: 1),
                    CurrencyId = table.Column<int>(type: "INTEGER", nullable: true),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: true),
                    Notes = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: true),
                    OrganisationId = table.Column<int>(type: "INTEGER", nullable: false),
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
                    OrganisationId = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VendorFinancials", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VendorFinancials_Organisations_OrganisationId",
                        column: x => x.OrganisationId,
                        principalSchema: "APP",
                        principalTable: "Organisations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                    OrganisationId = table.Column<int>(type: "INTEGER", nullable: false),
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
                        name: "FK_VendorPerformances_Organisations_OrganisationId",
                        column: x => x.OrganisationId,
                        principalSchema: "APP",
                        principalTable: "Organisations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VendorPerformances_Vendors_VendorId",
                        column: x => x.VendorId,
                        principalSchema: "APP",
                        principalTable: "Vendors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                    OrganisationId = table.Column<int>(type: "INTEGER", nullable: false),
                    CustomerId = table.Column<int>(type: "INTEGER", nullable: true),
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
                        name: "FK_BusinessAddresses_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalSchema: "APP",
                        principalTable: "Customers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BusinessAddresses_Organisations_OrganisationId",
                        column: x => x.OrganisationId,
                        principalSchema: "APP",
                        principalTable: "Organisations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                    OrganisationId = table.Column<int>(type: "INTEGER", nullable: false),
                    CustomerId = table.Column<int>(type: "INTEGER", nullable: true),
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
                        name: "FK_BusinessBankInfos_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalSchema: "APP",
                        principalTable: "Customers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BusinessBankInfos_Organisations_OrganisationId",
                        column: x => x.OrganisationId,
                        principalSchema: "APP",
                        principalTable: "Organisations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                    ContactPersonName = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    JobTitle = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    Department = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    Role = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    Email = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    PhoneNumber = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    MobileNumber = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    Extension = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    LinkedInProfile = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    IsPrimary = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: true),
                    Notes = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    OrganisationId = table.Column<int>(type: "INTEGER", nullable: false),
                    CustomerId = table.Column<int>(type: "INTEGER", nullable: true),
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
                        name: "FK_BusinessContacts_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalSchema: "APP",
                        principalTable: "Customers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BusinessContacts_Organisations_OrganisationId",
                        column: x => x.OrganisationId,
                        principalSchema: "APP",
                        principalTable: "Organisations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                    IssuingAuthority = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true),
                    TaxCategory = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    ValidFrom = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ValidTo = table.Column<DateTime>(type: "TEXT", nullable: true),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: true),
                    IsPrimary = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                    Notes = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    OrganisationId = table.Column<int>(type: "INTEGER", nullable: false),
                    CustomerId = table.Column<int>(type: "INTEGER", nullable: true),
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
                        name: "FK_BusinessTaxInfos_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalSchema: "APP",
                        principalTable: "Customers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BusinessTaxInfos_Organisations_OrganisationId",
                        column: x => x.OrganisationId,
                        principalSchema: "APP",
                        principalTable: "Organisations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BusinessTaxInfos_Vendors_VendorId",
                        column: x => x.VendorId,
                        principalSchema: "APP",
                        principalTable: "Vendors",
                        principalColumn: "Id");
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
                    OrganisationId = table.Column<int>(type: "INTEGER", nullable: false),
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
                    table.ForeignKey(
                        name: "FK_CustomerFinancials_Organisations_OrganisationId",
                        column: x => x.OrganisationId,
                        principalSchema: "APP",
                        principalTable: "Organisations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                    Status = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    CustomerId = table.Column<int>(type: "INTEGER", nullable: false),
                    OrganisationId = table.Column<int>(type: "INTEGER", nullable: false),
                    SalesOrderId = table.Column<int>(type: "INTEGER", nullable: true),
                    SubTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TaxAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AmountPaid = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
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
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Invoices_Organisations_OrganisationId",
                        column: x => x.OrganisationId,
                        principalSchema: "APP",
                        principalTable: "Organisations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Invoices_PriceList_PriceListId",
                        column: x => x.PriceListId,
                        principalSchema: "APP",
                        principalTable: "PriceList",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Invoices_SalesOrders_SalesOrderId",
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
                name: "IX_CustomerPriceList_CustomerId",
                schema: "APP",
                table: "CustomerPriceList",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountTransaction_InvoiceId",
                schema: "APP",
                table: "AccountTransaction",
                column: "InvoiceId");

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

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrders_CustomerId",
                schema: "APP",
                table: "SalesOrders",
                column: "CustomerId");

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
                name: "IX_BusinessAddress_Organisation_EntityType",
                schema: "APP",
                table: "BusinessAddresses",
                columns: new[] { "OrganisationId", "EntityType" });

            migrationBuilder.CreateIndex(
                name: "IX_BusinessAddresses_CustomerId",
                schema: "APP",
                table: "BusinessAddresses",
                column: "CustomerId");

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
                name: "IX_BusinessBankInfo_Organisation_EntityType",
                schema: "APP",
                table: "BusinessBankInfos",
                columns: new[] { "OrganisationId", "EntityType" });

            migrationBuilder.CreateIndex(
                name: "IX_BusinessBankInfos_CustomerId",
                schema: "APP",
                table: "BusinessBankInfos",
                column: "CustomerId");

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
                name: "IX_BusinessContact_Organisation_EntityType",
                schema: "APP",
                table: "BusinessContacts",
                columns: new[] { "OrganisationId", "EntityType" });

            migrationBuilder.CreateIndex(
                name: "IX_BusinessContacts_CustomerId",
                schema: "APP",
                table: "BusinessContacts",
                column: "CustomerId");

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
                name: "IX_BusinessTaxInfo_Organisation_EntityType",
                schema: "APP",
                table: "BusinessTaxInfos",
                columns: new[] { "OrganisationId", "EntityType" });

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
                name: "IX_BusinessTaxInfos_CustomerId",
                schema: "APP",
                table: "BusinessTaxInfos",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessTaxInfos_VendorId",
                schema: "APP",
                table: "BusinessTaxInfos",
                column: "VendorId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerFinancials_CustomerId",
                schema: "APP",
                table: "CustomerFinancials",
                column: "CustomerId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CustomerFinancials_OrganisationId",
                schema: "APP",
                table: "CustomerFinancials",
                column: "OrganisationId");

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
                name: "IX_Customer_CustomerName",
                schema: "APP",
                table: "Customers",
                column: "CustomerName");

            migrationBuilder.CreateIndex(
                name: "IX_Customer_CustomerType",
                schema: "APP",
                table: "Customers",
                column: "CustomerType");

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
                name: "IX_VendorFinancials_OrganisationId",
                schema: "APP",
                table: "VendorFinancials",
                column: "OrganisationId");

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
                name: "IX_VendorPerformance_Organisation_IsPreferredVendor",
                schema: "APP",
                table: "VendorPerformances",
                columns: new[] { "OrganisationId", "IsPreferredVendor" });

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

            migrationBuilder.AddForeignKey(
                name: "FK_AccountTransaction_Invoices_InvoiceId",
                schema: "APP",
                table: "AccountTransaction",
                column: "InvoiceId",
                principalSchema: "APP",
                principalTable: "Invoices",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerPriceList_Customers_CustomerId",
                schema: "APP",
                table: "CustomerPriceList",
                column: "CustomerId",
                principalSchema: "APP",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductInventory_Vendors_VendorId",
                schema: "APP",
                table: "ProductInventory",
                column: "VendorId",
                principalSchema: "APP",
                principalTable: "Vendors",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Vendors_VendorId",
                schema: "APP",
                table: "Products",
                column: "VendorId",
                principalSchema: "APP",
                principalTable: "Vendors",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseOrder_SalesOrders_OriginatingSalesOrderId",
                schema: "APP",
                table: "PurchaseOrder",
                column: "OriginatingSalesOrderId",
                principalSchema: "APP",
                principalTable: "SalesOrders",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseOrder_Vendors_VendorId",
                schema: "APP",
                table: "PurchaseOrder",
                column: "VendorId",
                principalSchema: "APP",
                principalTable: "Vendors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SalesOrderItem_SalesOrders_SalesOrderId",
                schema: "APP",
                table: "SalesOrderItem",
                column: "SalesOrderId",
                principalSchema: "APP",
                principalTable: "SalesOrders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SalesOrders_Customers_CustomerId",
                schema: "APP",
                table: "SalesOrders",
                column: "CustomerId",
                principalSchema: "APP",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SalesOrders_Organisations_OrganisationId",
                schema: "APP",
                table: "SalesOrders",
                column: "OrganisationId",
                principalSchema: "APP",
                principalTable: "Organisations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SalesOrders_PriceList_PriceListId",
                schema: "APP",
                table: "SalesOrders",
                column: "PriceListId",
                principalSchema: "APP",
                principalTable: "PriceList",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StockTransaction_SalesOrders_SalesOrderId",
                schema: "APP",
                table: "StockTransaction",
                column: "SalesOrderId",
                principalSchema: "APP",
                principalTable: "SalesOrders",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_VendorPriceList_Vendors_VendorId",
                schema: "APP",
                table: "VendorPriceList",
                column: "VendorId",
                principalSchema: "APP",
                principalTable: "Vendors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Vendors_Currencies_CurrencyId",
                schema: "APP",
                table: "Vendors",
                column: "CurrencyId",
                principalSchema: "APP",
                principalTable: "Currencies",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Vendors_Organisations_OrganisationId",
                schema: "APP",
                table: "Vendors",
                column: "OrganisationId",
                principalSchema: "APP",
                principalTable: "Organisations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Vendors_Vendors_ParentVendorId",
                schema: "APP",
                table: "Vendors",
                column: "ParentVendorId",
                principalSchema: "APP",
                principalTable: "Vendors",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountTransaction_Invoices_InvoiceId",
                schema: "APP",
                table: "AccountTransaction");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerPriceList_Customers_CustomerId",
                schema: "APP",
                table: "CustomerPriceList");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductInventory_Vendors_VendorId",
                schema: "APP",
                table: "ProductInventory");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Vendors_VendorId",
                schema: "APP",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseOrder_SalesOrders_OriginatingSalesOrderId",
                schema: "APP",
                table: "PurchaseOrder");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseOrder_Vendors_VendorId",
                schema: "APP",
                table: "PurchaseOrder");

            migrationBuilder.DropForeignKey(
                name: "FK_SalesOrderItem_SalesOrders_SalesOrderId",
                schema: "APP",
                table: "SalesOrderItem");

            migrationBuilder.DropForeignKey(
                name: "FK_SalesOrders_Customers_CustomerId",
                schema: "APP",
                table: "SalesOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_SalesOrders_Organisations_OrganisationId",
                schema: "APP",
                table: "SalesOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_SalesOrders_PriceList_PriceListId",
                schema: "APP",
                table: "SalesOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_StockTransaction_SalesOrders_SalesOrderId",
                schema: "APP",
                table: "StockTransaction");

            migrationBuilder.DropForeignKey(
                name: "FK_VendorPriceList_Vendors_VendorId",
                schema: "APP",
                table: "VendorPriceList");

            migrationBuilder.DropForeignKey(
                name: "FK_Vendors_Currencies_CurrencyId",
                schema: "APP",
                table: "Vendors");

            migrationBuilder.DropForeignKey(
                name: "FK_Vendors_Organisations_OrganisationId",
                schema: "APP",
                table: "Vendors");

            migrationBuilder.DropForeignKey(
                name: "FK_Vendors_Vendors_ParentVendorId",
                schema: "APP",
                table: "Vendors");

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
                name: "CustomerFinancials",
                schema: "APP");

            migrationBuilder.DropTable(
                name: "InvoiceItem",
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
                name: "Invoices",
                schema: "APP");

            migrationBuilder.DropTable(
                name: "Customers",
                schema: "APP");

            migrationBuilder.DropIndex(
                name: "IX_CustomerPriceList_CustomerId",
                schema: "APP",
                table: "CustomerPriceList");

            migrationBuilder.DropIndex(
                name: "IX_AccountTransaction_InvoiceId",
                schema: "APP",
                table: "AccountTransaction");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Vendors",
                schema: "APP",
                table: "Vendors");

            migrationBuilder.DropIndex(
                name: "IX_Vendor_Category",
                schema: "APP",
                table: "Vendors");

            migrationBuilder.DropIndex(
                name: "IX_Vendor_CurrencyId",
                schema: "APP",
                table: "Vendors");

            migrationBuilder.DropIndex(
                name: "IX_Vendor_Industry",
                schema: "APP",
                table: "Vendors");

            migrationBuilder.DropIndex(
                name: "IX_Vendor_IsManufacturer",
                schema: "APP",
                table: "Vendors");

            migrationBuilder.DropIndex(
                name: "IX_Vendor_OrganisationId_Category",
                schema: "APP",
                table: "Vendors");

            migrationBuilder.DropIndex(
                name: "IX_Vendor_OrganisationId_Status",
                schema: "APP",
                table: "Vendors");

            migrationBuilder.DropIndex(
                name: "IX_Vendor_OrganisationId_VendorCode",
                schema: "APP",
                table: "Vendors");

            migrationBuilder.DropIndex(
                name: "IX_Vendor_OrganisationId_VendorType",
                schema: "APP",
                table: "Vendors");

            migrationBuilder.DropIndex(
                name: "IX_Vendor_ParentVendorId",
                schema: "APP",
                table: "Vendors");

            migrationBuilder.DropIndex(
                name: "IX_Vendor_Status",
                schema: "APP",
                table: "Vendors");

            migrationBuilder.DropIndex(
                name: "IX_Vendor_VendorCode",
                schema: "APP",
                table: "Vendors");

            migrationBuilder.DropIndex(
                name: "IX_Vendor_VendorName",
                schema: "APP",
                table: "Vendors");

            migrationBuilder.DropIndex(
                name: "IX_Vendor_VendorType",
                schema: "APP",
                table: "Vendors");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SalesOrders",
                schema: "APP",
                table: "SalesOrders");

            migrationBuilder.DropIndex(
                name: "IX_SalesOrders_CustomerId",
                schema: "APP",
                table: "SalesOrders");

            migrationBuilder.DropColumn(
                name: "InvoiceId",
                schema: "APP",
                table: "AccountTransaction");

            migrationBuilder.DropColumn(
                name: "Category",
                schema: "APP",
                table: "Vendors");

            migrationBuilder.DropColumn(
                name: "CompanyName",
                schema: "APP",
                table: "Vendors");

            migrationBuilder.DropColumn(
                name: "CurrencyId",
                schema: "APP",
                table: "Vendors");

            migrationBuilder.DropColumn(
                name: "HasPortalAccess",
                schema: "APP",
                table: "Vendors");

            migrationBuilder.DropColumn(
                name: "IsDropshipVendor",
                schema: "APP",
                table: "Vendors");

            migrationBuilder.DropColumn(
                name: "IsManufacturer",
                schema: "APP",
                table: "Vendors");

            migrationBuilder.DropColumn(
                name: "Notes",
                schema: "APP",
                table: "Vendors");

            migrationBuilder.DropColumn(
                name: "ParentVendorId",
                schema: "APP",
                table: "Vendors");

            migrationBuilder.DropColumn(
                name: "PreferredCommunicationMethod",
                schema: "APP",
                table: "Vendors");

            migrationBuilder.DropColumn(
                name: "RequiresPOApproval",
                schema: "APP",
                table: "Vendors");

            migrationBuilder.DropColumn(
                name: "Status",
                schema: "APP",
                table: "Vendors");

            migrationBuilder.DropColumn(
                name: "StatusChangedDate",
                schema: "APP",
                table: "Vendors");

            migrationBuilder.DropColumn(
                name: "VendorType",
                schema: "APP",
                table: "Vendors");

            migrationBuilder.RenameTable(
                name: "Vendors",
                schema: "APP",
                newName: "Vendor",
                newSchema: "APP");

            migrationBuilder.RenameTable(
                name: "SalesOrders",
                schema: "APP",
                newName: "SalesOrder",
                newSchema: "APP");

            migrationBuilder.RenameColumn(
                name: "StatusChangeReason",
                schema: "APP",
                table: "Vendor",
                newName: "Address");

            migrationBuilder.RenameColumn(
                name: "PortalAccessEmail",
                schema: "APP",
                table: "Vendor",
                newName: "State");

            migrationBuilder.RenameColumn(
                name: "Industry",
                schema: "APP",
                table: "Vendor",
                newName: "Email");

            migrationBuilder.RenameIndex(
                name: "IX_SalesOrders_PriceListId",
                schema: "APP",
                table: "SalesOrder",
                newName: "IX_SalesOrder_PriceListId");

            migrationBuilder.RenameIndex(
                name: "IX_SalesOrders_OrganisationId",
                schema: "APP",
                table: "SalesOrder",
                newName: "IX_SalesOrder_OrganisationId");

            migrationBuilder.AddColumn<string>(
                name: "City",
                schema: "APP",
                table: "Vendor",
                type: "TEXT",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ContactPerson",
                schema: "APP",
                table: "Vendor",
                type: "TEXT",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Country",
                schema: "APP",
                table: "Vendor",
                type: "TEXT",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                schema: "APP",
                table: "Vendor",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                schema: "APP",
                table: "Vendor",
                type: "TEXT",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PostalCode",
                schema: "APP",
                table: "Vendor",
                type: "TEXT",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Vendor",
                schema: "APP",
                table: "Vendor",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SalesOrder",
                schema: "APP",
                table: "SalesOrder",
                column: "Id");

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

            migrationBuilder.AddForeignKey(
                name: "FK_ProductInventory_Vendor_VendorId",
                schema: "APP",
                table: "ProductInventory",
                column: "VendorId",
                principalSchema: "APP",
                principalTable: "Vendor",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Vendor_VendorId",
                schema: "APP",
                table: "Products",
                column: "VendorId",
                principalSchema: "APP",
                principalTable: "Vendor",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseOrder_SalesOrder_OriginatingSalesOrderId",
                schema: "APP",
                table: "PurchaseOrder",
                column: "OriginatingSalesOrderId",
                principalSchema: "APP",
                principalTable: "SalesOrder",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseOrder_Vendor_VendorId",
                schema: "APP",
                table: "PurchaseOrder",
                column: "VendorId",
                principalSchema: "APP",
                principalTable: "Vendor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SalesOrder_Organisations_OrganisationId",
                schema: "APP",
                table: "SalesOrder",
                column: "OrganisationId",
                principalSchema: "APP",
                principalTable: "Organisations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SalesOrder_PriceList_PriceListId",
                schema: "APP",
                table: "SalesOrder",
                column: "PriceListId",
                principalSchema: "APP",
                principalTable: "PriceList",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SalesOrderItem_SalesOrder_SalesOrderId",
                schema: "APP",
                table: "SalesOrderItem",
                column: "SalesOrderId",
                principalSchema: "APP",
                principalTable: "SalesOrder",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StockTransaction_SalesOrder_SalesOrderId",
                schema: "APP",
                table: "StockTransaction",
                column: "SalesOrderId",
                principalSchema: "APP",
                principalTable: "SalesOrder",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Vendor_Organisations_OrganisationId",
                schema: "APP",
                table: "Vendor",
                column: "OrganisationId",
                principalSchema: "APP",
                principalTable: "Organisations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VendorPriceList_Vendor_VendorId",
                schema: "APP",
                table: "VendorPriceList",
                column: "VendorId",
                principalSchema: "APP",
                principalTable: "Vendor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
