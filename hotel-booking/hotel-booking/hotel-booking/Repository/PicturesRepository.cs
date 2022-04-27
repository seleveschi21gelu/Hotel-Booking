using hotel_booking.DAL;
using hotel_booking.DAL.Models;
using hotel_booking.IRepository;
using Microsoft.EntityFrameworkCore;

namespace hotel_booking.Repository
{
    public class PicturesRepository : IPicturesRepository
    {
        readonly AppDBContext _dBContext;

        public PicturesRepository(AppDBContext dBContext)
        {
            _dBContext = dBContext;
        }

        public async Task AddPicturesAsync(Pictures model)
        {
            await _dBContext.Pictures.AddAsync(model);
            await _dBContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Pictures>> GetPicturesAsync()
        {
            var pictures = await _dBContext.Pictures.ToListAsync();

            return pictures;
        }

        public async Task DeletePictures(Pictures picture)
        {
            if (picture != null)
            {
                _dBContext.Pictures.Remove(picture);
            }
        }
    }
}
