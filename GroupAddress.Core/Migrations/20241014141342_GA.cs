using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GroupAddress.Core.Migrations
{
    /// <inheritdoc />
    public partial class GA : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MainGroupId",
                table: "GAs",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_GAs_MainGroupId",
                table: "GAs",
                column: "MainGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_GAs_MainGroups_MainGroupId",
                table: "GAs",
                column: "MainGroupId",
                principalTable: "MainGroups",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GAs_MainGroups_MainGroupId",
                table: "GAs");

            migrationBuilder.DropIndex(
                name: "IX_GAs_MainGroupId",
                table: "GAs");

            migrationBuilder.DropColumn(
                name: "MainGroupId",
                table: "GAs");
        }
    }
}
