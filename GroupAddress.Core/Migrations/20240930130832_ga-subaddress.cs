using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GroupAddress.Core.Migrations
{
    /// <inheritdoc />
    public partial class gasubaddress : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_SubGroups_Address",
                table: "SubGroups");

            migrationBuilder.DropIndex(
                name: "IX_GAs_Address",
                table: "GAs");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "SubGroups");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "MainGroups");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "GAs");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "GAs",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddColumn<int>(
                name: "SubAddress",
                table: "GAs",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SubAddress",
                table: "GAs");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "SubGroups",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "MainGroups",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "GAs",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT")
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "GAs",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_SubGroups_Address",
                table: "SubGroups",
                column: "Address");

            migrationBuilder.CreateIndex(
                name: "IX_GAs_Address",
                table: "GAs",
                column: "Address");
        }
    }
}
