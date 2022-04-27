using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace hotel_booking.Migrations
{
    public partial class addedHotelDetailstable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "HotelDetailsId",
                table: "Pictures",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "HotelDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HotelId = table.Column<int>(type: "int", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rooms = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Stars = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HotelDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HotelDetails_Hotels_HotelId",
                        column: x => x.HotelId,
                        principalTable: "Hotels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pictures_HotelDetailsId",
                table: "Pictures",
                column: "HotelDetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_HotelDetails_HotelId",
                table: "HotelDetails",
                column: "HotelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pictures_HotelDetails_HotelDetailsId",
                table: "Pictures",
                column: "HotelDetailsId",
                principalTable: "HotelDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pictures_HotelDetails_HotelDetailsId",
                table: "Pictures");

            migrationBuilder.DropTable(
                name: "HotelDetails");

            migrationBuilder.DropIndex(
                name: "IX_Pictures_HotelDetailsId",
                table: "Pictures");

            migrationBuilder.DropColumn(
                name: "HotelDetailsId",
                table: "Pictures");
        }
    }
}
