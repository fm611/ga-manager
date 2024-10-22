using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GroupAddress.Core.Migrations
{
    /// <inheritdoc />
    public partial class recode2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GAs_MainGroups_MainGroupId",
                table: "GAs");

            migrationBuilder.AlterColumn<string>(
                name: "MainGroupId",
                table: "GAs",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_GAs_MainGroups_MainGroupId",
                table: "GAs",
                column: "MainGroupId",
                principalTable: "MainGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GAs_MainGroups_MainGroupId",
                table: "GAs");

            migrationBuilder.AlterColumn<string>(
                name: "MainGroupId",
                table: "GAs",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddForeignKey(
                name: "FK_GAs_MainGroups_MainGroupId",
                table: "GAs",
                column: "MainGroupId",
                principalTable: "MainGroups",
                principalColumn: "Id");
        }
    }
}
