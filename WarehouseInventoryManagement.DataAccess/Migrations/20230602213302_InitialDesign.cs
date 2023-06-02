using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WarehouseInventoryManagement.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class InitialDesign : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DeviceStatus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Devices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Temperature = table.Column<double>(type: "float", nullable: false),
                    Pin = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DeviceStatusId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Devices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Devices_DeviceStatus_DeviceStatusId",
                        column: x => x.DeviceStatusId,
                        principalTable: "DeviceStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "DeviceStatus",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "READY" },
                    { 2, "ACTIVE" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Devices_DeviceStatusId",
                table: "Devices",
                column: "DeviceStatusId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Devices");

            migrationBuilder.DropTable(
                name: "DeviceStatus");
        }
    }
}
