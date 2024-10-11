using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GroupAddress.Core.Migrations
{
    /// <inheritdoc />
    public partial class DbCtx : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GATemplate_ItemPartTemplates_ItemPartTemplateId",
                table: "GATemplate");

            migrationBuilder.DropForeignKey(
                name: "FK_GATemplatePart_GATemplate_GATemplateId",
                table: "GATemplatePart");

            migrationBuilder.DropForeignKey(
                name: "FK_GATemplatePart_SubGroupTemplate_SubGroupTemplateId",
                table: "GATemplatePart");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GATemplatePart",
                table: "GATemplatePart");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GATemplate",
                table: "GATemplate");

            migrationBuilder.RenameTable(
                name: "GATemplatePart",
                newName: "GATemplateParts");

            migrationBuilder.RenameTable(
                name: "GATemplate",
                newName: "GATemplates");

            migrationBuilder.RenameIndex(
                name: "IX_GATemplatePart_SubGroupTemplateId",
                table: "GATemplateParts",
                newName: "IX_GATemplateParts_SubGroupTemplateId");

            migrationBuilder.RenameIndex(
                name: "IX_GATemplatePart_GATemplateId",
                table: "GATemplateParts",
                newName: "IX_GATemplateParts_GATemplateId");

            migrationBuilder.RenameIndex(
                name: "IX_GATemplate_ItemPartTemplateId",
                table: "GATemplates",
                newName: "IX_GATemplates_ItemPartTemplateId");

            migrationBuilder.AlterColumn<string>(
                name: "GATemplateId",
                table: "GATemplateParts",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ItemPartTemplateId",
                table: "GATemplates",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_GATemplateParts",
                table: "GATemplateParts",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GATemplates",
                table: "GATemplates",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GATemplateParts_GATemplates_GATemplateId",
                table: "GATemplateParts",
                column: "GATemplateId",
                principalTable: "GATemplates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GATemplateParts_SubGroupTemplate_SubGroupTemplateId",
                table: "GATemplateParts",
                column: "SubGroupTemplateId",
                principalTable: "SubGroupTemplate",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GATemplates_ItemPartTemplates_ItemPartTemplateId",
                table: "GATemplates",
                column: "ItemPartTemplateId",
                principalTable: "ItemPartTemplates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GATemplateParts_GATemplates_GATemplateId",
                table: "GATemplateParts");

            migrationBuilder.DropForeignKey(
                name: "FK_GATemplateParts_SubGroupTemplate_SubGroupTemplateId",
                table: "GATemplateParts");

            migrationBuilder.DropForeignKey(
                name: "FK_GATemplates_ItemPartTemplates_ItemPartTemplateId",
                table: "GATemplates");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GATemplates",
                table: "GATemplates");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GATemplateParts",
                table: "GATemplateParts");

            migrationBuilder.RenameTable(
                name: "GATemplates",
                newName: "GATemplate");

            migrationBuilder.RenameTable(
                name: "GATemplateParts",
                newName: "GATemplatePart");

            migrationBuilder.RenameIndex(
                name: "IX_GATemplates_ItemPartTemplateId",
                table: "GATemplate",
                newName: "IX_GATemplate_ItemPartTemplateId");

            migrationBuilder.RenameIndex(
                name: "IX_GATemplateParts_SubGroupTemplateId",
                table: "GATemplatePart",
                newName: "IX_GATemplatePart_SubGroupTemplateId");

            migrationBuilder.RenameIndex(
                name: "IX_GATemplateParts_GATemplateId",
                table: "GATemplatePart",
                newName: "IX_GATemplatePart_GATemplateId");

            migrationBuilder.AlterColumn<string>(
                name: "ItemPartTemplateId",
                table: "GATemplate",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "GATemplateId",
                table: "GATemplatePart",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GATemplate",
                table: "GATemplate",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GATemplatePart",
                table: "GATemplatePart",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GATemplate_ItemPartTemplates_ItemPartTemplateId",
                table: "GATemplate",
                column: "ItemPartTemplateId",
                principalTable: "ItemPartTemplates",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GATemplatePart_GATemplate_GATemplateId",
                table: "GATemplatePart",
                column: "GATemplateId",
                principalTable: "GATemplate",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GATemplatePart_SubGroupTemplate_SubGroupTemplateId",
                table: "GATemplatePart",
                column: "SubGroupTemplateId",
                principalTable: "SubGroupTemplate",
                principalColumn: "Id");
        }
    }
}
