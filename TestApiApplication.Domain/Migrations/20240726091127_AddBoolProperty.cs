using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestApiApplication.Domain.Migrations
{
    /// <inheritdoc />
    public partial class AddBoolProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ViolationCounter",
                table: "Employees");

            migrationBuilder.AddColumn<bool>(
                name: "IsFailed",
                table: "Shifts",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsFailed",
                table: "Shifts");

            migrationBuilder.AddColumn<double>(
                name: "ViolationCounter",
                table: "Employees",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
