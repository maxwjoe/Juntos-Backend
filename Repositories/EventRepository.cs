using Juntos.Data;
using Juntos.Interfaces;
using Juntos.Models;
using Microsoft.EntityFrameworkCore;

namespace Juntos.Repositories
{
    public class EventRepository : IEventRepository
    {

        private readonly DataContext _context;
        public EventRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Event> Create(Event eventObj)
        {
            eventObj.CreatedAt = DateTime.Now;
            eventObj.UpdatedAt = DateTime.Now;

            await _context.Events.AddAsync(eventObj);
            await Save();
            return eventObj;

        }
        public async Task<Event> Update(Event eventObj, EventDto updates)
        {
            eventObj.Title = updates.Title;
            eventObj.Description = updates.Description;
            eventObj.EventImageUrl = updates.EventImageUrl;
            eventObj.OwnerId = updates.OwnerId;
            eventObj.UpdatedAt = DateTime.Now;

            await Save();
            return eventObj;
        }

        public async Task<Event> Delete(Event eventObj)
        {
            if (eventObj == null)
            {
                return new Event();
            }

            _context.Events.Remove(eventObj);
            await Save();
            return eventObj;
        }

        public async Task<bool> Save()
        {
            var saved = await _context.SaveChangesAsync();
            return saved > 0;
        }

        public async Task<List<Event>> GetAll(int userId)
        {
            return await _context.Events.Where(i => i.OwnerId == userId).ToListAsync();
        }

        public async Task<Event> GetByIdAsync(int eventObjId)
        {

            return await _context.Events.FindAsync(eventObjId);
        }




    }
}