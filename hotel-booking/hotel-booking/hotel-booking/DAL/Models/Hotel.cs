using System.ComponentModel.DataAnnotations.Schema;

namespace hotel_booking.DAL.Models
{
    public class Hotel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string EmailAddress { get; set; }
        public string Phone { get; set; }
        public int EmployeesNumber { get; set; }
        public string Image { get; set; }

        [ForeignKey("HotelDetails")]
        public int? HotelDetailsId { get; set; }
    }
}
