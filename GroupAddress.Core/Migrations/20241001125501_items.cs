using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GroupAddress.Core.Migrations
{
    /// <inheritdoc />
    public partial class items : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GAs_Items_ItemPartId",
                table: "GAs");

            migrationBuilder.DropForeignKey(
                name: "FK_Items_Item_ItemId",
                table: "Items");

            migrationBuilder.DropForeignKey(
                name: "FK_Items_MainGroups_MainGroupId",
                table: "Items");

            migrationBuilder.DropTable(
                name: "Item");

            migrationBuilder.DropIndex(
                name: "IX_Items_ItemId",
                table: "Items");

            migrationBuilder.DropIndex(
                name: "IX_Items_MainGroupId",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "ItemId",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "MainGroupId",
                table: "Items");

            migrationBuilder.CreateTable(
                name: "ItemParts",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    ItemId = table.Column<string>(type: "TEXT", nullable: true),
                    MainGroupId = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemParts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemParts_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ItemParts_MainGroups_MainGroupId",
                        column: x => x.MainGroupId,
                        principalTable: "MainGroups",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ItemParts_ItemId",
                table: "ItemParts",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemParts_MainGroupId",
                table: "ItemParts",
                column: "MainGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_GAs_ItemParts_ItemPartId",
                table: "GAs",
                column: "ItemPartId",
                principalTable: "ItemParts",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GAs_ItemParts_ItemPartId",
                table: "GAs");

            migrationBuilder.DropTable(
                name: "ItemParts");

            migrationBuilder.AddColumn<string>(
                name: "ItemId",
                table: "Items",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MainGroupId",
                table: "Items",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Item",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Item", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Items_ItemId",
                table: "Items",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_MainGroupId",
                table: "Items",
                column: "MainGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_GAs_Items_ItemPartId",
                table: "GAs",
                column: "ItemPartId",
                principalTable: "Items",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Item_ItemId",
                table: "Items",
                column: "ItemId",
                principalTable: "Item",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_MainGroups_MainGroupId",
                table: "Items",
                column: "MainGroupId",
                principalTable: "MainGroups",
                principalColumn: "Id");
        }
    }
}
