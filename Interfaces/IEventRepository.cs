using Juntos.Models;

namespace Juntos.Interfaces
{
    public interface IEventRepository
    {

        // --- CRUD METHODS ---
        Task<Event> Create(Event eventobj);
        Task<Event> Update(Event eventobj, EventDto updates);
        Task<Event> Delete(int eventobjId);

        Task<bool> Save();


        // --- QUERY METHODS ---

        Task<List<Event>> GetAll();
        Task<Event> GetByIdAsync(int eventobjId);



    }
}