using Juntos.Data;
using Juntos.Interfaces;
using Juntos.Models;
using Microsoft.EntityFrameworkCore;

namespace Juntos.Repositories
{
    public class BookingRepository : IBookingRepository
    {

        private readonly DataContext _context;
        public BookingRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Booking> Create(Booking booking)
        {
            booking.CreatedAt = DateTime.Now;
            booking.UpdatedAt = DateTime.Now;

            await _context.Bookings.AddAsync(booking);
            await Save();
            return booking;

        }

        public async Task<Booking> Update(Booking booking, BookingDto updates)
        {
            booking.UserId = updates.UserId;
            booking.EventId = updates.EventId;
            booking.UpdatedAt = DateTime.Now;

            await Save();
            return booking;
        }

        public async Task<Booking> Delete(Booking booking)
        {
            if (booking == null)
            {
                return new Booking();
            }

            _context.Bookings.Remove(booking);
            await Save();
            return booking;
        }

        public async Task<bool> Save()
        {
            var saved = await _context.SaveChangesAsync();
            return saved > 0;
        }

        public async Task<List<Booking>> GetAll(int EventId)
        {
            return await _context.Bookings.Where(i => i.EventId == EventId).ToListAsync();
        }

        public async Task<Booking> GetByIdAsync(int bookingId)
        {
            return await _context.Bookings.FindAsync(bookingId);
        }

    }
}