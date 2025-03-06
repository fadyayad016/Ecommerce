using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce.Infrastructure.Sql.Migrations
{
    /// <inheritdoc />
    public partial class TblBarCode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BarCodeId",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "SalePrice",
                table: "Products",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.CreateTable(
                name: "Barcodes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BarcodeName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false, comment: "Name of the Barcode"),
                    ProductId = table.Column<int>(type: "int", nullable: false, comment: "Foreign key for Product"),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true, comment: "Status of the BarCode (active/inactive)"),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Barcodes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_BarCodeId",
                table: "Products",
                column: "BarCodeId");

            migrationBuilder.CreateIndex(
                name: "IX_Barcodes_BarcodeName",
                table: "Barcodes",
                column: "BarcodeName",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Barcodes_BarCodeId",
                table: "Products",
                column: "BarCodeId",
                principalTable: "Barcodes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Barcodes_BarCodeId",
                table: "Products");

            migrationBuilder.DropTable(
                name: "Barcodes");

            migrationBuilder.DropIndex(
                name: "IX_Products_BarCodeId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "BarCodeId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "SalePrice",
                table: "Products");
        }
    }
}
