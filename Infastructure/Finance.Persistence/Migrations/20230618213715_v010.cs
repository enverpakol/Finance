﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Finance.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class v010 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "UnitPrice",
                table: "InvoiceDetails",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UnitPrice",
                table: "InvoiceDetails");
        }
    }
}
