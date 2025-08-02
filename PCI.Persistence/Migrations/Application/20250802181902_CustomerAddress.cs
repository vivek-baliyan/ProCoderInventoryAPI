using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PCI.Persistence.Migrations.Application
{
    /// <inheritdoc />
    public partial class CustomerAddress : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrentBalance",
                schema: "APP",
                table: "CustomerFinancials");

            migrationBuilder.DropColumn(
                name: "DefaultDiscountPercentage",
                schema: "APP",
                table: "CustomerFinancials");

            migrationBuilder.DropColumn(
                name: "LastPaymentDate",
                schema: "APP",
                table: "CustomerFinancials");

            migrationBuilder.DropColumn(
                name: "LastSaleDate",
                schema: "APP",
                table: "CustomerFinancials");

            migrationBuilder.DropColumn(
                name: "MinimumOrderValue",
                schema: "APP",
                table: "CustomerFinancials");

            migrationBuilder.DropColumn(
                name: "Notes",
                schema: "APP",
                table: "CustomerFinancials");

            migrationBuilder.DropColumn(
                name: "OutstandingAmount",
                schema: "APP",
                table: "CustomerFinancials");

            migrationBuilder.DropColumn(
                name: "PaymentTermDays",
                schema: "APP",
                table: "CustomerFinancials");

            migrationBuilder.DropColumn(
                name: "TotalSalesLifetime",
                schema: "APP",
                table: "CustomerFinancials");

            migrationBuilder.DropColumn(
                name: "TotalSalesYTD",
                schema: "APP",
                table: "CustomerFinancials");

            migrationBuilder.DropColumn(
                name: "Country",
                schema: "APP",
                table: "CustomerAddresses");

            migrationBuilder.DropColumn(
                name: "State",
                schema: "APP",
                table: "CustomerAddresses");

            migrationBuilder.AlterColumn<int>(
                name: "PreferredPaymentMethod",
                schema: "APP",
                table: "CustomerFinancials",
                type: "INTEGER",
                nullable: false,
                defaultValue: 4,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 50,
                oldNullable: true,
                oldDefaultValue: "BankTransfer");

            migrationBuilder.AlterColumn<int>(
                name: "PaymentTerms",
                schema: "APP",
                table: "CustomerFinancials",
                type: "INTEGER",
                nullable: false,
                defaultValue: 3,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 100,
                oldNullable: true,
                oldDefaultValue: "Due on Receipt");

            migrationBuilder.AddColumn<int>(
                name: "CustomPaymentTermDays",
                schema: "APP",
                table: "CustomerFinancials",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CountryId",
                schema: "APP",
                table: "CustomerAddresses",
                type: "INTEGER",
                maxLength: 100,
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "StateId",
                schema: "APP",
                table: "CustomerAddresses",
                type: "INTEGER",
                maxLength: 100,
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_CustomerAddresses_CountryId",
                schema: "APP",
                table: "CustomerAddresses",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerAddresses_StateId",
                schema: "APP",
                table: "CustomerAddresses",
                column: "StateId");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerAddresses_Countries_CountryId",
                schema: "APP",
                table: "CustomerAddresses",
                column: "CountryId",
                principalSchema: "APP",
                principalTable: "Countries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerAddresses_States_StateId",
                schema: "APP",
                table: "CustomerAddresses",
                column: "StateId",
                principalSchema: "APP",
                principalTable: "States",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerAddresses_Countries_CountryId",
                schema: "APP",
                table: "CustomerAddresses");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerAddresses_States_StateId",
                schema: "APP",
                table: "CustomerAddresses");

            migrationBuilder.DropIndex(
                name: "IX_CustomerAddresses_CountryId",
                schema: "APP",
                table: "CustomerAddresses");

            migrationBuilder.DropIndex(
                name: "IX_CustomerAddresses_StateId",
                schema: "APP",
                table: "CustomerAddresses");

            migrationBuilder.DropColumn(
                name: "CustomPaymentTermDays",
                schema: "APP",
                table: "CustomerFinancials");

            migrationBuilder.DropColumn(
                name: "CountryId",
                schema: "APP",
                table: "CustomerAddresses");

            migrationBuilder.DropColumn(
                name: "StateId",
                schema: "APP",
                table: "CustomerAddresses");

            migrationBuilder.AlterColumn<string>(
                name: "PreferredPaymentMethod",
                schema: "APP",
                table: "CustomerFinancials",
                type: "TEXT",
                maxLength: 50,
                nullable: true,
                defaultValue: "BankTransfer",
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldDefaultValue: 4);

            migrationBuilder.AlterColumn<string>(
                name: "PaymentTerms",
                schema: "APP",
                table: "CustomerFinancials",
                type: "TEXT",
                maxLength: 100,
                nullable: true,
                defaultValue: "Due on Receipt",
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldDefaultValue: 3);

            migrationBuilder.AddColumn<decimal>(
                name: "CurrentBalance",
                schema: "APP",
                table: "CustomerFinancials",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "DefaultDiscountPercentage",
                schema: "APP",
                table: "CustomerFinancials",
                type: "decimal(5,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastPaymentDate",
                schema: "APP",
                table: "CustomerFinancials",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastSaleDate",
                schema: "APP",
                table: "CustomerFinancials",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "MinimumOrderValue",
                schema: "APP",
                table: "CustomerFinancials",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "Notes",
                schema: "APP",
                table: "CustomerFinancials",
                type: "TEXT",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "OutstandingAmount",
                schema: "APP",
                table: "CustomerFinancials",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "PaymentTermDays",
                schema: "APP",
                table: "CustomerFinancials",
                type: "INTEGER",
                nullable: false,
                defaultValue: 30);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalSalesLifetime",
                schema: "APP",
                table: "CustomerFinancials",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalSalesYTD",
                schema: "APP",
                table: "CustomerFinancials",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "Country",
                schema: "APP",
                table: "CustomerAddresses",
                type: "TEXT",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "State",
                schema: "APP",
                table: "CustomerAddresses",
                type: "TEXT",
                maxLength: 100,
                nullable: true);
        }
    }
}
