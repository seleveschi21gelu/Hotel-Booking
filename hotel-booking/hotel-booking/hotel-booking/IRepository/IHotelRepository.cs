using hotel_booking.DAL.Models;

namespace hotel_booking.IRepository
{
    public interface IHotelRepository
    {
        Task<IEnumerable<Hotel>> GetHotelsAsync();
        Task<int> AddHotelAsync(Hotel model);
        Task<Hotel> UpdateHotelAsync(int id, Hotel model);
        Task<Hotel> GetHotelAsync(int id);
        Task DeleteAsync(int id);
    }
}
