using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Finance.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class v016 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "InvoicePaymentEnum",
                table: "Invoices",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InvoicePaymentEnum",
                table: "Invoices");
        }
    }
}
