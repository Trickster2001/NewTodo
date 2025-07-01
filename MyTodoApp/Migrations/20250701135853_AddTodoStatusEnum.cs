using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyTodoApp.Migrations
{
    /// <inheritdoc />
    public partial class AddTodoStatusEnum : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCompleted",
                table: "todos");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "todos",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "todos");

            migrationBuilder.AddColumn<bool>(
                name: "IsCompleted",
                table: "todos",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
