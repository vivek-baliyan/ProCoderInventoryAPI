using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PCI.Persistence.Migrations.Application
{
    /// <inheritdoc />
    public partial class CustomerUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BusinessAddresses_Organisations_OrganisationId",
                schema: "APP",
                table: "BusinessAddresses");

            migrationBuilder.DropForeignKey(
                name: "FK_BusinessBankInfos_Organisations_OrganisationId",
                schema: "APP",
                table: "BusinessBankInfos");

            migrationBuilder.DropForeignKey(
                name: "FK_BusinessContacts_Organisations_OrganisationId",
                schema: "APP",
                table: "BusinessContacts");

            migrationBuilder.DropForeignKey(
                name: "FK_BusinessTaxInfos_Organisations_OrganisationId",
                schema: "APP",
                table: "BusinessTaxInfos");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerFinancials_Organisations_OrganisationId",
                schema: "APP",
                table: "CustomerFinancials");

            migrationBuilder.DropForeignKey(
                name: "FK_VendorFinancials_Organisations_OrganisationId",
                schema: "APP",
                table: "VendorFinancials");

            migrationBuilder.DropForeignKey(
                name: "FK_VendorPerformances_Organisations_OrganisationId",
                schema: "APP",
                table: "VendorPerformances");

            migrationBuilder.DropIndex(
                name: "IX_VendorPerformance_Organisation_IsPreferredVendor",
                schema: "APP",
                table: "VendorPerformances");

            migrationBuilder.DropIndex(
                name: "IX_VendorFinancials_OrganisationId",
                schema: "APP",
                table: "VendorFinancials");

            migrationBuilder.DropIndex(
                name: "IX_CustomerFinancials_OrganisationId",
                schema: "APP",
                table: "CustomerFinancials");

            migrationBuilder.DropIndex(
                name: "IX_BusinessTaxInfo_Organisation_EntityType",
                schema: "APP",
                table: "BusinessTaxInfos");

            migrationBuilder.DropIndex(
                name: "IX_BusinessContact_Organisation_EntityType",
                schema: "APP",
                table: "BusinessContacts");

            migrationBuilder.DropIndex(
                name: "IX_BusinessBankInfo_Organisation_EntityType",
                schema: "APP",
                table: "BusinessBankInfos");

            migrationBuilder.DropIndex(
                name: "IX_BusinessAddress_Organisation_EntityType",
                schema: "APP",
                table: "BusinessAddresses");

            migrationBuilder.DropColumn(
                name: "OrganisationId",
                schema: "APP",
                table: "VendorPerformances");

            migrationBuilder.DropColumn(
                name: "OrganisationId",
                schema: "APP",
                table: "VendorFinancials");

            migrationBuilder.DropColumn(
                name: "OrganisationId",
                schema: "APP",
                table: "CustomerFinancials");

            migrationBuilder.DropColumn(
                name: "IssuingAuthority",
                schema: "APP",
                table: "BusinessTaxInfos");

            migrationBuilder.DropColumn(
                name: "OrganisationId",
                schema: "APP",
                table: "BusinessTaxInfos");

            migrationBuilder.DropColumn(
                name: "TaxCategory",
                schema: "APP",
                table: "BusinessTaxInfos");

            migrationBuilder.DropColumn(
                name: "ValidFrom",
                schema: "APP",
                table: "BusinessTaxInfos");

            migrationBuilder.DropColumn(
                name: "ValidTo",
                schema: "APP",
                table: "BusinessTaxInfos");

            migrationBuilder.DropColumn(
                name: "ContactPersonName",
                schema: "APP",
                table: "BusinessContacts");

            migrationBuilder.DropColumn(
                name: "Department",
                schema: "APP",
                table: "BusinessContacts");

            migrationBuilder.DropColumn(
                name: "JobTitle",
                schema: "APP",
                table: "BusinessContacts");

            migrationBuilder.DropColumn(
                name: "Notes",
                schema: "APP",
                table: "BusinessContacts");

            migrationBuilder.DropColumn(
                name: "OrganisationId",
                schema: "APP",
                table: "BusinessContacts");

            migrationBuilder.DropColumn(
                name: "OrganisationId",
                schema: "APP",
                table: "BusinessBankInfos");

            migrationBuilder.DropColumn(
                name: "OrganisationId",
                schema: "APP",
                table: "BusinessAddresses");

            migrationBuilder.RenameColumn(
                name: "Role",
                schema: "APP",
                table: "BusinessContacts",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "LinkedInProfile",
                schema: "APP",
                table: "BusinessContacts",
                newName: "FirstName");

            migrationBuilder.RenameColumn(
                name: "Extension",
                schema: "APP",
                table: "BusinessContacts",
                newName: "Salutation");

            migrationBuilder.AddColumn<string>(
                name: "PaymentTerms",
                schema: "APP",
                table: "CustomerFinancials",
                type: "TEXT",
                maxLength: 100,
                nullable: true,
                defaultValue: "Due on Receipt");

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomerDocuments",
                schema: "APP");

            migrationBuilder.DropColumn(
                name: "PaymentTerms",
                schema: "APP",
                table: "CustomerFinancials");

            migrationBuilder.RenameColumn(
                name: "Salutation",
                schema: "APP",
                table: "BusinessContacts",
                newName: "Extension");

            migrationBuilder.RenameColumn(
                name: "LastName",
                schema: "APP",
                table: "BusinessContacts",
                newName: "Role");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                schema: "APP",
                table: "BusinessContacts",
                newName: "LinkedInProfile");

            migrationBuilder.AddColumn<int>(
                name: "OrganisationId",
                schema: "APP",
                table: "VendorPerformances",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OrganisationId",
                schema: "APP",
                table: "VendorFinancials",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OrganisationId",
                schema: "APP",
                table: "CustomerFinancials",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "IssuingAuthority",
                schema: "APP",
                table: "BusinessTaxInfos",
                type: "TEXT",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrganisationId",
                schema: "APP",
                table: "BusinessTaxInfos",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "TaxCategory",
                schema: "APP",
                table: "BusinessTaxInfos",
                type: "TEXT",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ValidFrom",
                schema: "APP",
                table: "BusinessTaxInfos",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ValidTo",
                schema: "APP",
                table: "BusinessTaxInfos",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ContactPersonName",
                schema: "APP",
                table: "BusinessContacts",
                type: "TEXT",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Department",
                schema: "APP",
                table: "BusinessContacts",
                type: "TEXT",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "JobTitle",
                schema: "APP",
                table: "BusinessContacts",
                type: "TEXT",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Notes",
                schema: "APP",
                table: "BusinessContacts",
                type: "TEXT",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrganisationId",
                schema: "APP",
                table: "BusinessContacts",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OrganisationId",
                schema: "APP",
                table: "BusinessBankInfos",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OrganisationId",
                schema: "APP",
                table: "BusinessAddresses",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_VendorPerformance_Organisation_IsPreferredVendor",
                schema: "APP",
                table: "VendorPerformances",
                columns: new[] { "OrganisationId", "IsPreferredVendor" });

            migrationBuilder.CreateIndex(
                name: "IX_VendorFinancials_OrganisationId",
                schema: "APP",
                table: "VendorFinancials",
                column: "OrganisationId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerFinancials_OrganisationId",
                schema: "APP",
                table: "CustomerFinancials",
                column: "OrganisationId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessTaxInfo_Organisation_EntityType",
                schema: "APP",
                table: "BusinessTaxInfos",
                columns: new[] { "OrganisationId", "EntityType" });

            migrationBuilder.CreateIndex(
                name: "IX_BusinessContact_Organisation_EntityType",
                schema: "APP",
                table: "BusinessContacts",
                columns: new[] { "OrganisationId", "EntityType" });

            migrationBuilder.CreateIndex(
                name: "IX_BusinessBankInfo_Organisation_EntityType",
                schema: "APP",
                table: "BusinessBankInfos",
                columns: new[] { "OrganisationId", "EntityType" });

            migrationBuilder.CreateIndex(
                name: "IX_BusinessAddress_Organisation_EntityType",
                schema: "APP",
                table: "BusinessAddresses",
                columns: new[] { "OrganisationId", "EntityType" });

            migrationBuilder.AddForeignKey(
                name: "FK_BusinessAddresses_Organisations_OrganisationId",
                schema: "APP",
                table: "BusinessAddresses",
                column: "OrganisationId",
                principalSchema: "APP",
                principalTable: "Organisations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BusinessBankInfos_Organisations_OrganisationId",
                schema: "APP",
                table: "BusinessBankInfos",
                column: "OrganisationId",
                principalSchema: "APP",
                principalTable: "Organisations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BusinessContacts_Organisations_OrganisationId",
                schema: "APP",
                table: "BusinessContacts",
                column: "OrganisationId",
                principalSchema: "APP",
                principalTable: "Organisations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BusinessTaxInfos_Organisations_OrganisationId",
                schema: "APP",
                table: "BusinessTaxInfos",
                column: "OrganisationId",
                principalSchema: "APP",
                principalTable: "Organisations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerFinancials_Organisations_OrganisationId",
                schema: "APP",
                table: "CustomerFinancials",
                column: "OrganisationId",
                principalSchema: "APP",
                principalTable: "Organisations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VendorFinancials_Organisations_OrganisationId",
                schema: "APP",
                table: "VendorFinancials",
                column: "OrganisationId",
                principalSchema: "APP",
                principalTable: "Organisations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VendorPerformances_Organisations_OrganisationId",
                schema: "APP",
                table: "VendorPerformances",
                column: "OrganisationId",
                principalSchema: "APP",
                principalTable: "Organisations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
