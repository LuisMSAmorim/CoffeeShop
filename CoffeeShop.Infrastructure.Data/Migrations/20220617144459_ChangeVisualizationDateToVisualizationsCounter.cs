using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoffeeShop.Infrastructure.Data.Migrations
{
    public partial class ChangeVisualizationDateToVisualizationsCounter : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastVisualization",
                table: "Coffee");

            migrationBuilder.AddColumn<int>(
                name: "VisualizationsNumber",
                table: "Coffee",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VisualizationsNumber",
                table: "Coffee");

            migrationBuilder.AddColumn<DateTime>(
                name: "LastVisualization",
                table: "Coffee",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
