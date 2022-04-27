using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace hotel_booking.Migrations
{
    public partial class updatemodels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Hotels_HotelDetails_HotelDetailsId",
                table: "Hotels");

            migrationBuilder.DropIndex(
                name: "IX_Hotels_HotelDetailsId",
                table: "Hotels");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
    }
}
