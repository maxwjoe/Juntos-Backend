using Juntos.Models;

namespace Juntos.Interfaces
{
    public interface IEventRepository
    {

        // --- CRUD METHODS ---
        Task<Event> Create(Event eventObj);
        Task<Event> Update(Event eventObj, EventDto updates);
        Task<Event> Delete(Event eventObj);

        Task<bool> Save();


        // --- QUERY METHODS ---

        Task<List<Event>> GetAll(int userId);
        Task<Event> GetByIdAsync(int eventObjId);



    }
}