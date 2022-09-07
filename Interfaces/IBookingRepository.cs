using Juntos.Models;

namespace Juntos.Interfaces
{
    public interface IBookingRepository
    {
        // --- CRUD METHODS ---
        Task<Booking> Create(Booking booking);
        Task<Booking> Update(Booking booking, BookingDto updates);
        Task<Booking> Delete(Booking booking);

        Task<bool> Save();


        // --- QUERY METHODS ---

        Task<List<Booking>> GetAll(int eventId);
        Task<Booking> GetByIdAsync(int bookingId);
    }
}