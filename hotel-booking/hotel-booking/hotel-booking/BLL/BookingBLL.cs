using AutoMapper;
using hotel_booking.DAL.Models;
using hotel_booking.DAL.ViewModels;
using hotel_booking.IBLL;
using hotel_booking.IRepository;
using hotel_booking.Response;

namespace hotel_booking.BLL
{
    public class BookingBLL : IBookingBLL
    {
        readonly IBookingRepository _bookingRepository;
        readonly IMapper _mapper;
        readonly IHotelRepository _hotelRepository;
        public BookingBLL(IBookingRepository bookingRepository, IMapper mapper, IHotelRepository hotelRepository)
        {
            _bookingRepository = bookingRepository;
            _mapper = mapper;
            _hotelRepository = hotelRepository;
        }

        public async Task AddBookingAsync(BookingVMCreate model)
        {
            var booking = _mapper.Map(model, new Booking());

            await _bookingRepository.AddBookingAsync(booking);
        }

        public async Task<IEnumerable<BookingResponse>> GetAllBookingsAsync()
        {
            var bookingList = await _bookingRepository.GetAllBookingAsync();
            var bookingResponseList = new List<BookingResponse>();

            foreach (var booking in bookingList)
            {
                var hotel = await _hotelRepository.GetHotelAsync(booking.HotelId);
                var bookingObj = new BookingResponse()
                {
                    Id = booking.Id,
                    FirstName = booking.FirstName,
                    LastName = booking.LastName,
                    DateOfBirth = booking.DateOfBirth,
                    Email = booking.Email,
                    Phone = booking.Phone,
                    Address = booking.Address,
                    StartBooking = booking.StartBooking,
                    EndBooking = booking.EndBooking,
                    HotelName = hotel.Name
                };

                bookingResponseList.Add(bookingObj);
            }

            return bookingResponseList;
        }

        public async Task<BookingResponse> GetBookingAsync(int id)
        {
            var booking = await _bookingRepository.GetBookingAsync(id);
            var hotel = await _hotelRepository.GetHotelAsync(booking.HotelId);
            string hotelName = "";

            if (hotel != null)
            {
                hotelName = hotel.Name;
            }

            var bookingResponse = new BookingResponse()
            {
                Id = booking.Id,
                FirstName = booking.FirstName,
                LastName = booking.LastName,
                Address = booking.Address,
                Email = booking.Email,
                DateOfBirth = booking.DateOfBirth,
                StartBooking = booking.StartBooking,
                EndBooking = booking.EndBooking,
                Phone = booking.Phone,
                HotelName = hotelName
            };

            return bookingResponse;
        }

        public async Task UpdateBookingAsync(int id, BookingVMUpdate model)
        {
            var booking = await _bookingRepository.GetBookingAsync(id);

            var newBooking = _mapper.Map(model, booking);

            await _bookingRepository.UpdateBookingAsync(id, newBooking);
        }
    }
}