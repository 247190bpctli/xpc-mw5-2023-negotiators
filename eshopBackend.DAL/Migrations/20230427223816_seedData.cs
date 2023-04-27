using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace eshopBackend.DAL.Migrations
{
    /// <inheritdoc />
    public partial class seedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Description", "ImageUrl", "Name" },
                values: new object[,]
                {
                    { new Guid("817a56e2-15a1-0a5e-73e9-8b02851f531f"), "Et numquam quia reiciendis eligendi molestiae in incidunt et facere.", "data:image/svg+xml;charset=UTF-8,%3Csvg%20xmlns%3D%22http%3A%2F%2Fwww.w3.org%2F2000%2Fsvg%22%20version%3D%221.1%22%20baseProfile%3D%22full%22%20width%3D%22200%22%20height%3D%22100%22%3E%3Crect%20width%3D%22100%25%22%20height%3D%22100%25%22%20fill%3D%22grey%22%2F%3E%3Ctext%20x%3D%22100%22%20y%3D%2250%22%20font-size%3D%2220%22%20alignment-baseline%3D%22middle%22%20text-anchor%3D%22middle%22%20fill%3D%22white%22%3E200x100%3C%2Ftext%3E%3C%2Fsvg%3E", "Hatchback" },
                    { new Guid("88a26359-c130-0418-3ba3-0cc7494c5287"), "Voluptatibus voluptatibus laudantium et nostrum dolor corporis nihil nostrum.", "data:image/svg+xml;charset=UTF-8,%3Csvg%20xmlns%3D%22http%3A%2F%2Fwww.w3.org%2F2000%2Fsvg%22%20version%3D%221.1%22%20baseProfile%3D%22full%22%20width%3D%22200%22%20height%3D%22100%22%3E%3Crect%20width%3D%22100%25%22%20height%3D%22100%25%22%20fill%3D%22grey%22%2F%3E%3Ctext%20x%3D%22100%22%20y%3D%2250%22%20font-size%3D%2220%22%20alignment-baseline%3D%22middle%22%20text-anchor%3D%22middle%22%20fill%3D%22white%22%3E200x100%3C%2Ftext%3E%3C%2Fsvg%3E", "Coupe" },
                    { new Guid("8f95f348-52c3-b4ae-f48f-ee06eba1bcbf"), "Voluptatem et rerum et aut rerum et voluptas cum aut.", "data:image/svg+xml;charset=UTF-8,%3Csvg%20xmlns%3D%22http%3A%2F%2Fwww.w3.org%2F2000%2Fsvg%22%20version%3D%221.1%22%20baseProfile%3D%22full%22%20width%3D%22200%22%20height%3D%22100%22%3E%3Crect%20width%3D%22100%25%22%20height%3D%22100%25%22%20fill%3D%22grey%22%2F%3E%3Ctext%20x%3D%22100%22%20y%3D%2250%22%20font-size%3D%2220%22%20alignment-baseline%3D%22middle%22%20text-anchor%3D%22middle%22%20fill%3D%22white%22%3E200x100%3C%2Ftext%3E%3C%2Fsvg%3E", "Cargo Van" },
                    { new Guid("9519ef64-efca-c060-1dc4-f57bb32e1e68"), "Ducimus deserunt totam sunt fugiat sapiente aut voluptate.", "data:image/svg+xml;charset=UTF-8,%3Csvg%20xmlns%3D%22http%3A%2F%2Fwww.w3.org%2F2000%2Fsvg%22%20version%3D%221.1%22%20baseProfile%3D%22full%22%20width%3D%22200%22%20height%3D%22100%22%3E%3Crect%20width%3D%22100%25%22%20height%3D%22100%25%22%20fill%3D%22grey%22%2F%3E%3Ctext%20x%3D%22100%22%20y%3D%2250%22%20font-size%3D%2220%22%20alignment-baseline%3D%22middle%22%20text-anchor%3D%22middle%22%20fill%3D%22white%22%3E200x100%3C%2Ftext%3E%3C%2Fsvg%3E", "Wagon" },
                    { new Guid("cbe04e16-5651-8dec-fef1-297ee520276a"), "Similique minus consectetur rerum necessitatibus voluptate et ut nihil.", "data:image/svg+xml;charset=UTF-8,%3Csvg%20xmlns%3D%22http%3A%2F%2Fwww.w3.org%2F2000%2Fsvg%22%20version%3D%221.1%22%20baseProfile%3D%22full%22%20width%3D%22200%22%20height%3D%22100%22%3E%3Crect%20width%3D%22100%25%22%20height%3D%22100%25%22%20fill%3D%22grey%22%2F%3E%3Ctext%20x%3D%22100%22%20y%3D%2250%22%20font-size%3D%2220%22%20alignment-baseline%3D%22middle%22%20text-anchor%3D%22middle%22%20fill%3D%22white%22%3E200x100%3C%2Ftext%3E%3C%2Fsvg%3E", "Convertible" }
                });

            migrationBuilder.InsertData(
                table: "Manufacturers",
                columns: new[] { "Id", "Description", "LogoUrl", "Name", "Origin" },
                values: new object[,]
                {
                    { new Guid("2a5e1227-6a0c-33f3-816e-1a3c387f1af4"), "Non odio laboriosam.", "data:image/svg+xml;charset=UTF-8,%3Csvg%20xmlns%3D%22http%3A%2F%2Fwww.w3.org%2F2000%2Fsvg%22%20version%3D%221.1%22%20baseProfile%3D%22full%22%20width%3D%22200%22%20height%3D%22100%22%3E%3Crect%20width%3D%22100%25%22%20height%3D%22100%25%22%20fill%3D%22grey%22%2F%3E%3Ctext%20x%3D%22100%22%20y%3D%2250%22%20font-size%3D%2220%22%20alignment-baseline%3D%22middle%22%20text-anchor%3D%22middle%22%20fill%3D%22white%22%3E200x100%3C%2Ftext%3E%3C%2Fsvg%3E", "Rolls Royce", "Comoros" },
                    { new Guid("389f64c4-a6fd-997e-b230-b40c9feb0e09"), "Et consequatur est accusantium perferendis delectus inventore.", "data:image/svg+xml;charset=UTF-8,%3Csvg%20xmlns%3D%22http%3A%2F%2Fwww.w3.org%2F2000%2Fsvg%22%20version%3D%221.1%22%20baseProfile%3D%22full%22%20width%3D%22200%22%20height%3D%22100%22%3E%3Crect%20width%3D%22100%25%22%20height%3D%22100%25%22%20fill%3D%22grey%22%2F%3E%3Ctext%20x%3D%22100%22%20y%3D%2250%22%20font-size%3D%2220%22%20alignment-baseline%3D%22middle%22%20text-anchor%3D%22middle%22%20fill%3D%22white%22%3E200x100%3C%2Ftext%3E%3C%2Fsvg%3E", "Ford", "Netherlands Antilles" },
                    { new Guid("4acf9fb9-f1c0-3e93-99f5-daaf3b654b91"), "Voluptatum consequuntur quo.", "data:image/svg+xml;charset=UTF-8,%3Csvg%20xmlns%3D%22http%3A%2F%2Fwww.w3.org%2F2000%2Fsvg%22%20version%3D%221.1%22%20baseProfile%3D%22full%22%20width%3D%22200%22%20height%3D%22100%22%3E%3Crect%20width%3D%22100%25%22%20height%3D%22100%25%22%20fill%3D%22grey%22%2F%3E%3Ctext%20x%3D%22100%22%20y%3D%2250%22%20font-size%3D%2220%22%20alignment-baseline%3D%22middle%22%20text-anchor%3D%22middle%22%20fill%3D%22white%22%3E200x100%3C%2Ftext%3E%3C%2Fsvg%3E", "Jaguar", "Slovakia (Slovak Republic)" },
                    { new Guid("ce233d69-1560-ca25-39c2-65f3aaee97be"), "Dolorem ut aliquid libero voluptas.", "data:image/svg+xml;charset=UTF-8,%3Csvg%20xmlns%3D%22http%3A%2F%2Fwww.w3.org%2F2000%2Fsvg%22%20version%3D%221.1%22%20baseProfile%3D%22full%22%20width%3D%22200%22%20height%3D%22100%22%3E%3Crect%20width%3D%22100%25%22%20height%3D%22100%25%22%20fill%3D%22grey%22%2F%3E%3Ctext%20x%3D%22100%22%20y%3D%2250%22%20font-size%3D%2220%22%20alignment-baseline%3D%22middle%22%20text-anchor%3D%22middle%22%20fill%3D%22white%22%3E200x100%3C%2Ftext%3E%3C%2Fsvg%3E", "BMW", "Luxembourg" },
                    { new Guid("ecfcfd47-73ae-1870-2fc7-9007cd0cf82a"), "Voluptatibus vero ducimus veniam eos.", "data:image/svg+xml;charset=UTF-8,%3Csvg%20xmlns%3D%22http%3A%2F%2Fwww.w3.org%2F2000%2Fsvg%22%20version%3D%221.1%22%20baseProfile%3D%22full%22%20width%3D%22200%22%20height%3D%22100%22%3E%3Crect%20width%3D%22100%25%22%20height%3D%22100%25%22%20fill%3D%22grey%22%2F%3E%3Ctext%20x%3D%22100%22%20y%3D%2250%22%20font-size%3D%2220%22%20alignment-baseline%3D%22middle%22%20text-anchor%3D%22middle%22%20fill%3D%22white%22%3E200x100%3C%2Ftext%3E%3C%2Fsvg%3E", "Ferrari", "Malaysia" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CartEntityId", "CategoryId", "Description", "Discriminator", "ImageUrl", "ManufacturerId", "Name", "Price", "Stock", "Weight" },
                values: new object[,]
                {
                    { new Guid("22173c19-03e4-1f35-1e5a-6d403ca72a3a"), null, new Guid("817a56e2-15a1-0a5e-73e9-8b02851f531f"), "Voluptates quae placeat est dolore.", "ProductEntity", "data:image/svg+xml;charset=UTF-8,%3Csvg%20xmlns%3D%22http%3A%2F%2Fwww.w3.org%2F2000%2Fsvg%22%20version%3D%221.1%22%20baseProfile%3D%22full%22%20width%3D%22200%22%20height%3D%22100%22%3E%3Crect%20width%3D%22100%25%22%20height%3D%22100%25%22%20fill%3D%22grey%22%2F%3E%3Ctext%20x%3D%22100%22%20y%3D%2250%22%20font-size%3D%2220%22%20alignment-baseline%3D%22middle%22%20text-anchor%3D%22middle%22%20fill%3D%22white%22%3E200x100%3C%2Ftext%3E%3C%2Fsvg%3E", new Guid("ecfcfd47-73ae-1870-2fc7-9007cd0cf82a"), "Mazda", 364.0, 2, 17.0 },
                    { new Guid("3d4c5b68-9a73-baf5-f63e-2ffd7cb80377"), null, new Guid("88a26359-c130-0418-3ba3-0cc7494c5287"), "Consequuntur sit cum provident.", "ProductEntity", "data:image/svg+xml;charset=UTF-8,%3Csvg%20xmlns%3D%22http%3A%2F%2Fwww.w3.org%2F2000%2Fsvg%22%20version%3D%221.1%22%20baseProfile%3D%22full%22%20width%3D%22200%22%20height%3D%22100%22%3E%3Crect%20width%3D%22100%25%22%20height%3D%22100%25%22%20fill%3D%22grey%22%2F%3E%3Ctext%20x%3D%22100%22%20y%3D%2250%22%20font-size%3D%2220%22%20alignment-baseline%3D%22middle%22%20text-anchor%3D%22middle%22%20fill%3D%22white%22%3E200x100%3C%2Ftext%3E%3C%2Fsvg%3E", new Guid("4acf9fb9-f1c0-3e93-99f5-daaf3b654b91"), "Audi", 113.0, 0, 0.0 },
                    { new Guid("59953808-11cc-5b6d-1b80-1fd1c7754b3f"), null, new Guid("9519ef64-efca-c060-1dc4-f57bb32e1e68"), "Eveniet a suscipit sed aspernatur.", "ProductEntity", "data:image/svg+xml;charset=UTF-8,%3Csvg%20xmlns%3D%22http%3A%2F%2Fwww.w3.org%2F2000%2Fsvg%22%20version%3D%221.1%22%20baseProfile%3D%22full%22%20width%3D%22200%22%20height%3D%22100%22%3E%3Crect%20width%3D%22100%25%22%20height%3D%22100%25%22%20fill%3D%22grey%22%2F%3E%3Ctext%20x%3D%22100%22%20y%3D%2250%22%20font-size%3D%2220%22%20alignment-baseline%3D%22middle%22%20text-anchor%3D%22middle%22%20fill%3D%22white%22%3E200x100%3C%2Ftext%3E%3C%2Fsvg%3E", new Guid("389f64c4-a6fd-997e-b230-b40c9feb0e09"), "Mazda", 209.0, 2, 44.0 },
                    { new Guid("8ab9bb7a-2cc9-a5c9-cabe-1fa9afa04c05"), null, new Guid("cbe04e16-5651-8dec-fef1-297ee520276a"), "Omnis dolor quo enim et velit dolor.", "ProductEntity", "data:image/svg+xml;charset=UTF-8,%3Csvg%20xmlns%3D%22http%3A%2F%2Fwww.w3.org%2F2000%2Fsvg%22%20version%3D%221.1%22%20baseProfile%3D%22full%22%20width%3D%22200%22%20height%3D%22100%22%3E%3Crect%20width%3D%22100%25%22%20height%3D%22100%25%22%20fill%3D%22grey%22%2F%3E%3Ctext%20x%3D%22100%22%20y%3D%2250%22%20font-size%3D%2220%22%20alignment-baseline%3D%22middle%22%20text-anchor%3D%22middle%22%20fill%3D%22white%22%3E200x100%3C%2Ftext%3E%3C%2Fsvg%3E", new Guid("2a5e1227-6a0c-33f3-816e-1a3c387f1af4"), "Land Rover", 142.0, 0, 22.0 },
                    { new Guid("e50160d7-a707-c20b-c910-382620fb67c6"), null, new Guid("8f95f348-52c3-b4ae-f48f-ee06eba1bcbf"), "Illo qui ad optio vitae ea.", "ProductEntity", "data:image/svg+xml;charset=UTF-8,%3Csvg%20xmlns%3D%22http%3A%2F%2Fwww.w3.org%2F2000%2Fsvg%22%20version%3D%221.1%22%20baseProfile%3D%22full%22%20width%3D%22200%22%20height%3D%22100%22%3E%3Crect%20width%3D%22100%25%22%20height%3D%22100%25%22%20fill%3D%22grey%22%2F%3E%3Ctext%20x%3D%22100%22%20y%3D%2250%22%20font-size%3D%2220%22%20alignment-baseline%3D%22middle%22%20text-anchor%3D%22middle%22%20fill%3D%22white%22%3E200x100%3C%2Ftext%3E%3C%2Fsvg%3E", new Guid("ce233d69-1560-ca25-39c2-65f3aaee97be"), "Chrysler", 483.0, 4, 23.0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("22173c19-03e4-1f35-1e5a-6d403ca72a3a"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("3d4c5b68-9a73-baf5-f63e-2ffd7cb80377"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("59953808-11cc-5b6d-1b80-1fd1c7754b3f"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("8ab9bb7a-2cc9-a5c9-cabe-1fa9afa04c05"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("e50160d7-a707-c20b-c910-382620fb67c6"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("817a56e2-15a1-0a5e-73e9-8b02851f531f"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("88a26359-c130-0418-3ba3-0cc7494c5287"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("8f95f348-52c3-b4ae-f48f-ee06eba1bcbf"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("9519ef64-efca-c060-1dc4-f57bb32e1e68"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("cbe04e16-5651-8dec-fef1-297ee520276a"));

            migrationBuilder.DeleteData(
                table: "Manufacturers",
                keyColumn: "Id",
                keyValue: new Guid("2a5e1227-6a0c-33f3-816e-1a3c387f1af4"));

            migrationBuilder.DeleteData(
                table: "Manufacturers",
                keyColumn: "Id",
                keyValue: new Guid("389f64c4-a6fd-997e-b230-b40c9feb0e09"));

            migrationBuilder.DeleteData(
                table: "Manufacturers",
                keyColumn: "Id",
                keyValue: new Guid("4acf9fb9-f1c0-3e93-99f5-daaf3b654b91"));

            migrationBuilder.DeleteData(
                table: "Manufacturers",
                keyColumn: "Id",
                keyValue: new Guid("ce233d69-1560-ca25-39c2-65f3aaee97be"));

            migrationBuilder.DeleteData(
                table: "Manufacturers",
                keyColumn: "Id",
                keyValue: new Guid("ecfcfd47-73ae-1870-2fc7-9007cd0cf82a"));
        }
    }
}
