using hotel_booking.DAL.ViewModels;
using hotel_booking.Response;

namespace hotel_booking.IBLL
{
    public interface IHotelDetailsBLL
    {
        Task AddHotelDetailsAsync(HotelDetailsVMCreate model);
        Task<IEnumerable<HotelDetailsResponse>> GetHotelDetailsListAsync();
        Task<HotelDetailsResponse> GetHotelDetails(int id);
        Task UpdateHotelDetailsAsync(HotelDetailsVMUpdate model);
    }
}
