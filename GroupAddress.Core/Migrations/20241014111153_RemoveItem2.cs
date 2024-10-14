using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GroupAddress.Core.Migrations
{
    /// <inheritdoc />
    public partial class RemoveItem2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GATemplates_ItemTemplates_ItemTemplateId",
                table: "GATemplates");

            migrationBuilder.DropColumn(
                name: "ItemTemplateId",
                table: "ItemTemplates");

            migrationBuilder.DropColumn(
                name: "ItemPartTemplateId",
                table: "GATemplates");

            migrationBuilder.AlterColumn<string>(
                name: "ItemTemplateId",
                table: "GATemplates",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_GATemplates_ItemTemplates_ItemTemplateId",
                table: "GATemplates",
                column: "ItemTemplateId",
                principalTable: "ItemTemplates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GATemplates_ItemTemplates_ItemTemplateId",
                table: "GATemplates");

            migrationBuilder.AddColumn<string>(
                name: "ItemTemplateId",
                table: "ItemTemplates",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ItemTemplateId",
                table: "GATemplates",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddColumn<string>(
                name: "ItemPartTemplateId",
                table: "GATemplates",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_GATemplates_ItemTemplates_ItemTemplateId",
                table: "GATemplates",
                column: "ItemTemplateId",
                principalTable: "ItemTemplates",
                principalColumn: "Id");
        }
    }
}
