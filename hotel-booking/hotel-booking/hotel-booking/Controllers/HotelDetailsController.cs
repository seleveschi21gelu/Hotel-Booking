using hotel_booking.DAL.ViewModels;
using hotel_booking.IBLL;
using hotel_booking.Response;
using Microsoft.AspNetCore.Mvc;

namespace hotel_booking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelDetailsController : ControllerBase
    {
        readonly IHotelDetailsBLL _hotelDetailsBLL;

        public HotelDetailsController(IHotelDetailsBLL hotelDetailsBLL)
        {
            _hotelDetailsBLL = hotelDetailsBLL;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<HotelDetailsResponse>>> GetHotelDetailsListAsync()
        {
            var hotelDetailsList = await _hotelDetailsBLL.GetHotelDetailsListAsync();

            return Ok(hotelDetailsList);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetHotelDetailsAsync(int id)
        {
            var hotelDetails = await _hotelDetailsBLL.GetHotelDetails(id);

            return hotelDetails != null ? Ok(hotelDetails) : NotFound($"Details for the hotel: {hotelDetails.HotelName} with ID:{id} were not found.");
        }

        [HttpPost]
        public async Task<ActionResult<HotelDetailsVMCreate>> AddHotelDetailsAsync(HotelDetailsVMCreate model)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            await _hotelDetailsBLL.AddHotelDetailsAsync(model);

            return Created(nameof(AddHotelDetailsAsync), model);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<HotelDetailsVMUpdate>> UpdateAsync(HotelDetailsVMUpdate model)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            await _hotelDetailsBLL.UpdateHotelDetailsAsync(model);

            return Ok(model);
        }

        [HttpPost("upload")]
        public async Task<ActionResult> UploadImage(IFormFile image, [FromQuery] string hotelName)
        {
            var imageFolder = @$"C:\gelu-azure\gelu-training\hotel-project\hotel-booking-web\src\assets\images\{hotelName}\";

            try
            {
                if (!Directory.Exists(imageFolder))
                {
                    Directory.CreateDirectory(imageFolder);
                }
                using (FileStream fileStream = System.IO.File.Create(imageFolder + image.FileName))
                {
                    image.CopyTo(fileStream);
                    fileStream.Flush();
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }

            return Created(nameof(UploadImage), image);
        }
    }
}
