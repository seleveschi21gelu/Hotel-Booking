using hotel_booking.DAL;
using hotel_booking.DAL.Models;
using hotel_booking.IRepository;
using Microsoft.EntityFrameworkCore;

namespace hotel_booking.Repository
{
    public class HotelRepository : IHotelRepository
    {
        readonly AppDBContext _dBContext;

        public HotelRepository(AppDBContext dBContext)
        {
            _dBContext = dBContext;
        }

        public async Task<Hotel> GetHotelAsync(int id)
                    => await _dBContext.Hotels.FirstOrDefaultAsync(h => h.Id == id);

        public async Task<IEnumerable<Hotel>> GetHotelsAsync()
        {
            var data = await _dBContext.Hotels.ToListAsync();

            return data;
        }

        public async Task<int> AddHotelAsync(Hotel model)
        {
            var hotel = await _dBContext.Hotels.AddAsync(model);

            await _dBContext.SaveChangesAsync();

            return hotel.Entity.Id;
        }

        public async Task<Hotel> UpdateHotelAsync(int id, Hotel model)
        {
            var hotel = _dBContext.Update(model);

            await _dBContext.SaveChangesAsync();

            return hotel.Entity;
        }

        public async Task DeleteAsync(int id)
        {
            var hotel = await _dBContext.Hotels.FirstOrDefaultAsync(h => h.Id == id);

            if (hotel != null)
            {
                _dBContext.Remove(hotel);
                await _dBContext.SaveChangesAsync();
            }
        }

    }
}
