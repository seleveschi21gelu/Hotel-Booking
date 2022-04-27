using hotel_booking.DAL.Models;
using hotel_booking.DAL.ViewModels;
using hotel_booking.Response;

namespace hotel_booking.IBLL
{
    public interface IBookingBLL
    {
        Task<IEnumerable<BookingResponse>> GetAllBookingsAsync();
        Task AddBookingAsync(BookingVMCreate model);
        Task UpdateBookingAsync(int id, BookingVMUpdate model);
        Task<BookingResponse> GetBookingAsync(int id);
    }
}
