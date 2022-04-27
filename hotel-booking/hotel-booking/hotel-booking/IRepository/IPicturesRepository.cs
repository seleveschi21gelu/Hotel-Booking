using hotel_booking.DAL.Models;

namespace hotel_booking.IRepository
{
    public interface IPicturesRepository
    {
        Task<IEnumerable<Pictures>> GetPicturesAsync();
        Task AddPicturesAsync(Pictures model);
        Task DeletePictures(Pictures picture);
    }
}