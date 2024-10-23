using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GroupAddress.Core.Migrations
{
    /// <inheritdoc />
    public partial class init2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ItemId",
                table: "GAs",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_GAs_ItemId",
                table: "GAs",
                column: "ItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_GAs_Item_ItemId",
                table: "GAs",
                column: "ItemId",
                principalTable: "Item",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GAs_Item_ItemId",
                table: "GAs");

            migrationBuilder.DropIndex(
                name: "IX_GAs_ItemId",
                table: "GAs");

            migrationBuilder.DropColumn(
                name: "ItemId",
                table: "GAs");
        }
    }
}
