﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PcMarket.Data.Migrations
{
    /// <inheritdoc />
    public partial class CantidadProducto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "Producto",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Producto");
        }
    }
}
