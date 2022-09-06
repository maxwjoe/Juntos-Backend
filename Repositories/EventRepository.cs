using Juntos.Data;
using Juntos.Interfaces;
using Juntos.Models;
using Microsoft.AspNetCore.Mvc;
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


        // --- CRUD METHODS ---

        public async Task<Event> Add(Event eventObj)
        {
            await _context.Events.AddAsync(eventObj);
            await Save();
            return eventObj;
        }

        //TODO: Allow Event Memberships update (Need to inject membership repository idk?)
        public async Task<Event> Update(Event eventObj, EventDto updates)
        {
            eventObj.Title = updates.Title;
            eventObj.Description = updates.Description;
            eventObj.AssociatedClub = updates.AssociatedClub;
            eventObj.OwnerId = updates.OwnerId;
            eventObj.Location = updates.Location;
            eventObj.DoesRepeat = updates.DoesRepeat;
            eventObj.CapacityLimit = updates.CapacityLimit;
            eventObj.BookingTimeLimitMinutes = updates.BookingTimeLimitMinutes;
            eventObj.RepeatOption = updates.RepeatOption;
            eventObj.EventDateAndTime = updates.EventDateAndTime;
            eventObj.UpdatedAt = DateTime.Now;

            await Save();
            return eventObj;
        }
        public async Task<Event> Delete(int Id)
        {
            Event tgtEvent = await _context.Events.FindAsync(Id);

            if (tgtEvent == null)
            {
                return new Event();
            }

            _context.Events.Remove(tgtEvent);
            await Save();
            return tgtEvent;
        }

        public async Task<bool> Save()
        {
            var saved = await _context.SaveChangesAsync();
            return saved > 0 ? true : false;
        }



        // --- COMPLEX METHODS ---

        public async Task<IEnumerable<Event>> GetAll()
        {
            return await _context.Events.ToListAsync();
        }
        public async Task<Event> GetByIdAsync(int id)
        {
            return await _context.Events.FirstOrDefaultAsync(i => i.Id == id);
        }
        public async Task<List<Event>> GetByClubAsync(int clubId)
        {
            return await _context.Events.Where(i => i.AssociatedClub == clubId).ToListAsync();
        }
    }
}