using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce.Infrastructure.Sql.Migrations
{
    /// <inheritdoc />
    public partial class updateBarCodeTbl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           

            migrationBuilder.AlterColumn<long>(
                name: "ProductId",
                table: "Barcodes",
                type: "bigint",
                nullable: false,
                comment: "Foreign key for Product",
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "Foreign key for Product");

            migrationBuilder.CreateIndex(
                name: "IX_Barcodes_ProductId",
                table: "Barcodes",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Barcodes_Products_ProductId",
                table: "Barcodes",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Barcodes_Products_ProductId",
                table: "Barcodes");

            migrationBuilder.DropIndex(
                name: "IX_Barcodes_ProductId",
                table: "Barcodes");

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "Barcodes",
                type: "int",
                nullable: false,
                comment: "Foreign key for Product",
                oldClrType: typeof(long),
                oldType: "bigint",
                oldComment: "Foreign key for Product");

           
        }
    }
}
