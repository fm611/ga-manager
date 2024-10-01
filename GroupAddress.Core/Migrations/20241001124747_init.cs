using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GroupAddress.Core.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.CreateTable(
                name: "ItemTemplates",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemTemplates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MainGroups",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    FillGASpaces = table.Column<bool>(type: "INTEGER", nullable: false),
                    FillGAToEnd = table.Column<bool>(type: "INTEGER", nullable: false),
                    NextItemId = table.Column<int>(type: "INTEGER", nullable: false),
                    SubAddress = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MainGroups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SubGroupTemplate",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    SubAddress = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubGroupTemplate", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ItemPartTemplate",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    ItemTemplateId = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemPartTemplate", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemPartTemplate_ItemTemplates_ItemTemplateId",
                        column: x => x.ItemTemplateId,
                        principalTable: "ItemTemplates",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    ItemId = table.Column<string>(type: "TEXT", nullable: true),
                    MainGroupId = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Items_Item_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Item",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Items_MainGroups_MainGroupId",
                        column: x => x.MainGroupId,
                        principalTable: "MainGroups",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SubGroups",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    MainGroupId = table.Column<string>(type: "TEXT", nullable: false),
                    SubAddress = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubGroups_MainGroups_MainGroupId",
                        column: x => x.MainGroupId,
                        principalTable: "MainGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GATemplate",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    BaseString = table.Column<string>(type: "TEXT", nullable: false),
                    ItemPartTemplateId = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GATemplate", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GATemplate_ItemPartTemplate_ItemPartTemplateId",
                        column: x => x.ItemPartTemplateId,
                        principalTable: "ItemPartTemplate",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "GAs",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    SubGroupId = table.Column<string>(type: "TEXT", nullable: true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    SubAddress = table.Column<int>(type: "INTEGER", nullable: false),
                    ItemPartId = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GAs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GAs_Items_ItemPartId",
                        column: x => x.ItemPartId,
                        principalTable: "Items",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_GAs_SubGroups_SubGroupId",
                        column: x => x.SubGroupId,
                        principalTable: "SubGroups",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "GATemplatePart",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    subGroupTemplateId = table.Column<string>(type: "TEXT", nullable: true),
                    AddonString = table.Column<string>(type: "TEXT", nullable: false),
                    GATemplateId = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GATemplatePart", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GATemplatePart_GATemplate_GATemplateId",
                        column: x => x.GATemplateId,
                        principalTable: "GATemplate",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_GATemplatePart_SubGroupTemplate_subGroupTemplateId",
                        column: x => x.subGroupTemplateId,
                        principalTable: "SubGroupTemplate",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_GAs_ItemPartId",
                table: "GAs",
                column: "ItemPartId");

            migrationBuilder.CreateIndex(
                name: "IX_GAs_SubGroupId",
                table: "GAs",
                column: "SubGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_GATemplate_ItemPartTemplateId",
                table: "GATemplate",
                column: "ItemPartTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_GATemplatePart_GATemplateId",
                table: "GATemplatePart",
                column: "GATemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_GATemplatePart_subGroupTemplateId",
                table: "GATemplatePart",
                column: "subGroupTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemPartTemplate_ItemTemplateId",
                table: "ItemPartTemplate",
                column: "ItemTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_ItemId",
                table: "Items",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_MainGroupId",
                table: "Items",
                column: "MainGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_SubGroups_MainGroupId",
                table: "SubGroups",
                column: "MainGroupId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GAs");

            migrationBuilder.DropTable(
                name: "GATemplatePart");

            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "SubGroups");

            migrationBuilder.DropTable(
                name: "GATemplate");

            migrationBuilder.DropTable(
                name: "SubGroupTemplate");

            migrationBuilder.DropTable(
                name: "Item");

            migrationBuilder.DropTable(
                name: "MainGroups");

            migrationBuilder.DropTable(
                name: "ItemPartTemplate");

            migrationBuilder.DropTable(
                name: "ItemTemplates");
        }
    }
}
