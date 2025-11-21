using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Technova_ecom.Migrations
{
    /// <inheritdoc />
    public partial class AlteringCategoryTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "display",
                table: "Category",
                newName: "display_order");

            migrationBuilder.AlterColumn<int>(
                name: "display_order",
                table: "Category",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "display_order",
                table: "Category",
                newName: "display");

            migrationBuilder.AlterColumn<string>(
                name: "display",
                table: "Category",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
