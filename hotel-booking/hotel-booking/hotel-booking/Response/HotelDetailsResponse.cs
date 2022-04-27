using hotel_booking.DAL.Models;
using hotel_booking.DAL.ViewModels;

namespace hotel_booking.Response
{
    public class HotelDetailsResponse
    {
        public int Id { get; set; }
        public int? HotelId { get; set; }
        public string HotelName { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Rooms { get; set; }
        public decimal Stars { get; set; }
        public List<string>? Pictures { get; set; }
    }
}
