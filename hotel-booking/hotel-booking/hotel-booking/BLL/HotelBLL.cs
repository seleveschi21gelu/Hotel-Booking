using AutoMapper;
using hotel_booking.DAL.Models;
using hotel_booking.DAL.ViewModels;
using hotel_booking.IBLL;
using hotel_booking.IRepository;

namespace hotel_booking.BLL
{
    public class HotelBLL : IHotelBLL
    {
        readonly IHotelRepository _hotelRepository;
        readonly IHotelDetailsRepository _hotelDetailsRepository;
        readonly IMapper _autoMapper;

        public HotelBLL(IHotelRepository hotelRepository, IMapper autoMapper, IHotelDetailsRepository hotelDetailsRepository)
        {
            _hotelRepository = hotelRepository;
            _autoMapper = autoMapper;
            _hotelDetailsRepository = hotelDetailsRepository;
        }

        public async Task<IEnumerable<Hotel>> GetHotelsAsync()
        {
            var hotels = await _hotelRepository.GetHotelsAsync();

            return hotels;
        }

        public async Task<Hotel> GetHotelAsync(int id)
                        => await _hotelRepository.GetHotelAsync(id);

        public async Task<int> AddHotelAsync(HotelVMCreate model)
        {
            var hotel = _autoMapper.Map(model, new Hotel());

            var hotelId = await _hotelRepository.AddHotelAsync(hotel);

            return hotelId;
        }

        public async Task<HotelVMUpdate> UpdateHotelAsync(int id, HotelVMUpdate model)
        {
            var hotel = await GetHotelAsync(id);

            if (!model.Image.Equals(hotel.Image))
            {
                DeleteImage(hotel.Image);
            }
            var hotelDetails = await _hotelDetailsRepository.GetHotelDetails(id);
            if (hotelDetails != null)
            {
                hotel.HotelDetailsId = hotelDetails.Id;
            }

            var hotelUpdated = _autoMapper.Map(model, hotel);

            await _hotelRepository.UpdateHotelAsync(id, hotelUpdated);

            return model;
        }

        public async Task DeleteAsync(int id)
        {
            var hotel = await GetHotelAsync(id);

            DeleteImage(hotel.Image);

            var hotelDetails = await _hotelDetailsRepository.GetHotelDetails(id);
            if (hotelDetails != null)
            {
                if (hotelDetails.Pictures.Any())
                {
                    foreach (var img in hotelDetails.Pictures)
                    {
                        if (img != null)
                        {
                            DeleteImage(img.Image);
                        }
                    }
                }
            }
            await _hotelDetailsRepository.DeleteHotelDetails(id);

            await _hotelRepository.DeleteAsync(id);
        }

        private void DeleteImage(string image)
        {
            String source = @"C:\gelu-azure\gelu-training\hotel-project\hotel-booking-web\src\";
            if (source != null && !source.Contains(image))
            {
                File.Delete(source + image);
            }
        }

    }
}
