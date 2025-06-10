using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TravelRouteAPI.Migrations
{
    /// <inheritdoc />
    public partial class MAPICreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Routes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Origin = table.Column<string>(type: "TEXT", nullable: false),
                    Destination = table.Column<string>(type: "TEXT", nullable: false),
                    Price = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Routes", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Routes",
                columns: new[] { "Id", "Destination", "Origin", "Price" },
                values: new object[,]
                {
                    { 1, "BRC", "GRU", 10m },
                    { 2, "SCL", "BRC", 5m },
                    { 3, "CDG", "GRU", 75m },
                    { 4, "SCL", "GRU", 20m },
                    { 5, "ORL", "GRU", 56m },
                    { 6, "CDG", "ORL", 5m },
                    { 7, "ORL", "SCL", 20m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Routes");
        }
    }
}
