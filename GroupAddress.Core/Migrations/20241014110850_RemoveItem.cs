using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GroupAddress.Core.Migrations
{
    /// <inheritdoc />
    public partial class RemoveItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GAs_ItemParts_ItemPartId",
                table: "GAs");

            migrationBuilder.DropForeignKey(
                name: "FK_GATemplates_ItemPartTemplates_ItemPartTemplateId",
                table: "GATemplates");

            migrationBuilder.DropTable(
                name: "ItemParts");

            migrationBuilder.DropTable(
                name: "ItemPartTemplates");

            migrationBuilder.DropIndex(
                name: "IX_GATemplates_ItemPartTemplateId",
                table: "GATemplates");

            migrationBuilder.RenameColumn(
                name: "ItemPartId",
                table: "GAs",
                newName: "ItemId");

            migrationBuilder.RenameIndex(
                name: "IX_GAs_ItemPartId",
                table: "GAs",
                newName: "IX_GAs_ItemId");

            migrationBuilder.AddColumn<string>(
                name: "ItemTemplateId",
                table: "ItemTemplates",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MainGroupId",
                table: "Items",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ItemTemplateId",
                table: "GATemplates",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Items_MainGroupId",
                table: "Items",
                column: "MainGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_GATemplates_ItemTemplateId",
                table: "GATemplates",
                column: "ItemTemplateId");

            migrationBuilder.AddForeignKey(
                name: "FK_GAs_Items_ItemId",
                table: "GAs",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GATemplates_ItemTemplates_ItemTemplateId",
                table: "GATemplates",
                column: "ItemTemplateId",
                principalTable: "ItemTemplates",
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
                name: "FK_GAs_Items_ItemId",
                table: "GAs");

            migrationBuilder.DropForeignKey(
                name: "FK_GATemplates_ItemTemplates_ItemTemplateId",
                table: "GATemplates");

            migrationBuilder.DropForeignKey(
                name: "FK_Items_MainGroups_MainGroupId",
                table: "Items");

            migrationBuilder.DropIndex(
                name: "IX_Items_MainGroupId",
                table: "Items");

            migrationBuilder.DropIndex(
                name: "IX_GATemplates_ItemTemplateId",
                table: "GATemplates");

            migrationBuilder.DropColumn(
                name: "ItemTemplateId",
                table: "ItemTemplates");

            migrationBuilder.DropColumn(
                name: "MainGroupId",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "ItemTemplateId",
                table: "GATemplates");

            migrationBuilder.RenameColumn(
                name: "ItemId",
                table: "GAs",
                newName: "ItemPartId");

            migrationBuilder.RenameIndex(
                name: "IX_GAs_ItemId",
                table: "GAs",
                newName: "IX_GAs_ItemPartId");

            migrationBuilder.CreateTable(
                name: "ItemParts",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    ItemId = table.Column<string>(type: "TEXT", nullable: true),
                    MainGroupId = table.Column<string>(type: "TEXT", nullable: true),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
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

            migrationBuilder.CreateTable(
                name: "ItemPartTemplates",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    ItemTemplateId = table.Column<string>(type: "TEXT", nullable: true),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemPartTemplates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemPartTemplates_ItemTemplates_ItemTemplateId",
                        column: x => x.ItemTemplateId,
                        principalTable: "ItemTemplates",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_GATemplates_ItemPartTemplateId",
                table: "GATemplates",
                column: "ItemPartTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemParts_ItemId",
                table: "ItemParts",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemParts_MainGroupId",
                table: "ItemParts",
                column: "MainGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemPartTemplates_ItemTemplateId",
                table: "ItemPartTemplates",
                column: "ItemTemplateId");

            migrationBuilder.AddForeignKey(
                name: "FK_GAs_ItemParts_ItemPartId",
                table: "GAs",
                column: "ItemPartId",
                principalTable: "ItemParts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GATemplates_ItemPartTemplates_ItemPartTemplateId",
                table: "GATemplates",
                column: "ItemPartTemplateId",
                principalTable: "ItemPartTemplates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
