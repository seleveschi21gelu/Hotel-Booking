using hotel_booking.DAL.Models;
using hotel_booking.DAL.ViewModels;

namespace hotel_booking.IBLL
{
    public interface IHotelBLL
    {
        Task<IEnumerable<Hotel>> GetHotelsAsync();
        Task<Hotel> GetHotelAsync(int id);
        Task<int> AddHotelAsync(HotelVMCreate model);
        Task<HotelVMUpdate> UpdateHotelAsync(int id, HotelVMUpdate model);
        Task DeleteAsync(int id);
        //Task DeletePhoto(string image);
    }
}
