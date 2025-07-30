using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PCI.Persistence.Migrations.Application
{
    /// <inheritdoc />
    public partial class ProductCahnges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_ItemGroup_ItemGroupId",
                schema: "APP",
                table: "Products");

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

            migrationBuilder.DropTable(
                name: "BatchNumber",
                schema: "APP");

            migrationBuilder.DropTable(
                name: "SerialNumber",
                schema: "APP");

            migrationBuilder.DropIndex(
                name: "IX_ProductTax_ProductId",
                schema: "APP",
                table: "ProductTax");

            migrationBuilder.DropIndex(
                name: "IX_Product_IsActive",
                schema: "APP",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Product_ItemGroupId",
                schema: "APP",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Product_OrganisationId_IsActive",
                schema: "APP",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "IsDropShipment",
                schema: "APP",
                table: "SalesOrders");

            migrationBuilder.DropColumn(
                name: "ShippingAddress",
                schema: "APP",
                table: "SalesOrders");

            migrationBuilder.DropColumn(
                name: "BatchTracking",
                schema: "APP",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Description",
                schema: "APP",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "IsActive",
                schema: "APP",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ItemGroupId",
                schema: "APP",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "SerialNumberTracking",
                schema: "APP",
                table: "Products");

            migrationBuilder.RenameIndex(
                name: "IX_SalesOrders_OrganisationId",
                schema: "APP",
                table: "SalesOrders",
                newName: "IX_SalesOrder_OrganisationId");

            migrationBuilder.RenameIndex(
                name: "IX_SalesOrders_CustomerId",
                schema: "APP",
                table: "SalesOrders",
                newName: "IX_SalesOrder_CustomerId");

            migrationBuilder.RenameColumn(
                name: "IsTaxable",
                schema: "APP",
                table: "Products",
                newName: "IsReturnable");

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalAmount",
                schema: "APP",
                table: "SalesOrders",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "TaxAmount",
                schema: "APP",
                table: "SalesOrders",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "SubTotal",
                schema: "APP",
                table: "SalesOrders",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                schema: "APP",
                table: "SalesOrders",
                type: "TEXT",
                maxLength: 20,
                nullable: true,
                defaultValue: "Draft",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "QuoteNumber",
                schema: "APP",
                table: "SalesOrders",
                type: "TEXT",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReferenceNumber",
                schema: "APP",
                table: "SalesOrders",
                type: "TEXT",
                maxLength: 100,
                nullable: true);

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

            migrationBuilder.AddCheckConstraint(
                name: "CK_SalesOrder_SubTotal_NonNegative",
                schema: "APP",
                table: "SalesOrders",
                sql: "SubTotal >= 0");

            migrationBuilder.AddCheckConstraint(
                name: "CK_SalesOrder_TaxAmount_NonNegative",
                schema: "APP",
                table: "SalesOrders",
                sql: "TaxAmount >= 0");

            migrationBuilder.AddCheckConstraint(
                name: "CK_SalesOrder_TotalAmount_NonNegative",
                schema: "APP",
                table: "SalesOrders",
                sql: "TotalAmount >= 0");

            migrationBuilder.CreateIndex(
                name: "IX_ProductTax_ProductId",
                schema: "APP",
                table: "ProductTax",
                column: "ProductId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_SalesOrders_Customers_CustomerId",
                schema: "APP",
                table: "SalesOrders",
                column: "CustomerId",
                principalSchema: "APP",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SalesOrders_Organisations_OrganisationId",
                schema: "APP",
                table: "SalesOrders",
                column: "OrganisationId",
                principalSchema: "APP",
                principalTable: "Organisations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SalesOrders_PriceList_PriceListId",
                schema: "APP",
                table: "SalesOrders",
                column: "PriceListId",
                principalSchema: "APP",
                principalTable: "PriceList",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.DropTable(
                name: "ProductItemGroups",
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

            migrationBuilder.DropIndex(
                name: "IX_SalesOrder_OrderDate",
                schema: "APP",
                table: "SalesOrders");

            migrationBuilder.DropIndex(
                name: "IX_SalesOrder_OrderNumber",
                schema: "APP",
                table: "SalesOrders");

            migrationBuilder.DropIndex(
                name: "IX_SalesOrder_OrganisationId_CustomerId",
                schema: "APP",
                table: "SalesOrders");

            migrationBuilder.DropIndex(
                name: "IX_SalesOrder_OrganisationId_OrderDate",
                schema: "APP",
                table: "SalesOrders");

            migrationBuilder.DropIndex(
                name: "IX_SalesOrder_OrganisationId_Status",
                schema: "APP",
                table: "SalesOrders");

            migrationBuilder.DropIndex(
                name: "IX_SalesOrder_ReferenceNumber",
                schema: "APP",
                table: "SalesOrders");

            migrationBuilder.DropIndex(
                name: "IX_SalesOrder_Status",
                schema: "APP",
                table: "SalesOrders");

            migrationBuilder.DropCheckConstraint(
                name: "CK_SalesOrder_SubTotal_NonNegative",
                schema: "APP",
                table: "SalesOrders");

            migrationBuilder.DropCheckConstraint(
                name: "CK_SalesOrder_TaxAmount_NonNegative",
                schema: "APP",
                table: "SalesOrders");

            migrationBuilder.DropCheckConstraint(
                name: "CK_SalesOrder_TotalAmount_NonNegative",
                schema: "APP",
                table: "SalesOrders");

            migrationBuilder.DropIndex(
                name: "IX_ProductTax_ProductId",
                schema: "APP",
                table: "ProductTax");

            migrationBuilder.DropColumn(
                name: "QuoteNumber",
                schema: "APP",
                table: "SalesOrders");

            migrationBuilder.DropColumn(
                name: "ReferenceNumber",
                schema: "APP",
                table: "SalesOrders");

            migrationBuilder.RenameIndex(
                name: "IX_SalesOrder_OrganisationId",
                schema: "APP",
                table: "SalesOrders",
                newName: "IX_SalesOrders_OrganisationId");

            migrationBuilder.RenameIndex(
                name: "IX_SalesOrder_CustomerId",
                schema: "APP",
                table: "SalesOrders",
                newName: "IX_SalesOrders_CustomerId");

            migrationBuilder.RenameColumn(
                name: "IsReturnable",
                schema: "APP",
                table: "Products",
                newName: "IsTaxable");

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalAmount",
                schema: "APP",
                table: "SalesOrders",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldDefaultValue: 0m);

            migrationBuilder.AlterColumn<decimal>(
                name: "TaxAmount",
                schema: "APP",
                table: "SalesOrders",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldDefaultValue: 0m);

            migrationBuilder.AlterColumn<decimal>(
                name: "SubTotal",
                schema: "APP",
                table: "SalesOrders",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldDefaultValue: 0m);

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                schema: "APP",
                table: "SalesOrders",
                type: "TEXT",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 20,
                oldNullable: true,
                oldDefaultValue: "Draft");

            migrationBuilder.AddColumn<bool>(
                name: "IsDropShipment",
                schema: "APP",
                table: "SalesOrders",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ShippingAddress",
                schema: "APP",
                table: "SalesOrders",
                type: "TEXT",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "BatchTracking",
                schema: "APP",
                table: "Products",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                schema: "APP",
                table: "Products",
                type: "TEXT",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                schema: "APP",
                table: "Products",
                type: "INTEGER",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddColumn<int>(
                name: "ItemGroupId",
                schema: "APP",
                table: "Products",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "SerialNumberTracking",
                schema: "APP",
                table: "Products",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "BatchNumber",
                schema: "APP",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ProductId = table.Column<int>(type: "INTEGER", nullable: false),
                    BatchNumberValue = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ExpiryDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ManufactureDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "TEXT", nullable: true),
                    QuantityAvailable = table.Column<int>(type: "INTEGER", nullable: false),
                    Status = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true)
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
                name: "SerialNumber",
                schema: "APP",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ProductId = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ReturnedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    SerialNumberValue = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    SoldDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Status = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true)
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

            migrationBuilder.CreateIndex(
                name: "IX_ProductTax_ProductId",
                schema: "APP",
                table: "ProductTax",
                column: "ProductId",
                unique: true);

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
                name: "IX_Product_OrganisationId_IsActive",
                schema: "APP",
                table: "Products",
                columns: new[] { "OrganisationId", "IsActive" });

            migrationBuilder.CreateIndex(
                name: "IX_BatchNumber_ProductId",
                schema: "APP",
                table: "BatchNumber",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_SerialNumber_ProductId",
                schema: "APP",
                table: "SerialNumber",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ItemGroup_ItemGroupId",
                schema: "APP",
                table: "Products",
                column: "ItemGroupId",
                principalSchema: "APP",
                principalTable: "ItemGroup",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

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
        }
    }
}
