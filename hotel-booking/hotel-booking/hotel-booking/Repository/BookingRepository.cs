using hotel_booking.DAL;
using hotel_booking.DAL.Models;
using hotel_booking.IRepository;
using Microsoft.EntityFrameworkCore;

namespace hotel_booking.Repository
{
    public class BookingRepository : IBookingRepository
    {
        readonly AppDBContext _appDBContext;

        public BookingRepository(AppDBContext appDBContext)
        {
            _appDBContext = appDBContext;
        }

        public async Task AddBookingAsync(Booking model)
        {
            await _appDBContext.AddAsync(model);
            await _appDBContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Booking>> GetAllBookingAsync()
        {
            var bookingList = await _appDBContext.Bookings.ToListAsync();

            return bookingList;
        }

        public async Task<Booking> GetBookingAsync(int id)
        {
            return await _appDBContext.Bookings.FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task UpdateBookingAsync(int id, Booking booking)
        {
            _appDBContext.Bookings.Update(booking);

            await _appDBContext.SaveChangesAsync();
        }
    }
}
