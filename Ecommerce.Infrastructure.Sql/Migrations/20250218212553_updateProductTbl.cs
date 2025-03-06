using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce.Infrastructure.Sql.Migrations
{
    /// <inheritdoc />
    public partial class updateProductTbl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Barcodes_BarCodeId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_BarCodeId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "BarCodeId",
                table: "Products");

            //migrationBuilder.AddColumn<long>(
            //    name: "ProductId1",
            //    table: "Barcodes",
            //    type: "bigint",
            //    nullable: false,
            //    defaultValue: 0L);

            //migrationBuilder.CreateIndex(
            //    name: "IX_Barcodes_ProductId1",
            //    table: "Barcodes",
            //    column: "ProductId1");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Barcodes_Products_ProductId1",
            //    table: "Barcodes",
            //    column: "ProductId1",
            //    principalTable: "Products",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_Barcodes_Products_ProductId1",
            //    table: "Barcodes");

            //migrationBuilder.DropIndex(
            //    name: "IX_Barcodes_ProductId1",
            //    table: "Barcodes");

            //migrationBuilder.DropColumn(
            //    name: "ProductId1",
            //    table: "Barcodes");

            migrationBuilder.AddColumn<int>(
                name: "BarCodeId",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Products_BarCodeId",
                table: "Products",
                column: "BarCodeId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Barcodes_BarCodeId",
                table: "Products",
                column: "BarCodeId",
                principalTable: "Barcodes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
