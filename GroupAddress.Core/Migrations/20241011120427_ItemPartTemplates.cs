using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GroupAddress.Core.Migrations
{
    /// <inheritdoc />
    public partial class ItemPartTemplates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GATemplate_ItemPartTemplate_ItemPartTemplateId",
                table: "GATemplate");

            migrationBuilder.DropForeignKey(
                name: "FK_GATemplatePart_SubGroupTemplate_subGroupTemplateId",
                table: "GATemplatePart");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemPartTemplate_ItemTemplates_ItemTemplateId",
                table: "ItemPartTemplate");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ItemPartTemplate",
                table: "ItemPartTemplate");

            migrationBuilder.RenameTable(
                name: "ItemPartTemplate",
                newName: "ItemPartTemplates");

            migrationBuilder.RenameColumn(
                name: "subGroupTemplateId",
                table: "GATemplatePart",
                newName: "SubGroupTemplateId");

            migrationBuilder.RenameIndex(
                name: "IX_GATemplatePart_subGroupTemplateId",
                table: "GATemplatePart",
                newName: "IX_GATemplatePart_SubGroupTemplateId");

            migrationBuilder.RenameIndex(
                name: "IX_ItemPartTemplate_ItemTemplateId",
                table: "ItemPartTemplates",
                newName: "IX_ItemPartTemplates_ItemTemplateId");

            migrationBuilder.AddColumn<int>(
                name: "SubAddress",
                table: "GATemplate",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ItemPartTemplates",
                table: "ItemPartTemplates",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GATemplate_ItemPartTemplates_ItemPartTemplateId",
                table: "GATemplate",
                column: "ItemPartTemplateId",
                principalTable: "ItemPartTemplates",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GATemplatePart_SubGroupTemplate_SubGroupTemplateId",
                table: "GATemplatePart",
                column: "SubGroupTemplateId",
                principalTable: "SubGroupTemplate",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemPartTemplates_ItemTemplates_ItemTemplateId",
                table: "ItemPartTemplates",
                column: "ItemTemplateId",
                principalTable: "ItemTemplates",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GATemplate_ItemPartTemplates_ItemPartTemplateId",
                table: "GATemplate");

            migrationBuilder.DropForeignKey(
                name: "FK_GATemplatePart_SubGroupTemplate_SubGroupTemplateId",
                table: "GATemplatePart");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemPartTemplates_ItemTemplates_ItemTemplateId",
                table: "ItemPartTemplates");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ItemPartTemplates",
                table: "ItemPartTemplates");

            migrationBuilder.DropColumn(
                name: "SubAddress",
                table: "GATemplate");

            migrationBuilder.RenameTable(
                name: "ItemPartTemplates",
                newName: "ItemPartTemplate");

            migrationBuilder.RenameColumn(
                name: "SubGroupTemplateId",
                table: "GATemplatePart",
                newName: "subGroupTemplateId");

            migrationBuilder.RenameIndex(
                name: "IX_GATemplatePart_SubGroupTemplateId",
                table: "GATemplatePart",
                newName: "IX_GATemplatePart_subGroupTemplateId");

            migrationBuilder.RenameIndex(
                name: "IX_ItemPartTemplates_ItemTemplateId",
                table: "ItemPartTemplate",
                newName: "IX_ItemPartTemplate_ItemTemplateId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ItemPartTemplate",
                table: "ItemPartTemplate",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GATemplate_ItemPartTemplate_ItemPartTemplateId",
                table: "GATemplate",
                column: "ItemPartTemplateId",
                principalTable: "ItemPartTemplate",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GATemplatePart_SubGroupTemplate_subGroupTemplateId",
                table: "GATemplatePart",
                column: "subGroupTemplateId",
                principalTable: "SubGroupTemplate",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemPartTemplate_ItemTemplates_ItemTemplateId",
                table: "ItemPartTemplate",
                column: "ItemTemplateId",
                principalTable: "ItemTemplates",
                principalColumn: "Id");
        }
    }
}
