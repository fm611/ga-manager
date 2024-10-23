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
            migrationBuilder.DropForeignKey(
                name: "FK_GAs_Item_ItemId",
                table: "GAs");

            migrationBuilder.DropForeignKey(
                name: "FK_Item_MainGroups_MainGroupId",
                table: "Item");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Item",
                table: "Item");

            migrationBuilder.RenameTable(
                name: "Item",
                newName: "Items");

            migrationBuilder.RenameIndex(
                name: "IX_Item_MainGroupId",
                table: "Items",
                newName: "IX_Items_MainGroupId");

            migrationBuilder.AddColumn<string>(
                name: "ItemTemplateId",
                table: "GAs",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Items",
                table: "Items",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "ItemTemplates",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    SubGroupNames = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemTemplates", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GAs_ItemTemplateId",
                table: "GAs",
                column: "ItemTemplateId");

            migrationBuilder.AddForeignKey(
                name: "FK_GAs_ItemTemplates_ItemTemplateId",
                table: "GAs",
                column: "ItemTemplateId",
                principalTable: "ItemTemplates",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GAs_Items_ItemId",
                table: "GAs",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_MainGroups_MainGroupId",
                table: "Items",
                column: "MainGroupId",
                principalTable: "MainGroups",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GAs_ItemTemplates_ItemTemplateId",
                table: "GAs");

            migrationBuilder.DropForeignKey(
                name: "FK_GAs_Items_ItemId",
                table: "GAs");

            migrationBuilder.DropForeignKey(
                name: "FK_Items_MainGroups_MainGroupId",
                table: "Items");

            migrationBuilder.DropTable(
                name: "ItemTemplates");

            migrationBuilder.DropIndex(
                name: "IX_GAs_ItemTemplateId",
                table: "GAs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Items",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "ItemTemplateId",
                table: "GAs");

            migrationBuilder.RenameTable(
                name: "Items",
                newName: "Item");

            migrationBuilder.RenameIndex(
                name: "IX_Items_MainGroupId",
                table: "Item",
                newName: "IX_Item_MainGroupId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Item",
                table: "Item",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GAs_Item_ItemId",
                table: "GAs",
                column: "ItemId",
                principalTable: "Item",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Item_MainGroups_MainGroupId",
                table: "Item",
                column: "MainGroupId",
                principalTable: "MainGroups",
                principalColumn: "Id");
        }
    }
}
