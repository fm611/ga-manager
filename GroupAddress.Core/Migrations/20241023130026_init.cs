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
                name: "MainGroups",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    SubAddress = table.Column<int>(type: "INTEGER", nullable: false),
                    DefaultBlockLength = table.Column<int>(type: "INTEGER", nullable: false),
                    SubGroupNames = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MainGroups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Item",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    MainGroupId = table.Column<string>(type: "TEXT", nullable: true),
                    SubGroupNames = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Item", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Item_MainGroups_MainGroupId",
                        column: x => x.MainGroupId,
                        principalTable: "MainGroups",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "GAs",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    ItemId = table.Column<string>(type: "TEXT", nullable: true),
                    MainGroupId = table.Column<string>(type: "TEXT", nullable: true),
                    Addresse_GA = table.Column<int>(type: "INTEGER", nullable: false),
                    Addresse_MainGroup = table.Column<int>(type: "INTEGER", nullable: false),
                    Addresse_MiddleGroup = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GAs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GAs_Item_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Item",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_GAs_MainGroups_MainGroupId",
                        column: x => x.MainGroupId,
                        principalTable: "MainGroups",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_GAs_ItemId",
                table: "GAs",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_GAs_MainGroupId",
                table: "GAs",
                column: "MainGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Item_MainGroupId",
                table: "Item",
                column: "MainGroupId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GAs");

            migrationBuilder.DropTable(
                name: "Item");

            migrationBuilder.DropTable(
                name: "MainGroups");
        }
    }
}
