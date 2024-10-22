using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GroupAddress.Core.Migrations
{
    /// <inheritdoc />
    public partial class recode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MainGroups",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    DefaultBlockLength = table.Column<int>(type: "INTEGER", nullable: false),
                    SubAddress = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MainGroups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    MainGroupId = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.Id);
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
                name: "GAs",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    SubGroupId = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    SubAddress = table.Column<int>(type: "INTEGER", nullable: false),
                    MainGroupId = table.Column<string>(type: "TEXT", nullable: true),
                    ItemId = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GAs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GAs_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_GAs_MainGroups_MainGroupId",
                        column: x => x.MainGroupId,
                        principalTable: "MainGroups",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_GAs_SubGroups_SubGroupId",
                        column: x => x.SubGroupId,
                        principalTable: "SubGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                name: "IX_GAs_SubGroupId",
                table: "GAs",
                column: "SubGroupId");

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
                name: "Items");

            migrationBuilder.DropTable(
                name: "SubGroups");

            migrationBuilder.DropTable(
                name: "MainGroups");
        }
    }
}
