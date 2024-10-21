using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GroupAddress.Core.Migrations
{
    /// <inheritdoc />
    public partial class ga5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GAs_SubGroups_SubGroupId",
                table: "GAs");

            migrationBuilder.AlterColumn<string>(
                name: "SubGroupId",
                table: "GAs",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_GAs_SubGroups_SubGroupId",
                table: "GAs",
                column: "SubGroupId",
                principalTable: "SubGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GAs_SubGroups_SubGroupId",
                table: "GAs");

            migrationBuilder.AlterColumn<string>(
                name: "SubGroupId",
                table: "GAs",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddForeignKey(
                name: "FK_GAs_SubGroups_SubGroupId",
                table: "GAs",
                column: "SubGroupId",
                principalTable: "SubGroups",
                principalColumn: "Id");
        }
    }
}
