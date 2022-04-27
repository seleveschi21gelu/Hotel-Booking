using hotel_booking.DAL.Models;
using hotel_booking.DAL.ViewModels;
using hotel_booking.IBLL;
using hotel_booking.IRepository;
using hotel_booking.Response;

namespace hotel_booking.BLL
{
    public class HotelDetailsBLL : IHotelDetailsBLL
    {
        readonly IHotelDetailsRepository _hotelDetailsRepository;
        readonly IHotelRepository _hotelRepository;
        readonly IPicturesRepository _picturesRepository;

        public HotelDetailsBLL(IHotelDetailsRepository hotelDetailsRepository, IHotelRepository hotelRepository, IPicturesRepository picturesRepository)
        {
            _hotelDetailsRepository = hotelDetailsRepository;
            _hotelRepository = hotelRepository;
            _picturesRepository = picturesRepository;
        }

        public async Task<IEnumerable<HotelDetailsResponse>> GetHotelDetailsListAsync()
        {
            var hotelDetailsList = await _hotelDetailsRepository.GetHotelDetailsListAsync();
            var hotelDetailsResponseList = new List<HotelDetailsResponse>();

            foreach (var item in hotelDetailsList)
            {
                var pictureVMList = new List<PicturesVM>();

                foreach (var pic in item.Pictures)
                {
                    var picture = new PicturesVM()
                    {
                        Image = pic.Image,
                        HotelDetailsId = pic.HotelDetailsId
                    };

                    pictureVMList.Add(picture);
                }

                var hotel = await _hotelRepository.GetHotelAsync(item.HotelId);

                if (hotel != null)
                {
                    var hotelDetailsResponse = new HotelDetailsResponse()
                    {
                        Id = item.Id,
                        HotelId = item.HotelId,
                        HotelName = hotel.Name,
                        City = item.City,
                        Country = item.Country,
                        Address = hotel.Address,
                        Phone = hotel.Phone,
                        Rooms = item.Rooms,
                        Stars = item.Stars,
                        Pictures = item.Pictures.Select(p => p.Image).ToList()
                    };

                    hotelDetailsResponseList.Add(hotelDetailsResponse);
                }
            }

            return hotelDetailsResponseList;
        }

        public async Task<HotelDetailsResponse> GetHotelDetails(int id)
        {
            var hotel = await _hotelRepository.GetHotelAsync(id);
            if (hotel != null)
            {
                var hotelDetails = await _hotelDetailsRepository.GetHotelDetails(id);
                if (hotelDetails != null)
                {
                    var hotelDetailsResponse = new HotelDetailsResponse()
                    {
                        Id = hotelDetails.Id,
                        HotelId = hotel.Id,
                        HotelName = hotel.Name,
                        City = hotelDetails.City,
                        Country = hotelDetails.Country,
                        Address = hotel.Address,
                        Phone = hotel.Phone,
                        Rooms = hotelDetails.Rooms,
                        Stars = hotelDetails.Stars,
                        Pictures = hotelDetails.Pictures.Select(p => p.Image).ToList()
                    };

                    return hotelDetailsResponse;
                };
            }
            return new HotelDetailsResponse();
        }

        public async Task AddHotelDetailsAsync(HotelDetailsVMCreate model)
        {
            var pictures = new List<Pictures>();
            if (model.Pictures.Any())
            {
                foreach (var imageName in model.Pictures)
                {
                    pictures.Add(new Pictures { Image = imageName });
                }
            }
            var hotelDetails = new HotelDetails()
            {
                HotelId = model.HotelId,
                City = model.City,
                Country = model.Country,
                Rooms = model.Rooms,
                Stars = model.Stars,
                Pictures = pictures
            };

            await _hotelDetailsRepository.AddHotelDetailsAsync(hotelDetails);
        }

        private void DeleteImage(string image, string hotelName)
        {
            string source = $@"C:\gelu-azure\gelu-training\hotel-project\hotel-booking-web\src\assets\images\{hotelName}";
            string file = source + @"\" + image;
            if (File.Exists(file))
            {
                File.Delete(file);
            }
        }

        public async Task UpdateHotelDetailsAsync(HotelDetailsVMUpdate model)
        {
            var hotelDetails = await _hotelDetailsRepository.GetHotelDetails(model.HotelId);
            var hotel = await _hotelRepository.GetHotelAsync(model.HotelId);
            var path = $"assets/images/{model.HotelId}/";

            foreach (var pic in hotelDetails.Pictures)
            {
                pic.Image = pic.Image.Remove(0, path.Length);
                foreach (var newPics in model.Pictures)
                {
                    var p = newPics.Remove(0, path.Length);

                    if (pic.Image != p)
                    {
                        DeleteImage(pic.Image, hotel.Name);
                    }
                }
            }

            var existingPicturesList = new List<Pictures>(hotelDetails.Pictures);

            foreach (var picture in existingPicturesList)
            {
                await _picturesRepository.DeletePictures(picture);
            }

            var picturesList = new List<Pictures>();


            foreach (var img in model.Pictures)
            {
                var picture = new Pictures()
                {
                    HotelDetailsId = hotelDetails.Id,
                    Image = img
                };

                picturesList.Add(picture);
            }

            hotelDetails.City = model.City;
            hotelDetails.Country = model.Country;
            hotelDetails.Rooms = model.Rooms;
            hotelDetails.Stars = model.Stars;
            hotelDetails.Pictures = picturesList;

            await _hotelDetailsRepository.UpdateHotelDetails(hotelDetails.Id, hotelDetails);
        }
    }
}
