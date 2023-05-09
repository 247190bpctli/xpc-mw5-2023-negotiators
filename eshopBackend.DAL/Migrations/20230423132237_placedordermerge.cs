using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eshopBackend.DAL.Migrations
{
    /// <inheritdoc />
    public partial class placedordermerge : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_PlacedOrders_PlacedOrderEntityId",
                table: "Products");

            migrationBuilder.DropTable(
                name: "PlacedOrders");

            migrationBuilder.DropIndex(
                name: "IX_Products_PlacedOrderEntityId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "PlacedOrderEntityId",
                table: "Products");

            migrationBuilder.AddColumn<bool>(
                name: "Finalized",
                table: "Carts",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastEdit",
                table: "Carts",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Finalized",
                table: "Carts");

            migrationBuilder.DropColumn(
                name: "LastEdit",
                table: "Carts");

            migrationBuilder.AddColumn<Guid>(
                name: "PlacedOrderEntityId",
                table: "Products",
                type: "char(36)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PlacedOrders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    DeliveryAddress = table.Column<string>(type: "longtext", nullable: true),
                    DeliveryType = table.Column<int>(type: "int", nullable: true),
                    PaymentDetails = table.Column<string>(type: "longtext", nullable: true),
                    PaymentType = table.Column<int>(type: "int", nullable: true),
                    Timestamp = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlacedOrders", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Products_PlacedOrderEntityId",
                table: "Products",
                column: "PlacedOrderEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_PlacedOrders_PlacedOrderEntityId",
                table: "Products",
                column: "PlacedOrderEntityId",
                principalTable: "PlacedOrders",
                principalColumn: "Id");
        }
    }
}
