using hotel_booking.DAL.Models;

namespace hotel_booking.IRepository
{
    public interface IHotelDetailsRepository
    {
        Task<IEnumerable<HotelDetails>> GetHotelDetailsListAsync();
        Task AddHotelDetailsAsync(HotelDetails hotelDetails);
        Task<HotelDetails> GetHotelDetails(int id);
        Task UpdateHotelDetails(int id, HotelDetails model);
        Task DeleteHotelDetails(int id);
    }
}
