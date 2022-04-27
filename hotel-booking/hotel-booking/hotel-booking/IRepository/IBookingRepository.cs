using hotel_booking.DAL.Models;

namespace hotel_booking.IRepository
{
    public interface IBookingRepository
    {
        Task AddBookingAsync(Booking model);
        Task<IEnumerable<Booking>> GetAllBookingAsync();
        Task UpdateBookingAsync(int id, Booking booking);
        Task<Booking> GetBookingAsync(int id);
    }
}
