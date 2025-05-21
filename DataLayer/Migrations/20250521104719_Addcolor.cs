using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class Addcolor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ColorId",
                table: "OrderDetails",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "ColorPrice",
                table: "OrderDetails",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateTable(
                name: "ColorProducts",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ColorCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Count = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<float>(type: "real", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    ProductId = table.Column<long>(type: "bigint", nullable: false),
                    ProductId1 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ColorProducts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ColorProducts_Products_ProductId1",
                        column: x => x.ProductId1,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ColorProducts_ProductId1",
                table: "ColorProducts",
                column: "ProductId1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ColorProducts");

            migrationBuilder.DropColumn(
                name: "ColorId",
                table: "OrderDetails");

            migrationBuilder.DropColumn(
                name: "ColorPrice",
                table: "OrderDetails");
        }
    }
}
