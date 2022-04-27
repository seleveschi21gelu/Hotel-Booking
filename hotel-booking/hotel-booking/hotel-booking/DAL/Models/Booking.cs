using System.ComponentModel.DataAnnotations.Schema;

namespace hotel_booking.DAL.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public DateTime StartBooking { get; set; }
        public DateTime EndBooking { get; set; }
        
        [ForeignKey("Hotel")]
        public int HotelId { get; set; }
    }
}
