using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestApiApplication.Domain.Migrations
{
    /// <inheritdoc />
    public partial class AddPropertyCounter : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "ViolationCounter",
                table: "Employees",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ViolationCounter",
                table: "Employees");
        }
    }
}
