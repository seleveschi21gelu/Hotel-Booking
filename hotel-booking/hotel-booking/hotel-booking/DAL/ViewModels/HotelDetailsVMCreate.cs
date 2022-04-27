using hotel_booking.DAL.Models;

namespace hotel_booking.DAL.ViewModels
{
    public class HotelDetailsVMCreate
    {
        public int HotelId { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Rooms { get; set; }
        public decimal Stars { get; set; }
        public List<string> Pictures { get; set; }
    }

    public class HotelDetailsVMUpdate : HotelDetailsVMCreate
    {
        public int Id { get; set; }
    }
}
