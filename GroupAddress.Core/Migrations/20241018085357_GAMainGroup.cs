using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GroupAddress.Core.Migrations
{
    /// <inheritdoc />
    public partial class GAMainGroup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FillGASpaces",
                table: "MainGroups");

            migrationBuilder.DropColumn(
                name: "FillGAToEnd",
                table: "MainGroups");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "FillGASpaces",
                table: "MainGroups",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "FillGAToEnd",
                table: "MainGroups",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }
    }
}
