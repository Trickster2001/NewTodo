using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyTodoApp.Migrations
{
    /// <inheritdoc />
    public partial class AddedDueDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateOnly>(
                name: "DueDate",
                table: "todos",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DueDate",
                table: "todos");
        }
    }
}
