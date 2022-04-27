using AutoMapper;
using hotel_booking.DAL.Models;
using hotel_booking.DAL.ViewModels;

namespace hotel_booking.DAL
{
    public class AppAutoMapper : Profile
    {
        public AppAutoMapper()
        {
            CreateMap<Hotel, HotelVMCreate>().ReverseMap();
            CreateMap<Hotel, HotelVMUpdate>().ReverseMap();
            CreateMap<Booking, BookingVMCreate>().ReverseMap();
            CreateMap<Booking, BookingVMUpdate>().ReverseMap();
        }
    }
}
