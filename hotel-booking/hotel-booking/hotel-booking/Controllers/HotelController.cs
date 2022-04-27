using hotel_booking.DAL.Models;
using hotel_booking.DAL.ViewModels;
using hotel_booking.IBLL;
using Microsoft.AspNetCore.Mvc;

namespace hotel_booking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelController : ControllerBase
    {
        readonly IHotelBLL _hotelBll;
        readonly IWebHostEnvironment _environment;

        public HotelController(IHotelBLL hotelBll, IWebHostEnvironment environment)
        {
            _hotelBll = hotelBll;
            _environment = environment;
        }

        [HttpPost]
        public async Task<ActionResult> AddHotelAsync(HotelVMCreate hotelVM)
        {
            if (!ModelState.IsValid)
                BadRequest("Something went wrong");

            return Created(nameof(AddHotelAsync), await _hotelBll.AddHotelAsync(hotelVM));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Hotel>>> GetHotelsAsync()
                    => Ok(await _hotelBll.GetHotelsAsync());

        [HttpGet("{id}")]
        public async Task<ActionResult<Hotel>> GetHotelAsync(int id)
        {
            var hotel = await _hotelBll.GetHotelAsync(id);

            return hotel != null ? Ok(hotel) : NotFound($"Hotel with id: {id} wasn't found!");
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Hotel>> UpdateHotelAsync(HotelVMUpdate model)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var hotel = await _hotelBll.UpdateHotelAsync(model.Id, model);

            return hotel != null ? Ok(hotel) : NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            await _hotelBll.DeleteAsync(id);

            return Ok();
        }

        [HttpPost("upload")]
        public async Task<ActionResult> UploadImage(IFormFile image, [FromQuery] string name)
        {
            var angularFolder = @$"C:\gelu-azure\gelu-training\hotel-project\hotel-booking-web\src\assets\images\{name}\";
            var coverPageFolder = angularFolder + "cover-page\\";

            try
            {
                if (image.Length > 0)
                {
                    if (!Directory.Exists(angularFolder))
                    {
                        Directory.CreateDirectory(angularFolder);
                    }
                    if (!Directory.Exists(coverPageFolder))
                    {
                        Directory.CreateDirectory(coverPageFolder);
                    }
                    using (FileStream fileStream = System.IO.File.Create(coverPageFolder + image.FileName))
                    {
                        image.CopyTo(fileStream);
                        fileStream.Flush();
                    }
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
