using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Finance.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class v008 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceDetails_InvoiceDetails_InvoiceDetailId",
                table: "InvoiceDetails");

            migrationBuilder.DropIndex(
                name: "IX_InvoiceDetails_InvoiceDetailId",
                table: "InvoiceDetails");

            migrationBuilder.DropColumn(
                name: "InvoiceDetailId",
                table: "InvoiceDetails");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "InvoiceDetailId",
                table: "InvoiceDetails",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceDetails_InvoiceDetailId",
                table: "InvoiceDetails",
                column: "InvoiceDetailId");

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceDetails_InvoiceDetails_InvoiceDetailId",
                table: "InvoiceDetails",
                column: "InvoiceDetailId",
                principalTable: "InvoiceDetails",
                principalColumn: "Id");
        }
    }
}
