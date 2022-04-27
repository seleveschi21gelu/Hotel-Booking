using hotel_booking.DAL.ViewModels;
using hotel_booking.IBLL;
using hotel_booking.Response;
using Microsoft.AspNetCore.Mvc;

namespace hotel_booking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        readonly IBookingBLL _bookingBLL;

        public BookingController(IBookingBLL bookingBLL)
        {
            _bookingBLL = bookingBLL;
        }

        [HttpGet]
        public async Task<ActionResult<BookingResponse>> GetAllBookingList()
        {
            var bookingList = await _bookingBLL.GetAllBookingsAsync();

            return Ok(bookingList);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BookingResponse>> GetBookingByIdAsync(int id)
        {
            var booking = await _bookingBLL.GetBookingAsync(id);

            return booking != null ? Ok(booking) : NotFound($"Booking with ID: {id}");
        }

        [HttpPost]
        public async Task<ActionResult> AddBookingAsync(BookingVMCreate bookingVM)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            await _bookingBLL.AddBookingAsync(bookingVM);

            return Ok(bookingVM);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateBookingAsync(int id, BookingVMUpdate bookingVM)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            await _bookingBLL.UpdateBookingAsync(id, bookingVM);

            return Ok(bookingVM);
        }
    }
}
