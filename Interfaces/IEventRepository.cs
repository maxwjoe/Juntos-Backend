using Juntos.Models;

namespace Juntos.Interfaces
{
    public interface IEventRepository
    {

        // --- CRUD METHODS ---

        Task<Event> Add(Event eventObj);
        Task<Event> Update(Event eventObj, EventDto updates);
        Task<Event> Delete(int Id);
        Task<bool> Save();


        // --- COMPLEX METHODS ---

        Task<IEnumerable<Event>> GetAll();
        Task<Event> GetByIdAsync(int id);
        Task<List<Event>> GetByClubAsync(int clubId);

    }
}