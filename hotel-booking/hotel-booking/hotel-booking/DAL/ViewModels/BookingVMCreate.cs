namespace hotel_booking.DAL.ViewModels
{
    public class BookingVMCreate
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public DateTime StartBooking { get; set; }
        public DateTime EndBooking { get; set; }
        public int HotelId { get; set; }
    }

    public class BookingVMUpdate : BookingVMCreate
    {
        public int Id { get; set; }
    }
}
