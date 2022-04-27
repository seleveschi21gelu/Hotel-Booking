namespace hotel_booking.DAL.Models
{
    public class Pictures
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public int HotelDetailsId { get; set; }
        //public HotelDetails HotelDetails { get; set; }
    }
}
