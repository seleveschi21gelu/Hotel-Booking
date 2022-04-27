using hotel_booking.DAL;
using hotel_booking.DAL.Models;
using hotel_booking.IRepository;
using Microsoft.EntityFrameworkCore;

namespace hotel_booking.Repository
{
    public class HotelDetailsRepository : IHotelDetailsRepository
    {
        readonly AppDBContext _dBContext;

        public HotelDetailsRepository(AppDBContext dBContext)
        {
            _dBContext = dBContext;
        }

        public async Task<IEnumerable<HotelDetails>> GetHotelDetailsListAsync()
        {
            var hotelDetailsList = await _dBContext.HotelDetails.Include(p => p.Pictures).ToListAsync();

            return hotelDetailsList;
        }

        public async Task<HotelDetails> GetHotelDetails(int id)
        {
            var hotelDetails = await _dBContext.HotelDetails.Include(hd => hd.Pictures).FirstOrDefaultAsync(hd => hd.HotelId == id);

            await _dBContext.SaveChangesAsync();

            return hotelDetails;
        }

        public async Task AddHotelDetailsAsync(HotelDetails hotelDetailsModel)
        {
            var hotelDetails = await _dBContext.HotelDetails.AddAsync(hotelDetailsModel);
            await _dBContext.SaveChangesAsync();

            var hotel = await _dBContext.Hotels.FirstOrDefaultAsync(h => h.Id == hotelDetailsModel.HotelId);
            if (hotel != null)
            {
                hotelDetails.Entity.HotelId = hotel.Id;
                hotel.HotelDetailsId = hotelDetails.Entity.Id;
                _dBContext.Hotels.Update(hotel);
                await _dBContext.SaveChangesAsync();
            }
        }

        public async Task UpdateHotelDetails(int id, HotelDetails model)
        {
            _dBContext.HotelDetails.Update(model);
            await _dBContext.SaveChangesAsync();
        }

        public async Task DeleteHotelDetails(int id)
        {
            var hotelDetails = await _dBContext.HotelDetails.Include(hd => hd.Pictures).FirstOrDefaultAsync(hd => hd.Id == id);

            if (hotelDetails != null)
            {
                _dBContext.HotelDetails.Remove(hotelDetails);
            }
        }
    }
}
