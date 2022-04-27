using System.ComponentModel.DataAnnotations.Schema;

namespace hotel_booking.DAL.Models
{
    public class HotelDetails
    {
        public int Id { get; set; }
        [ForeignKey("Hotels")]
        public int HotelId { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Rooms { get; set; }
        public decimal Stars { get; set; }
        public List<Pictures>? Pictures { get; set; }
    }
}
