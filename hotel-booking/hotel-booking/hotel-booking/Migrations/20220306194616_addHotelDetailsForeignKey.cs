using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace hotel_booking.Migrations
{
    public partial class addHotelDetailsForeignKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HotelDetails_Hotels_HotelId",
                table: "HotelDetails");

            migrationBuilder.DropIndex(
                name: "IX_HotelDetails_HotelId",
                table: "HotelDetails");

            migrationBuilder.AddColumn<int>(
                name: "HotelDetailsId",
                table: "Hotels",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Hotels_HotelDetailsId",
                table: "Hotels",
                column: "HotelDetailsId",
                unique: true,
                filter: "[HotelDetailsId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Hotels_HotelDetails_HotelDetailsId",
                table: "Hotels",
                column: "HotelDetailsId",
                principalTable: "HotelDetails",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Hotels_HotelDetails_HotelDetailsId",
                table: "Hotels");

            migrationBuilder.DropIndex(
                name: "IX_Hotels_HotelDetailsId",
                table: "Hotels");

            migrationBuilder.DropColumn(
                name: "HotelDetailsId",
                table: "Hotels");

            migrationBuilder.CreateIndex(
                name: "IX_HotelDetails_HotelId",
                table: "HotelDetails",
                column: "HotelId");

            migrationBuilder.AddForeignKey(
                name: "FK_HotelDetails_Hotels_HotelId",
                table: "HotelDetails",
                column: "HotelId",
                principalTable: "Hotels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
