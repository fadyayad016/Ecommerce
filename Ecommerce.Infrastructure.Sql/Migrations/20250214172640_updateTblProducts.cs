using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce.Infrastructure.Sql.Migrations
{
    /// <inheritdoc />
    public partial class updateTblProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Products",
                type: "bit",
                nullable: false,
                defaultValue: false,
                comment: "The Status of the product (active/inactive)");

            migrationBuilder.AddColumn<bool>(
                name: "IsSerial",
                table: "Products",
                type: "bit",
                nullable: false,
                defaultValue: false,
                comment: "The product Is Serial (Yes/No)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "IsSerial",
                table: "Products");
        }
    }
}
