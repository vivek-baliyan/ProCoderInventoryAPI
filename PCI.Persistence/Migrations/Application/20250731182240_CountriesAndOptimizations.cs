using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PCI.Persistence.Migrations.Application
{
    /// <inheritdoc />
    public partial class CountriesAndOptimizations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_Customers_CustomerId",
                schema: "APP",
                table: "Invoices");

            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_Organisations_OrganisationId",
                schema: "APP",
                table: "Invoices");

            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_PriceList_PriceListId",
                schema: "APP",
                table: "Invoices");

            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_SalesOrders_SalesOrderId",
                schema: "APP",
                table: "Invoices");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "APP",
                table: "States",
                type: "TEXT",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Longitude",
                schema: "APP",
                table: "States",
                type: "decimal(11,8)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Latitude",
                schema: "APP",
                table: "States",
                type: "decimal(10,8)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                schema: "APP",
                table: "Invoices",
                type: "TEXT",
                maxLength: 20,
                nullable: true,
                defaultValue: "Draft",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "AmountPaid",
                schema: "APP",
                table: "Invoices",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddColumn<int>(
                name: "CustomerId1",
                schema: "APP",
                table: "Invoices",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                schema: "APP",
                table: "Customers",
                type: "BLOB",
                nullable: true);

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
                name: "IX_Invoices_CustomerId1",
                schema: "APP",
                table: "Invoices",
                column: "CustomerId1");

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
                name: "IX_Invoices_Status",
                schema: "APP",
                table: "Invoices",
                column: "Status");

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_Customers_CustomerId",
                schema: "APP",
                table: "Invoices",
                column: "CustomerId",
                principalSchema: "APP",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_Customers_CustomerId1",
                schema: "APP",
                table: "Invoices",
                column: "CustomerId1",
                principalSchema: "APP",
                principalTable: "Customers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_Organisations_OrganisationId",
                schema: "APP",
                table: "Invoices",
                column: "OrganisationId",
                principalSchema: "APP",
                principalTable: "Organisations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_PriceList_PriceListId",
                schema: "APP",
                table: "Invoices",
                column: "PriceListId",
                principalSchema: "APP",
                principalTable: "PriceList",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_SalesOrders_SalesOrderId",
                schema: "APP",
                table: "Invoices",
                column: "SalesOrderId",
                principalSchema: "APP",
                principalTable: "SalesOrders",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_Customers_CustomerId",
                schema: "APP",
                table: "Invoices");

            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_Customers_CustomerId1",
                schema: "APP",
                table: "Invoices");

            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_Organisations_OrganisationId",
                schema: "APP",
                table: "Invoices");

            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_PriceList_PriceListId",
                schema: "APP",
                table: "Invoices");

            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_SalesOrders_SalesOrderId",
                schema: "APP",
                table: "Invoices");

            migrationBuilder.DropIndex(
                name: "IX_States_Name",
                schema: "APP",
                table: "States");

            migrationBuilder.DropIndex(
                name: "IX_States_StateCode",
                schema: "APP",
                table: "States");

            migrationBuilder.DropIndex(
                name: "IX_Invoices_CustomerId1",
                schema: "APP",
                table: "Invoices");

            migrationBuilder.DropIndex(
                name: "IX_Invoices_InvoiceDate",
                schema: "APP",
                table: "Invoices");

            migrationBuilder.DropIndex(
                name: "IX_Invoices_InvoiceNumber",
                schema: "APP",
                table: "Invoices");

            migrationBuilder.DropIndex(
                name: "IX_Invoices_Status",
                schema: "APP",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "CustomerId1",
                schema: "APP",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                schema: "APP",
                table: "Customers");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "APP",
                table: "States",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<decimal>(
                name: "Longitude",
                schema: "APP",
                table: "States",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(11,8)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Latitude",
                schema: "APP",
                table: "States",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,8)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                schema: "APP",
                table: "Invoices",
                type: "TEXT",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 20,
                oldNullable: true,
                oldDefaultValue: "Draft");

            migrationBuilder.AlterColumn<decimal>(
                name: "AmountPaid",
                schema: "APP",
                table: "Invoices",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldDefaultValue: 0m);

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_Customers_CustomerId",
                schema: "APP",
                table: "Invoices",
                column: "CustomerId",
                principalSchema: "APP",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_Organisations_OrganisationId",
                schema: "APP",
                table: "Invoices",
                column: "OrganisationId",
                principalSchema: "APP",
                principalTable: "Organisations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_PriceList_PriceListId",
                schema: "APP",
                table: "Invoices",
                column: "PriceListId",
                principalSchema: "APP",
                principalTable: "PriceList",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_SalesOrders_SalesOrderId",
                schema: "APP",
                table: "Invoices",
                column: "SalesOrderId",
                principalSchema: "APP",
                principalTable: "SalesOrders",
                principalColumn: "Id");
        }
    }
}
