using hotel_booking.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace hotel_booking.DAL
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options) { }

        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Pictures> Pictures { get; set; }
        public DbSet<HotelDetails> HotelDetails { get; set; }
        public DbSet<Booking> Bookings { get; set; }
    }
}
